import { observer } from 'mobx-react-lite';
import HomePage from '../../customer/home/HomePage';
import { Outlet } from 'react-router-dom';
import ModalContainer from '../../customer/components/ModalContainer';

function App() {  
  return (
    <>
      <ModalContainer />
      <Outlet />
    </>
  );
};

export default observer(App);
