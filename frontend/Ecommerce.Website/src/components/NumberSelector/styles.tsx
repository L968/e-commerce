import { styled } from '@mui/system';
import { Box, Input as MuiInput } from '@mui/material';

export const Container = styled(Box)({
    display: 'flex',
    alignItems: 'center',
    border: '1px solid #E5E5E5',
    borderRadius: '4px',
    padding: '0 10px',
    height: '32px',
})

export const Input = styled(MuiInput)({
    '& input[type=number]::-webkit-inner-spin-button, & input[type=number]::-webkit-outer-spin-button': {
        '-webkit-appearance': 'none',
        margin: 0,
    },
    '& input[type=number]': {
        '-moz-appearance': 'textfield',
    },
    maxWidth: '20px',
    textDecoration: 'none',
    borderBottom: 'none',
    outline: 'none',
})

export const Value = styled('div')({
    padding: '0 10px'
})
