import moment from 'moment';
import Image from 'next/image';
import Button from '../Button';
import { OrderItem } from '@/interfaces/OrderItem';
import ShoppingBagIcon from '@mui/icons-material/ShoppingBag';
import { Container, CreatedAt, Header, Order, Item, Info, Status, ProductName, Description, UserActions } from './styles';
import Link from 'next/link';

interface OrderListItemProps {
    orderId: string
    status: string
    createdAt: Date
    items: OrderItem[]
}

export default function OrderListItem({ orderId, status, createdAt, items }: OrderListItemProps) {
    return (
        <Container>
            <Header>
                <CreatedAt variant='h2'>{moment(createdAt).format('LL')}</CreatedAt>
            </Header>

            {items.map(item => (
                <Order>
                    <Item>
                        <Image
                            src={item.productImagePath}
                            alt={item.productName}
                            width={72}
                            height={72}
                            style={{
                                borderRadius: '6px',
                                border: '1px solid rgba(0, 0, 0, .1)'
                            }}
                        />

                        <Info>
                            <Status>{status}</Status>
                            <ProductName>{item.productName}</ProductName>
                            <Description>{item.description || 'description'}</Description>
                        </Info>
                    </Item>

                    <UserActions>
                        <Link href={`/orders/${orderId}`}>
                            <Button variant='contained'>Order Details</Button>
                        </Link>
                        <Button variant='contained' startIcon={<ShoppingBagIcon />}>Reorder</Button>
                    </UserActions>
                </Order>
            ))}
        </Container>
    )
}
