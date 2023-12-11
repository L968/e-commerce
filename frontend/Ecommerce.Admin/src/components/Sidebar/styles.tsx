import { styled } from '@mui/system';
import MuiDrawer from '@mui/material/Drawer';
import { ListItemButton } from '@mui/material';

const drawerWidth = 240;

export const Drawer = styled(MuiDrawer)(({ theme }) => ({
    width: drawerWidth,
    flexShrink: 0,
    '& .MuiDrawer-paper': {
        width: drawerWidth,
        boxSizing: 'border-box',
    },
}))

export const ChildListItemButton = styled(ListItemButton)(({ theme }) => ({
    padding: '5px 0 5px 73px',
}))
