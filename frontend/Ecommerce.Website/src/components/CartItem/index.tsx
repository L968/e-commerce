import Image from 'next/image';
import { Button } from '@mui/material';
import currencyFormat from '@/utils/currencyFormat';
import NumberSelector from '@/components/NumberSelector';
import { CartInfo, Container, Price, ProductName } from './styles';

interface CartItemProps {
    cartItemId: number
    imageSource: string
    productName: string
    quantity: number
    originalPrice: number
    discountedPrice: number
    setQuantity: (cartItemId: number, newQuantity: number) => void
    onDelete: (cartItemId: number) => void
}

export default function CartItem({ cartItemId, imageSource, productName, quantity, originalPrice, discountedPrice, setQuantity, onDelete }: CartItemProps) {
    return (
        <Container>
            <Image src={imageSource} width={64} height={64} alt='product-image' />

            <CartInfo>
                <ProductName>{productName}</ProductName>
                <div>
                    <Button onClick={() => onDelete(cartItemId)}>Delete</Button>
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
