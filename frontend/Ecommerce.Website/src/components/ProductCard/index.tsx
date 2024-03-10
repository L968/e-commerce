import Image from 'next/image';
import { Rating } from '@mui/material';
import { useRouter } from 'next/navigation'
import { CardInfo, Container, Prices, ProductCategory, ProductDiscountedPrice, ProductName, ProductOriginalPrice, RatingContainer, RatingSpan } from './styles';

export interface ProductCardProps {
    id: string;
    name: string;
    categoryName: string;
    originalPrice: number;
    discountedPrice: number;
    imageSource: string;
    rating: number;
    reviewsCount: number;
}

export default function ProductCard(product: ProductCardProps) {
    const router = useRouter();
    const { id, imageSource, name, categoryName, originalPrice, discountedPrice, rating, reviewsCount } = product;

    return (
        <Container onClick={() => router.push(`/product/${id}`)}>
            <Image
                src={imageSource}
                alt={`product-${name}`}
                width={239}
                height={300}
            />

            <CardInfo>
                <ProductName variant='h5'>{name}</ProductName>
                <ProductCategory>{categoryName}</ProductCategory>
                <Prices>
                    {originalPrice !== discountedPrice &&
                        <ProductOriginalPrice>${originalPrice}</ProductOriginalPrice>
                    }
                    <ProductDiscountedPrice>${discountedPrice}</ProductDiscountedPrice>
                </Prices>

                <RatingContainer>
                    <Rating name='read-only' precision={0.5} value={rating} readOnly />
                    <RatingSpan>{reviewsCount}</RatingSpan>
                </RatingContainer>
            </CardInfo>
        </Container>
    )
}
