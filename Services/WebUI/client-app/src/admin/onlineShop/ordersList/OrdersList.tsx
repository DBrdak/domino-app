import {OnlineOrder} from "../../../global/models/order";
import {Typography} from "@mui/material";

interface Props {
    orders: OnlineOrder[]
}

export function OrdersList({orders}: Props) {
    return (
        <>
            {orders.map(o => <Typography key={o.id}>{o.id}</Typography>)}
        </>
    );
}