import { styled } from '@mui/system';
import Box from '@mui/material/Box';

export const Container = styled(Box)({
    display: 'flex',
    padding: '24px',
    border: '1px solid #E5E5E5',
    backgroundColor: '#FFF',
})

export const CartInfo = styled(Box)({
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'space-between',
    marginLeft: '24px',
    marginRight: '16px',
    width: '390px',
})

export const ProductName = styled('span')({
    fontSize: '16px',
    fontWeight: 600,
    overflow: 'hidden',
    whiteSpace: 'nowrap',
    textOverflow: 'ellipsis',
})

export const Price = styled('span')({
    fontSize: '20px',
    marginLeft: 'auto',
})
