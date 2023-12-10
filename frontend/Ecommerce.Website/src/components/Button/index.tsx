import MuiButton from '@mui/material/Button';
import { styled } from '@mui/system';

const Button = styled(MuiButton)(({ theme }) => ({
    color: '#FFF',
    backgroundColor: theme.palette.primary.main,
    '&:hover': {
        backgroundColor: theme.palette.primary.main,
    },
}))

export default Button;
