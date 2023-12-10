import { styled } from '@mui/system';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Button from '@/components/Button';

export const Header = styled(Box)({
    display: 'flex',
    alignItems: 'center',
    height: '64px',
    padding: '0 80px 0 24px',
    backgroundColor: '#FAFAFA',
})

export const ProductContainer = styled(Box)({
    display: 'flex',
    gap: '30px',
    backgroundColor: '#FAFAFA',
    padding: '0px 195px 48px 195px',
})

export const ProductInfo = styled(Box)({
    width: '510px',
    height: '471px',
    paddingLeft: '24px',
})

export const Colors = styled(Box)({
    display: 'flex',
    gap: '6.077px',
    marginTop: '29px',
})

export const ImageContainer = styled(Box)({
    width: '506px',
    height: '546px',
})

export const CarouselIndicators = styled(Box)({
    display: 'flex',
    justifyContent: 'center',
    marginTop: '21px',
    width: '219px',
    height: '75px',
    gap: '19px',
})

export const ProductName = styled(Typography)({
    fontSize: '20px',
    color: '#252B42',
    lineHeight: '30px',
    marginTop: '11px',
}) as typeof Typography

export const RatingContainer = styled(Box)({
    display: 'flex',
    alignItems: 'center',
    gap: '10px',
    marginTop: '12px',
})

export const RatingSpan = styled('span')({
    color: '#737373',
    fontWeight: 600
})

export const ProductPrice = styled(Typography)({
    fontSize: '24px',
    fontWeight: 600,
    color: '#252B42',
    lineHeight: '32px',
    letterSpacing: '0.1px',
    marginTop: '22px',
    marginBottom: '5px',
}) as typeof Typography

export const Availability = styled(Typography)({
    fontSize: '14px',
    fontWeight: 600,
    color: '#737373',
    lineHeight: '24px',
    letterSpacing: '0.2px',
}) as typeof Typography

export const AvailabilityValue = styled('span')({
    fontSize: '14px',
    fontWeight: 600,
    color: '#23A6F0',
    lineHeight: '24px',
    letterSpacing: '0.2px',
})

export const ProductDescription = styled('p')({
    color: '#858585',
    fontSize: '16px',
    lineHeight: '20px',
    width: '464px',
    marginTop: '32px',
    marginBottom: '27px',
})

export const ButtonsContainer = styled(Box)({
    display: 'inline-flex',
    alignItems: 'center',
    gap: '10px',
    marginTop: '67px',
    'svg': {
        margin: '10px',
    }
})

export const SelectOptionButton = styled(Button)({
    display: 'flex',
    padding: '10px 20px',
    flexDirection: 'column',
    alignItems: 'center',
    gap: '10px',
})