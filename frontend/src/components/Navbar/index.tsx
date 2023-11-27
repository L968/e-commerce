import Link from '../Link';
import Grid from '@mui/material/Grid';
import AppBar from '@mui/material/AppBar';

import PhoneIcon from '@mui/icons-material/Phone';
import EmailIcon from '@mui/icons-material/Email';
import InstagramIcon from '@mui/icons-material/Instagram';
import YouTubeIcon from '@mui/icons-material/YouTube';
import FacebookIcon from '@mui/icons-material/Facebook';
import TwitterIcon from '@mui/icons-material/Twitter';
import PersonIcon from '@mui/icons-material/Person';
import SearchIcon from '@mui/icons-material/Search';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import FavoriteIcon from '@mui/icons-material/Favorite';

import { BrandName, NavMenuItems, StyledNavbar, DarkToolbar, LightToolbar, CenteredGrid, NavMenuItem, UserActions, UserAction } from './styles';

export default function Navbar() {
    return (
        <StyledNavbar>
            <AppBar position="static" elevation={0}>
                <DarkToolbar variant='dense'>
                    <Grid container>
                        <CenteredGrid item xs={2}>
                            <PhoneIcon />
                            (255) 555-0118
                        </CenteredGrid>
                        <CenteredGrid item xs={4}>
                            <EmailIcon />
                            example@example.com
                        </CenteredGrid>
                        <CenteredGrid item xs={4}>
                            Follow Us and get a chance to win 80% off
                        </CenteredGrid>
                        <CenteredGrid item xs={2}>
                            Follow Us:
                            <InstagramIcon />
                            <YouTubeIcon />
                            <FacebookIcon />
                            <TwitterIcon />
                        </CenteredGrid>
                    </Grid>
                </DarkToolbar>

                <LightToolbar>
                    <BrandName variant="h6" component="div">
                        Brand Name
                    </BrandName>

                    <NavMenuItems>
                        <NavMenuItem>
                            <Link href='/home'>Home</Link>
                        </NavMenuItem>
                        <NavMenuItem>
                            <Link href='/shop'>Shop</Link>
                        </NavMenuItem>
                        <NavMenuItem>
                            <Link href='/about'>About</Link>
                        </NavMenuItem>
                        <NavMenuItem>
                            <Link href='/blog'>Blog</Link>
                        </NavMenuItem>
                        <NavMenuItem>
                            <Link href='/contact'>Contact</Link>
                        </NavMenuItem>
                        <NavMenuItem>
                            <Link href='/pages'>Pages</Link>
                        </NavMenuItem>
                    </NavMenuItems>

                    <UserActions>
                        <UserAction>
                            <PersonIcon />
                            <Link href='/login'>Login</Link>
                            &nbsp;/&nbsp;
                            <Link href='/register'>Register</Link>
                        </UserAction>
                        <UserAction>
                            <SearchIcon />
                        </UserAction>
                        <UserAction>
                            <ShoppingCartIcon /> 1
                        </UserAction>
                        <UserAction>
                            <FavoriteIcon /> 1
                        </UserAction>
                    </UserActions>
                </LightToolbar>
            </AppBar>
        </StyledNavbar>
    );
}
