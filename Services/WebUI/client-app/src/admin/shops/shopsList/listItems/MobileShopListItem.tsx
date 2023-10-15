import {useStore} from "../../../../global/stores/store";
import {MobileShop} from "../../../../global/models/shop";
import {IconButton, Stack, TableCell, TableRow, Typography} from "@mui/material";
import ShopEditModal from "../modals/ShopEditModal";
import {
    AddLocationAlt,
    Delete,
    Edit,
    Https, LocationOff, LocationOn,
    NoEncryption,
    PersonAdd,
    PersonRemove,
    WrongLocation
} from "@mui/icons-material";
import ConfirmModal from "../../../../components/ConfirmModal";
import {observer} from "mobx-react-lite";


interface Props {
    shop: MobileShop
}

function MobileShopListItem({shop}: Props) {
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
                    <IconButton style={{borderRadius: '0px', color: '#A0A0A0', flexDirection:'column', width: '12.5%'}}
                                size='medium' onClick={() => modalStore.openModal(
                        <ConfirmModal text={`Czy na pewno chcesz zablokować możliwość zamawiania produktu ${shop.shopName}?`}
                                      onConfirm={() => console.log()}/>)}
                    >
                        <PersonAdd />
                        <Typography variant='caption'>Dodaj sprzedawcę</Typography>
                    </IconButton>
                    <IconButton style={{borderRadius: '0px', color: '#109800', flexDirection:'column', width: '12.5%'}}
                                size='medium' onClick={() => modalStore.openModal(
                        <ConfirmModal text={`Czy na pewno chcesz przywrócić możliwość zamawiania produktu ${shop.shopName}?`}
                                      onConfirm={() => console.log()}/>)}
                    >
                        <PersonRemove />
                        <Typography variant='caption'>Usuń sprzedawcę</Typography>
                    </IconButton>
                    <IconButton style={{borderRadius: '0px', color: '#042cab', flexDirection:'column', width: '12.5%'}} size='medium'
                                onClick={() => modalStore.openModal(
                                    <ShopEditModal shop={shop} onSubmit={(editedShop) => console.log(editedShop)}/>)}>
                        <AddLocationAlt />
                        <Typography variant='caption'>Nowy punkt sprzedaży</Typography>
                    </IconButton>
                    <IconButton style={{borderRadius: '0px', color: '#b50000', flexDirection:'column', width: '12.5%'}} size='medium'
                                onClick={() => modalStore.openModal(
                                    <ShopEditModal shop={shop} onSubmit={(editedShop) => console.log(editedShop)}/>)}>
                        <WrongLocation />
                        <Typography variant='caption'>Usuń punkt sprzedaży</Typography>
                    </IconButton>
                    <IconButton style={{borderRadius: '0px', color: '#777777', flexDirection:'column', width: '12.5%'}} size='medium'
                                onClick={() => modalStore.openModal(
                                    <ShopEditModal shop={shop} onSubmit={(editedShop) => console.log(editedShop)}/>)}>
                        <LocationOff />
                        <Typography variant='caption'>Zablokuj punkt sprzedaży</Typography>
                    </IconButton>
                    <IconButton style={{borderRadius: '0px', color: '#00949b', flexDirection:'column', width: '12.5%'}} size='medium'
                                onClick={() => modalStore.openModal(
                                    <ShopEditModal shop={shop} onSubmit={(editedShop) => console.log(editedShop)}/>)}>
                        <LocationOn />
                        <Typography variant='caption'>Odblokuj punkt sprzedaży</Typography>
                    </IconButton>
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

export default observer(MobileShopListItem)