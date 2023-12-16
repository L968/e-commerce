import GetVariantsResponse from "./GetVariantsResponse"

export default interface GetCategoryResponse {
    id: string
    name: string
    description: string
    variants: GetVariantsResponse[]
}
