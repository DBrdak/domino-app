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
    info: {
      main: '#2269d0'
    },
    text: {
      primary: '#000000'
    },
    error: {
      main: red.A400,
    },

  },
  components: {
    MuiCssBaseline: {
      styleOverrides: {
        '*::-webkit-scrollbar': {
          width: '10px',
        },
        '*::-webkit-scrollbar-track': {
          background: '#f5f5f5',
        },
        '*::-webkit-scrollbar-thumb': {
          backgroundColor: '#888',
          borderRadius: '15px',
          '&:hover': {
            backgroundColor: '#555',
          },
        },
      },
    },
  },
});

export default theme;