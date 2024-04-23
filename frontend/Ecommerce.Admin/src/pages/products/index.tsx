import api from '@/services/api';
import { toast } from 'react-toastify';
import { useEffect, useState } from 'react';
import ProductCard from '@/components/ProductCard';
import { Container, Header, Main, ProductsContainer } from './styles';
import { CircularProgress, Grid, TextField, Typography } from '@mui/material';
import GetProductListResponse from '@/interfaces/api/responses/GetProductListResponse';

export default function Products() {
    const [products, setProducts] = useState<GetProductListResponse>();
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        api.get<GetProductListResponse>('/product')
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

                {loading
                    ? <CircularProgress />
                    : <>
                        {products &&
                            <ProductsContainer container spacing={3}>
                                {products.items.map(product => (
                                    <Grid key={product.id} item sm>
                                        <ProductCard {...product} />
                                    </Grid>
                                ))}
                            </ProductsContainer>
                        }
                    </>
                }
            </Container>
        </Main>
    )
}
