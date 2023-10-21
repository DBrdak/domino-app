import OrdersList from "./ordersList/OrdersList";
import {useStore} from "../../global/stores/store";
import {useEffect} from "react";
import LoadingComponent from "../../components/LoadingComponent";
import {observer} from "mobx-react-lite";
import {Stack, Typography} from "@mui/material";

function OrdersListSection() {
    const {adminOrderStore} = useStore()

    useEffect(() => {
        adminOrderStore.loadOrders()
    }, [adminOrderStore])

    return (
        !adminOrderStore.loading ?
            <Stack direction={'column'} style={{justifyContent: 'center', alignItems: 'center'}} spacing={3}>
                <Typography variant={'h4'}>Zamówienia Online</Typography>
                <OrdersList orders={adminOrderStore.orders} />
            </Stack>
            :
            <div style={{width: '100%', height: '100%', display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                <LoadingComponent />
            </div>
    );
}

export default observer(OrdersListSection)