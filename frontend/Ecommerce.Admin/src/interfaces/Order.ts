import OrderItem from './OrderItem';

export default interface Order {
    id: string
    status: string
    paymentMethod: string
    subtotal: number
    shippingCost: number
    totalAmount: number
    totalDiscount: number
    shippingAddress: string
    createdAt: Date
    items: OrderItem[]
    history: any[]
}
