import React from 'react'
import {CheckCircleOutline} from '@mui/icons-material'
import './componentStyles.css'

function CompletionMark() {
  return (
    <CheckCircleOutline 
    style={{ 
      color: 'green', 
      fontSize: 60, 
      animation: 'scaleAndSpin 0.5s forwards, pulse 1.5s forwards',
      animationFillMode: 'forwards',
      borderRadius: '50%',
      backgroundColor: 'rgba(0, 255, 0, 0.1)',
      marginTop: 20
    }} />
  )
}

export default CompletionMark