import Box from '@mui/material/Box';
import { styled } from '@mui/system';
import { Typography } from '@mui/material';

export const Container = styled('article')({
    borderRadius: '6px',
    marginBottom: '16px',
    backgroundColor: '#FFF',
    border: '1px solid rgba(0, 0, 0, .1)',
})

export const Header = styled(Box)({
    borderBottom: '1px solid rgba(0, 0, 0, .1)',
})

export const CreatedAt = styled(Typography)({
    fontWeight: 600,
    fontSize: '14px',
    padding: '16px 0 16px 24px',
}) as typeof Typography

export const Order = styled(Box)({
    display: 'flex',
    justifyContent: 'space-between',
    padding: '24px',
    borderBottom: '1px solid rgba(0, 0, 0, .1)',
})

export const Item = styled(Box)({
    display: 'flex',
    width: '50%',
    paddingRight: '32px',
})

export const Info = styled(Box)({
    display: 'flex',
    flexDirection: 'column',
    marginLeft: '24px',
})

export const Status = styled('p')({
    fontSize: '14px',
    fontWeight: 600,
    margin: 0,
    marginBottom: '6px',
})

export const ProductName = styled('p')({
    margin: 0,
    marginTop: '16px',
    fontSize: '14px',
    color: 'rgba(0, 0, 0, .55)',
})

export const Description = styled('p')({
    margin: 0,
    fontSize: '14px',
    color: 'rgba(0, 0, 0, .55)',
})

export const UserActions = styled(Box)({
    display: 'flex',
    flexDirection: 'column',
    gap: '15px',
    height: '100%'
})

