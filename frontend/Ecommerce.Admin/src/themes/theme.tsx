import { createTheme } from '@mui/material/styles';

export const theme = createTheme({
  spacing: 5,
  palette: {
    mode: 'dark',
    primary: {
      main: '#264CC8',
    },
    secondary: {
      main: '#FFF'
    },
    background: {
      default: '#151432',
      paper: '#1F1E43'
    }
  },
  typography: {
    fontFamily: 'Montserrat, Roboto, Helvetica, Arial, sans-serif',
  }
});
