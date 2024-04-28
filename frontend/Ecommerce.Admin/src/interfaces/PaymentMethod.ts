enum PaymentMethod {
    PayPal
}

export const paymentMethodValues = Object.keys(PaymentMethod).filter((key: any) => !isNaN(Number(PaymentMethod[key])));

export default PaymentMethod;
