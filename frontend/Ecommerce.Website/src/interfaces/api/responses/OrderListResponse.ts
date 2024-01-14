import { OrderItem } from "@/interfaces/OrderItem"

export interface OrderListResponse {
    id: string
    status: string
    shippingCost: number
    discount: number
    totalAmount: number
    shippingAddress: string
    createdAt: Date
    items: OrderItem[]
}
