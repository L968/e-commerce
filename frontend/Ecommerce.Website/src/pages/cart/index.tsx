import Link from 'next/link';
import api from '@/services/api';
import { toast } from 'react-toastify';
import { Divider } from '@mui/material';
import Button from '@/components/Button';
import { useEffect, useState } from 'react';
import CartItem from '@/interfaces/CartItem';
import axios, { CancelTokenSource } from 'axios';
import currencyFormat from '@/utils/currencyFormat';
import PrivateRoute from '@/components/PrivateRoute';
import CartItemComponent from '@/components/CartItem';
import OrderCheckoutItem from '@/interfaces/OrderCheckoutItem';
import { useOrderCheckout } from '@/contexts/orderCheckoutContext';
import { CartList, Container, EmptyCartList, EmptySummary, Main, PriceContainer, PriceContainerContent, PriceContainerRow, PriceContainerTitle, TotalPrice } from './styles';

function Cart() {
    const { setOrderCheckout } = useOrderCheckout();

    const [cancelTokenSource, setCancelTokenSource] = useState<CancelTokenSource | null>(null);
    const [cartItems, setCartItems] = useState<CartItem[]>([]);
    const [shipping, setShipping] = useState<number>(50);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        getCartItems();
    }, []);

    function getCartItems(): void {
        api.get<CartItem[]>('/cart')
            .then(response => setCartItems(response.data))
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

    function handleAddToOrderCheckout() {
        const items: OrderCheckoutItem[] = cartItems.map(item => ({
            productCombinationId: item.product.id,
            quantity: item.quantity
        }));

        setOrderCheckout(items);
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
                                    productId={cartItem.product.productId}
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

                            <Link href='/checkout'>
                                <Button
                                    fullWidth
                                    variant='contained'
                                    onClick={handleAddToOrderCheckout}
                                >
                                    Continue purchase
                                </Button>
                            </Link>
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

export default function Private() {
    return (
        <PrivateRoute>
            <Cart />
        </PrivateRoute>
    )
}
