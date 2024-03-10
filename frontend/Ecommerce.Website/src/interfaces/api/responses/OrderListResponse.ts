import OrderHistory from '@/interfaces/OrderHistory';
import { OrderItem } from '@/interfaces/OrderItem';
import OrderStatus from '@/interfaces/OrderStatus';

export interface OrderListResponse {
    id: string
    status: OrderStatus
    shippingCost: number
    discount: number
    subtotal: number
    totalAmount: number
    shippingAddress: string
    createdAt: Date
    history: OrderHistory[]
    items: OrderItem[]
}
