import { ReactNode } from 'react';
import { Button } from '@mui/material';
import OrderStatus from '@/interfaces/OrderStatus';
import PaymentMethod from '@/interfaces/PaymentMethod';
import getOrderStatusColor from '@/utils/getOrderStatusColor';
import { Container, Content, Status, UserActions } from './styles';
import currencyFormat from '@/utils/currencyFormat';

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
                        <h2>Pay {currencyFormat(totalAmount)} via Pix to secure your purchase</h2>
                        <p>The payment will be approved instantly.</p>
                        <p>Follow the instructions to pay and secure your product.</p>
                    </>
                )
            default: break;
        }
    }

    function getActions(): ReactNode {
        switch (status) {
            case 'Pending Payment':
                return (
                    <>
                        <Button size='small' variant='contained' sx={{ textTransform: 'none' }}>View instructions</Button>
                        <Button size='small' sx={{ textTransform: 'none' }}>Pay by another method</Button>
                    </>
                )
            default: break;
        }
    }

    return (
        <Container>
            <Content isPendingPayment={status === 'Pending Payment'}>
                <Status color={getOrderStatusColor(status)}>{status}</Status>
                {getContent()}
            </Content>

            <UserActions>
                {getActions()}
            </UserActions>
        </Container>
    )
}
