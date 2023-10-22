import OrdersList from "./ordersList/OrdersList";
import {useStore} from "../../global/stores/store";
import {useEffect, useState} from "react";
import LoadingComponent from "../../components/LoadingComponent";
import {observer} from "mobx-react-lite";
import {Box, Button, IconButton, Popover, Stack, Typography} from "@mui/material";
import {runInAction} from "mobx";
import {FilterAlt} from "@mui/icons-material";
import {OrderFilter} from "./ordersList/OrderFilter";
import ordersList from "./ordersList/OrdersList";
import {OnlineOrder} from "../../global/models/order";

function OrdersListSection() {
    const {adminOrderStore, adminShopStore} = useStore()
    const [shopName, setShopName] = useState<string | null>(null)
    const [orders, setOrders] = useState<OnlineOrder[] | null>(null)

    useEffect(() => {
        runInAction(async () => {
            await adminShopStore.loadShops()
            await adminOrderStore.loadOrders()
            setOrders(adminOrderStore.orders)
        })
    }, [adminOrderStore, adminShopStore])

    useEffect(() => {

        if(shopName === null) {
            adminOrderStore.orders && setOrders(adminOrderStore.orders)
        }

        shopName && adminOrderStore.orders && setOrders([...adminOrderStore.orders].filter(o => o.shop?.shopName === shopName))
    }, [adminOrderStore.orders, shopName])

    return (
        !adminOrderStore.loading && !adminShopStore.loading && orders ?
            <Stack direction={'column'} style={{position: 'relative', justifyContent: 'center', alignItems: 'center'}} spacing={3}>
                <Box position='absolute' top="15px" left="15px" width={'200px'}>
                    <OrderFilter shops={adminShopStore.shops} handleShopChange={sn => setShopName(sn)} />
                </Box>
                <Typography variant="h4">Zamówienia Online</Typography>
                <OrdersList orders={orders} />
            </Stack>
            :
            <div style={{width: '100%', height: '100%', display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                <LoadingComponent />
            </div>
    );
}

export default observer(OrdersListSection)