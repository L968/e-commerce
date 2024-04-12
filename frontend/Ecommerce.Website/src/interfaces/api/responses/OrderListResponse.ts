import { Order } from '@/interfaces/Order';

export interface OrderListResponse {
    page: number
    itemsPerPage: number
    totalItems: number
    totalPages: number
    items: Order[]
}
