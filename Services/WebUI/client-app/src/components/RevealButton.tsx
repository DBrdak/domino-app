import React, { useState } from 'react';
import { IconButton } from '@mui/material';
import { ArrowDropDown, ArrowDropUp } from '@mui/icons-material';
import './componentStyles.css'
import { observer } from 'mobx-react-lite';

interface RevealButtonProps {
  buttonText: string;
  revealComponent: React.ReactNode;
}

const RevealButton: React.FC<RevealButtonProps> = ({
  buttonText,
  revealComponent,
}) => {
  const [isRevealed, setIsRevealed] = useState(false);

  const handleButtonClick = () => {
    setIsRevealed(!isRevealed);
  };

  return (
    <div className='reveal-button-container'>
      <IconButton style={{backgroundColor: '#FFFFFF', width: '100%', borderRadius: '20px'}} onClick={handleButtonClick}>
        {isRevealed ? <ArrowDropUp color='primary' fontSize='medium'></ArrowDropUp> : 
        <ArrowDropDown color='primary' fontSize='medium'></ArrowDropDown>}
      </IconButton>
        {isRevealed && <div className='reveal-content'>{revealComponent}</div>}
    </div>
  );
};

export default observer(RevealButton);