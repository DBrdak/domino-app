import { Undo } from '@mui/icons-material'
import { IconButton, Typography, useMediaQuery } from '@mui/material'
import { Link } from 'react-router-dom'
import theme from '../../global/layout/theme'

function ResetCategoryButton() {
  const isMobile = useMediaQuery(theme.breakpoints.down('md'))

  return (
    isMobile ?
    <Link to={'/produkty'} style={{ position: 'fixed', bottom: 20, left: 20, zIndex: '100' }}>
      <IconButton color='secondary' style={{ width: '56px', height:'56px', backgroundColor: '#D7D7D7' }}>
        <Undo />
      </IconButton>
    </Link>
    :
    <Link to={'/produkty'}>
      <IconButton style={{width: '100%', borderRadius: '20px', backgroundColor: '#FFFFFF', marginBottom: '15px'}}>
      <Undo style={{marginRight: '5px'}} />
      <Typography variant='h6'>Zmień  kategorię</Typography>
      </IconButton>
    </Link>
  )
}

export default ResetCategoryButton