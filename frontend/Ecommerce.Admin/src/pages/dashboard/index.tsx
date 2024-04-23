import apiOrder from '@/services/apiOrder';
import { toast } from 'react-toastify';
import { useEffect, useState } from 'react';
import { Container, Main } from './styles';
import { CircularProgress, Typography } from '@mui/material';
import OrderStatusCountResponse from '@/interfaces/api/responses/OrderStatusCountResponse';

export default function Dashboard() {
    const [orderStatusCount, setOrderStatusCount] = useState<OrderStatusCountResponse>();
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        apiOrder.get<OrderStatusCountResponse>('/order/status-count')
            .then(response => setOrderStatusCount(response.data))
            .catch(error => toast.error("Error 500"))
            .finally(() => setLoading(false));
    }, []);

    if (loading) {
        return <CircularProgress />
    }

    if (!orderStatusCount) {
        return <></>
    }

    return (
        <Main>
            <Typography variant='h1'>Dashboard</Typography>
            <Container>
                {orderStatusCount.shippedCount}
                {orderStatusCount.processingCount}
                {orderStatusCount.pendingPaymentCount}
            </Container>
        </Main>
    )
}
