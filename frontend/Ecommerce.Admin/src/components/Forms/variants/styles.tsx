import { styled } from '@mui/system';
import { IconButton, List, ListItem, ListItemText, Paper } from '@mui/material';

export const Main = styled('main')({
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    width: '100%',
    height: '90vh'
})

export const Container = styled(Paper)({
    display: 'flex',
    flexDirection: 'column',
    padding: '30px',
    minWidth: '500px',
    borderRadius: '10px',
    '& h1': {
        fontSize: '25px',
    },
    '& h2': {
        fontSize: '20px',
        marginTop: '15px',
    }
})

export const Form = styled('form')({
    display: 'flex',
    flexDirection: 'column',
    gap: '20px',
})

export const StyledList = styled(List)({
    border: '1px solid #DDD',
    borderRadius: '8px',
    boxShadow: '2px 4px 8px rgba(0, 0, 0, .5)',
    background: '#1e1e33',
})

export const StyledListItem = styled(ListItem)({
    '&:not(:last-child)': {
        borderBottom: '1px solid #DDD',
    },
})
