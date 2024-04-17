import api from '@/services/apiOrder';
import { toast } from 'react-toastify';
import { useEffect, useState } from 'react';
import { CircularProgress } from '@mui/material';
import Pagination from '@/components/Pagination';
import PrivateRoute from '@/components/PrivateRoute';
import { PaginationContainer, Main, Title } from './styles';
import OrderListItem from '@/components/pages/order/OrderListItem';
import { OrderListResponse } from '@/interfaces/api/responses/OrderListResponse';

function Orders() {
    const [loading, setLoading] = useState<boolean>(true);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [orders, setOrders] = useState<OrderListResponse>();

    useEffect(() => {
        api.get<OrderListResponse>(`/order?page=${currentPage}&pageSize=10`)
            .then(res => setOrders(res.data))
            .catch(err => toast.error('Error 500'))
            .finally(() => setLoading(false));
    }, [currentPage]);

    function handlePageChange(newPage: number) {
        setCurrentPage(newPage);
    }

    return (
        <Main>
            <Title variant='h1'>Your Orders</Title>

            {loading || !orders
                ? <CircularProgress />
                : <>
                    {orders.items.map(order =>
                        <OrderListItem
                            key={order.id}
                            orderId={order.id}
                            status={order.status}
                            createdAt={order.createdAt}
                            items={order.items}
                        />
                    )}

                    <PaginationContainer>
                        <Pagination
                            page={currentPage}
                            totalPages={orders.totalPages}
                            onChangePage={handlePageChange}
                        />
                    </PaginationContainer>
                </>
            }
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
