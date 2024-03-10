import { styled } from '@mui/system';
import { Box, Typography } from '@mui/material';

export const Container = styled(Box)({
    backgroundColor: '#F5F5F5',
    borderRadius: '6px',
})

export const Product = styled(Box)({
    padding: '7px 24px',
    display: 'flex',
    gap: '12px',
    justifyContent: 'space-between',
    alignItems: 'center',
    borderBottom: '1px solid rgba(0,0,0,.1)'
})

export const ProductName = styled(Typography)({
    fontSize: '13px',
}) as typeof Typography
