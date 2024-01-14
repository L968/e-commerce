import { Main, Title } from './styles';
import api from '@/services/apiOrder';
import { toast } from 'react-toastify';
import { useEffect, useState } from 'react';
import PrivateRoute from '@/components/PrivateRoute';
import OrderListItem from '@/components/OrderListItem';
import { OrderListResponse } from '@/interfaces/api/responses/OrderListResponse';

function Orders() {
    const [orders, setOrders] = useState<OrderListResponse[]>([]);

    useEffect(() => {
        api.get<OrderListResponse[]>('/order')
            .then(res => setOrders(res.data))
            .catch(err => toast.error('Error 500'));
    }, []);

    return (
        <Main>
            <Title variant='h1'>Orders</Title>

            {orders.map(order =>
                <OrderListItem
                    orderId={order.id}
                    status={order.status}
                    createdAt={order.createdAt}
                    items={order.items}
                />
            )}
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
