import { Paper, Typography } from '@mui/material';
import { styled } from '@mui/system';

export const Main = styled('main')({
    padding: '30px',
    width: '100%'
})

export const Title = styled(Typography)({
    fontSize: '50px',
}) as typeof Typography

export const Container = styled(Paper)({
    display: 'flex',
    flexDirection: 'column',
    padding: '30px',
    marginTop: '15px',
    borderRadius: '10px',
})