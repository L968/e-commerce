import { styled } from '@mui/system';
import { Box } from '@mui/material';

export const Container = styled(Box)({
    backgroundColor: '#FFF',
    borderRadius: '6px',
    overflow: 'hidden',
    boxShadow: '0 8px 16px 0 rgba(0, 0, 0, .1)',
})

export const Content = styled(Box)<{ isPendingPayment: boolean }>(({ isPendingPayment }) => ({
    padding: '24px',
    borderLeft: isPendingPayment ? '4px solid #FF7733' : 'none',
    '& > h2': {
        fontSize: '18px',
    },
    '& > p': {
        fontSize: '14px',
        marginTop: '5px',
        marginBottom: 0,
    },
}))

export const Status = styled('span')<{ color: string }>(({ color }) => ({
    fontSize: '12px',
    fontWeight: 600,
    color: color,
}))

export const UserActions = styled(Box)({
    display: 'flex',
    gap: '8px',
    borderTop: '1px solid rgba(0, 0, 0, .1)',
    padding: '18px 24px',
})
