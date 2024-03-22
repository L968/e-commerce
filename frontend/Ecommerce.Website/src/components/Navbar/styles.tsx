import { styled } from '@mui/system';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import MuiAvatar from '@mui/material/Avatar';
import Typography from '@mui/material/Typography';

export const StyledNavbar = styled(Box)({
    flexGrow: 1,
})

export const DarkToolbar = styled(Toolbar)({
    backgroundColor: '#252B42',
    color: '#FFF',
    fontSize: '12px',
})


export const LightToolbar = styled(Toolbar)({
    backgroundColor: '#FFF',
})

export const BrandName = styled(Typography)({
    color: '#252B42',
    marginRight: '100px',
    fontWeight: 600,
}) as typeof Typography

export const NavMenuItems = styled('div')({
    display: 'flex',
    flexGrow: 1
})

export const NavMenuItem = styled('div')({
    color: '#737373',
    fontWeight: 600,
    margin: '0 20px',
})

export const UserActions = styled(Box)(({ theme }) => ({
    display: 'flex',
    color: theme.palette.primary.main,
}))

export const UserAction = styled(Box)({
    display: 'flex',
    alignItems: 'center',
    marginLeft: '40px',
})

export const Avatar = styled(MuiAvatar)({
    marginRight: '5px',
    width: '30px',
    height: '30px',
    fontSize: '15px',
})
