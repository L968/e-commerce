import ProductCombination from './ProductCombination';

export default interface CartItem {
    id: number
    quantity: number
    product: ProductCombination
}
