import moment from 'moment';
import api from '@/services/apiOrder';
import { toast } from 'react-toastify';
import { useRouter } from 'next/router';
import { Order } from '@/interfaces/Order';
import { useEffect, useState } from 'react';
import { CircularProgress } from '@mui/material';
import currencyFormat from '@/utils/currencyFormat';
import PrivateRoute from '@/components/PrivateRoute';
import { FeedbackContainer, HelpContainer, ProductContainer } from '@/components/pages/order/OrderContainers';
import { Aside, AsideContent, Container, Divider, Main, PurchaseSummaryRow, PurchaseSummarySubtitle, PurchaseSummaryTitle } from './styles';

function OrderComponent() {
    const router = useRouter();

    const [order, setOrder] = useState<Order | null>(null);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        const orderId = router.query.id;

        if (!orderId) return;

        api.get<Order>('/order/' + orderId)
            .then(response => setOrder(response.data))
            .catch(error => toast.error('Error 500'))
            .finally(() => setLoading(false));
    }, [router.query]);

    if (loading) {
        return <CircularProgress />
    }

    if (!order) {
        router.push('/404');
        return;
    }

    return (
        <Main>
            <Container>
                <ProductContainer items={order.items} />
                <FeedbackContainer status={order.status} totalAmount={order.totalAmount} paymentMethod={order.paymentMethod} />
                <HelpContainer status={order.status} />
            </Container>
            <Aside>
                <AsideContent>
                    <PurchaseSummaryTitle variant='h2'>Purchase Summary</PurchaseSummaryTitle>
                    <PurchaseSummarySubtitle>{moment(order.createdAt).format('MMMM D, YYYY')} | # {order.id}</PurchaseSummarySubtitle>

                    <Divider />

                    {order.items.map(item =>
                        <PurchaseSummaryRow>
                            <span>{item.productName} ({item.quantity})</span>
                            <span>{currencyFormat(item.totalPrice)}</span>
                        </PurchaseSummaryRow>
                    )}

                    <PurchaseSummaryRow>
                        <span>Shipping</span>
                        <span>{currencyFormat(order.shippingCost)}</span>
                    </PurchaseSummaryRow>

                    <Divider />

                    <PurchaseSummaryRow>
                        <span>Total</span>
                        <span>{currencyFormat(order.totalAmount)}</span>
                    </PurchaseSummaryRow>
                </AsideContent>
            </Aside>
        </Main>
    )
}

export default function Private() {
    return (
        <PrivateRoute>
            <OrderComponent />
        </PrivateRoute>
    )
}
