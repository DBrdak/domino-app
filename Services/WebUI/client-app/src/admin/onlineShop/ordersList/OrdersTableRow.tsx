import {
    Box,
    ButtonGroup,
    Collapse,
    IconButton, Stack,
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableRow,
    Typography
} from "@mui/material";
import DateTimeRangeDisplay from "../../../components/DateTimeRangeDisplay";
import {
    Clear,
    Delete,
    Done, Edit,
    KeyboardArrowDown,
    KeyboardArrowUp, Print,
    PrintDisabled,
    Remove,
    Visibility
} from "@mui/icons-material";
import React, {useState} from "react";
import {OnlineOrder, OrderItem} from "../../../global/models/order";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../global/stores/store";
import ConfirmModal from "../../../components/ConfirmModal";
import SmsMessages from "../../../global/utils/smsMessages";

interface Props {
    order: OnlineOrder
}

function OrdersTableRow({order}: Props) {
    const {modalStore, adminOrderStore} = useStore()
    const [open, setOpen] = useState(false)
    const [liveOrderItems, setLiveOrderItems] = useState<OrderItem[]>(order.items)

    const handleOrderAccept = () => {
        modalStore.closeModal()

        order.id && adminOrderStore.updateOrder({
            orderId: order.id,
            status: liveOrderItems.length === order.items.length ? 'Potwierdzone' : 'Potwierdzone ze zmianami',
            smsMessage: liveOrderItems.length === order.items.length ? SmsMessages.OrderAccepted : SmsMessages.OrderModified,
            modifiedOrderItems: liveOrderItems.length === order.items.length ? null : liveOrderItems,
            isPrinted: null
        })
    }

    const handleOrderReject = () => {
        modalStore.closeModal()
        order.id && adminOrderStore.updateOrder({
            orderId: order.id,
            status: 'Odrzucone',
            smsMessage: SmsMessages.OrderRejected,
            modifiedOrderItems: null,
            isPrinted: null
        })
    }

    const handleOrderPrintLost = () => {
        modalStore.closeModal()

        order.id && adminOrderStore.updateOrder({
            orderId: order.id,
            status: null,
            smsMessage: null,
            modifiedOrderItems: null,
            isPrinted: false
        })
    }

    const handleOrderItemReject = (item: OrderItem) => {
        modalStore.closeModal()
        setLiveOrderItems(prev => prev.filter(i => i.id !== item.id))
    }

    return (
        <>
            <TableRow key={order.id}>
                <TableCell>
                    <IconButton
                        aria-label="expand row"
                        onClick={() => setOpen(!open)}
                    >
                        {open ? <KeyboardArrowUp /> : <KeyboardArrowDown />}
                    </IconButton>
                </TableCell>
                <TableCell style={{textAlign: 'center'}}>
                    <Typography variant={'subtitle1'}>{order.deliveryLocation.name}</Typography>
                </TableCell>
                <TableCell style={{textAlign: 'center'}}>
                    <Typography variant={'subtitle1'}>{order.shop?.shopName}</Typography>
                </TableCell>
                <TableCell style={{textAlign: 'center'}}>
                    <Typography variant={'subtitle1'}>{<DateTimeRangeDisplay date={order.deliveryDate} />}</Typography>
                </TableCell>
                <TableCell style={{display: 'flex', justifyContent: 'center', borderBottom: 'none', minHeight: '100%'}}>
                    {order.status?.statusMessage === 'Oczekuje na potwierdzenie' &&
                        <ButtonGroup>
                            <IconButton onClick={() => modalStore.openModal(
                                <ConfirmModal onConfirm={handleOrderAccept} important
                                              text={`Czy na pewno chcesz przyjąć zamówienie?`}/>)}
                                        color={'success'} style={{flexDirection: 'column'}}>
                                <Done/>
                                <Typography variant={'caption'} >Przyjmij</Typography>
                            </IconButton>
                            <IconButton onClick={() => modalStore.openModal(
                                <ConfirmModal onConfirm={handleOrderReject} important
                                              text={`Czy na pewno chcesz odrzucić zamówienie?`}/>)}
                                        color={'error'} style={{flexDirection: 'column'}}>
                                <Clear/>
                                <Typography variant={'caption'} >Odrzuć</Typography>
                            </IconButton>
                        </ButtonGroup>
                    }
                    {(order.status?.statusMessage === 'Potwierdzone' || order.status?.statusMessage === 'Potwierdzone ze zmianami') &&
                        !adminOrderStore.ordersToPrint.find(o => o.id === order.id) &&
                        <IconButton onClick={() => modalStore.openModal(
                            <ConfirmModal onConfirm={handleOrderPrintLost} text={`Czy na pewno chcesz oznaczyć zamówienie jako niewydrukowane?`}/>)}
                                    color={'error'} style={{flexDirection: 'column', borderRadius:'2px'}}>
                            <PrintDisabled/>
                            <Typography variant={'caption'} >Anuluj wydruk</Typography>
                        </IconButton>
                    }
                </TableCell>
            </TableRow>
            <TableRow>
                <TableCell style={{paddingBottom: 0, paddingTop: 0}} colSpan={6}>
                    <Collapse in={open} timeout="auto" unmountOnExit>
                        <Box sx={{margin: 1}}>
                            <Stack direction={'row'} spacing={5} justifyContent={'center'} padding={'10px 0px'}>
                                <Typography variant={'subtitle1'}>
                                    <strong>Imię:</strong> {order.firstName}
                                </Typography>
                                <Typography variant={'subtitle1'}>
                                    <strong>Nazwisko:</strong> {order.lastName}
                                </Typography>
                                <Typography variant={'subtitle1'}>
                                    <strong>Numer telefonu:</strong> {order.phoneNumber}
                                </Typography>
                                <Typography variant={'subtitle1'}>
                                    <strong>Numer zamówienia:</strong> {order.id}
                                </Typography>
                            </Stack>
                            <Table size="small" aria-label="purchases">
                                <TableHead>
                                    <TableRow>
                                        <TableCell align={'center'}><strong>Asortyment</strong></TableCell>
                                        <TableCell align={'center'}><strong>Ilość</strong></TableCell>
                                        <TableCell align={'center'}><strong>Cena</strong></TableCell>
                                        <TableCell align={'center'}><strong>Wartość</strong></TableCell>
                                        <TableCell align={'center'}></TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {liveOrderItems.map((item) => (
                                        <TableRow key={item.id}>
                                            <TableCell align={'center'}>{item.productName}</TableCell>
                                            <TableCell
                                                align={'center'}>{item.quantity.value} {item.quantity.unit.code}</TableCell>
                                            <TableCell align={'center'}>
                                                {item.price.amount} {item.price.currency.code}/{item.price.unit?.code}
                                            </TableCell>
                                            <TableCell
                                                align={'center'}>{item.totalValue.amount} {item.totalValue.currency.code}</TableCell>
                                            <TableCell align={'center'}>
                                                {(order.status?.statusMessage === 'Oczekuje na potwierdzenie' && liveOrderItems.length > 1) &&
                                                    <IconButton onClick={() => modalStore.openModal(
                                                        <ConfirmModal onConfirm={() => handleOrderItemReject(item)}
                                                                      important
                                                                      text={`Czy na pewno chcesz odrzucić produkt ${item.productName}`}/>)}
                                                                color={'error'} style={{flexDirection: 'column', borderRadius: '2px'}}>
                                                        <Delete/>
                                                        <Typography variant={'caption'}>Odrzuć produkt</Typography>
                                                    </IconButton>
                                                }
                                            </TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </Box>
                    </Collapse>
                </TableCell>
            </TableRow>
        </>
    );
}

export default observer(OrdersTableRow)