import { styled } from '@mui/system';
import { Box, Typography } from '@mui/material';

export const Main = styled('main')({
    display: 'flex',
    flexDirection: 'column',
    gap: '10px',
    padding: '20px 60px',
    backgroundColor: '#F5F5F5',
})

export const CartContainer = styled(Box)({
    border: '1px solid #ccc',
    padding: '20px',
})

export const Section = styled(Box)({
    display: 'flex',
    flexDirection: 'column',
    gap: '5px',
    border: '1px solid #D5D9D9',
    borderRadius: '6px',
    padding: '20px',
    backgroundColor: '#FFF',
    '& span': {
        fontSize: '14px',
    }
})

export const SectionTitle = styled(Typography)({
    fontSize: '18px',
    fontWeight: 600,
    marginBottom: '5px',
}) as typeof Typography

export const SectionContent = styled(Box)({
    display: 'flex',
    alignItems: 'center',
    gap: '10px',
})

export const ItemContainer = styled(Box)({
    display: 'flex',
    padding: '24px',
    border: '1px solid #E5E5E5',
    backgroundColor: '#FFF',
})

export const ItemInfo = styled(Box)({
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
