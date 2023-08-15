import React, { useEffect } from 'react';
import { Container, Typography, Grid, Paper, Theme, styled, Box, useMediaQuery, Stack, Divider } from '@mui/material';
import './aboutPageStyles.css';
import NavBar from '../components/NavBar';
import { Padding } from '@mui/icons-material';
import theme from '../../global/layout/theme';
import { setPageTitle } from '../../global/utils/pageTitle';

const AboutMain: React.FC = () => {
  useEffect(() => {
    setPageTitle('O Nas')
  }, [])

  const isMobile = useMediaQuery(theme.breakpoints.down('sm'))
  return (
    <>
      <NavBar />
      {isMobile ? 
      <div style={{display: 'flex', justifyContent: 'center', margin: '20px 0px 0px 0px'}}>
        <Box maxWidth="lg">
          <Paper style={{width: '100%', padding: '20px'}}>
            <Typography variant='h3' textAlign={'center'}>Kilka słów o naszej firmie</Typography>
            <Divider style={{margin: '15px 0px 15px 0px'}} />
            <Stack direction={'column'} style={{textAlign: 'center', width: '100%'}}>
              <Typography variant="body1" paragraph className='aboutParagraphMobile'>
                Od 2004 roku jesteśmy niezmiennie związani z branżą mięsną, specjalizując się w uboju trzody chlewnej, rozbiorze oraz wędliniarstwie. Nasza pasja do wytwarzania najwyższej jakości produktów mięsnych sprawia, że jesteśmy dumni z naszej długoletniej tradycji w tej dziedzinie.
              </Typography>
              <Typography variant="body1" paragraph className='aboutParagraphMobile'>
                Jesteśmy lokalnym zakładem mięsnym, a naszą główną misją jest dostarczanie klientom wyjątkowych i smacznych produktów. Dbamy o to, aby każdy krok produkcji był wykonywany z najwyższą dbałością o szczegóły, co pozwala nam osiągnąć najwyższą jakość naszych mięs i wędlin.
              </Typography>
              <img src="/assets/composition.jpg" alt="Image 1" width={'100%'} style={{marginBottom: '16px'}}/>
              <Typography variant="body1" paragraph className='aboutParagraphMobile'>
                  Nasze mięso pochodzi wyłącznie od zaufanych lokalnych rolników, którzy dzielą nasze zobowiązanie do etycznych praktyk hodowlanych. Dzięki temu możemy zapewnić, że wszystkie nasze produkty są wytworzone z naturalnie odżywianych i zdrowych świń, co przekłada się na doskonały smak naszych wędlin.
              </Typography>
              <Typography variant="body1" paragraph className='aboutParagraphMobile'>
                Wyróżnia nas rodzinną atmosfera, która towarzyszy nam od samego początku naszej działalności. Jako mały zakład mięsny ceniący tradycję, możemy zapewnić najwyższy standard naszych wyrobów i utrzymać naszą reputację jako godnego zaufania dostawcy mięsa i wędlin.
              </Typography>
              <img src="/assets/composition.jpg" alt="Image 2" width={'100%'} style={{marginBottom: '16px'}}/>
              <Typography variant="body1" paragraph className='aboutParagraphMobile'>
                  Nasza oferta jest różnorodna i starannie wyselekcjonowana, aby sprostać oczekiwaniom naszych klientów. Wędliny, które wytwarzamy, są efektem naszej pasji i zaangażowania, a jakość naszych produktów stanowi nieodłączną część naszej firmy.
              </Typography>
              <Typography variant="body1" paragraph className='aboutParagraphMobile'> 
                Jesteśmy dumni z naszych korzeni i zamiłowania do naszej pracy. Dlatego z dumą podkreślamy, że nasza firma to nie tylko miejsce pracy, ale także nasz dom, który chcemy dzielić z naszymi klientami. Wierzymy, że jakość i autentyczność naszych produktów sprawiają, że nasi klienci powracają do nas z uśmiechem na twarzy.
              </Typography>
              <img src="/assets/composition.jpg" alt="Image 3" width={'100%'} style={{marginBottom: '16px'}}/>
            </Stack>
          </Paper>
        </Box>
      </div>
      :
      <div style={{display: 'flex', justifyContent: 'center', margin: '20px 0px 0px 0px'}}>
        <Box maxWidth="lg">
          <Paper style={{width: '100%', padding: '20px'}}>
            <Typography variant='h3' textAlign={'center'}>Kilka słów o naszej firmie</Typography>
            <Divider style={{margin: '25px 0px 25px 0px'}} />
            <Grid container spacing={4}>
              <Grid item xs={12} md={6}>
                <img src="/assets/composition.jpg" alt="Image 1" width={500} height={300} />
              </Grid>
              <Grid item xs={12} md={6} className='aboutGird'>
                <Typography variant="body1" paragraph className='aboutParagraph'>
                  Od 2004 roku jesteśmy niezmiennie związani z branżą mięsną, specjalizując się w uboju trzody chlewnej, rozbiorze oraz wędliniarstwie. Nasza pasja do wytwarzania najwyższej jakości produktów mięsnych sprawia, że jesteśmy dumni z naszej długoletniej tradycji w tej dziedzinie.
                </Typography>
                <Typography variant="body1" paragraph className='aboutParagraph'>
                  Jesteśmy lokalnym zakładem mięsnym, a naszą główną misją jest dostarczanie klientom wyjątkowych i smacznych produktów. Dbamy o to, aby każdy krok produkcji był wykonywany z najwyższą dbałością o szczegóły, co pozwala nam osiągnąć najwyższą jakość naszych mięs i wędlin.
                </Typography>
              </Grid>

              <Grid item xs={12} md={6} className='aboutGird'>
                <Typography variant="body1" paragraph className='aboutParagraph'>
                  Nasze mięso pochodzi wyłącznie od zaufanych lokalnych rolników, którzy dzielą nasze zobowiązanie do etycznych praktyk hodowlanych. Dzięki temu możemy zapewnić, że wszystkie nasze produkty są wytworzone z naturalnie odżywianych i zdrowych świń, co przekłada się na doskonały smak naszych wędlin.
                </Typography>
                <Typography variant="body1" paragraph className='aboutParagraph'>
                  Wyróżnia nas rodzinną atmosfera, która towarzyszy nam od samego początku naszej działalności. Jako mały zakład mięsny ceniący tradycję, możemy zapewnić najwyższy standard naszych wyrobów i utrzymać naszą reputację jako godnego zaufania dostawcy mięsa i wędlin.
                </Typography>
              </Grid>
              <Grid item xs={12} md={6}>
                <img src="/assets/composition.jpg" alt="Image 2" width={500} height={300} />
              </Grid>

              <Grid item xs={12} md={6}>
                <img src="/assets/composition.jpg" alt="Image 3" width={500} height={300} />
              </Grid>
              <Grid item xs={12} md={6} className='aboutGird'>
                <Typography variant="body1" paragraph className='aboutParagraph'>
                  Nasza oferta jest różnorodna i starannie wyselekcjonowana, aby sprostać oczekiwaniom naszych klientów. Wędliny, które wytwarzamy, są efektem naszej pasji i zaangażowania, a jakość naszych produktów stanowi nieodłączną część naszej firmy.
                </Typography>
                <Typography variant="body1" paragraph className='aboutParagraph'> 
                  Jesteśmy dumni z naszych korzeni i zamiłowania do naszej pracy. Dlatego z dumą podkreślamy, że nasza firma to nie tylko miejsce pracy, ale także nasz dom, który chcemy dzielić z naszymi klientami. Wierzymy, że jakość i autentyczność naszych produktów sprawiają, że nasi klienci powracają do nas z uśmiechem na twarzy.
                </Typography>
              </Grid>
            </Grid>
          </Paper>
        </Box>
      </div>
        }
    </>
  );
};

export default AboutMain;
