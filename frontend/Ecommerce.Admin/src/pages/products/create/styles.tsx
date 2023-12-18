import { styled } from '@mui/system';
import { Paper } from '@mui/material';

export const Main = styled('main')({
    display: 'flex',
    justifyContent: 'center',
    flexDirection: 'column',
    alignItems: 'center',
    width: '100%',
    padding: '30px',
    minHeight: '100vh',
})

export const Container = styled(Paper)({
    display: 'flex',
    flexDirection: 'column',
    padding: '30px',
    marginTop: '15px',
    borderRadius: '10px',
    '& h1': {
        fontSize: '20px'
    },
})

export const Form = styled('form')({
    display: 'flex',
    flexDirection: 'column',
    gap: '20px'
})
