import styled from 'styled-components';
import { popularProducts } from '../../services/data';
import Product from '../Product';

export default function Products() {
    return (
        <Container>
            {popularProducts.map((item) => (
                <Product item={item} key={item.id} />
            ))}
        </Container>
    );
};

const Container = styled.div`
    padding: 20px;
    display: flex;
    flex-wrap: wrap;
    justify-content: space-between;
`;