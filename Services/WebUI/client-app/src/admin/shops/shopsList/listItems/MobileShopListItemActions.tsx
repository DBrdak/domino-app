import {useStore} from "../../../../global/stores/store";
import {MobileShop, SalePoint} from "../../../../global/models/shop";
import {IconButton, Typography} from "@mui/material";
import {
    AddLocationAlt,
    Delete,
    LocationOff, LocationOn,
    WrongLocation
} from "@mui/icons-material";
import ConfirmModal from "../../../../components/ConfirmModal";
import {observer} from "mobx-react-lite";
import {MobileShopNewSalePointModal} from "../modals/mobileShop/MobileShopNewSalePointModal";
import {MobileShopSalePointRemoveModal} from "../modals/mobileShop/MobileShopSalePointRemoveModal";
import {MobileShopSalePointDisableModal} from "../modals/mobileShop/MobileShopSalePointDisableModal";
import {MobileShopSalePointEnableModal} from "../modals/mobileShop/MobileShopSalePointEnableModal";
import {MobileShopUpdateSalePointModal} from "../modals/mobileShop/MobileShopUpdateSalePointModal";


interface Props {
    shop: MobileShop
}

function MobileShopListItemActions({shop}: Props) {
    const {modalStore, adminShopStore} = useStore()

    const handleSalePointAdd = (newSalePoint: SalePoint) => {
        adminShopStore.setShopToUpdateId(shop.id)
        adminShopStore.setMobileShopUpdateValues(newSalePoint, null, null, null, null)
        modalStore.closeModal()
        adminShopStore.updateShop()
    }

    const handleSalePointDelete = (salePointToDelete: SalePoint) => {
        adminShopStore.setShopToUpdateId(shop.id)
        adminShopStore.setMobileShopUpdateValues(null, salePointToDelete, null, null, null)
        modalStore.closeModal()
        adminShopStore.updateShop()
    }

    const handleSalePointDisable = (salePointToDisable: SalePoint) => {
        adminShopStore.setShopToUpdateId(shop.id)
        adminShopStore.setMobileShopUpdateValues(null, null, salePointToDisable, null, null)
        modalStore.closeModal()
        adminShopStore.updateShop()
    }

    const handleSalePointEnable = (salePointToEnable: SalePoint) => {
        adminShopStore.setShopToUpdateId(shop.id)
        adminShopStore.setMobileShopUpdateValues(null, null, null, salePointToEnable, null)
        modalStore.closeModal()
        adminShopStore.updateShop()
    }

    function handleSalePointUpdate(updatedSalePoint: SalePoint) {
        adminShopStore.setShopToUpdateId(shop.id)
        adminShopStore.setMobileShopUpdateValues(null, null, null, null, updatedSalePoint)
        modalStore.closeModal()
        adminShopStore.updateShop()
    }

    return (
        <>
            <IconButton style={{borderRadius: '0px', color: '#042cab', flexDirection:'column', width: '10%'}} size='medium'
                        onClick={() => modalStore.openModal(
                            <MobileShopNewSalePointModal shop={shop} existingSalePoints={adminShopStore.salePoints}
                                                         onSubmit={(newSalePoint) => handleSalePointAdd(newSalePoint)}/>)}>
                <AddLocationAlt />
                <Typography variant='caption'>Nowy punkt sprzedaży</Typography>
            </IconButton>
            <IconButton style={{borderRadius: '0px', color: '#042cab', flexDirection:'column', width: '10%'}} size='medium'
                        disabled={shop.salePoints.length < 1} onClick={() => modalStore.openModal(
                            <MobileShopUpdateSalePointModal salePoints={shop.salePoints}
                                                         onSubmit={(updatedSalePoint) => handleSalePointUpdate(updatedSalePoint)}/>)}>
                <AddLocationAlt />
                <Typography variant='caption'>Edytuj punkt sprzedaży</Typography>
            </IconButton>
            <IconButton style={{borderRadius: '0px', color: '#b50000', flexDirection:'column', width: '10%'}} size='medium'
                        disabled={shop.salePoints.length < 1} onClick={() => modalStore.openModal(
                            <MobileShopSalePointRemoveModal salePoints={shop.salePoints}
                                                            onSubmit={(salePointToDelete) => handleSalePointDelete(salePointToDelete)}/>)}>
                <WrongLocation />
                <Typography variant='caption'>Usuń punkt sprzedaży</Typography>
            </IconButton>
            <IconButton style={{borderRadius: '0px', color: '#777777', flexDirection:'column', width: '10%'}} size='medium'
                        disabled={shop.salePoints.filter(sp => !sp.isClosed).length < 1}
                        onClick={() => modalStore.openModal(
                            <MobileShopSalePointDisableModal salePoints={shop.salePoints.filter(sp => !sp.isClosed)}
                                                             onSubmit={(salePointToDisable) => handleSalePointDisable(salePointToDisable)}/>)}>
                <LocationOff />
                <Typography variant='caption'>Zablokuj punkt sprzedaży</Typography>
            </IconButton>
            <IconButton style={{borderRadius: '0px', color: '#00949b', flexDirection:'column', width: '10%'}} size='medium'
                        disabled={shop.salePoints.filter(sp => sp.isClosed).length < 1}
                        onClick={() => modalStore.openModal(
                            <MobileShopSalePointEnableModal salePoints={shop.salePoints.filter(sp => sp.isClosed)}
                                                            onSubmit={(salePointToEnable) => handleSalePointEnable(salePointToEnable)}/>)}>
                <LocationOn />
                <Typography variant='caption'>Odblokuj punkt sprzedaży</Typography>
            </IconButton>
        </>
    )
}

export default observer(MobileShopListItemActions)