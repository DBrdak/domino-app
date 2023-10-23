import {MapContainer, Marker, Popup, TileLayer} from "react-leaflet";
import React, {useState} from "react";
import {Paper, Stack, Typography} from "@mui/material";
import {MobileShop, SalePoint, StationaryShop} from "../../../global/models/shop";
import {MobileSalePointInfoCard} from "./MobileSalePointInfoCard";
import {StationarySalePointInfoCard} from "./StationarySalePointInfoCard";
import LoadingComponent from "../../../components/LoadingComponent";
import {Location} from '../../../global/models/common'
import {getCenterFromShopLocations} from "../../../global/utils/locationService";

interface Props {
    stationaryShops: StationaryShop[],
    mobileShops: MobileShop[]
    locations: Location[]
}

export function SalePointMap({mobileShops, stationaryShops, locations}: Props) {
    const [mobileInfo, setMobileInfo] = useState<{ location: Location, shops: MobileShop[] } | null>(null)
    const [stationaryInfo, setStationaryInfo] = useState<StationaryShop | null>(null)

    const combinedCenter = getCenterFromShopLocations(stationaryShops, mobileShops)

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
                                }}>
                            <Popup>
                                {l.name}
                            </Popup>
                        </Marker>
                    ))
                }
                {stationaryShops.map(ss => (
                    <Marker key={ss.id} riseOnHover eventHandlers={{
                        click: () => {
                            setMobileInfo(null)
                            setStationaryInfo(ss)
                        }
                    }}
                            position={{lng: Number(ss.location.longitude), lat: Number(ss.location.latitude)}}>
                        <Popup>
                            {ss.location.name}
                        </Popup>
                    </Marker>
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
