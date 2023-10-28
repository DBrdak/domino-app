import {DeliveryPoint, MobileShop, StationaryShop} from "../models/shop";

export const getCenterFromShopLocations = (stationaryShops: StationaryShop[], mobileShops: MobileShop[]) => {
    const mobileCenter = mobileShops.length > 0 && mobileShops
        .flatMap(ms => ms.salePoints)
        .reduce(
            (center, sp) => {
                center.lat += Number(sp.location.latitude)
                center.lng += Number(sp.location.longitude)
                return center
            },
            {lat: 0, lng: 0}
        )

    const numSalePoints = mobileShops
        .flatMap(ms => ms.salePoints)
        .length;

    const isMobileCenterDefined = mobileCenter && mobileCenter.lng !== 0 && mobileCenter.lat !== 0

    if(isMobileCenterDefined) {
        mobileCenter.lat /= numSalePoints;
        mobileCenter.lng /= numSalePoints;
    }

    const stationaryCenter = stationaryShops.length > 0 && stationaryShops.reduce((center, shop) => {
        center.lat += Number(shop.location.latitude)
        center.lng += Number(shop.location.longitude)
        return center
    }, {lat: 0, lng: 0})

    const numStationaryShops = stationaryShops.length

    const isStationaryCenterDefined = stationaryCenter && stationaryCenter.lng !== 0 && stationaryCenter.lat !== 0

    if(isStationaryCenterDefined) {
        stationaryCenter.lat /= numStationaryShops;
        stationaryCenter.lng /= numStationaryShops;
    }


    const combinedCenter = isMobileCenterDefined && isStationaryCenterDefined && {
        lat: (stationaryCenter.lat + mobileCenter.lat) / 2,
        lng: (stationaryCenter.lng + mobileCenter.lng) / 2
    }

    if(combinedCenter) return combinedCenter

    if(stationaryCenter) return stationaryCenter

    if(mobileCenter) return mobileCenter

    return {lat: 52.8054070118536, lng: 20.11809186478481}
}

export const getCenterFromDeliveryPointLocations = (deliveryPoints: DeliveryPoint[]) => {
    const center = deliveryPoints
        .map(dp => dp.location)
        .reduce(
            (center, l) => {
                center.lat += Number(l.latitude)
                center.lng += Number(l.longitude)
                return center
            },
            {lat: 0, lng: 0}
        )

    const numberOfDeliveryPoints = deliveryPoints.length;

    center.lat /= numberOfDeliveryPoints;
    center.lng /= numberOfDeliveryPoints;

    return center
}