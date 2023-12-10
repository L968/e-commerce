import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import { styled } from '@mui/system';

export const Container = styled(Box)({
    display: 'flex',
    width: '239px',
    flexDirection: 'column',
    alignItems: 'flex-start',
    cursor: 'pointer',
    '& img': {
        transition: 'filter 0.2s',
    },
    '& img:hover': {
        filter: 'brightness(1.1)',
    },
})

export const CardInfo = styled(Box)({
    display: 'flex',
    alignItems: 'center',
    alignSelf: 'stretch',
    flexDirection: 'column',
    gap: '10px',
    padding: '25px 25px 35px 25px',
})

export const ProductName = styled(Typography)({
    fontSize: '16px',
    color: '#252B42',
    fontWeight: 600
}) as typeof Typography

export const ProductCategory = styled(Typography)({
    color: '#737373',
    fontWeight: 600
}) as typeof Typography

export const Prices = styled(Box)({
    display: 'flex',
    padding: '5px 3px',
    gap: '5px',
})

export const ProductOriginalPrice = styled(Typography)({
    fontSize: '16px',
    color: '#BDBDBD',
    fontWeight: 600,
    textDecoration: 'line-through'
}) as typeof Typography

export const ProductDiscountedPrice = styled(Typography)({
    fontSize: '16px',
    color: '#23856D',
    fontWeight: 600
}) as typeof Typography

export const Colors = styled(Box)({
    display: 'flex',
    gap: '6.077px',
})
