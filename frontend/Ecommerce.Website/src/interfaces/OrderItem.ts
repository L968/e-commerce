export interface OrderItem {
    id: number
    productCombinationId: string
    productName: string
    productSku: string
    productImagePath: string
    description: string
    totalAmount: number
    productUnitPrice: number
    productDiscount: any
    quantity: number
}
