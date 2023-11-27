import { Badge } from '@mui/material';
import styled from 'styled-components';
import { Link } from 'react-router-dom';
import { Search as SearchIcon, ShoppingCartOutlined as ShoppingCartIcon } from '@mui/icons-material';
import { mobile } from '../../responsive';

export default function Navbar() {
    return (
        <Container>
            <Wrapper>
                <Left>
                    <Link to='/'>
                        <Logo>Lama.</Logo>
                    </Link>
                </Left>
                <Center>
                    <SearchContainer>
                        <Input placeholder='Search for items' />
                        <SearchIcon />
                    </SearchContainer>
                </Center>
                <Right>
                    <Link to='/register'>
                        <MenuItem>Register</MenuItem>
                    </Link>
                    <Link to='/login'>
                        <MenuItem>Sign In</MenuItem>
                    </Link>
                    <Link to='/cart'>
                        <MenuItem>
                            <Badge badgeContent={4} color='primary'>
                                <ShoppingCartIcon />
                            </Badge>
                        </MenuItem>
                    </Link>
                </Right>
            </Wrapper>
        </Container>
    )
}

const Container = styled.div`
    height: 60px;
    ${mobile({ height: '50px' })}
`;

const Wrapper = styled.div`
    padding: 10px 20px;
    display: flex;
    align-items: center;
    justify-content: space-between;
    ${mobile({ padding: '10px 0px' })}
`;

const Left = styled.div`
    flex: 1;
    display: flex;
    align-items: center;
`;

const Logo = styled.h1`
    font-weight: bold;
    ${mobile({ fontSize: '24px' })}
`;

const Center = styled.div`
    flex: 1;
    text-align: center;
`;

const SearchContainer = styled.div`
    border: 0.5px solid lightgray;
    display: flex;
    align-items: center;
    margin-left: 25px;
    padding: 5px;
`;

const Input = styled.input`
    border: none;
    width: 100%;
    background: #FAFAFA;
    ${mobile({ width: '100px' })}
`;

const Right = styled.div`
    flex: 1;
    display: flex;
    align-items: center;
    justify-content: flex-end;
    ${mobile({ justifyContent: 'center', flex: 2 })}
`;

const MenuItem = styled.div`
    font-size: 14px;
    cursor: pointer;
    margin-left: 25px;
    ${mobile({ fontSize: '12px', marginLeft: '10px' })}
`;