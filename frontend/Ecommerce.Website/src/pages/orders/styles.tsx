import { styled } from '@mui/system';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';

export const Main = styled('main')({
    padding: '40px 220px',
    backgroundColor: '#EDEDED',
})

export const Title = styled(Typography)({
    fontSize: '24px',
    fontWeight: 600,
    marginBottom: '20px',
}) as typeof Typography

export const PaginationContainer = styled(Box)({
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
})
