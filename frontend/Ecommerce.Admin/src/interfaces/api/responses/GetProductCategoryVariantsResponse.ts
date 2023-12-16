export default interface GetProductCategoryVariantsResponse {
    name: string
    options: VariantOption[]
}

export interface VariantOption {
    id: number
    name: string
}
