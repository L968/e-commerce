export default interface GetProductListResponse {
    page: number
    itemsPerPage: number
    totalItems: number
    totalPages: number
    items: Product[]
}

export interface Product {
    id: string;
    name: string;
    categoryName: string;
    originalPrice: number;
    discountedPrice: number;
    imageSource: string;
    rating: number;
    reviewsCount: number;
}
