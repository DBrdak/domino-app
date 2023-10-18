import {useStore} from "../../../../global/stores/store";
import {MobileShop, Seller} from "../../../../global/models/shop";
import {IconButton, Stack, TableCell, TableRow, Typography} from "@mui/material";
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
import MobileShopListItem from "./MobileShopListItemActions";
import StationaryShopListItem from "./StationaryShopListItemActions";
import {SellerDeleteModal} from "../modals/shop/SellerDeleteModal";
import {SellerAddModal} from "../modals/shop/SellerAddModal";
import {useEffect} from "react";


interface Props {
    shop: any
}

function ShopListItem({shop}: Props) {
    const {modalStore, adminShopStore} = useStore()

    const handleShopDelete = () => {
        modalStore.closeModal()
        adminShopStore.deleteShop(shop.id)
    }

    const handleSellerDelete = (seller: Seller) => {
        adminShopStore.setShopToUpdateId(shop.id)
        adminShopStore.setShopUpdateValues(null, seller)
        modalStore.closeModal()
        adminShopStore.updateShop()
    }

    const handleSellerAdd = (seller: Seller) => {
        adminShopStore.setShopToUpdateId(shop.id)
        adminShopStore.setShopUpdateValues(seller, null)
        modalStore.closeModal()
        adminShopStore.updateShop()
    }

    return (
        <TableRow>
            <TableCell style={{textAlign: 'center'}}>
                <Typography variant='h5'>{shop.shopName}</Typography>
            </TableCell>
            <TableCell>
                <Stack direction={'row'} style={{display: 'flex', justifyContent: 'space-around'}} >
                    <IconButton style={{borderRadius: '0px', color: '#109800', flexDirection:'column', width: '14.28%'}}
                                size='medium' onClick={() => modalStore.openModal(
                        <SellerAddModal onSubmit={(seller) => handleSellerAdd(seller)} />
                    )}
                    >
                        <PersonAdd />
                        <Typography variant='caption'>Dodaj sprzedawcę</Typography>
                    </IconButton>
                    <IconButton style={{borderRadius: '0px', color: '#5e5e5e', flexDirection:'column', width: '14.28%'}} disabled={shop.sellers.length < 1}
                                size='medium' onClick={() => modalStore.openModal(
                                    <SellerDeleteModal sellers={shop.sellers} onDelete={(seller) => handleSellerDelete(seller)} />
                    )}
                    >
                        <PersonRemove />
                        <Typography variant='caption'>Usuń sprzedawcę</Typography>
                    </IconButton>
                    {shop.location ? <StationaryShopListItem shop={shop}/> : <MobileShopListItem shop={shop} />}
                    <IconButton color={'primary'} style={{borderRadius: '0px', flexDirection:'column', width: '14.28%'}}
                                size='medium' onClick={() => modalStore.openModal(
                        <ConfirmModal text={`Czy na pewno chcesz usunąc sklep ${shop.shopName}?`} important={true}
                                      onConfirm={() => handleShopDelete()}/>)}
                    >
                        <Delete />
                        <Typography variant='caption'>Usuń sklep</Typography>
                    </IconButton>
                </Stack>
            </TableCell>
        </TableRow>
    )
}

export default observer(ShopListItem)