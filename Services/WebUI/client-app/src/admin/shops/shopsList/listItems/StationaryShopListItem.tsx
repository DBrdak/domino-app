import {IconButton, Stack, TableCell, TableRow, Typography} from "@mui/material";
import {
    CalendarMonth,
    Delete,
    Edit,
    EditCalendar, EventAvailable, EventBusy,
    Https,
    NoEncryption,
    PersonAdd,
    PersonRemove
} from "@mui/icons-material";
import {observer} from "mobx-react-lite";
import {StationaryShop} from "../../../../global/models/shop";
import {useStore} from "../../../../global/stores/store";
import ShopEditModal from "../modals/ShopEditModal";
import ConfirmModal from "../../../../components/ConfirmModal";

interface Props {
    shop: StationaryShop
}

function StationaryShopListItem({shop}: Props) {
    const {modalStore, adminShopStore} = useStore()

    return (
        <TableRow>
            <TableCell style={{textAlign: 'center'}}>
                <Typography variant='h5'>{shop.shopName}</Typography>
            </TableCell>
            <TableCell>
                <Stack direction={'row'} style={{display: 'flex', justifyContent: 'space-around'}} >
                    <IconButton style={{borderRadius: '0px', color: '#000000', flexDirection:'column', width: '12.5%'}} size='medium'
                                onClick={() => modalStore.openModal(
                                    <ShopEditModal shop={shop} onSubmit={(editedShop) => console.log(editedShop)}/>)}>
                        <Edit />
                        <Typography variant='caption'>Edytuj</Typography>
                    </IconButton>
                    <IconButton style={{borderRadius: '0px', color: '#2b9119', flexDirection:'column', width: '12.5%'}}
                                size='medium' onClick={() => modalStore.openModal(
                        <ConfirmModal text={`Czy na pewno chcesz zablokować możliwość zamawiania produktu ${shop.shopName}?`}
                                      onConfirm={() => console.log()}/>)}
                    >
                        <PersonAdd />
                        <Typography variant='caption'>Dodaj sprzedawcę</Typography>
                    </IconButton>
                    <IconButton style={{borderRadius: '0px', color: '#b50000', flexDirection:'column', width: '12.5%'}}
                                size='medium' onClick={() => modalStore.openModal(
                        <ConfirmModal text={`Czy na pewno chcesz przywrócić możliwość zamawiania produktu ${shop.shopName}?`}
                                      onConfirm={() => console.log()}/>)}
                    >
                        <PersonRemove />
                        <Typography variant='caption'>Usuń sprzedawcę</Typography>
                    </IconButton>
                    {shop.weekSchedule.find(x => !x.isClosed) ?
                        <IconButton style={{borderRadius: '0px', color: '#042cab', flexDirection:'column', width: '50%'}} size='medium'
                                    onClick={() => modalStore.openModal(
                                        <ShopEditModal shop={shop} onSubmit={(editedShop) => console.log(editedShop)}/>)}>
                            <CalendarMonth />
                            <Typography variant='caption'>Stwórz tydzień pracy</Typography>
                        </IconButton>
                        :
                        <>
                        <IconButton style={{borderRadius: '0px', color: '#042cab', flexDirection:'column', width: '16.67%'}} size='medium'
                                    onClick={() => modalStore.openModal(
                                        <ShopEditModal shop={shop} onSubmit={(editedShop) => console.log(editedShop)}/>)}>
                            <EditCalendar />
                            <Typography variant='caption'>Edytuj dzień pracy</Typography>
                        </IconButton>
                        <IconButton style={{borderRadius: '0px', color: '#777777', flexDirection:'column', width: '16.67%'}} size='medium'
                                    onClick={() => modalStore.openModal(
                                        <ShopEditModal shop={shop} onSubmit={(editedShop) => console.log(editedShop)}/>)}>
                            <EventBusy />
                            <Typography variant='caption'>Zablokuj dzień pracy</Typography>
                        </IconButton>
                        <IconButton style={{borderRadius: '0px', color: '#00949b', flexDirection:'column', width: '16.67%'}} size='medium'
                                    onClick={() => modalStore.openModal(
                                        <ShopEditModal shop={shop} onSubmit={(editedShop) => console.log(editedShop)}/>)}>
                            <EventAvailable />
                            <Typography variant='caption'>Odblokuj dzień pracy</Typography>
                        </IconButton>
                        </>
                    }
                    <IconButton color={'primary'} style={{borderRadius: '0px', flexDirection:'column', width: '12.5%'}}
                                size='medium' onClick={() => modalStore.openModal(
                        <ConfirmModal text={`Czy na pewno chcesz usunąc sklep ${shop.shopName}?`} important={true}
                                      onConfirm={() => console.log()}/>)}
                    >
                        <Delete />
                        <Typography variant='caption'>Usuń sklep</Typography>
                    </IconButton>
                </Stack>
            </TableCell>
        </TableRow>
    )
}

export default observer(StationaryShopListItem)