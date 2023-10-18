import {OrdersList} from "./ordersList/OrdersList";
import {useStore} from "../../global/stores/store";
import {useEffect} from "react";
import LoadingComponent from "../../components/LoadingComponent";
import {observer} from "mobx-react-lite";

function OrdersListSection() {
    const {adminOrderStore} = useStore()

    useEffect(() => {
        adminOrderStore.loadOrders()
    }, [adminOrderStore])

    return (
        !adminOrderStore.loading ?
            <OrdersList orders={adminOrderStore.orders} />
            :
            <LoadingComponent />
    );
}

export default observer(OrdersListSection)