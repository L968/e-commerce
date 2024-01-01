import api from '@/services/api';
import { toast } from 'react-toastify';
import { Divider } from '@mui/material';
import Button from '@/components/Button';
import { useEffect, useState } from 'react';
import CartItem from '@/interfaces/CartItem';
import axios, { CancelTokenSource } from 'axios';
import currencyFormat from '@/utils/currencyFormat';
import CartItemComponent from '@/components/CartItem';
import { CartList, Container, EmptyCartList, EmptySummary, Main, PriceContainer, PriceContainerContent, PriceContainerRow, PriceContainerTitle, TotalPrice } from './styles';

export default function Cart() {
    const [cancelTokenSource, setCancelTokenSource] = useState<CancelTokenSource | null>(null);
    const [cartItems, setCartItems] = useState<CartItem[]>([]);
    const [shipping, setShipping] = useState<number>(50);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        getCartItems();
    }, []);

    function getCartItems(): void {
        api.get<CartItem[]>('/cart')
            .then(response => {
                setCartItems(response.data)
                console.log(response.data);
            })
            .catch(error => toast.error('Error 500'))
            .finally(() => setLoading(false));
    }

    function getTotalAmount(): number {
        let totalAmount = 0;

        for (const cartItem of cartItems) {
            const { quantity, product } = cartItem;
            const itemTotal = quantity * product.discountedPrice;
            totalAmount += itemTotal;
        }

        return totalAmount;
    }

    function updateCartItemQuantity(cartItemId: number, newQuantity: number) {
        if (cancelTokenSource) {
            cancelTokenSource.cancel('Operation canceled by the user.');
        }

        const newCancelTokenSource = axios.CancelToken.source();
        setCancelTokenSource(newCancelTokenSource);

        setCartItems((prevCartItems) => {
            return prevCartItems.map((cartItem) => {
                if (cartItem.id === cartItemId) {
                    return { ...cartItem, quantity: newQuantity };
                }
                return cartItem;
            });
        });

        api.patch(`/cart/update-item-quantity/${cartItemId}`,
            { quantity: newQuantity },
            { cancelToken: newCancelTokenSource.token }
        )
            .catch(err => {
                if (!axios.isCancel(err)) {
                    console.log(err);
                }
            });
    }

    function handleDeleteCartItem(cartItemId: number): void {
        api.delete(`/cart/remove-item/${cartItemId}`)
            .then(res => getCartItems())
            .catch(err => toast.error('Error 500'));
    }

    return (
        <Main>
            <Container>
                <CartList>
                    {cartItems.length > 0
                        ?
                        <>
                            {cartItems.map(cartItem => (
                                <CartItemComponent
                                    key={cartItem.id}
                                    cartItemId={cartItem.id}
                                    imageSource={cartItem.product.images[0]}
                                    productName={cartItem.product.name}
                                    quantity={cartItem.quantity}
                                    originalPrice={cartItem.product.originalPrice}
                                    discountedPrice={cartItem.product.discountedPrice}
                                    setQuantity={updateCartItemQuantity}
                                    onDelete={handleDeleteCartItem}
                                />
                            ))}
                        </>
                        :
                        <EmptyCartList>
                            Build a shopping cart!
                        </EmptyCartList>
                    }
                </CartList>

                <PriceContainer>
                    <PriceContainerTitle>Purchase summary</PriceContainerTitle>
                    <Divider />
                    {cartItems.length > 0
                        ?
                        <PriceContainerContent>
                            <PriceContainerRow>
                                <span>Products ({cartItems.length})</span>
                                <span>{currencyFormat(getTotalAmount())}</span>
                            </PriceContainerRow>
                            <PriceContainerRow>
                                <span>Shipping</span>
                                <span>{currencyFormat(shipping)}</span>
                            </PriceContainerRow>
                            <TotalPrice>
                                <span>Total</span>
                                <span>{currencyFormat(getTotalAmount() + shipping)}</span>
                            </TotalPrice>

                            <Button fullWidth variant='contained'>Continue purchase</Button>
                        </PriceContainerContent>
                        :
                        <EmptySummary>
                            Here, you will find the values of your purchase as soon as you add products.
                        </EmptySummary>
                    }
                </PriceContainer>
            </Container>
        </Main>
    )
}
