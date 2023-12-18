import Image from 'next/image';
import { Button } from '@mui/material';
import { useRouter } from 'next/navigation';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import { ActionButtons, CardInfo, Container, Price, ProductName } from './styles';
import GetProductListResponse from '@/interfaces/api/responses/GetProductListResponse';

export default function ProductCard(product: GetProductListResponse) {
    const router = useRouter();
    const { id, name, discountedPrice: price, imageSource } = product;

    function handleEdit (e: React.MouseEvent) {
        router.push(`/product/${id}`)
        console.log('Editar produto:', id);
    };

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
                    <Button
                        variant="contained"
                        color="secondary"
                        size='small'
                        onClick={handleEdit}
                        startIcon={<EditIcon />}
                    >
                        Edit
                    </Button>

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
