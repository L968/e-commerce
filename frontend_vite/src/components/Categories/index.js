import styled from 'styled-components';
import { categories } from '../../services/data';
import CategoryItem from '../CategoryItem';
import { mobile } from '../../responsive';

export default function Categories() {
    return (
        <Container>
            {categories.map((item) => (
                <CategoryItem item={item} key={item.id} />
            ))}
        </Container>
    );
};

const Container = styled.div`
    display: flex;
    padding: 20px;
    justify-content: space-between;
    ${mobile({ padding: '0px', flexDirection:'column' })}
`;