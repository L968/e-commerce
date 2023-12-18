export default interface GetProductListResponse {
    id: string;
    name: string;
    categoryName: string;
    originalPrice: number;
    discountedPrice: number;
    imageSource: string;
    rating: number;
    reviewsCount: number;
}
