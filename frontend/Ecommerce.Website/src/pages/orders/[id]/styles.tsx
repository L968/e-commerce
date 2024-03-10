import { Box, Typography, Divider as MuiDivider } from '@mui/material';
import { styled } from '@mui/system';

export const Main = styled('main')({
    display: 'flex',
})

export const Container = styled(Box)({
    display: 'flex',
    flexDirection: 'column',
    gap: '16px',
    maxWidth: '100vw',
    padding: '40px 220px',
    backgroundColor: '#EDEDED',
    flex: 1,
})

export const Aside = styled('aside')({
    backgroundColor: '#F5F5F5',
    padding: '105px 48px 48px 48px',
})

export const AsideContent = styled(Box)({
    width: '380px',
})

export const PurchaseSummaryTitle = styled(Typography)({
    fontSize: '16px',
    fontWeight: 600,
    marginBottom: '4px',
}) as typeof Typography

export const PurchaseSummarySubtitle = styled(Typography)({
    fontSize: '12px',
    color: 'rgba(0, 0, 0, .55)'
}) as typeof Typography

export const PurchaseSummaryRow = styled(Typography)({
    fontSize: '14px',
    color: 'rgba(0, 0, 0, .55)',
    marginBottom: '12px',
    display: 'flex',
    justifyContent: 'space-between',
}) as typeof Typography

export const Product = styled(Box)({
    padding: '14px 18px',
    borderBottom: '1px solid #D5D9D9'
})

export const Divider = styled(MuiDivider)({
    margin: '16px 0'
})
