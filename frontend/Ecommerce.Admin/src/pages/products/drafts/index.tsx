import Link from 'next/link';
import api from '@/services/api';
import { toast } from 'react-toastify';
import { useState, useEffect } from 'react';
import DeleteIcon from '@mui/icons-material/Delete';
import { Product } from '@/interfaces/api/responses/GetProductListResponse';
import { ActionsContainer, Container, ListItemButton, Main } from './styles';
import { Button, CircularProgress, IconButton, List, ListItem, ListItemText, Typography } from '@mui/material';

export default function ProductDrafts() {
    const [products, setProducts] = useState<Product[]>([]);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        loadDrafts();
    }, [])

    function loadDrafts() {
        api.get<Product[]>('/product/drafts')
            .then(response => setProducts(response.data))
            .catch(error => toast.error('Error 500'))
            .finally(() => setLoading(false));
    }

    function handleDeleteDraft(productId: string): void {
        api.delete(`/product/${productId}`)
            .then(res => loadDrafts())
            .catch(error => toast.error('Error 500'))
    }

    return (
        <Main>
            <Container>
                <Typography variant='h1'>Which draft do you want to continue?</Typography>

                {!loading && products.length <= 0 &&
                    <Typography variant='h1'>No drafts found</Typography>
                }

                {loading && products
                    ? <CircularProgress />
                    : <List>
                        {products!.map((product) => (
                            <ListItem sx={{ display: 'flex', alignItems: 'center' }}>
                                <Link
                                    key={product.id}
                                    style={{ all: 'unset', width: '100%' }}
                                    href={`/products/${product.id}/edit`}
                                >
                                    <ListItemButton>
                                        <ListItemText
                                            primary={product.name}
                                            secondary={`Category: ${product.categoryName}`}
                                        />
                                    </ListItemButton>
                                </Link>
                                <IconButton onClick={_ => handleDeleteDraft(product.id)} sx={{ height: '100%' }}>
                                    <DeleteIcon />
                                </IconButton>
                            </ListItem>
                        ))}
                    </List>
                }

                <ActionsContainer>
                    <Link href='/products/create'>
                        <Button variant='contained'>Create new product</Button>
                    </Link>
                </ActionsContainer>
            </Container>
        </Main>
    )
}
