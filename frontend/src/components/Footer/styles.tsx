import { styled } from '@mui/system';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';

export const FooterHeader = styled(Box)({
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: '40px 200px',
    backgroundColor: '#FAFAFA',
    zIndex: 1,
    position: 'relative'
})

export const BrandName = styled('span')({
    fontSize: '24px',
    fontWeight: 600,
    color: '#252B42',
})

export const SocialMediaIcons = styled('div')(({ theme }) => ({
    color: theme.palette.primary.main,
    '& > *': {
        margin: '0 7px'
    }
}))

export const FooterContent = styled(Box)({
    padding: '40px 200px',
    backgroundColor: '#FFF',
})

export const FooterCategory = styled('div')({
    fontWeight: 600,
    fontSize: '16px',
    color: '#252B42',
})

export const FooterSubCategory = styled('div')({
    color: '#737373',
    marginTop: '20px',
})

export const EmailTextField = styled(TextField)({
    marginTop: '20px',
    backgroundColor: '#F9F9F9',
    [`& fieldset`]: {
        borderTopRightRadius: 0,
        borderBottomRightRadius: 0,
    },
})

export const SubscribeButton = styled(Button)(({ theme }) => ({
    color: '#FFF',
    marginTop: '20px',
    borderTopLeftRadius: 0,
    borderBottomLeftRadius: 0,
    backgroundColor: theme.palette.primary.main,
    height: '56px',
    '&:hover': {
        backgroundColor: theme.palette.primary.main,
    },
}))

export const FooterCredits = styled(Box)({
    backgroundColor: '#FAFAFA',
    padding: '20px 200px',
    color: '#737373',
})
