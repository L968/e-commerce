import { styled } from '@mui/system';
import { Box, ListItemButton as MuiListItemButton, Paper } from '@mui/material';

export const Main = styled('main')(({ theme }) => ({
    height: '100vh',
    backgroundColor: theme.palette.background.main,
    flexGrow: 1,
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',

}))

export const Container = styled(Paper)({
    minWidth: '752px',
    maxWidth: '816px',
    '& h1': {
        fontSize: '20px',
        padding: '32px',
        fontWeight: 600,
    },
})

export const ListItemButton = styled(MuiListItemButton)({
    paddingLeft: '32px',
})

export const ActionsContainer = styled(Box)({
    width: '100%',
    padding: '0px 32px 20px 32px',
})
