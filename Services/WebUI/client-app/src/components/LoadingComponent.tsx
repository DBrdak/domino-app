import React from 'react';
import CircularProgress from '@mui/material/CircularProgress';
import Box from '@mui/material/Box';
import { Stack, Typography } from '@mui/material';
import { Fullscreen } from '@mui/icons-material';

interface Props {
  text?: string
  fullScreen?: boolean
}

const LoadingComponent: React.FC<Props> = ({text, fullScreen}: Props) => {
    const fullScreenMode:React.CSSProperties = {width: '100vw', height: '100vh', display: 'flex', justifyContent: 'center', alignItems:'center'}

    return (
      <div style={fullScreen ? fullScreenMode : {}}>
        <Stack 
          display="flex" 
          justifyContent="center" 
          alignItems="center"
          direction={'column'}
          spacing={'10px'}
        >
          <CircularProgress />
          {text && <Typography variant='subtitle1'>{text}</Typography>}
        </Stack>
      </div>
    );
}

export default LoadingComponent;