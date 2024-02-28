import PaymentMethod from '@/interfaces/PaymentMethod';

export default interface OrderCheckoutRequest {
    orderCheckoutItems: OrderCheckoutItemRequest[]
    shippingAddressId: number
    paymentMethod: PaymentMethod
}

export interface OrderCheckoutItemRequest {
    productCombinationId: string
    quantity: number
}
