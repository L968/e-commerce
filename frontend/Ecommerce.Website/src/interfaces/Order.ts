import OrderHistory from '@/interfaces/OrderHistory';
import { OrderItem } from '@/interfaces/OrderItem';
import OrderStatus from '@/interfaces/OrderStatus';
import PaymentMethod from '@/interfaces/PaymentMethod';

export interface Order {
    id: string
    status: OrderStatus
    paymentMethod: PaymentMethod
    subtotal: number
    shippingCost: number
    totalAmount: number
    totalDiscount: number
    shippingAddress: string
    createdAt: Date

    items: OrderItem[]
    history: OrderHistory[]
}
