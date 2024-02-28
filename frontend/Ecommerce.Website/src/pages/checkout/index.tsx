import Link from 'next/link';
import Image from 'next/image';
import api from '@/services/api';
import { toast } from 'react-toastify';
import { useRouter } from 'next/router';
import Button from '@/components/Button';
import Address from '@/interfaces/Address';
import apiOrder from '@/services/apiOrder';
import { useEffect, useState } from 'react';
import { Avatar, Divider } from '@mui/material';
import currencyFormat from '@/utils/currencyFormat';
import PrivateRoute from '@/components/PrivateRoute';
import PaymentMethod from '@/interfaces/PaymentMethod';
import CreditCardIcon from '@mui/icons-material/CreditCard';
import LoadingButton from '@/components/Button/LoadingButton';
import NumberSelector from '@/components/NumberSelector/default';
import { useOrderCheckout } from '@/contexts/orderCheckoutContext';
import OrderCheckoutRequest from '@/interfaces/api/requests/OrderCheckoutRequest';
import { Container, ItemContainer, ItemInfo, Main, Price, PriceContainer, PriceContainerContent, PriceContainerRow, PriceContainerTitle, ProductName, Section, SectionContainer, SectionContent, SectionTitle, TotalPrice } from './styles';

const defaultAddressId = 1;
const shipping = 50;

function Checkout() {
    const router = useRouter();
    const { orderCheckoutItems, setOrderCheckout } = useOrderCheckout();

    if (orderCheckoutItems.length === 0) {
        router.push('/cart');
        return;
    }

    const [deliveryInfo, setDeliveryInfo] = useState<Address | null>(null);
    const [paymentMethod] = useState<PaymentMethod>(PaymentMethod.Pix);
    const [loading, setLoading] = useState<boolean>(false);

    const totalAmount = orderCheckoutItems.reduce((total, item) => {
        return total + item.product.discountedPrice * item.quantity;
    }, 0);

    useEffect(() => {
        api.get<Address>(`/address/${defaultAddressId}`)
            .then(response => setDeliveryInfo(response.data))
            .catch(error => toast.error('Error 500'));
    }, []);

    function handlePlaceOrder() {
        if (!deliveryInfo) return;

        const data: OrderCheckoutRequest = {
            orderCheckoutItems: orderCheckoutItems.map(item => ({
                productCombinationId: item.product.id,
                quantity: item.quantity
            })),
            shippingAddressId: deliveryInfo.id,
            paymentMethod: paymentMethod,
        }

        setLoading(true);

        apiOrder.post('/order', data)
            .then(res => toast.success('Order placed successfully'))
            .catch(err => toast.error('Error 500'))
            .finally(() => setLoading(false));
    }

    function handleDeleteItem(productId: string) {
        const updatedItems = orderCheckoutItems.filter(item => item.product.id !== productId);
        setOrderCheckout(updatedItems);
    }

    function updateItemQuantity(productId: string, newQuantity: number) {
        const updatedItems = orderCheckoutItems.map((item) => {
            if (item.product.id === productId) {
                return { ...item, quantity: newQuantity };
            }
            return item;
        });

        setOrderCheckout(updatedItems);
    }

    return (
        <Main>
            <Container>
                <SectionContainer>
                    <Section>
                        <SectionTitle variant='h3'>1 Delivery Information</SectionTitle>
                        <div>
                            <div>{deliveryInfo?.recipientFullName.toUpperCase()}</div>
                            <div>{deliveryInfo?.streetName}, {deliveryInfo?.buildingNumber}</div>
                            <div>{deliveryInfo?.complement}, {deliveryInfo?.neighborhood}</div>
                            <div>{deliveryInfo?.city}, {deliveryInfo?.state} {deliveryInfo?.postalCode}</div>
                        </div>
                    </Section>

                    <Section>
                        <SectionTitle variant='h3'>2 Payment Method</SectionTitle>

                        <SectionContent>
                            <Avatar>
                                <CreditCardIcon />
                            </Avatar>
                            Credit card: **** 9999
                        </SectionContent>
                    </Section>

                    <Section>
                        <SectionTitle variant='h3'>3 Order Summary</SectionTitle>

                        {orderCheckoutItems.map(item =>
                            <ItemContainer key={item.product.id}>
                                <Link href={`/product/${item.product.id}`}>
                                    <Image src={item.product.images[0]} width={64} height={64} alt='product-image' />
                                </Link>

                                <ItemInfo>
                                    <ProductName>
                                        <Link href={`/product/${item.product.id}`} style={{ textDecoration: 'none', color: 'inherit' }}>
                                            {item.product.name}
                                        </Link>
                                    </ProductName>

                                    <div>
                                        <Button onClick={() => handleDeleteItem(item.product.id)}>Delete</Button>
                                    </div>
                                </ItemInfo>

                                <NumberSelector
                                    value={item.quantity}
                                    setValue={(newQuantity) => updateItemQuantity(item.product.id, newQuantity)}
                                />

                                <Price>{currencyFormat(item.product.discountedPrice * item.quantity)}</Price>
                            </ItemContainer>
                        )}
                    </Section>
                </SectionContainer>

                <PriceContainer>
                    <PriceContainerTitle>Purchase summary</PriceContainerTitle>
                    <Divider />
                    <PriceContainerContent>
                        <PriceContainerRow>
                            <span>Products ({orderCheckoutItems.length})</span>
                            <span>{currencyFormat(totalAmount)}</span>
                        </PriceContainerRow>
                        <PriceContainerRow>
                            <span>Shipping</span>
                            <span>{currencyFormat(shipping)}</span>
                        </PriceContainerRow>
                        <TotalPrice>
                            <span>Total</span>
                            <span>{currencyFormat(totalAmount + shipping)}</span>
                        </TotalPrice>

                        <LoadingButton onClick={handlePlaceOrder} loading={loading} fullWidth>Place Order</LoadingButton>
                    </PriceContainerContent>
                </PriceContainer>
            </Container>
        </Main>
    )
}

export default function Private() {
    return (
        <PrivateRoute>
            <Checkout />
        </PrivateRoute>
    )
}
