import {useStore} from "../../../../global/stores/store";
import {MobileShop, SalePoint} from "../../../../global/models/shop";
import {IconButton, Typography} from "@mui/material";
import ShopEditModal from "../modals/ShopEditModal";
import {
    AddLocationAlt,
    Delete,
    LocationOff, LocationOn,
    WrongLocation
} from "@mui/icons-material";
import ConfirmModal from "../../../../components/ConfirmModal";
import {observer} from "mobx-react-lite";
import {MobileShopNewSalePointModal} from "../modals/MobileShopNewSalePointModal";
import {MobileShopSalePointRemoveModal} from "../modals/MobileShopSalePointRemoveModal";
import {MobileShopSalePointDisableModal} from "../modals/MobileShopSalePointDisableModal";
import {MobileShopSalePointEnableModal} from "../modals/MobileShopSalePointEnableModal";


interface Props {
    shop: MobileShop
}

function MobileShopListItemActions({shop}: Props) {
    const {modalStore, adminShopStore} = useStore()

    const handleSalePointAdd = (newSalePoint: SalePoint) => {
        adminShopStore.setShopToUpdateId(shop.id)
        adminShopStore.setMobileShopUpdateValues(newSalePoint, null, null, null)
        modalStore.closeModal()
        adminShopStore.updateShop()
    }

    const handleSalePointDelete = (salePointToDelete: SalePoint) => {
        adminShopStore.setShopToUpdateId(shop.id)
        adminShopStore.setMobileShopUpdateValues(null, salePointToDelete, null, null)
        modalStore.closeModal()
        adminShopStore.updateShop()
    }

    const handleSalePointDisable = (salePointToDisable: SalePoint) => {
        adminShopStore.setShopToUpdateId(shop.id)
        adminShopStore.setMobileShopUpdateValues(null, null, salePointToDisable, null)
        modalStore.closeModal()
        adminShopStore.updateShop()
    }

    const handleSalePointEnable = (salePointToEnable: SalePoint) => {
        adminShopStore.setShopToUpdateId(shop.id)
        adminShopStore.setMobileShopUpdateValues(null, null, null, salePointToEnable)
        modalStore.closeModal()
        adminShopStore.updateShop()
    }

    return (
        <>
            <IconButton style={{borderRadius: '0px', color: '#042cab', flexDirection:'column', width: '14.28%'}} size='medium'
                        onClick={() => modalStore.openModal(
                            <MobileShopNewSalePointModal/>)}>
                <AddLocationAlt />
                <Typography variant='caption'>Nowy punkt sprzedaży</Typography>
            </IconButton>
            <IconButton style={{borderRadius: '0px', color: '#b50000', flexDirection:'column', width: '14.28%'}} size='medium'
                        onClick={() => modalStore.openModal(
                            <MobileShopSalePointRemoveModal/>)}>
                <WrongLocation />
                <Typography variant='caption'>Usuń punkt sprzedaży</Typography>
            </IconButton>
            <IconButton style={{borderRadius: '0px', color: '#777777', flexDirection:'column', width: '14.28%'}} size='medium'
                        onClick={() => modalStore.openModal(
                            <MobileShopSalePointDisableModal/>)}>
                <LocationOff />
                <Typography variant='caption'>Zablokuj punkt sprzedaży</Typography>
            </IconButton>
            <IconButton style={{borderRadius: '0px', color: '#00949b', flexDirection:'column', width: '14.28%'}} size='medium'
                        onClick={() => modalStore.openModal(
                            <MobileShopSalePointEnableModal/>)}>
                <LocationOn />
                <Typography variant='caption'>Odblokuj punkt sprzedaży</Typography>
            </IconButton>
        </>
    )
}

export default observer(MobileShopListItemActions)