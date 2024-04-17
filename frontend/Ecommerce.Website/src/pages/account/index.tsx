import Link from 'next/link';
import { Avatar } from '@mui/material';
import PrivateRoute from '@/components/PrivateRoute';
import PlaceIcon from '@mui/icons-material/Place';
import PersonIcon from '@mui/icons-material/Person';
import CreditCardIcon from '@mui/icons-material/CreditCard';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import { Container, List, ListItem, ListItemContent, ListItemDescription, Main, UserEmail, UserInformations, Username } from './styles';

const menus = [
    {
        url: '/account/personal-data',
        icon: <PersonIcon />,
        header: 'Personal data',
        description: 'Data representing the account on this website',
    },
    {
        url: '/account/addresses',
        icon: <PlaceIcon />,
        header: 'Addresses',
        description: 'Saved addresses in your account',
    },
    {
        url: '/account/credit-cards',
        icon: <CreditCardIcon />,
        header: 'Cards',
        description: 'Cards saved in your account',
    },
];

function Account() {
    return (
        <Main>
            <Container>
                <Avatar
                    alt='James Bond'
                    sx={{ width: 80, height: 80 }}
                />

                <UserInformations>
                    <Username variant='h2'>James Bond</Username>
                    <UserEmail>jamesbond@test.com</UserEmail>
                </UserInformations>
            </Container>

            <Container>
                <List>
                    {menus.map(menu => (
                        <Link href={menu.url} style={{ textDecoration: 'none', color: 'inherit' }}>
                            <ListItem>
                                <Avatar sx={{ bgcolor: 'primary.main' }}>
                                    {menu.icon}
                                </Avatar>

                                <ListItemContent>
                                    <span>{menu.header}</span>
                                    <ListItemDescription>{menu.description}</ListItemDescription>
                                </ListItemContent>

                                <ChevronRightIcon sx={{ marginLeft: 'auto' }} />
                            </ListItem>
                        </Link>
                    ))}
                </List>
            </Container>
        </Main>
    )
}

export default function Private() {
    return (
        <PrivateRoute>
            <Account />
        </PrivateRoute>
    )
}
