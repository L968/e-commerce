import Link from '../Link';
import { useState } from 'react';
import { AppBar, Menu, MenuItem, Grid, GridProps } from '@mui/material';

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
import ArrowDropDownIcon from '@mui/icons-material/ArrowDropDown';

import { useAuth } from '@/contexts/authContext';
import { BrandName, NavMenuItems, StyledNavbar, DarkToolbar, LightToolbar, NavMenuItem, UserActions, UserAction, Avatar } from './styles';

export default function Navbar() {
    const user = {};
    const { isAuthenticated, logout } = useAuth();
    const [anchorEl, setAnchorEl] = useState<HTMLElement | null>(null);

    function handleClose() {
        setAnchorEl(null);
    }

    function handleLogout() {
        logout();
        handleClose();
    }

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

                <LightToolbar variant='dense'>
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
                        {isAuthenticated ? (
                            <UserAction
                                onMouseOver={(e) => setAnchorEl(e.currentTarget)}
                                sx={{ cursor: 'pointer' }}
                            >
                                <Avatar {...stringAvatar('James Bond')} />
                                {'James'}
                                <ArrowDropDownIcon />
                            </UserAction>
                        ) : (
                            <UserAction>
                                <PersonIcon />
                                <Link href='/login'>Login</Link>
                                &nbsp;/&nbsp;
                                <Link href='/register'>Register</Link>
                            </UserAction>
                        )}
                        <UserAction>
                            <SearchIcon />
                        </UserAction>
                        {isAuthenticated &&
                            <>
                                <UserAction>
                                    <Link href='/cart'>
                                        <ShoppingCartIcon /> 1
                                    </Link>
                                </UserAction>
                                <UserAction>
                                    <FavoriteIcon /> 1
                                </UserAction>
                            </>
                        }
                    </UserActions>

                    <Menu
                        anchorEl={anchorEl}
                        open={Boolean(anchorEl)}
                        onClose={handleClose}
                        MenuListProps={{ onMouseLeave: handleClose }}
                        anchorOrigin={{
                            vertical: 'top',
                            horizontal: 'left',
                        }}
                        transformOrigin={{
                            vertical: 'top',
                            horizontal: 'left',
                        }}
                    >
                        <MenuItem onClick={handleClose}>
                            <Link href='/profile'>Profile</Link>
                        </MenuItem>
                        <MenuItem onClick={handleClose}>
                            <Link href='/orders'>Orders</Link>
                        </MenuItem>
                        <MenuItem onClick={handleLogout}>
                            Logout
                        </MenuItem>
                    </Menu>
                </LightToolbar>
            </AppBar>
        </StyledNavbar >
    )
}

function stringToColor(string: string): string {
    let hash = 0;
    let i;

    for (i = 0; i < string.length; i += 1) {
        hash = string.charCodeAt(i) + ((hash << 5) - hash);
    }

    let color = '#';

    for (i = 0; i < 3; i += 1) {
        const value = (hash >> (i * 8)) & 0xff;
        color += `00${value.toString(16)}`.slice(-2);
    }

    return color;
}

function stringAvatar(name: string) {
    return {
        sx: {
            bgcolor: stringToColor(name),
        },
        children: `${name.split(' ')[0][0]}${name.split(' ')[1][0]}`,
    };
}

function CenteredGrid(props: GridProps) {
    const { children, ...rest } = props;
    return (
        <Grid
            {...rest}
            sx={{
                display: 'flex',
                alignItems: 'center',
                '& > *': {
                    margin: '0 6px',
                },
            }}
        >
            {children}
        </Grid>
    );
}
