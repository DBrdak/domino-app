import {useStore} from "../../../global/stores/store";
import React, {useEffect, useState} from "react";
import {observer} from "mobx-react-lite";
import {MobileShop, SalePoint, StationaryShop} from "../../../global/models/shop";
import {SalePointMap} from "./SalePointMap";

function SalePointMapSection() {
    const {adminShopStore} = useStore()


    useEffect(() => {
        adminShopStore.loadShops()
    }, [])

    return (
        <div style={{width: '100%', height: '100%' ,display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
            <SalePointMap stationaryShops={adminShopStore.stationaryShops} mobileShops={adminShopStore.mobileShops} locations={adminShopStore.salePoints.flatMap(sp => sp.location)} />
        </div>
    );
}

export default observer(SalePointMapSection)