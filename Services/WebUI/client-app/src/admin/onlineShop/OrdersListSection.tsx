import OrdersList from "./ordersList/OrdersList";
import {useStore} from "../../global/stores/store";
import {useEffect, useState} from "react";
import LoadingComponent from "../../components/LoadingComponent";
import {observer} from "mobx-react-lite";
import {Button, Stack, Typography} from "@mui/material";
import {runInAction} from "mobx";
import {Print} from "@mui/icons-material";
import {OrderFilter} from "./ordersList/OrderFilter";
import {OnlineOrder} from "../../global/models/order";

function OrdersListSection() {
    const {adminOrderStore, adminShopStore} = useStore()
    const [shopName, setShopName] = useState<string>('')
    const [orders, setOrders] = useState<OnlineOrder[] | null>(null)

    useEffect(() => {
        runInAction(async () => {
            await adminShopStore.loadShops()
            await adminOrderStore.loadOrders()
            setOrders(adminOrderStore.orders)
        })
    }, [adminOrderStore, adminShopStore])

    useEffect(() => {
        if(shopName === '') {
            adminOrderStore.orders && setOrders(adminOrderStore.orders)
            return
        }

        adminOrderStore.orders && setOrders([...adminOrderStore.orders].filter(o => o.shop?.shopName === shopName))
    }, [shopName, adminOrderStore.orders])

    async function handleOrdersDownload() {
        await adminOrderStore.downloadOrders()
        await adminOrderStore.loadOrders(false)
    }

    return (
        orders &&
            <Stack direction={'column'} style={{position: 'relative', justifyContent: 'center', alignItems: 'center'}}
                   spacing={3}>
                <Stack direction={'row'} justifyContent={'space-between'} position='absolute' top="15px" left="15px"
                       right={'15px'}>
                    <OrderFilter shops={adminShopStore.shops} handleShopChange={sn => setShopName(sn)} selectedName={shopName}/>
                    {adminOrderStore.loadingPdf || adminOrderStore.loading || adminShopStore.loading || !orders ?
                        <Button variant={'contained'} style={{flexDirection: 'column'}} disabled>
                            <LoadingComponent/>
                        </Button>
                        :
                        <Button variant={'contained'} style={{flexDirection: 'column'}}
                                onClick={async () => await handleOrdersDownload()}
                                disabled={!orders || adminOrderStore.ordersToPrint.length === 0 || orders.length === 0}>
                            <Print/>
                            <Typography variant={'caption'}>Drukuj zamówienia</Typography>
                        </Button>
                    }
                </Stack>
                <Typography variant="h4">Zamówienia Online</Typography>
                <OrdersList orders={orders}/>
            </Stack>
    );
}

export default observer(OrdersListSection)