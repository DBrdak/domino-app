import {OnlineOrder} from "../../../global/models/order";
import {
    Button, Collapse,
    List,
    ListItemButton,
    ListItemIcon,
    ListItemText,
    ListSubheader,
    Stack,
    Typography
} from "@mui/material";
import {useState} from "react";
import {ExpandLess, ExpandMore} from "@mui/icons-material";
import {PendingOrdersList} from "./PendingOrdersList";
import {ReceivedOrdersList} from "./ReceivedOrdersList";
import {RejectedOrdersList} from "./RejectedOrdersList";
import AcceptedOrdersList from "./AcceptedOrdersList";

interface Props {
    orders: OnlineOrder[]
}

function OrdersList({orders} : Props) {
    const [openedLists, setOpenedLists] = useState<boolean[]>([false, false, false, false])

    return (
        <List
            sx={{ width: '100%' }}
        >
            <ListItemButton style={{textAlign: 'center'}}
                onClick={() => setOpenedLists([!openedLists[0], openedLists[1], openedLists[2], openedLists[3]])}>
                {openedLists[0] ? <ExpandLess /> : <ExpandMore />}
                <ListItemText>
                    <Typography variant={'h6'}>Oczekujące</Typography>
                </ListItemText>
                {openedLists[0] ? <ExpandLess /> : <ExpandMore />}
            </ListItemButton>
            <Collapse in={openedLists[0]} timeout="auto" unmountOnExit>
                <PendingOrdersList />
            </Collapse>
            <ListItemButton style={{textAlign: 'center'}}
                            onClick={() => setOpenedLists([openedLists[0], !openedLists[1], openedLists[2], openedLists[3]])}>
                {openedLists[1] ? <ExpandLess /> : <ExpandMore />}
                <ListItemText>
                    <Typography variant={'h6'}>Zaakcepowane</Typography>
                </ListItemText>
                {openedLists[1] ? <ExpandLess /> : <ExpandMore />}
            </ListItemButton>
            <Collapse in={openedLists[1]} timeout="auto" unmountOnExit>
                <AcceptedOrdersList orders={orders} />
            </Collapse>
            <ListItemButton style={{textAlign: 'center'}}
                            onClick={() => setOpenedLists([openedLists[0], openedLists[1], !openedLists[2], openedLists[3]])}>
                {openedLists[2] ? <ExpandLess /> : <ExpandMore />}
                <ListItemText>
                    <Typography variant={'h6'}>Odebrane</Typography>
                </ListItemText>
                {openedLists[2] ? <ExpandLess /> : <ExpandMore />}
            </ListItemButton>
            <Collapse in={openedLists[2]} timeout="auto" unmountOnExit>
                <ReceivedOrdersList />
            </Collapse>
            <ListItemButton style={{textAlign: 'center'}}
                            onClick={() => setOpenedLists([openedLists[0], openedLists[1], openedLists[2], !openedLists[3]])}>
                {openedLists[3] ? <ExpandLess /> : <ExpandMore />}
                <ListItemText>
                    <Typography variant={'h6'}>Odrzucone</Typography>
                </ListItemText>
                {openedLists[3] ? <ExpandLess /> : <ExpandMore />}
            </ListItemButton>
            <Collapse in={openedLists[3]} timeout="auto" unmountOnExit>
                <RejectedOrdersList />
            </Collapse>
        </List>
    );
}

export default OrdersList