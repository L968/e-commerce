import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import { styled } from '@mui/system';

export const Banner = styled(Box)({
    display: 'flex',
    alignItems: 'center',
    margin: '30px 50px',
    borderRadius: '15px',
    background: 'linear-gradient(90deg, hsla(191, 93%, 79%, 1) 0%, hsla(160, 63%, 80%, 1) 100%)',
})

export const BannerContent = styled('div')({
    marginLeft: '200px',
})

export const BannerHeader = styled('h2')({
    color: '#2A7CC7',
    fontSize: '16px',
})

export const BannerTitle = styled('h1')({
    color: '#252B42',
    fontSize: '58px',
})

export const BannerText = styled('span')({
    color: '#737373',
    fontSize: '20px',
})

export const ShopButton = styled(Button)(({ theme }) => ({
    fontSize: '20px',
    fontWeight: 600,
    width: '200px',
    height: '52px',
    marginTop: '30px',
    color: '#FFF',
    backgroundColor: theme.palette.primary.main,
    '&:hover': {
        backgroundColor: theme.palette.primary.main,
    },
}))

export const Clients = styled('div')({
    display: 'flex',
    justifyContent: 'center',
})
