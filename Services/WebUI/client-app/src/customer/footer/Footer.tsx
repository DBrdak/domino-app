import React from 'react';
import {Typography, Container, Grid, Link, Box, Theme} from '@mui/material';
import {makeStyles} from '@mui/styles';

const useStyles = makeStyles((theme: Theme) => ({
    footer: {
        backgroundColor: theme.palette.primary.main,
        color: theme.palette.common.white,
        padding: theme.spacing(6, 0),
    },
    footerText: {
        marginBottom: theme.spacing(2),
    },
}));

const Footer: React.FC = () => {
    const classes = useStyles();

    return ( classes &&
        <footer className={classes.footer}>
            <Container maxWidth="lg">
                <Grid container spacing={3}>
                    <Grid item xs={12} md={4}>
                        <Typography variant="h6" className={classes.footerText}>
                            Zakład Mięsny Domino
                        </Typography>
                        <Typography variant="body2">
                            Pólka-Raciąż 75A
                            <br />
                            09-140 Raciąż
                        </Typography>
                    </Grid>
                    <Grid item xs={12} md={4}>

                    </Grid>
                    <Grid item xs={12} md={4}>
                        <Typography variant="h6" className={classes.footerText}>
                            Kontakt
                        </Typography>
                        <Typography variant="body2">
                            Email: domino-mieso@wp.pl
                            <br />
                            Telefon: 23 679 00 18
                        </Typography>
                    </Grid>
                </Grid>
                <Box mt={3}>
                    <Typography variant="body2" align="center">
                        &copy; {new Date().getFullYear()} Firma Handlowa Domino Krzysztof Brdak. All rights reserved.<br/>
                        Made with <span style={{color: 'white'}}>&hearts;</span> by {}
                        <a href="https://www.github.com/DBrdak" target="_blank" rel="noopener noreferrer"
                        style={{color:'white'}}>
                        DBrdak
                        </a>
                    </Typography>
                </Box>
            </Container>
        </footer>
    );
};

export default Footer;
