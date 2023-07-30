import { Copyright } from '@mui/icons-material';
import { Container, Box, Typography } from '@mui/material';
import React from 'react';

function App() {  
  return (
    <Container maxWidth="sm">
    <Box sx={{ my: 4 }}>
      <Typography variant="h1" component="h1" gutterBottom>
        Hello World!
      </Typography>
    </Box>
  </Container>
  );
};

export default App;
