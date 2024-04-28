enum OrderStatus {
    PendingPayment,
    Processing,
    Shipped,
    Delivered,
    Cancelled,
    Refunded,
    Returned
}

export const orderStatusOptions = (Object.keys(OrderStatus) as Array<keyof typeof OrderStatus>)
    .filter(key => !isNaN(Number(OrderStatus[key])))
    .map((key) => ({
        value: OrderStatus[key],
        label: key.replace(/([a-z])([A-Z])/g, '$1 $2')
    }))

export default OrderStatus;
