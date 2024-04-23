import { styled } from '@mui/system';
import Paper from '@mui/material/Paper';

export const Main = styled('main')(({ theme }) => ({
    padding: '30px',
    width: '100%'
}))


export const Container = styled(Paper)({
    display: 'flex',
    flexDirection: 'column',
    padding: '30px',
    gap: '15px',
    marginTop: '15px',
    borderRadius: '10px',
})