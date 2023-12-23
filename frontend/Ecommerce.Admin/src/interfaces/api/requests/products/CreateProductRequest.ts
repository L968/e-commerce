export default interface CreateProductRequest {
    name: string
    description: string
    productCategoryId: string
    active: boolean
    visible: boolean
}
