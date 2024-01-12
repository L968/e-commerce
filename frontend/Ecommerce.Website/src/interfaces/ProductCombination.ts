export default interface ProductCombination {
    id: string
    productId: string
    name: string
    combinationString: string
    originalPrice: number
    discountedPrice: number
    stock: number
    length: number
    width: number
    height: number
    weight: number
    images: string[]
}
