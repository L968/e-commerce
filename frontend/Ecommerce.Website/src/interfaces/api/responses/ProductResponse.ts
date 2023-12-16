export default interface ProductResponse {
    id: string
    name: string
    description: string
    rating: number
    combinations: ProductCombination[]
    variations: Variation[]
    reviews: any[]
}

export interface ProductCombination {
    id: string
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

export interface Variation {
    name: string
    options: string[]
}
