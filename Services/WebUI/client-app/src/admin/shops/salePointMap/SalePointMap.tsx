import {MapContainer, Marker, TileLayer} from "react-leaflet";
import React, {useState} from "react";
import {Paper, Stack, Typography} from "@mui/material";
import {MobileShop, SalePoint, StationaryShop} from "../../../global/models/shop";
import {MobileSalePointInfoCard} from "./MobileSalePointInfoCard";
import {StationarySalePointInfoCard} from "./StationarySalePointInfoCard";
import LoadingComponent from "../../../components/LoadingComponent";
import {Location} from '../../../global/models/common'

interface Props {
    stationaryShops: StationaryShop[],
    mobileShops: MobileShop[]
    locations: Location[]
}

export function SalePointMap({mobileShops, stationaryShops, locations}: Props) {
    const [mobileInfo, setMobileInfo] = useState<{ location: Location, shops: MobileShop[] } | null>(null)
    const [stationaryInfo, setStationaryInfo] = useState<StationaryShop | null>(null)

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

    return (
        <Stack direction={'row'} style={{
            width: '100%', height: '100%', display: 'flex', borderRadius: '30px',
            justifyContent: 'center', alignItems: 'center', boxShadow: '0px 0px 15px 0px #BBBBBB'
        }}>
            <MapContainer
                center={[combinedCenter.lat, combinedCenter.lng]}
                zoom={9}
                style={{height: '100%', width: '125%', borderRadius: '30px 0px 0px 30px'}}>
                <TileLayer
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />
                {locations.map((l, i) => (
                        <Marker key={i} riseOnHover eventHandlers={{
                            click: () => {
                                setStationaryInfo(null)
                                setMobileInfo({
                                    location: l,
                                    shops: mobileShops.filter(ms =>
                                        ms.salePoints.filter(sp => sp.location.name === l.name).length > 0)
                                })
                            }
                        }}
                                position={{
                                    lng: Number(l.longitude),
                                    lat: Number(l.latitude)
                                }}/>
                    ))
                }
                {stationaryShops.map(ss => (
                    <Marker key={ss.id} riseOnHover eventHandlers={{
                        click: () => {
                            setMobileInfo(null)
                            setStationaryInfo(ss)
                        }
                    }}
                            position={{lng: Number(ss.location.longitude), lat: Number(ss.location.latitude)}}/>
                ))
                }
            </MapContainer>
            <Paper style={{
                height: '100%', width: '75%', borderRadius: '0px 30px 30px 0px', boxShadow: 'none',
                display: 'flex', alignItems: 'center', justifyContent: 'center', padding: '30px 0px 30px 0px'
            }}>
                {mobileInfo || stationaryInfo ?
                    mobileInfo ?
                        <MobileSalePointInfoCard location={mobileInfo.location} shops={mobileInfo.shops}/> :
                        (stationaryInfo ?
                            <StationarySalePointInfoCard shop={stationaryInfo}/> :
                            <Typography variant={'h6'}>Wybierz punkt sprzedaży</Typography>)
                    :
                    <Typography variant={'h6'}>Wybierz punkt sprzedaży</Typography>
                }
            </Paper>
        </Stack>
    );
}
