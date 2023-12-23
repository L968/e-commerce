import { Box } from '@mui/material';
import { styled } from '@mui/system';

export const Form = styled('form')({
    display: 'flex',
    flexDirection: 'column',
    gap: '20px'
})

export const CombinationsContainer = styled(Box)({
    display: 'flex',
    flexDirection: 'column',
    gap: '20px'
})

export const Container = styled(Box)({
    display: 'flex',
    flexDirection: 'column',
    gap: '20px',
    border: '1px solid #9E9E9E',
    padding: '20px'
})

export const Header = styled(Box)({
    display: 'flex',
    alignItems: 'center',
})
