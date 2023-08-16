import { observer } from 'mobx-react-lite';
import Modal from '@mui/material/Modal';
import Box from '@mui/material/Box';
import { useMediaQuery } from '@mui/material';
import theme from '../global/layout/theme';
import { useStore } from '../global/stores/store';

function ModalContainer() {
  const { modalStore } = useStore();
  const isMobile = useMediaQuery(theme.breakpoints.down('md'))

  return (
    <Modal
      open={modalStore.modal.open}
      onClose={modalStore.closeModal}
      aria-labelledby="modal-modal-title"
      aria-describedby="modal-modal-description"
    >
      {isMobile ?
        <Box 
          sx={{ 
            position: 'absolute', 
            top: '50%', 
            left: '50%',
            transform: 'translate(-50%, -50%)', 
            width: '90%' , 
            bgcolor: 'background.paper', 
            border: '1px solid #000', 
            boxShadow: 24, 
            p: 4 
          }}
        >
          {modalStore.modal.body}
        </Box>
        :
        <Box 
          sx={{ 
            position: 'absolute', 
            top: '50%', 
            left: '50%',
            transform: 'translate(-50%, -50%)', 
            width: '25%' , 
            bgcolor: 'background.paper', 
            border: '1px solid #000', 
            boxShadow: 24, 
            p: 4 
          }}
        >
          {modalStore.modal.body}
        </Box>
      }
    </Modal>
  );
}

export default observer(ModalContainer);