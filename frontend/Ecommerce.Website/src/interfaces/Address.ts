export default interface Address {
    id: number
    recipientFullName: string
    recipientPhoneNumber: string
    postalCode: string
    streetName: string
    buildingNumber: string
    complement: string | null
    neighborhood: string | null
    city: string
    state: string
    country: string
    additionalInformation: string | null
}
