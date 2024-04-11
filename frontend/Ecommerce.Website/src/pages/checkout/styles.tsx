import { styled } from '@mui/system';
import { Box, FormControlLabel, FormControlLabelProps, Typography } from '@mui/material';

export const Main = styled('main')({
    display: 'flex',
    alignItems: 'center',
    flexDirection: 'column',
    padding: '32px 0 60px 0',
    backgroundColor: '#F5F5F5',
})

export const Container = styled(Box)({
    display: 'flex',
    gap: '32px',
    width: '1200px',
})

export const SectionContainer = styled(Box)({
    display: 'flex',
    flexDirection: 'column',
    gap: '10px',
    width: '770px',
})

export const CartContainer = styled(Box)({
    border: '1px solid #ccc',
    padding: '20px',
})

export const Section = styled(Box)({
    display: 'flex',
    flexDirection: 'column',
    gap: '5px',
    border: '1px solid #D5D9D9',
    borderRadius: '6px',
    padding: '20px',
    backgroundColor: '#FFF',
    '& span': {
        fontSize: '14px',
    }
})

export const SectionTitle = styled(Typography)({
    fontSize: '18px',
    fontWeight: 600,
    marginBottom: '5px',
}) as typeof Typography

export const SectionContent = styled(Box)({
    display: 'flex',
    alignItems: 'center',
    gap: '10px',
})

export const ItemContainer = styled(Box)({
    display: 'flex',
    padding: '24px',
    border: '1px solid #E5E5E5',
    backgroundColor: '#FFF',
})

export const ItemInfo = styled(Box)({
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'space-between',
    marginLeft: '24px',
    marginRight: '16px',
    width: '390px',
})

export const ProductName = styled('span')({
    fontSize: '16px',
    fontWeight: 600,
    overflow: 'hidden',
    whiteSpace: 'nowrap',
    textOverflow: 'ellipsis',
})

export const Price = styled('span')({
    fontSize: '20px',
    marginLeft: '20px',
})

export const PriceContainer = styled(Box)({
    width: '360px',
    height: 'fit-content',
    borderRadius: '6px',
    boxShadow: '0 1px 2px rgba(0,0,0,.12)',
    padding: '20px 24px 24px 24px',
    backgroundColor: '#FFF',
})

export const PriceContainerTitle = styled(Box)({
    fontSize: '16px',
    fontWeight: 600,
    paddingBottom: '20px',
})

export const PriceContainerContent = styled(Box)({
    paddingTop: '20px',
})

export const PriceContainerRow = styled(Box)({
    display: 'flex',
    justifyContent: 'space-between',
    fontSize: '14px',
    marginBottom: '8px',
})

export const TotalPrice = styled(Box)({
    display: 'flex',
    justifyContent: 'space-between',
    fontSize: '18px',
    fontWeight: 600,
    marginBottom: '24px',
    marginTop: '30px',
})

export const StyledFormControlLabel = styled((props: FormControlLabelProps) => <FormControlLabel {...props} />)(
    ({ theme, checked }) => ({
        '&.MuiFormControlLabel-root': {
            padding: theme.spacing(1, 2),
            borderRadius: theme.shape.borderRadius,
            margin: theme.spacing(.5, 0),
            ...(checked
                ? {
                    backgroundColor: '#FCF5EE',
                    border: '1px solid #FBD8B4',
                }
                : {
                    '&:hover': {
                        backgroundColor: 'rgba(0, 0, 0, 0.04)',
                    },
                }),
        },
    })
)
