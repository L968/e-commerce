export default interface GetVariantsResponse {
    id: number
    name: string
    options: Option[]
}

export interface Option {
    id: number
    name: string
}
