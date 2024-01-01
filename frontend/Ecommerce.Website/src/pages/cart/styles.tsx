import { styled } from '@mui/system';
import Box from '@mui/material/Box';

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

export const CartList = styled(Box)({
    width: '808px',
    minHeight: '400px',
    borderRadius: '6px',
    overflow: 'hidden',
})

export const EmptyCartList = styled(Box)({
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center',
    textAlign: 'center',
    padding: '48px 16px 0 0',
    fontSize: '20px',
    backgroundColor: '#FFF',
    minHeight: '400px',
})

export const EmptySummary = styled(Box)({
    marginTop: '24px',
    color: '#B5B5B5',
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

export const PriceContainerContent= styled(Box)({
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
