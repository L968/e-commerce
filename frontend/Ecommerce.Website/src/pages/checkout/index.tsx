import api from '@/services/api';
import { toast } from 'react-toastify';
import Button from '@/components/Button';
import Address from '@/interfaces/Address';
import apiOrder from '@/services/apiOrder';
import { useEffect, useState } from 'react';
import currencyFormat from '@/utils/currencyFormat';
import PaymentMethod from '@/interfaces/PaymentMethod';
import { useOrderCheckout } from '@/contexts/orderCheckoutContext';
import OrderCheckoutRequest from '@/interfaces/api/requests/OrderCheckoutRequest';
import { Main, DeliveryContainer, PaymentContainer, OrderSummary } from './styles';

const defaultAddressId = 1;

export default function Checkout() {
    const { orderCheckoutItems } = useOrderCheckout();

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

    return (
        <Main>
            <DeliveryContainer>
                <h2>Delivery Information</h2>
                <div>
                    <label htmlFor="fullName">Full Name: </label>
                    {deliveryInfo?.recipientFullName}
                </div>
                <div>
                    <label htmlFor="address">Address: </label>
                    {deliveryInfo?.streetName}
                </div>
                <div>
                    <label htmlFor="city">City: </label>
                    {deliveryInfo?.city}
                </div>
                <div>
                    <label htmlFor="zipCode">Zip Code: </label>
                    {deliveryInfo?.postalCode}
                </div>
            </DeliveryContainer>

            <PaymentContainer>
                <h2>Payment Method</h2>
                Credit card: **** 9999
            </PaymentContainer>

            <OrderSummary>
                <h2>Order Summary</h2>
                {orderCheckoutItems.map(item => <div>{item.productCombinationId}: {item.quantity}</div>)}
                <p>Total: {currencyFormat(totalPrice)}</p>
                <Button onClick={handlePlaceOrder}>Place Order</Button>
            </OrderSummary>
        </Main>
    )
}
