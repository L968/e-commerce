import PaymentMethod from '@/interfaces/PaymentMethod';

export default interface OrderCheckoutRequest {
    orderCheckoutItems: OrderCheckoutItemRequest[]
    shippingAddressId: string
    paymentMethod: PaymentMethod
}

export interface OrderCheckoutItemRequest {
    productCombinationId: string
    quantity: number
}
