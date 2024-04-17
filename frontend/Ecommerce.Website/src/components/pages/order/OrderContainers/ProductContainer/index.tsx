import Image from 'next/image';
import { OrderItem } from '@/interfaces/OrderItem';
import { Container, Product, ProductName } from './styles';

interface ProductContainerProps {
    items: OrderItem[]
}

export default function ProductContainer({ items }: ProductContainerProps) {
    return (
        <Container>
            {items.map((item, i) => (
                <Product key={i}>
                    <ProductName variant='h1'>{item.productName}</ProductName>

                    <Image
                        src={item.productImagePath}
                        alt={item.productName}
                        width={48}
                        height={48}
                        style={{
                            borderRadius: '25px',
                            border: '1px solid rgba(0, 0, 0, .1)'
                        }}
                    />
                </Product>
            ))}
        </Container>
    )
}
