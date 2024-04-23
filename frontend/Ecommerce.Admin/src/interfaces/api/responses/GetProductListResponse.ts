import Product from '@/interfaces/Product';

export default interface GetProductListResponse {
    page: number
    itemsPerPage: number
    totalItems: number
    totalPages: number
    items: Product[]
}
