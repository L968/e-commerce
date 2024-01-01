import { useEffect, useState } from 'react';
import Image from 'next/image';
import { useRouter } from 'next/router';
import { toast } from 'react-toastify';
import Rating from '@mui/material/Rating';
import Divider from '@mui/material/Divider';
import VisibilityIcon from '@mui/icons-material/Visibility';
import { CircularProgress, IconButton } from '@mui/material';
import ShoppingCartOutlinedIcon from '@mui/icons-material/ShoppingCartOutlined';
import FavoriteBorderOutlinedIcon from '@mui/icons-material/FavoriteBorderOutlined';

import api from '@/services/api';
import Option from '@/interfaces/Option';
import Variant from '@/interfaces/Variant';
import Breadcrumbs from '@/components/Breadcrumbs';
import currencyFormat from '@/utils/currencyFormat';
import VariantSelector from '@/components/VariantSelector';
import ProductCombination from '@/interfaces/ProductCombination';
import ProductResponse from '@/interfaces/api/responses/ProductResponse';
import { Availability, AvailabilityValue, ButtonsContainer, CarouselImageContainer, CarouselIndicators, Header, ImageContainer, ProductContainer, ProductDescription, ProductInfo, ProductName, ProductPrice, RatingContainer, RatingSpan, SelectOptionButton, SelectedImageContainer, VariantsContainer } from './styles';

export default function Product() {
    const router = useRouter();
    const [product, setProduct] = useState<ProductResponse>();
    const [loading, setLoading] = useState<boolean>(true);
    const [selectedImage, setSelectedImage] = useState<string>('');
    const [selectedProductCombination, setSelectedProductCombination] = useState<ProductCombination>();
    const [selectedVariants, setSelectedVariants] = useState<{ [key: string]: Option }>({});

    useEffect(() => {
        const productId = router.query.id;

        if (!productId) return;

        api.get<ProductResponse>('/product/' + productId)
            .then(response => {
                setProduct(response.data);
                setSelectedProductCombination(response.data.combinations[0]);
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
            .catch(err => toast.error('Error 500'))
    }

    function handleSelectVariant(variantName: string, selectedOption: Option) {
        setSelectedVariants((prevVariants) => {
            return {
                ...prevVariants,
                [variantName]: selectedOption
            };
        });
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

        return null;
    }

    function getValidOptions(variant: Variant): Option[] {
        const validCombinations = product?.combinations.filter((combination) => {
            const selectedVariantEntries = Object.entries(selectedVariants).filter(
                ([key]) => key !== variant.name
            ); // Excluir a variante atual das seleções

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

    if (loading) {
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
                        <Image
                            alt='product-image'
                            src={selectedImage}
                            layout='fill'
                            objectFit='contain'
                        />
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
                                    width={80}
                                    height={80}
                                />
                            </CarouselImageContainer>
                        )}
                    </CarouselIndicators>
                </ImageContainer>

                <ProductInfo>
                    <ProductName variant='h4'>{product?.name}</ProductName>

                    <RatingContainer>
                        <Rating name='read-only' precision={0.5} value={product?.rating} readOnly />
                        <RatingSpan>{product?.reviews.length} Reviews</RatingSpan>
                    </RatingContainer>

                    <ProductPrice variant='h3'>{currencyFormat(selectedProductCombination?.discountedPrice)}</ProductPrice>

                    <Availability variant='h6'>
                        Availability:&nbsp;
                        <AvailabilityValue>{selectedProductCombination?.stock} In Stock</AvailabilityValue>
                    </Availability>

                    <ProductDescription>
                        {product?.description}
                    </ProductDescription>

                    <Divider />

                    <VariantsContainer>
                        {product?.variants.map((variant) => (
                            <VariantSelector
                                key={variant.id}
                                variant={variant}
                                selectedVariant={selectedVariants[variant.name]}
                                onSelectVariant={(selectedOption) => handleSelectVariant(variant.name, selectedOption)}
                                validOptions={getValidOptions(variant)}
                            />
                        ))}
                    </VariantsContainer>

                    <ButtonsContainer>
                        <SelectOptionButton>Select Options</SelectOptionButton>
                        <FavoriteBorderOutlinedIcon />
                        <IconButton onClick={handleAddToCart}>
                            <ShoppingCartOutlinedIcon />
                        </IconButton>
                        <VisibilityIcon />
                    </ButtonsContainer>
                </ProductInfo>
            </ProductContainer>
        </main>
    )
}
