import OrdersList from "./OrdersList";
import {OnlineOrder} from "../../../global/models/order";
import {List, ListItem, ListItemText, Typography} from "@mui/material";
import React from "react";
import DateTimeRangeDisplay from "../../../components/DateTimeRangeDisplay";

interface Props {
    orders: OnlineOrder[]
}

function AcceptedOrdersList({orders} : Props) {
    return (
        <List>
            {orders.map(o=> (
                <ListItem key={o.id}>
                    <ListItemText primary={
                        <React.Fragment>
                            <Typography variant={'h6'}>{o.deliveryLocation.name}</Typography>
                        </React.Fragment>
                    } />
                    <ListItemText primary={
                        <React.Fragment>
                            <Typography variant={'h6'}>{'Sklep 2'}</Typography>
                        </React.Fragment>
                    } />
                    <ListItemText primary={
                        <React.Fragment>
                            <Typography variant={'subtitle1'}>{<DateTimeRangeDisplay date={o.deliveryDate} />}</Typography>
                        </React.Fragment>
                    } />
                </ListItem>
            ))}
        </List>
    );
}

export default AcceptedOrdersList