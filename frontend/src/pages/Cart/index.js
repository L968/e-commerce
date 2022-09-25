import styled from 'styled-components';
import { mobile } from '../../responsive';
import { IconButton } from '@mui/material';
import Footer from '../../components/Footer';
import Navbar from '../../components/Navbar';
import { useNavigate } from 'react-router-dom';
import { Add, Remove } from '@mui/icons-material';
import { Fragment, useEffect, useState } from 'react';
import Announcement from '../../components/Announcement';
import { cart, orderSummary as orderSummaryData} from '../../services/data';

export default function Cart() {
    const navigate = useNavigate();

    const [cartItems, setCartItems] = useState([]);
    const [orderSummary, setOrderSummary] = useState({});

    useEffect(() => {
        setCartItems(cart);
        setOrderSummary(orderSummaryData);
    }, []);

    return (
        <Container>
            <Navbar />
            <Announcement />
            <Wrapper>
                <Title>YOUR BAG</Title>
                <Top>
                    <TopButton onClick={() => navigate('/products')}>CONTINUE SHOPPING</TopButton>
                    <TopTexts>
                        <TopText>Shopping Bag(2)</TopText>
                        <TopText>Your Wishlist (0)</TopText>
                    </TopTexts>
                    <TopButton type='filled' onClick={() => navigate('/checkout')}>CHECKOUT NOW</TopButton>
                </Top>
                <Bottom>
                    <Info>
                        {cartItems.length > 0
                            ? cartItems.map((cartItem, i) =>
                                <Fragment key={i}>
                                    <Product>
                                        <ProductDetail>
                                            <Image src={cartItem.image} />
                                            <Details>
                                                <ProductName>
                                                    <b>Product:</b> {cartItem.name}
                                                </ProductName>
                                                <ProductId>
                                                    <b>ID:</b> {cartItem.id}
                                                </ProductId>
                                                <ProductColor color={cartItem.color} />
                                                <ProductSize>
                                                    <b>Size:</b> {cartItem.size}
                                                </ProductSize>
                                            </Details>
                                        </ProductDetail>
                                        <PriceDetail>
                                            <ProductAmountContainer>
                                                <IconButton>
                                                    <Remove />
                                                </IconButton>
                                                <ProductAmount>{cartItem.amount}</ProductAmount>
                                                <IconButton>
                                                    <Add />
                                                </IconButton>
                                            </ProductAmountContainer>
                                            <ProductPrice>$ {cartItem.price}</ProductPrice>
                                        </PriceDetail>
                                    </Product>
                                    {i !== cartItems.length - 1 && <Hr />}
                                </ Fragment>
                            )
                            :
                            <EmptyCartWrapper>
                                <EmptyCartImage src='https://m.media-amazon.com/images/G/15/cart/empty/kettle-desaturated._CB424694027_.svg' />
                                <h2>Your Cart is empty</h2>
                            </EmptyCartWrapper>
                        }
                    </Info>
                    <Summary>
                        <SummaryTitle>ORDER SUMMARY</SummaryTitle>
                        <SummaryItem>
                            <SummaryItemText>Subtotal</SummaryItemText>
                            <SummaryItemPrice>$ {orderSummary.subtotal}</SummaryItemPrice>
                        </SummaryItem>
                        <SummaryItem>
                            <SummaryItemText>Estimated Shipping</SummaryItemText>
                            <SummaryItemPrice>$ {orderSummary.estimatedShipping}</SummaryItemPrice>
                        </SummaryItem>
                        <SummaryItem>
                            <SummaryItemText>Shipping Discount</SummaryItemText>
                            <SummaryItemPrice>$ {orderSummary.shippingDiscount}</SummaryItemPrice>
                        </SummaryItem>
                        <SummaryItem type='total'>
                            <SummaryItemText>Total</SummaryItemText>
                            <SummaryItemPrice>$ {orderSummary.total}</SummaryItemPrice>
                        </SummaryItem>
                        <Button onClick={() => navigate('/checkout')}>CHECKOUT NOW</Button>
                    </Summary>
                </Bottom>
            </Wrapper>
            <Footer />
        </Container>
    );
};

const Container = styled.div``;

const Wrapper = styled.div`
    padding: 20px;
    ${mobile({ padding: '10px' })}
`;

const Title = styled.h1`
    font-weight: 300;
    text-align: center;
`;

const Top = styled.div`
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 20px;
`;

const TopTexts = styled.div`
`;

const TopText = styled.span`
    text-decoration: underline;
    cursor: pointer;
    margin: 0px 10px;
`;

const TopButton = styled.button`
  padding: 10px;
  font-weight: 600;
  cursor: pointer;
  border: ${props => props.type === "filled" && "none"};
  background-color: ${props => props.type === "filled" ? "black" : "transparent"};
  color: ${props => props.type === "filled" && "white"};
`;

const Bottom = styled.div`
    display: flex;
    justify-content: space-between;
    ${mobile({ flexDirection: 'column' })}
`;

const Info = styled.div`
    flex: 3;
`;

const Product = styled.div`
    display: flex;
    justify-content: space-between;
    ${mobile({ flexDirection: 'column' })}
`;

const ProductDetail = styled.div`
    flex: 2;
    display: flex;
`;

const Image = styled.img`
    width: 200px;
`;

const Details = styled.div`
    padding: 20px;
    display: flex;
    flex-direction: column;
    justify-content: space-around;
`;

const ProductName = styled.span``;

const ProductId = styled.span``;

const ProductColor = styled.div`
    width: 20px;
    height: 20px;
    border-radius: 50%;
    background-color: ${(props) => props.color};
`;

const ProductSize = styled.span``;

const PriceDetail = styled.div`
    flex: 1;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
`;

const ProductAmountContainer = styled.div`
    display: flex;
    align-items: center;
    margin-bottom: 20px;
`;

const ProductAmount = styled.div`
    font-size: 24px;
    margin: 5px;
    ${mobile({ margin: '5px 15px' })}
`;

const ProductPrice = styled.div`
    font-size: 30px;
    font-weight: 200;
    ${mobile({ marginBottom: '20px' })}
`;

const Hr = styled.hr`
    background-color: #eee;
    border: none;
    height: 1px;
`;

const Summary = styled.div`
    flex: 1;
    border: 0.5px solid lightgray;
    border-radius: 10px;
    padding: 20px;
    height: 50vh;
`;

const SummaryTitle = styled.h1`
    font-weight: 200;
`;

const SummaryItem = styled.div`
    margin: 30px 0px;
    display: flex;
    justify-content: space-between;
    font-weight: ${(props) => props.type === 'total' && '500'};
    font-size: ${(props) => props.type === 'total' && '24px'};
`;

const SummaryItemText = styled.span``;

const SummaryItemPrice = styled.span``;

const Button = styled.button`
    width: 100%;
    padding: 10px;
    background-color: black;
    color: white;
    font-weight: 600;
    cursor: pointer;
`;

const EmptyCartWrapper = styled.div`
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100%;
`;

const EmptyCartImage = styled.img`
    width: 300px;
`;