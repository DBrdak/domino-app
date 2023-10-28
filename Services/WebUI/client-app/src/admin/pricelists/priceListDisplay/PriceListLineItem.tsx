import {LineItem, PriceList} from "../../../global/models/priceList";
import {IconButton, Stack, TableCell, TableRow, Typography} from "@mui/material";
import {Delete, Edit, Visibility} from "@mui/icons-material";
import PriceListView from "./PriceListView";
import React from "react";
import ConfirmModal from "../../../components/ConfirmModal";
import {useStore} from "../../../global/stores/store";
import { observer } from "mobx-react-lite";
import LineItemUpdateModal from "./priceListUpperSection/LineItemUpdateModal";

interface Props {
    priceList: PriceList
    lineItem: LineItem
}

function PriceListLineItem({priceList, lineItem}: Props) {
    const {modalStore, adminPriceListStore} = useStore()

    function handleLineItemDelete(): void {
        modalStore.openModal(
            <ConfirmModal
                onConfirm={() => {
                    modalStore.closeModal()
                    adminPriceListStore.removeLineItem(priceList.id, lineItem.name)
                }}
                text={`Czy na pewno chcesz usunąć produkt ${lineItem.name} z cennika ${priceList.name}`}
                important
            />
        )
    }

    return (
        <TableRow>
            <TableCell style={{textAlign: 'center'}}>
                <Typography variant='h5'>
                    {lineItem.name}
                </Typography>
            </TableCell>
            <TableCell style={{textAlign: 'center'}}>
                <Typography variant='h5'>
                    {lineItem.price.amount} {lineItem.price.currency.code}/{lineItem.price.unit?.code}
                </Typography>
            </TableCell>
            <TableCell>
                <Stack direction={'row'} style={{display: 'flex', justifyContent: 'space-around'}} >
                    <IconButton style={{flexDirection: 'column', color: '#FF4747', width:'50%', borderRadius: '0px'}}
                                onClick={() => handleLineItemDelete()}>
                        <Delete/>
                        <Typography variant='caption'>Usuń</Typography>
                    </IconButton>
                    <IconButton style={{flexDirection: 'column', color: '#000000', width:'50%', borderRadius: '0px'}}
                                onClick={() => modalStore.openModal(<LineItemUpdateModal priceListId={priceList.id} lineItem={lineItem}/>)}>
                        <Edit />
                        <Typography variant='caption'>Edytuj</Typography>
                    </IconButton>
                </Stack>
            </TableCell>
        </TableRow>
    );
}

export default observer(PriceListLineItem)