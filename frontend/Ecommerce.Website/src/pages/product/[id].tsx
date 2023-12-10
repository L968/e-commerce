import { useEffect, useState } from "react";
import Image from "next/image";
import { useRouter } from "next/router";
import { toast } from "react-toastify";
import Rating from "@mui/material/Rating";
import Divider from "@mui/material/Divider";
import VisibilityIcon from '@mui/icons-material/Visibility';
import ShoppingCartOutlinedIcon from '@mui/icons-material/ShoppingCartOutlined';
import FavoriteBorderOutlinedIcon from '@mui/icons-material/FavoriteBorderOutlined';

import api from '../../services/api';
import ColorOption from "@/components/ColorOption";
import Breadcrumbs from "@/components/Breadcrumbs";
import ProductCarousel from "@/components/ProductCarousel";
import ProductResponse, { ProductCombination } from "@/interfaces/ProductResponse";
import { Availability, AvailabilityValue, ButtonsContainer, CarouselIndicators, Colors, Header, ImageContainer, ProductContainer, ProductDescription, ProductInfo, ProductName, ProductPrice, RatingContainer, RatingSpan, SelectOptionButton } from "./styles";
import currencyFormat from "@/utils/currencyFormat";

export default function Product() {
    const router = useRouter();
    const [product, setProduct] = useState<ProductResponse>();
    const [selectedProductCombination, setSelectedProductCombination] = useState<ProductCombination>();

    useEffect(() => {
        const productId = router.query.id;

        if (!productId) return;

        api.get<ProductResponse>('/product/' + productId)
            .then(response => {
                setProduct(response.data);
                setSelectedProductCombination(response.data.combinations[0]);
            })
            .catch(error => toast.error("Error 500"));
    }, [router.query]);

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
                    <ProductCarousel style={{ height: '450px' }} />

                    <CarouselIndicators>
                        <Image
                            src='/assets/product-thumb-1.png'
                            alt='product-thumb'
                            width={100}
                            height={75}
                        />
                        <Image
                            src='/assets/product-thumb-2.png'
                            alt='product-thumb'
                            width={100}
                            height={75}
                        />
                    </CarouselIndicators>
                </ImageContainer>

                <ProductInfo>
                    <ProductName variant="h4">{product?.name}</ProductName>

                    <RatingContainer>
                        <Rating name="read-only" precision={0.5} value={product?.rating} readOnly />
                        <RatingSpan>{product?.reviews.length} Reviews</RatingSpan>
                    </RatingContainer>

                    <ProductPrice variant="h3">{currencyFormat(selectedProductCombination?.discountedPrice)}</ProductPrice>

                    <Availability variant="h6">
                        Availability:&nbsp;
                        <AvailabilityValue>{selectedProductCombination?.stock} In Stock</AvailabilityValue>
                    </Availability>

                    <ProductDescription>
                        {product?.description}
                    </ProductDescription>

                    <Divider />

                    <Colors>
                        <ColorOption width={30} height={30} color='#23A6F0' />
                        <ColorOption width={30} height={30} color='#23856D' />
                        <ColorOption width={30} height={30} color='#E77C40' />
                        <ColorOption width={30} height={30} color='#23856D' />
                    </Colors>

                    <ButtonsContainer>
                        <SelectOptionButton>Select Options</SelectOptionButton>
                        <FavoriteBorderOutlinedIcon  />
                        <ShoppingCartOutlinedIcon />
                        <VisibilityIcon />
                    </ButtonsContainer>
                </ProductInfo>
            </ProductContainer>
        </main>
    )
}
