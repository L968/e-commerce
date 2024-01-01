import Variant from '../../Variant';
import ProductCombination from '../../ProductCombination';

export default interface ProductResponse {
    id: string
    name: string
    description: string
    rating: number
    combinations: ProductCombination[]
    variants: Variant[]
    reviews: any[]
}
