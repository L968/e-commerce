import Variant from './Variant';

export default interface ProductListItem {
    id: string
    name: string
    categoryName: string
    originalPrice: number
    discountedPrice: number
    imageSource: string
    rating: number
    reviewsCount: number
    variations: Variant[]
}
