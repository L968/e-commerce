import { createTheme } from '@mui/material/styles';
import { green } from '@mui/material/colors';

export const theme = createTheme({
  spacing: 5,
  palette: {
    primary: {
      main: '#23A6F0',
      contrastText: '#FFF',
    },
    secondary: {
      main: green[500],
    },
  },
  typography: {
    fontFamily: 'Montserrat, Roboto, Helvetica, Arial, sans-serif',
  }
});
