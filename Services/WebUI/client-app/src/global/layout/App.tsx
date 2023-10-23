import { observer } from 'mobx-react-lite';
import HomePage from '../../customer/home/HomePage';
import { Outlet, ScrollRestoration } from 'react-router-dom';
import ModalContainer from '../../components/ModalContainer';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css'

function App() {  
  return (
    <>
      <ScrollRestoration />
      <ModalContainer />
      <ToastContainer position='bottom-right' theme='colored' />
      <Outlet />
    </>
  );
};

export default observer(App);
