import Link from 'next/link';
import api from '@/services/api';
import { toast } from 'react-toastify';
import { useState, useEffect } from 'react';
import { Button, CircularProgress, List, ListItemText, Typography } from '@mui/material';
import { ActionsContainer, Container, ListItemButton, Main } from './styles';
import GetProductListResponse from '@/interfaces/api/responses/GetProductListResponse';

export default function ProductDrafts() {
    const [products, setProducts] = useState<GetProductListResponse[]>([]);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        api.get<GetProductListResponse[]>('/product/drafts')
            .then(response => setProducts(response.data))
            .catch(error => toast.error('Error 500'))
            .finally(() => setLoading(false));
    }, [])

    if (loading) {
        return <CircularProgress />
    }

    return (
        <Main>
            <Container>
                {loading
                    ? <CircularProgress />
                    : <>
                        {!loading && !products
                            ? <Typography variant='h1'>No drafts found</Typography>
                            : <>
                                <Typography variant='h1'>Which draft do you want to continue?</Typography>

                                <List>
                                    {products.map((product) => (
                                        <Link
                                            key={product.id}
                                            style={{ all: 'unset' }}
                                            href={`/products/${product.id}/edit`}
                                        >
                                            <ListItemButton >
                                                <ListItemText
                                                    primary={product.name}
                                                    secondary={`Category: ${product.categoryName}`}
                                                />
                                            </ListItemButton>
                                        </Link>
                                    ))}
                                </List>
                            </>}

                        <ActionsContainer>
                            <Link href='/products/create'>
                                <Button variant='contained' >Create new product</Button>
                            </Link>
                        </ActionsContainer>
                    </>}
            </Container>
        </Main>
    )
}
