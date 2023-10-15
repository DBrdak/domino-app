import {ShopsList} from "./ShopsList";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../global/stores/store";
import ShopsListUpperSection from "./ShopsListUpperSection";
import {useEffect} from "react";

function ShopsListSection() {
    const {adminShopStore} = useStore()

    useEffect(() => {
        adminShopStore.loadShops()
    }, [])

    return (
        <>
            <ShopsListUpperSection />
            <ShopsList shops={adminShopStore.shops} loading={adminShopStore.loading} />
        </>
    );
}

export default observer(ShopsListSection)