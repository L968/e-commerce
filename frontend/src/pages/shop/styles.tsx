import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import MuiBreadcrumbs from "@mui/material/Breadcrumbs";
import { styled } from '@mui/system';
import Grid from '@mui/material/Grid';

export const Header = styled(Box)({
    backgroundColor: '#FAFAFA',
    height: '64px',
    display: 'flex',
    alignItems: 'center',
    padding: '0 80px 0 24px'
})

export const Title = styled(Typography)({
    color: '#252B42',
    marginRight: '100px',
    fontWeight: 600,
}) as typeof Typography

export const Breadcrumbs = styled(MuiBreadcrumbs)({
    marginLeft: 'auto'
})

export const BreadcrumbItem = styled(Typography)({
    color: '#252B42',
    fontWeight: 600,
})

export const CardContainer = styled(Grid)({
    display: 'flex',
    justifyContent: 'center',
    marginBottom: '50px',
})

export const Card = styled(Grid)({
    height: '223px',
    width: '223px',
    backgroundColor: 'red',
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    margin: '0 10px'
})