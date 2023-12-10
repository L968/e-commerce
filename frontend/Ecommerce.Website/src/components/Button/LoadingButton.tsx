import MuiLoadingButton from '@mui/lab/LoadingButton';
import { styled } from '@mui/system';

const Button = styled(MuiLoadingButton)(({ theme }) => ({
    color: '#FFF',
    backgroundColor: theme.palette.primary.main,
    '&:hover': {
        backgroundColor: theme.palette.primary.main,
    },
    '.MuiLoadingButton-loadingIndicator': {
        color: '#FFF',
    },
}))

export default Button;
