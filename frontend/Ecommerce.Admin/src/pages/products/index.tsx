import { Container, Header, Main } from './styles';
import { TextField, Typography } from '@mui/material';
import ProductCard from '@/components/ProductCard';

export default function Products() {
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

                <ProductCard
                    id='x'
                    name='Test'
                    price={99.90}
                    imageSource='https://lcoiecommercestorage.blob.core.windows.net/images/4dbe281e-1d4c-40f5-8944-3d2cb2bc1666.png'
                />
            </Container>
        </Main>
    )
}
