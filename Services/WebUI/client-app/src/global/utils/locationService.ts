import {DeliveryPoint, MobileShop, StationaryShop} from "../models/shop";

export const getCenterFromShopLocations = (stationaryShops: StationaryShop[], mobileShops: MobileShop[]) => {
    const mobileCenter = mobileShops
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

    mobileCenter.lat /= numSalePoints;
    mobileCenter.lng /= numSalePoints;

    const stationaryCenter = stationaryShops.reduce((center, shop) => {
        center.lat += Number(shop.location.latitude)
        center.lng += Number(shop.location.longitude)
        return center
    }, {lat: 0, lng: 0})

    const numStationaryShops = stationaryShops.length

    stationaryCenter.lat /= numStationaryShops;
    stationaryCenter.lng /= numStationaryShops;

    const combinedCenter = {
        lat: (stationaryCenter.lat + mobileCenter.lat) / 2,
        lng: (stationaryCenter.lng + mobileCenter.lng) / 2
    }

    return combinedCenter
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