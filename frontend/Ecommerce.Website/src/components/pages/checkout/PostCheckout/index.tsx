import Link from 'next/link';
import Address from '@/interfaces/Address';
import { Badge, Button } from '@mui/material';
import CheckIcon from '@mui/icons-material/Check';
import ShoppingBagIcon from '@mui/icons-material/ShoppingBag';
import { BannerContainer, Container, DetailsAction, DetailsContainer, DetailsTitle, SuccessBanner, Title } from './styles';

interface PostCheckoutProps {
    address?: Address
}

export default function PostCheckout({ address }: PostCheckoutProps) {
    function formatAddress(address: Address): string {
        let formattedAddress = `${address.streetName} ${address.buildingNumber}`;

        if (address.complement) {
            formattedAddress += `, ${address.complement}`;
        }

        return formattedAddress;
    }

    return (
        <Container>
            <SuccessBanner>
                <BannerContainer>
                    <Title variant='h1'>We will notify you when your purchase is on the way</Title>
                    <Badge
                        variant='standard'
                        showZero
                        color='success'
                        sx={{ "& .MuiBadge-badge": { height: 20, width: 20 } }}
                        badgeContent={<CheckIcon sx={{ fontSize: '17px' }} />}
                        anchorOrigin={{
                            vertical: 'bottom',
                            horizontal: 'right',
                        }}
                    >
                        <ShoppingBagIcon sx={{ color: '#FFF', fontSize: '55px' }} />
                    </Badge>
                </BannerContainer>
            </SuccessBanner>

            <DetailsContainer>
                <DetailsTitle>
                    <h2>
                        {address
                            ? <>Shipping to {formatAddress(address)}</>
                            : <>Thank you for your purchase!</>
                        }
                    </h2>
                </DetailsTitle>
                <DetailsAction>
                    <Link href='/orders'>
                        <Button variant='contained' size='small'>View in my orders</Button>
                    </Link>
                </DetailsAction>
            </DetailsContainer>
        </Container>
    )
}
