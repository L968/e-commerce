import Image from "next/image";
import ColorOption from "../ColorOption";
import { useRouter } from 'next/navigation'
import { CardInfo, Colors, Container, Prices, ProductCategory, ProductDiscountedPrice, ProductName, ProductOriginalPrice } from "./styles";

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
    const { id, imageSource, name, categoryName, originalPrice, discountedPrice } = product;

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
                <Colors>
                    <ColorOption width={17} height={16} color='#23A6F0' />
                    <ColorOption width={17} height={16} color='#23856D' />
                    <ColorOption width={17} height={16} color='#E77C40' />
                    <ColorOption width={17} height={16} color='#23856D' />
                </Colors>
            </CardInfo>
        </Container>
    )
}
