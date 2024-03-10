import { ReactNode } from 'react';
import { Container, Title } from './styles';
import OrderStatus from '@/interfaces/OrderStatus';

interface HelpContainerProps {
    status: OrderStatus
    children?: ReactNode
}


export default function HelpContainer({ status }: HelpContainerProps) {
    if (!status) {
        throw new Error('Empty status')
    }

    function getOptions(): ReactNode {
        switch (status) {
            case 'Pending Payment':
                return (
                    <>
                        <div>Cancel Purchase</div>
                        <div>I made the payment but don't see it credited</div>
                        <div>When will I receive the purchase</div>
                    </>
                )
            case 'Shipped':
                return (
                    <>
                        <div>I want to change the delivery address</div>
                        <div>What if I'm not there to receive the purchase?</div>
                        <div>What happens if I cancel the purchase</div>
                        <div>How to take care of the product if I need to return it</div>
                        <div>I need help with the NF-e</div>
                    </>
                )
            default: throw new Error('Payment status not parameterized');
        }
    }

    return (
        <Container>
            <Title variant='h2'>Help with purchase</Title>
            {getOptions()}
        </Container>
    )
}
