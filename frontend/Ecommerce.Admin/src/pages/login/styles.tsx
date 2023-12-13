import { styled } from '@mui/system';

export const Main = styled('main')(({ theme }) => ({
    height: '100vh',
    backgroundColor: theme.palette.background.main,
    flexGrow: 1,
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
}))

export const Form = styled('form')({
    display: 'flex',
    flexDirection: 'column',
    gap: '24px',
})
