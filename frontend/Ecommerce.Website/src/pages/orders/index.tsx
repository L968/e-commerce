import api from '@/services/apiOrder';
import { Main, Title } from './styles';
import { toast } from 'react-toastify';
import { useEffect, useState } from 'react';
import { CircularProgress } from '@mui/material';
import PrivateRoute from '@/components/PrivateRoute';
import OrderListItem from '@/components/pages/order/OrderListItem';
import { OrderListResponse } from '@/interfaces/api/responses/OrderListResponse';

function Orders() {
    const [loading, setLoading] = useState<boolean>(true);
    const [orders, setOrders] = useState<OrderListResponse[]>([]);

    useEffect(() => {
        api.get<OrderListResponse[]>('/order')
            .then(res => setOrders(res.data))
            .catch(err => toast.error('Error 500'))
            .finally(() => setLoading(false));
    }, []);

    return (
        <Main>
            <Title variant='h1'>Orders</Title>

            {loading
                ?
                <CircularProgress />
                :
                <>
                    {orders.map(order =>
                        <OrderListItem
                            orderId={order.id}
                            status={order.status}
                            createdAt={order.createdAt}
                            items={order.items}
                        />
                    )}
                </>}
        </Main>
    )
}

export default function Private() {
    return (
        <PrivateRoute>
            <Orders />
        </PrivateRoute>
    )
}
