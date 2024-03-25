import Image from 'next/image';
import Link from 'next/link';
import api from '@/services/api';
import { useState } from 'react';
import { Button } from '@mui/material';
import { toast } from 'react-toastify';
import { LoadingButton } from '@mui/lab';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import { Product } from '@/interfaces/api/responses/GetProductListResponse';
import { ActionButtons, CardInfo, Container, Price, ProductName } from './styles';

export default function ProductCard(product: Product) {
    const { id, name, discountedPrice: price, imageSource } = product;
    const [loading, setLoading] = useState<boolean>(false);

    function handleDelete() {
        setLoading(true);

        api.delete('/product/' + id)
            .then(response => toast.success("Product deleted"))
            .catch(error => toast.error("Error 500"))
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
                            variant="contained"
                            color="secondary"
                            size='small'
                            startIcon={<EditIcon />}
                        >
                            Edit
                        </Button>
                    </Link>

                    <LoadingButton
                        sx={{ color: 'red' }}
                        variant="contained"
                        size='small'
                        color="secondary"
                        onClick={handleDelete}
                        startIcon={<DeleteIcon />}
                        loading={loading}
                    >
                        Delete
                    </LoadingButton>
                </ActionButtons>
            </CardInfo>
        </Container>
    )
}
