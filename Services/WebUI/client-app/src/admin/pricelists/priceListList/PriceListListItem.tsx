import React from 'react'
import {PriceList} from "../../../global/models/priceList";
import {useStore} from "../../../global/stores/store";
import {IconButton, Stack, TableCell, TableRow, Typography} from "@mui/material";
import ProductEditModal from "../../onlineShop/productCreation/ProductEditModal";
import {Delete, Edit, Https, NoEncryption, Preview, Visibility} from "@mui/icons-material";
import ConfirmModal from "../../../components/ConfirmModal";
import {observer} from "mobx-react-lite";
import PriceListView from "../priceListDisplay/PriceListView";

interface Props {
    priceList: PriceList
}

function ProductListItem({priceList}: Props) {
    const {modalStore,adminLayoutStore, adminPriceListStore} = useStore()

    function handlePriceListDelete(): void {
        modalStore.openModal(
            <ConfirmModal
                onConfirm={() => {
                    modalStore.closeModal()
                    adminPriceListStore.removePriceList(priceList.id)
                }}
                text={`Czy na pewno chcesz usunąć cennik kontrahenta ${priceList.contractor.name}`}
                important
            />
        )
    }

    function handlePriceListDisplay() {
        adminPriceListStore.setSelectedPriceList(priceList)
        adminLayoutStore.setSection(<PriceListView />)
    }

    return (
        <TableRow>
            <TableCell style={{textAlign: 'center'}}>
                <Typography variant='h5'>{priceList.name}</Typography>
            </TableCell>
            <TableCell style={{textAlign: 'center'}}>
                <Typography variant='h5'>
                    {priceList.contractor.name === 'Retail' ? 'Detal' : priceList.contractor.name}
            </Typography>
            </TableCell>
            <TableCell>
                <Stack direction={'row'} style={{display: 'flex', justifyContent: 'space-around'}} >
                    {priceList.contractor.name !== 'Retail' &&
                        <IconButton style={{flexDirection: 'column', color: '#FF4747', width:'50%', borderRadius: '0px'}}
                        onClick={() => handlePriceListDelete()}>
                            <Delete/>
                            <Typography variant='caption'>Usuń</Typography>
                        </IconButton>
                    }
                    <IconButton style={{flexDirection: 'column', color: '#000000', width:'50%', borderRadius: '0px'}}
                    onClick={() => handlePriceListDisplay()}>
                        <Visibility />
                        <Typography variant='caption'>Wyświetl</Typography>
                    </IconButton>
                </Stack>
            </TableCell>
        </TableRow>
    )
}

export default observer(ProductListItem)