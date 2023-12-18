import { styled } from '@mui/system';
import { Box, Paper, Typography } from '@mui/material';

export const Container = styled(Paper)({
    display: 'flex',
    width: '239px',
    flexDirection: 'column',
    borderRadius: '8px',
    overflow: 'hidden',
    border: '1px solid #FFF',
})

export const CardInfo = styled(Box)({
    display: 'flex',
    alignItems: 'center',
    flexDirection: 'column',
    gap: '5px',
    padding: '20px',
})

export const ProductName = styled(Typography)({
    fontSize: '16px',
    fontWeight: 600
}) as typeof Typography

export const Price = styled(Typography)({
    fontSize: '20px',
    fontWeight: 600
}) as typeof Typography

export const ActionButtons = styled(Box)({
    display: 'flex',
    gap: '10px',
    marginTop: '10px',
})
