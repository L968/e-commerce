import Image from 'next/image';
import Link from 'next/link';
import api from '@/services/api';
import { useState } from 'react';
import { toast } from 'react-toastify';
import { LoadingButton } from '@mui/lab';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import { Product } from '@/interfaces/api/responses/GetProductListResponse';
import { ActionButtons, CardInfo, Container, Price, ProductName } from './styles';
import { Button, Dialog, DialogActions, DialogContent, DialogTitle } from '@mui/material';

export default function ProductCard(product: Product) {
    const { id, name, discountedPrice: price, imageSource } = product;
    const [loading, setLoading] = useState<boolean>(false);
    const [openDeleteDialog, setOpenDeleteDialog] = useState<boolean>(false);

    function handleDelete() {
        setLoading(true);

        api.delete('/product/' + id)
            .then(response => toast.success('Product deleted'))
            .catch(error => toast.error('Error 500'))
            .finally(() => setLoading(false));
    }

    return (
        <Container>
            <Image
                src={imageSource}
                alt={`product-${name}`}
                width={239}
                height={300}
            />

            <CardInfo>
                <ProductName variant='h3'>{name}</ProductName>
                <Price>${price}</Price>

                <ActionButtons>
                    <Link href={`/products/${id}/edit`}>
                        <Button
                            variant='contained'
                            color='secondary'
                            size='small'
                            startIcon={<EditIcon />}
                        >
                            Edit
                        </Button>
                    </Link>

                    <LoadingButton
                        sx={{ color: 'red' }}
                        variant='contained'
                        size='small'
                        color='secondary'
                        onClick={() => setOpenDeleteDialog(true)}
                        startIcon={<DeleteIcon />}
                        loading={loading}
                    >
                        Delete
                    </LoadingButton>
                </ActionButtons>
            </CardInfo>

            <Dialog open={openDeleteDialog} onClose={() => setOpenDeleteDialog(false)}>
                <DialogTitle>Confirm Deletion</DialogTitle>
                <DialogContent>
                    Are you sure you want to delete "{name}"?
                </DialogContent>
                <DialogActions>
                    <Button
                        variant='contained'
                        onClick={() => setOpenDeleteDialog(false)}
                        disabled={loading}
                    >
                        Cancel
                    </Button>

                    <LoadingButton
                        variant='contained'
                        color='error'
                        onClick={handleDelete}
                        loading={loading}
                    >
                        Delete
                    </LoadingButton>
                </DialogActions>
            </Dialog>
        </Container>
    )
}
