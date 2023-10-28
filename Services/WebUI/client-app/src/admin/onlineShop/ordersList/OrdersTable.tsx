import OrdersList from "./OrdersList";
import {OnlineOrder} from "../../../global/models/order";
import {
    ButtonGroup,
    Divider,
    IconButton,
    List,
    ListItem,
    ListItemText,
    Table, TableBody, TableCell, TableHead,
    TableRow,
    Typography
} from "@mui/material";
import React from "react";
import DateTimeRangeDisplay from "../../../components/DateTimeRangeDisplay";
import {Visibility} from "@mui/icons-material";
import OrdersTableRow from "./OrdersTableRow";
import LoadingComponent from "../../../components/LoadingComponent";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../global/stores/store";
import LoadingTableRow from "../../../components/LoadingTableRow";

interface Props {
    orders: OnlineOrder[]
}

function OrdersTable({orders} : Props) {
    const {adminOrderStore, adminShopStore} = useStore()

    return (
        adminOrderStore.loading || adminShopStore.loading ?
            <LoadingComponent />
            :
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell />
                        <TableCell style={{textAlign: 'center'}}>
                            <Typography variant={'subtitle1'}><strong>Lokalizacja</strong></Typography>
                        </TableCell>
                        <TableCell style={{textAlign: 'center'}}>
                            <Typography variant={'subtitle1'}><strong>Sklep</strong></Typography>
                        </TableCell>
                        <TableCell style={{textAlign: 'center'}}>
                            <Typography variant={'subtitle1'}><strong>Data odbioru</strong></Typography>
                        </TableCell>
                        <TableCell />
                    </TableRow>
                </TableHead>
                <TableBody>
                    {orders.some(o => o.id === adminOrderStore.loadingOrder.orderId) && adminOrderStore.loadingOrder.loading ?
                        <LoadingTableRow cells={4} rows={orders.length}/>
                        :
                        orders.map(o=> <OrdersTableRow key={o.id} order={o}/>)
                    }
                </TableBody>
            </Table>
    );
}

export default observer(OrdersTable)