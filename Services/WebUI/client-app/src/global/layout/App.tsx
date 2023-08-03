import { observer } from 'mobx-react-lite';
import HomePage from '../../customer/home/HomePage';
import { Outlet } from 'react-router-dom';

function App() {  
  return (
    <Outlet />
  );
};

export default observer(App);
