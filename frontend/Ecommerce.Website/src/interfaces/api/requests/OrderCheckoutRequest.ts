import OrderCheckoutItem from '@/interfaces/OrderCheckoutItem';
import PaymentMethod from '@/interfaces/PaymentMethod';

export default interface OrderCheckoutRequest {
    orderCheckoutItems: OrderCheckoutItem[]
    shippingAddressId: number
    paymentMethod: PaymentMethod
}
