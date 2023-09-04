import React, { ReactNode } from 'react'
import SideNavbar from './SideNavbar/SideNavbar'
import TopNavbar from './TopNavbar/TopNavbar'
import { Grid, Paper } from '@mui/material'
import styled from '@emotion/styled'

interface Props {
  children: ReactNode
}

const NavbarLayout: React.FC<Props> = ({ children }: Props) => {
  const LayoutContainer = styled.div`
    display: flex;
    height: 100vh;
    overflow: hidden;
  `;

  const ContentArea = styled.div`
    display: flex;
    flex-direction: column;
    flex: 1;
  `;

  const StyledPaper = styled(Paper)({
    width: '80%',
    padding: '20px',
    height: '100%',
    overflowY: 'auto',
    scrollbarWidth: 'thin',
    '&::-webkit-scrollbar': {
      width: '6px',
    },
    '&::-webkit-scrollbar-thumb': {
      backgroundColor: 'rgba(0,0,0,.5)',
    },
    '&::-webkit-scrollbar-track': {
      backgroundColor: 'rgba(0,0,0,.1)',
    }
  });

  return (
    <LayoutContainer>
      <SideNavbar />
      <ContentArea>
        <TopNavbar />
        { children &&
        <div style={{display: 'flex', height:'88vh', justifyContent: 'center', alignItems: 'start', padding: '25px 0px'}}>
          <StyledPaper>
            {children}
          </StyledPaper>
        </div>}
      </ContentArea>
    </LayoutContainer>
  );
};

export default NavbarLayout