import OrderHistory from '@/interfaces/OrderHistory';
import { OrderItem } from '@/interfaces/OrderItem';
import OrderStatus from '@/interfaces/OrderStatus';

export interface OrderListResponse {
    page: number
    itemsPerPage: number
    totalItems: number
    totalPages: number
    items: OrderResponse[]
}

interface OrderResponse {
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
