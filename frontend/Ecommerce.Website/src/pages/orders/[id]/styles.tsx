import { Box, Typography } from '@mui/material';
import { styled } from '@mui/system';

export const Main = styled('main')({
    display: 'flex',
})

export const Container = styled(Box)({
    maxWidth: '100vw',
    padding: '40px 220px',
    backgroundColor: '#EDEDED',
})

export const Aside = styled('aside')({
    backgroundColor: '#F5F5F5',
    height: '100%',
    padding: '105px 48px 48px 48px',
})

export const AsideContent = styled(Box)({

})

export const ProductsContainer = styled(Box)({
    backgroundColor: '#FFF',
    borderRadius: '6px',
    border: '1px solid #D5D9D9'
})

export const Product = styled(Box)({
    padding: '14px 18px',
    borderBottom: '1px solid #D5D9D9'
})
