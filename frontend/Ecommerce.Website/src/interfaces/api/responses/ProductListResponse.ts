import ProductListItem from '../../ProductListItem';

export default interface ProductListResponse {
    page: number
    itemsPerPage: number
    totalItems: number
    totalPages: number
    items: ProductListItem[]
}
