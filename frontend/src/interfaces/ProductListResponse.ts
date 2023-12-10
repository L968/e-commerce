export default interface ProductListResponse {
    id: string
    name: string
    categoryName: string
    originalPrice: number
    discountedPrice: number
    imageSource: string
    rating: number
    reviewsCount: number
    variations: Variation[]
}

export interface Variation {
    name: string
    options: string[]
}
