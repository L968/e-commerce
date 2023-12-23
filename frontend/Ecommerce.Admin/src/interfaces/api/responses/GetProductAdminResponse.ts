export default interface GetProductAdminResponse {
    id: string
    name: string
    description: string
    active: boolean
    visible: boolean
    category: Category
    combinations: Combination[]
}

export interface Category {
    id: string
    name: string
    description: string
    variants: any[]
}

export interface Combination {
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
