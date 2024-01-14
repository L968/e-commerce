import api from '@/services/apiOrder';
import { toast } from 'react-toastify';
import { useRouter } from 'next/router';
import { useEffect, useState } from 'react';
import PrivateRoute from '@/components/PrivateRoute';
import { CircularProgress, Divider } from '@mui/material';
import { Aside, AsideContent, Container, Main, Product, ProductsContainer } from './styles';
import { notFound } from 'next/navigation';
import Image from 'next/image';

function Order() {
    const router = useRouter();
    const [order, setOrder] = useState();
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        const orderId = router.query.id;

        if (!orderId) return;

        api.get('/order/' + orderId)
            .then(response => setOrder(response.data))
            .catch(error => toast.error('Error 500'))
            .finally(() => setLoading(false));
    }, [router.query]);

    if (loading) {
        return <CircularProgress />
    }

    if (!order) {
        notFound();
    }

    return (
        <Main>
            <Container>
                <ProductsContainer>
                </ProductsContainer>
            </Container>
            <Aside>
                <AsideContent>
                    <span>Detalhe da compra</span>
                    <span>6 de dezembro de 2023 | # 2000007076226444</span>
                    <Divider />
                    <div>
                        <span>Produto</span>
                        <span>R$ 2589</span>
                    </div>
                    <div>
                        <span>Produto</span>
                        <span>R$ 2589</span>
                    </div>
                    <Divider />
                    <div>
                        <span>Pagamento</span>
                        <span>R$ 2589</span>
                    </div>
                    <Divider />
                    <div>
                        <span>Total</span>
                        <span>R$ 2589</span>
                    </div>
                </AsideContent>
            </Aside>
        </Main>
    )
}

export default function Private() {
    return (
        <PrivateRoute>
            <Order />
        </PrivateRoute>
    )
}
