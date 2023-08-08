import { createTheme } from '@mui/material/styles';
import { red } from '@mui/material/colors';

const theme = createTheme({
  palette: {
    background: {
      default: '#E4E4E4'
    },
    primary: {
      main: '#C32B28',
    },
    secondary: {
      main: '#FFFFFF',
    },
    text: {
      primary: '#000000'
    },
    error: {
      main: red.A400,
    },
  },
});

export default theme;