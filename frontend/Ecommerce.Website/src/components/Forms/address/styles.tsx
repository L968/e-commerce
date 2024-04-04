import { styled } from '@mui/system';
import Typography from '@mui/material/Typography';

export const Main = styled('main')({
    padding: '30px 0',
    backgroundColor: '#EDEDED',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    gap: '16px',
})

export const Form = styled('form')({
    display: 'flex',
    flexDirection: 'column',
    width: '720px',
    height: 'auto',
    minHeight: '128px',
    backgroundColor: '#FFF',
    borderRadius: '6px',
    padding: '24px',
    boxShadow: '0 1px 2px 0 rgba(0, 0, 0, .12)',
})

export const Title = styled(Typography)({
    fontSize: '20px',
    fontWeight: 600,
    margin: 0,
    width: '768px',
}) as typeof Typography
