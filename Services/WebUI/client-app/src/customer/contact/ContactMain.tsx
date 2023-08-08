import { Typography, Box, Paper, Divider, Stack, useMediaQuery } from "@mui/material";
import GoogleMap from './GoogleMap';
import EmailForm from './EmailForm';
import NavBar from "../components/NavBar";
import theme from "../../global/layout/theme";

const ContactMain: React.FC = () => {
  const isMobile = useMediaQuery(theme.breakpoints.down('sm'))

    const handleSubmit = (email: string, message: string) => {
        console.log(email, message);
    };

    return (
      <>
        <NavBar />
        <div style={{display: 'flex', justifyContent: 'center', margin: '20px 0px 0px 0px'}}>
        <Box maxWidth="lg">
          <Paper style={{width: '100%', padding: '100px'}}>
            <Typography textAlign={'center'} width={'100%'} variant="h3">Skontaktuj się z nami</Typography>
            <Divider style={{margin: '20px 0px 25px 0px'}}/>
            {isMobile ?
            <Stack direction={'column'}>
              <div style={{padding: '15px', width: '100%', display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                <GoogleMap apiKey={process.env.REACT_APP_MAPS_API_KEY as string} lat={52.805368774018085} lng={20.118366064444672} />
              </div>
              <div style={{width: '100%', display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                <EmailForm onSubmit={handleSubmit} />
              </div>
            </Stack>
            :
            <Stack direction={'row'} width={'100%'}>
              <div style={{padding: '20px', width: '70%', display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                <GoogleMap apiKey={process.env.REACT_APP_MAPS_API_KEY as string} lat={52.805368774018085} lng={20.118366064444672} />
              </div>
              <div style={{width: '50%', display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                <EmailForm onSubmit={handleSubmit} />
              </div>
            </Stack>
            }
            <Typography variant="h6">Adres:</Typography>
            <Typography variant="body1">Pólka-Raciąż 75a 09-140 Raciąż</Typography>
          </Paper>
        </Box>
      </div>
      </>
    );
};

export default ContactMain;
