import { Typography } from '@mui/material';
import { styled } from '@mui/system';

export const Main = styled('main')({
    padding: '40px 220px',
    backgroundColor: '#EDEDED',
})

export const Title = styled(Typography)({
    fontSize: '24px',
    fontWeight: 600,
    marginBottom: '20px',
}) as typeof Typography
