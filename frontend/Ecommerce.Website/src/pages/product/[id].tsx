import Image from 'next/image';
import { toast } from 'react-toastify';
import { useRouter } from 'next/router';
import Rating from '@mui/material/Rating';
import { useEffect, useState } from 'react';
import Divider from '@mui/material/Divider';
import { CircularProgress, Grid, IconButton } from '@mui/material';
import ShoppingCartOutlinedIcon from '@mui/icons-material/ShoppingCartOutlined';
import FavoriteBorderOutlinedIcon from '@mui/icons-material/FavoriteBorderOutlined';

import api from '@/services/api';
import Review from '@/components/Review';
import Option from '@/interfaces/Option';
import Variant from '@/interfaces/Variant';
import Breadcrumbs from '@/components/Breadcrumbs';
import currencyFormat from '@/utils/currencyFormat';
import VariantSelector from '@/components/VariantSelector';
import ProductCombination from '@/interfaces/ProductCombination';
import { useOrderCheckout } from '@/contexts/orderCheckoutContext';
import ProductResponse from '@/interfaces/api/responses/ProductResponse';
import { Availability, AvailabilityValue, ButtonsContainer, CarouselImageContainer, CarouselIndicators, Header, ImageContainer, ProductContainer, ProductDescription, ProductInfo, ProductName, ProductPrice, RatingContainer, RatingSpan, BuyNowButton, SelectedImageContainer, VariantsContainer, ReviewsSection, ReviewsTitle, RatingAverage, ReviewsRating, ReviewsRatingInfo } from './styles';

export default function Product() {
    const router = useRouter();
    const { setOrderCheckout } = useOrderCheckout();

    const [product, setProduct] = useState<ProductResponse>();
    const [loading, setLoading] = useState<boolean>(true);
    const [selectedImage, setSelectedImage] = useState<string>('');
    const [selectedProductCombination, setSelectedProductCombination] = useState<ProductCombination>();
    const [selectedVariants, setSelectedVariants] = useState<{ [key: string]: Option }>({});
    const [lastOptionSelected, setLastOptionSelected] = useState<Option>();

    useEffect(() => {
        const productId = router.query.id;

        if (!productId) return;

        api.get<ProductResponse>('/product/' + productId)
            .then(response => {
                setProduct(response.data);
                setSelectedProductCombination(response.data.combinations[0]);
                setSelectedVariantsByCombinationString(response.data.variants, response.data.combinations[0].combinationString);
            })
            .catch(error => toast.error('Error 500'))
            .finally(() => setLoading(false));
    }, [router.query]);

    useEffect(() => {
        if (selectedProductCombination) {
            setSelectedImage(selectedProductCombination.images[0]);
        }
    }, [selectedProductCombination]);

    useEffect(() => {
        const combination = findMatchingCombination();

        if (combination) {
            setSelectedProductCombination(combination)
        }
    }, [selectedVariants]);

    function handleAddToCart(): void {
        const data = {
            quantity: 1,
            productCombinationId: selectedProductCombination!.id
        }

        api.post('/cart/add-item', data)
            .then(res => toast.success('Cart item added successfully'))
            .catch(err => {
                if (err.response.status === 401) {
                    return router.push('/login');
                }

                toast.error('Error 500')
            })
    }

    function handleSelectVariant(variantName: string, selectedOption: Option): void {
        setLastOptionSelected(selectedOption);

        setSelectedVariants((prevVariants) => {
            return {
                ...prevVariants,
                [variantName]: selectedOption
            };
        });
    }

    function handleBuyNow(): void {
        if (!selectedProductCombination) return;

        setOrderCheckout([{
            product: selectedProductCombination,
            quantity: 1
        }]);

        router.push('/checkout');
    }

    function setSelectedVariantsByCombinationString(variants: Variant[], combinationString: string): void {
        variants.forEach(variant => {
            const selectedOption = variant.options.find(option => {
                return combinationString.includes(`${variant.name}=${option.name}`);
            });
            if (selectedOption) {
                selectedVariants[variant.name] = selectedOption;
            }
        });

        setSelectedVariants(selectedVariants);
    }

    function findMatchingCombination(): ProductCombination | null {
        if (!product) {
            return null;
        }

        const selectedVariantEntries = Object.entries(selectedVariants).filter(([_, selectedVariant]) => selectedVariant);

        if (selectedVariantEntries.length === product.variants.length) {
            for (const combination of product.combinations) {
                const combinationParts = combination.combinationString.split('/');

                const isMatch = selectedVariantEntries.every(([key, selectedVariant]) => {
                    return combinationParts.includes(`${key}=${selectedVariant.name}`);
                });

                if (isMatch) {
                    return combination;
                }
            }
        }

        const firstMatchingCombination = findFirstMatchingCombination(lastOptionSelected!.name);

        if (firstMatchingCombination) {
            setSelectedVariantsByCombinationString(product.variants, firstMatchingCombination.combinationString);
            return firstMatchingCombination;
        }

        return null;
    }

    function findFirstMatchingCombination(optionName: string): ProductCombination | null {
        if (!product) return null;

        for (const combination of product.combinations) {
            const combinationParts = combination.combinationString.split('/');

            if (combinationParts.some(part => part.endsWith(`=${optionName}`))) {
                return combination;
            }
        }

        return null;
    }

    function getValidOptions(variant: Variant): Option[] {
        const validCombinations = product?.combinations.filter((combination) => {
            const selectedVariantEntries = Object.entries(selectedVariants).filter(
                ([key]) => key !== variant.name
            );

            const combinationParts = combination.combinationString.split('/');
            return selectedVariantEntries.every(([key, selectedVariant]) => {
                return combinationParts.includes(`${key}=${selectedVariant.name}`);
            });
        });

        const validOptions: Option[] = (validCombinations ?? [])
            .map((combination) => {
                const combinationParts = combination.combinationString.split('/');
                const variantPart = combinationParts.find((part) => part.startsWith(`${variant.name}=`));
                const optionName = variantPart?.split('=')[1];
                return variant.options.find((option) => option.name === optionName);
            })
            .filter((option): option is Option => option !== undefined);

        return validOptions;
    }

    if (loading || !product) {
        return <CircularProgress />
    }

    return (
        <main>
            <Header>
                <Breadcrumbs
                    items={[
                        { href: '/home', text: 'Home' },
                        { href: '/shop', text: 'Shop' },
                        { href: `/product/${router.query.id}`, text: product?.name ?? '' },
                    ]}
                />
            </Header>

            <ProductContainer>
                <ImageContainer>
                    <SelectedImageContainer>
                        {selectedImage &&
                            <Image
                                alt='product-image'
                                src={selectedImage}
                                fill
                                priority
                                style={{ objectFit: 'contain' }}
                            />
                        }
                    </SelectedImageContainer>

                    <CarouselIndicators>
                        {selectedProductCombination?.images.map(image =>
                            <CarouselImageContainer
                                key={image}
                                onMouseEnter={() => setSelectedImage(image)}
                                isSelected={image === selectedImage}
                            >
                                <Image
                                    src={image}
                                    alt='product-thumb'
                                    priority
                                    width={80}
                                    height={80}
                                />
                            </CarouselImageContainer>
                        )}
                    </CarouselIndicators>
                </ImageContainer>

                <ProductInfo>
                    <ProductName variant='h4'>{product.name}</ProductName>

                    <RatingContainer>
                        <Rating precision={0.5} value={product.rating} readOnly />
                        <RatingSpan>{product.reviews.length} {product.reviews.length === 1 ? 'Review' : 'Reviews'}</RatingSpan>
                    </RatingContainer>

                    <ProductPrice variant='h3'>{currencyFormat(selectedProductCombination?.discountedPrice)}</ProductPrice>

                    <Availability variant='h6'>
                        Availability:&nbsp;
                        <AvailabilityValue>{selectedProductCombination?.stock} In Stock</AvailabilityValue>
                    </Availability>

                    <ProductDescription>
                        {product.description}
                    </ProductDescription>

                    <Divider />

                    <VariantsContainer>
                        {product.variants.map((variant) => (
                            <VariantSelector
                                key={variant.id}
                                variant={variant}
                                selectedVariant={selectedVariants[variant.name]}
                                onSelectVariant={selectedOption => handleSelectVariant(variant.name, selectedOption)}
                                validOptions={getValidOptions(variant)}
                            />
                        ))}
                    </VariantsContainer>

                    <ButtonsContainer>
                        <BuyNowButton variant='contained' onClick={handleBuyNow}>BUY NOW</BuyNowButton>
                        <FavoriteBorderOutlinedIcon />
                        <IconButton onClick={handleAddToCart}>
                            <ShoppingCartOutlinedIcon />
                        </IconButton>
                    </ButtonsContainer>
                </ProductInfo>
            </ProductContainer>

            {product.reviews.length > 0 && (
                <ReviewsSection>
                    <ReviewsTitle variant='h3'>Reviews</ReviewsTitle>

                    <ReviewsRating>
                        <RatingAverage>{product.rating.toFixed(1)}</RatingAverage>
                        <ReviewsRatingInfo>
                            <Rating precision={0.5} value={product.rating} readOnly />
                            <RatingSpan>{product.reviews.length} {product.reviews.length === 1 ? 'Review' : 'Reviews'}</RatingSpan>
                        </ReviewsRatingInfo>
                    </ReviewsRating>

                    <Grid container spacing={5}>
                        {product.reviews.map((review, i) =>
                            <Grid key={i} item xs={12} md={6}>
                                <Review
                                    rating={review.rating}
                                    description={review.description}
                                    createdAt={review.createdAt}
                                />
                            </Grid>
                        )}
                    </Grid>
                </ReviewsSection>
            )}
        </main>
    )
}
