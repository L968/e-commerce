import Product from '../../Product';

export default interface ProductListResponse {
    page: number
    itemsPerPage: number
    totalItems: number
    totalPages: number
    items: Product[]
}
