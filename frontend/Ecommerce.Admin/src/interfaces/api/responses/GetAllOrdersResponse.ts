import Order from '@/interfaces/Order';

export default interface GetAllOrdersResponse {
    page: number
    itemsPerPage: number
    totalItems: number
    totalPages: number
    items: Order[]
}
