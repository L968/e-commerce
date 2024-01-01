import Image from 'next/image';
import Link from 'next/link';
import { Button } from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import { ActionButtons, CardInfo, Container, Price, ProductName } from './styles';
import { Product } from '@/interfaces/api/responses/GetProductListResponse';

export default function ProductCard(product: Product) {
    const { id, name, discountedPrice: price, imageSource } = product;

    function handleDelete(e: React.MouseEvent) {
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


                    <Button
                        sx={{ color: 'red' }}
                        variant="contained"
                        size='small'
                        color="secondary"
                        onClick={handleDelete}
                        startIcon={<DeleteIcon />}
                    >
                        Delete
                    </Button>
                </ActionButtons>
            </CardInfo>
        </Container>
    )
}
