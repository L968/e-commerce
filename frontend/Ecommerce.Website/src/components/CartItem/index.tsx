import Link from 'next/link';
import Image from 'next/image';
import { Button } from '@mui/material';
import currencyFormat from '@/utils/currencyFormat';
import NumberSelector from '@/components/NumberSelector';
import { CartInfo, Container, Price, ProductName } from './styles';

interface CartItemProps {
    cartItemId: number
    productId: string
    imageSource: string
    productName: string
    quantity: number
    originalPrice: number
    discountedPrice: number
    setQuantity: (cartItemId: number, newQuantity: number) => void
    onDelete: (cartItemId: number) => void
}

export default function CartItem({ cartItemId, productId, imageSource, productName, quantity, originalPrice, discountedPrice, setQuantity, onDelete }: CartItemProps) {
    return (
        <Container>
            <Link href={`/product/${productId}`}>
                <Image src={imageSource} width={64} height={64} alt='product-image' />
            </Link>

            <CartInfo>
                <ProductName>
                    <Link href={`/product/${productId}`} style={{ textDecoration: 'none', color: 'inherit' }}>
                        {productName}
                    </Link>
                </ProductName>

                <div>
                    <Button onClick={() => onDelete(cartItemId)} variant='contained'>Delete</Button>
                </div>
            </CartInfo>

            <NumberSelector
                cartItemId={cartItemId}
                value={quantity}
                setValue={setQuantity}
            />

            <Price>{currencyFormat(discountedPrice * quantity)}</Price>
        </Container>
    )
}
