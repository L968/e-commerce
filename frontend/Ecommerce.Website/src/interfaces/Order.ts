import OrderHistory from '@/interfaces/OrderHistory';
import { OrderItem } from '@/interfaces/OrderItem';
import OrderStatus from '@/interfaces/OrderStatus';
import PaymentMethod from '@/interfaces/PaymentMethod';

export interface Order {
    id: string
    status: OrderStatus
    paymentMethod: PaymentMethod
    shippingCost: number
    discount: number
    subtotal: number
    totalAmount: number
    shippingAddress: string
    createdAt: Date
    history: OrderHistory[]
    items: OrderItem[]
}
