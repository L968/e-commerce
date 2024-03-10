import { Box, Typography } from '@mui/material';
import { styled } from '@mui/system';

export const Container = styled(Box)({
    backgroundColor: '#FFF',
    borderRadius: '6px',
    boxShadow: '0 8px 16px 0 rgba(0, 0, 0, .1)',
    '& > div': {
        fontSize: '14px',
        padding: '14px 16px',
        borderBottom: '1px solid rgba(0, 0, 0, 0.1)',
        '&:hover': {
            cursor: 'pointer',
            backgroundColor: '#F5F5F5',
        },
    }
})

export const Title = styled(Typography)({
    fontSize: '16px',
    fontWeight: 600,
    padding: '12px 16px',
    borderBottom: '1px solid rgba(0, 0, 0, 0.1)',
}) as typeof Typography
