import { styled } from '@mui/system';
import Box from "@mui/material/Box";
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';

export const Banner = styled(Box)({
    display: 'flex',
    alignItems: 'center',
    margin: '30px 89px 55px 59px',
    borderRadius: '20px',
    background: 'linear-gradient(90deg, hsla(191, 93%, 79%, 1) 0%, hsla(160, 63%, 80%, 1) 100%)',
})

export const BannerContent = styled('div')({
    marginLeft: '206px',
    display: 'flex',
    width: '570px',
    flexDirection: 'column',
    justifyContent: 'center',
    alignItems: 'flex-start',
    gap: '30px',
    flexShrink: 0,
})

export const BannerHeader = styled(Typography)({
    color: '#2A7CC7',
    fontSize: '16px',
    fontWeight: 600,
    lineHeight: '24px',
}) as typeof Typography

export const BannerTitle = styled(Typography)({
    color: '#252B42',
    fontSize: '58px',
    fontWeight: 1000,
    fontStyle: 'normal',
    lineHeight: '80px'
}) as typeof Typography

export const BannerText = styled('span')({
    color: '#737373',
    fontSize: '20px',
})

export const ShopButton = styled(Button)(({ theme }) => ({
    fontSize: '20px',
    fontWeight: 600,
    width: '200px',
    height: '52px',
}))

export const Clients = styled('div')({
    display: 'flex',
    justifyContent: 'center',
})
