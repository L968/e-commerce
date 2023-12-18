import { styled } from '@mui/system';
import { Paper } from '@mui/material';

export const Main = styled('main')({
    padding: '30px',
    width: '100%'
})

export const Container = styled(Paper)({
    display: 'flex',
    flexDirection: 'column',
    padding: '30px',
    marginTop: '15px',
    borderRadius: '10px',
})
