import { styled } from '@mui/system';
import { Paper, Typography } from '@mui/material';

export const Main = styled('main')({
    padding: '30px',
    minWidth: '600px',
    '& h1': {
        fontSize: '20px'
    }
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

export const Form = styled('form')({
    display: 'flex',
    flexDirection: 'column',
    gap: '20px'
})
