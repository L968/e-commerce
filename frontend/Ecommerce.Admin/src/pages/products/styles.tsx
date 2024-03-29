import { Box, Grid, Paper } from '@mui/material';
import { styled } from '@mui/system';

export const Main = styled('main')({
    padding: '30px',
    width: '100%'
})

export const Container = styled(Paper)({
    display: 'flex',
    flexDirection: 'column',
    padding: '18px',
    gap: '20px',
})


export const Header = styled(Box)({
    display: 'flex',
})

export const ProductsContainer = styled(Grid)({
    display: 'flex',
})
