import { styled } from '@mui/system';
import MuiButton from '@mui/material/Button';

const Button = styled(MuiButton)(({ theme }) => ({
    color: '#FFF',
    backgroundColor: theme.palette.primary.main,
    '&:hover': {
        backgroundColor: theme.palette.primary.main,
    },
}))

export default Button;
