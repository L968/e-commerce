import { styled } from '@mui/system';
import Box from '@mui/material/Box';
import MuiList from '@mui/material/List';
import MuiListItem from '@mui/material/ListItem';
import Typography from '@mui/material/Typography';

export const Main = styled('main')({
    padding: '30px 0',
    backgroundColor: '#EDEDED',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    gap: '16px',
})

export const Container = styled(Box)({
    display: 'flex',
    alignItems: 'center',
    width: '720px',
    height: 'auto',
    minHeight: '128px',
    backgroundColor: '#FFF',
    borderRadius: '6px',
    padding: '0 24px',
    boxShadow: '0 1px 2px 0 rgba(0, 0, 0, .12)',
})

export const Title = styled(Typography)({
    fontSize: '20px',
    fontWeight: 600,
    margin: 0,
    width: '768px',
}) as typeof Typography

export const List = styled(MuiList)({
    display: 'flex',
    flexDirection: 'column',
    padding: '16px 0',
    widht: '100%',
    flex: '1',
})

export const ListItem = styled(MuiListItem)({
    height: '100px',
})

export const ListItemContent = styled(Box)({
    display: 'flex',
    flexDirection: 'column',
    marginLeft: '16px',
    '& p': {
        margin: 0,
        marginBottom: '5px'
    }
})

export const ListItemDescription = styled('span')({
    fontSize: '16px',
    color: 'rgba(0, 0, 0, .55)',
})
