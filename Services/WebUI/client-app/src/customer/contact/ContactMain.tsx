import { Typography, Box, Paper, Divider, Stack, useMediaQuery } from "@mui/material";
import EmailForm from './EmailForm';
import NavBar from "../components/NavBar";
import theme from "../../global/layout/theme";
import Map from './Map'
import { useEffect } from "react";
import { setPageTitle } from "../../global/utils/pageTitle";

const ContactMain: React.FC = () => {
  const isMobile = useMediaQuery(theme.breakpoints.down('sm'))

  useEffect(() => {
    setPageTitle('Kontakt')
  }, [])

    const handleSubmit = (email: string, message: string) => {
        console.log(email, message);
    };

    return (
      <>
        <NavBar />
        <div style={{display: 'flex', justifyContent: 'center', margin: '20px 0px 20px 0px'}}>
        <Box maxWidth="lg">
          <Paper style={{width: '100%', padding: '60px'}}>
            <Typography textAlign={'center'} width={'100%'} variant="h3">Skontaktuj się z nami</Typography>
            <Divider style={{margin: '20px 0px 25px 0px'}}/>
            {isMobile ?
            <Stack direction={'column'}>
              <div style={{padding: '15px', width: '100%', display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                <Map />
              </div>
              <div style={{width: '100%', display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                <EmailForm onSubmit={handleSubmit} />
              </div>
            </Stack>
            :
            <Stack direction={'row'} width={'100%'}>
              <div style={{padding: '20px', width: '70%', display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                <Map />
              </div>
              <div style={{width: '50%', display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                <EmailForm onSubmit={handleSubmit} />
              </div>
            </Stack>
            }
            <Typography textAlign={'center'} marginTop={'8px'} variant="h6">Adres:</Typography>
            <Typography textAlign={'center'} variant="body1">Pólka-Raciąż 75a 09-140 Raciąż</Typography>
            <Typography textAlign={'center'} marginTop={'8px'} variant="h6">Numer telefonu:</Typography>
            <Typography textAlign={'center'} variant="body1">23 679 00 18</Typography>
          </Paper>
        </Box>
      </div>
      </>
    );
};

export default ContactMain;
