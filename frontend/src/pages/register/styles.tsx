import { styled } from '@mui/system';
import Box from '@mui/material/Box';
import Grid from "@mui/material/Grid";
import Typography from '@mui/material/Typography';

export const Main = styled('main')({
    padding: '0 350px',
    backgroundColor: '#FAFAFA',
})

export const Container = styled(Grid)({
    backgroundColor: '#FFF',
})

export const LoginArea = styled(Grid)({
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
})

export const LoginFrame = styled(Box)({
    display: 'inline-flex',
    flexDirection: 'column',
    alignItems: 'center',
    width: '330px',
})

export const BrandName = styled(Typography)({
    fontSize: '20px',
    fontWeight: 600,
    marginBottom: '40px',
}) as typeof Typography

export const Form = styled('form')({
    display: 'flex',
    flexDirection: 'column',
    gap: '24px',
})