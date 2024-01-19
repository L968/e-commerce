import Image from 'next/image';
import api from '@/services/api';
import { toast } from 'react-toastify';
import { useRouter } from 'next/router';
import Button from '@/components/Button';
import Address from '@/interfaces/Address';
import apiOrder from '@/services/apiOrder';
import { useEffect, useState } from 'react';
import currencyFormat from '@/utils/currencyFormat';
import PrivateRoute from '@/components/PrivateRoute';
import PaymentMethod from '@/interfaces/PaymentMethod';
import CreditCardIcon from '@mui/icons-material/CreditCard';
import { useOrderCheckout } from '@/contexts/orderCheckoutContext';
import OrderCheckoutRequest from '@/interfaces/api/requests/OrderCheckoutRequest';
import { ItemContainer, ItemInfo, Main, Price, ProductName, Section, SectionContent, SectionTitle } from './styles';
import { Avatar } from '@mui/material';
import Link from 'next/link';
import NumberSelector from '@/components/NumberSelector/default';

const defaultAddressId = 1;

function Checkout() {
    const router = useRouter();
    const { orderCheckoutItems } = useOrderCheckout();

    if (orderCheckoutItems.length === 0) {
        router.push('/cart');
        return;
    }

    const [totalPrice, setTotalPrice] = useState<number>(0);
    const [deliveryInfo, setDeliveryInfo] = useState<Address | null>(null);
    const [paymentMethod] = useState<PaymentMethod>(PaymentMethod.Pix);

    useEffect(() => {
        api.get<Address>(`/address/${defaultAddressId}`)
            .then(response => setDeliveryInfo(response.data))
            .catch(error => toast.error('Error 500'));
    }, []);

    function handlePlaceOrder() {
        if (!deliveryInfo) return;

        const data: OrderCheckoutRequest = {
            orderCheckoutItems: orderCheckoutItems,
            shippingAddressId: deliveryInfo.id,
            paymentMethod: paymentMethod,
        }

        apiOrder.post('/order', data)
            .then(res => toast.success('Order placed successfully'))
            .catch(err => toast.error('Error 500'));
    }

    function handleDeleteItem() {

    }

    return (
        <Main>
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
                    <ItemContainer>
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
                                <Button onClick={handleDeleteItem}>Delete</Button>
                            </div>
                        </ItemInfo>

                        <NumberSelector
                            value={item.quantity}
                            setValue={setQuantity}
                        />

                        <Price>{currencyFormat(item.product.discountedPrice * item.quantity)}</Price>
                    </ItemContainer>
                )}
                <p>Total: {currencyFormat(totalPrice)}</p>
                <Button onClick={handlePlaceOrder}>Place Order</Button>
            </Section>
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
