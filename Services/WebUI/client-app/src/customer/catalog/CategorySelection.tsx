import { Paper, Stack, Typography } from '@mui/material'
import { Link } from 'react-router-dom'
import NavBar from '../components/NavBar'
import { observer } from 'mobx-react-lite'
import './catalogStyles.css'

function CategorySelection() {
    return (
      <div style={{ backgroundColor: '#E4E4E4', height: "100vh"}}>
        <NavBar />
        <div className='categoryContainer'>
          <Paper style={{padding: '40px'}}>
            <Stack direction={'column'} className="categoryStack">
              <Typography variant='h4' textAlign={'center'}   >Wybierz kategorię</Typography>
              <Stack direction={'row'} className="categoryStack">
                <Link to={'mięso'} className="categoryLink">
                  <Stack direction={'column'} spacing={0} className="categoryItem">
                    <img src="/assets/examples/meat.jpeg" alt="Mięso" className="categoryImage" />
                    <Typography variant='h5' className="categoryLabel">Mięso</Typography>
                  </Stack>
                </Link>
                <Link to={'wędliny'} className="categoryLink">
                  <Stack direction={'column'} spacing={0} className="categoryItem">
                    <img src="/assets/examples/sausage.jpg" alt="Wędliny" className="categoryImage" />
                    <Typography variant='h5' className="categoryLabel">Wędliny</Typography>
                  </Stack>
                </Link>
              </Stack>
            </Stack>
          </Paper>
        </div>
      </div>
    )
}

export default observer(CategorySelection)
