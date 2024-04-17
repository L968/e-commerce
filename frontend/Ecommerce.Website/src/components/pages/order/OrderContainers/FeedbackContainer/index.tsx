import { ReactNode } from 'react';
import OrderStatus from '@/interfaces/OrderStatus';
import currencyFormat from '@/utils/currencyFormat';
import PaymentMethod from '@/interfaces/PaymentMethod';
import getOrderStatusColor from '@/utils/getOrderStatusColor';
import { Button, Container, Content, Status, UserActions } from './styles';

interface FeedbackContainerProps {
    status: OrderStatus
    totalAmount: number
    paymentMethod: PaymentMethod
}

export default function FeedbackContainer({ status, totalAmount, paymentMethod }: FeedbackContainerProps) {
    if (!status) {
        throw new Error('Empty status')
    }

    function getContent(): ReactNode {
        switch (status) {
            case 'Pending Payment':
                return (
                    <>
                        <h2>Pay {currencyFormat(totalAmount)} via {paymentMethod} to secure your purchase</h2>
                        <p>The payment will be approved instantly.</p>
                        <p>Follow the instructions to pay and secure your product.</p>
                    </>
                )
            case 'Processing':
                return (
                    <>
                        <h2>Your order is being processed</h2>
                        <p>We are preparing your order for shipment.</p>
                        <p>Once your order is shipped, you will receive a tracking number to track your package.</p>
                    </>
                )
            case 'Cancelled':
                return (
                    <>
                        <h2>The deadline for paying your purchase has expired</h2>
                        <p>We canceled the purchase because the deadline for payment has expired</p>
                    </>
                )
            default: throw new Error('Payment status not parameterized');
        }
    }

    function getActions(): ReactNode {
        switch (status) {
            case 'Pending Payment':
                return (
                    <>
                        <Button size='small' variant='contained'>View instructions</Button>
                        <Button size='small'>Pay by another method</Button>
                    </>
                )
            case 'Processing':
                return (
                    <>
                        <Button size='small' variant='contained'>Shipping details</Button>
                        <Button size='small'>Cancel purchase</Button>
                    </>
                )
            case 'Cancelled':
                return <Button size='small' variant='contained'>Buy again</Button>
            default: throw new Error('Payment status not parameterized');
        }
    }

    return (
        <Container>
            <Content orderStatus={status}>
                <Status color={getOrderStatusColor(status)}>{status}</Status>
                {getContent()}
            </Content>

            <UserActions>
                {getActions()}
            </UserActions>
        </Container>
    )
}
