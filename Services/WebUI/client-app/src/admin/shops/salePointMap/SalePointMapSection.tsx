import {useStore} from "../../../global/stores/store";
import React, {useEffect, useState} from "react";
import {observer} from "mobx-react-lite";
import {MobileShop, SalePoint, StationaryShop} from "../../../global/models/shop";
import {SalePointMap} from "./SalePointMap";
import {toast} from "react-toastify";
import loading = toast.loading;
import LoadingComponent from "../../../components/LoadingComponent";
import {MapContainer, Marker, Popup, TileLayer} from "react-leaflet";

function SalePointMapSection() {
    const {adminShopStore} = useStore()


    useEffect(() => {
        adminShopStore.loadShops()
    }, [])

    return (
        <div style={{width: '100%', height: '100%' ,display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
            {adminShopStore.loading ?
                <LoadingComponent />
                :
                ((adminShopStore.stationaryShops.length > 0 && adminShopStore.mobileShops.length > 0 && adminShopStore.salePoints.length > 0) ?
                    <SalePointMap stationaryShops={adminShopStore.stationaryShops}
                           mobileShops={adminShopStore.mobileShops}
                           locations={adminShopStore.salePoints.flatMap(sp => sp.location)}/>
                    :
                        <MapContainer
                            center={[52.80537130233171, 20.118007397537376]}
                            zoom={9}
                            style={{height: '100%', width: '125%', borderRadius: '30px 0px 0px 30px'}}>
                            <TileLayer
                                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                            />
                        </MapContainer>
                )
            }
        </div>
    );
}

export default observer(SalePointMapSection)