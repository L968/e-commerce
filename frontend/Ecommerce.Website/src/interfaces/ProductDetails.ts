import Review from './Review';
import Variant from './Variant';
import ProductCombination from './ProductCombination';

export default interface ProductDetails {
    id: string
    name: string
    description: string
    active: boolean
    rating: number
    combinations: ProductCombination[]
    variants: Variant[]
    reviews: Review[]
}
