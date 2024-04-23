import apiOrder from '@/services/apiOrder';
import { toast } from 'react-toastify';
import { useEffect, useState } from 'react';
import { Container, Main } from './styles';
import { Typography } from '@mui/material';

export default function Orders() {
    const [orders, setOrders] = useState();
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        apiOrder.get('/order/all')
            .then(response => setOrders(response.data))
            .catch(error => toast.error("Error 500"))
            .finally(() => setLoading(false));
    }, []);

    if (!loading && !orders) {
        return;
    }

    return (
        <Main>
            <Typography variant='h1'>Orders</Typography>
            <Container>
            </Container>
        </Main>
    )
}
