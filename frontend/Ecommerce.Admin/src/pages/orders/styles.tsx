import { styled } from '@mui/system';
import Paper from '@mui/material/Paper';

export const Main = styled('main')(({ theme }) => ({
}))


export const Container = styled(Paper)({
    display: 'flex',
    flexDirection: 'column',
    padding: '18px',
    gap: '20px',
})