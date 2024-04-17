import { styled } from '@mui/system';
import Box from '@mui/material/Box';
import MuiButton from '@mui/material/Button';
import OrderStatus from '@/interfaces/OrderStatus';
import getOrderStatusColor from '@/utils/getOrderStatusColor';

export const Container = styled(Box)({
    backgroundColor: '#FFF',
    borderRadius: '6px',
    overflow: 'hidden',
    boxShadow: '0 8px 16px 0 rgba(0, 0, 0, .1)',
})

export const Content = styled(Box)<{ orderStatus: OrderStatus }>(({ orderStatus }) => {
    let borderStyle = 'none';
    const borderColor = getOrderStatusColor(orderStatus);

    if (orderStatus === 'Pending Payment' || orderStatus === 'Cancelled') {
        borderStyle = `4px solid ${borderColor}`;
    }

    return {
        padding: '24px',
        borderLeft: borderStyle,
        '& > h2': {
            fontSize: '18px',
        },
        '& > p': {
            fontSize: '14px',
            marginTop: '5px',
            marginBottom: 0,
        },
    };
});

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

export const Button = styled(MuiButton)({
    textTransform: 'none',
})
