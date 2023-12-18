import { Container, Header, Main, ProductsContainer } from './styles';
import { CircularProgress, Grid, TextField, Typography } from '@mui/material';
import ProductCard from '@/components/ProductCard';
import api from '@/services/api';
import { toast } from 'react-toastify';
import { useEffect, useState } from 'react';
import GetProductListResponse from '@/interfaces/api/responses/GetProductListResponse';

export default function Products() {
    const [products, setProducts] = useState<GetProductListResponse[]>([]);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        api.get<GetProductListResponse[]>('/product')
            .then(response => setProducts(response.data))
            .catch(error => toast.error("Error 500"))
            .finally(() => setLoading(false));
    }, []);

    return (
        <Main>
            <Typography variant='h1'>Products</Typography>
            <Container>
                <Header>
                    <TextField
                        label='Search'
                        variant='outlined'
                        size='small'
                        sx={{ width: '400px' }}
                    />
                </Header>

                {loading && <CircularProgress />}

                <ProductsContainer container spacing={3}>
                    {products.map(product => (
                        <Grid item sm>
                            <ProductCard {...product} />
                        </Grid>
                    ))}
                </ProductsContainer>
            </Container>
        </Main>
    )
}
