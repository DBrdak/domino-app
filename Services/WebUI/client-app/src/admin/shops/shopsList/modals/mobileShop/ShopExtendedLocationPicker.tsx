import React, {useEffect, useState} from 'react';
import {MapContainer, TileLayer, Marker, Popup, useMapEvents, useMapEvent} from 'react-leaflet';
import { Location } from '../../../../../global/models/common'
import {LatLng, LeafletEvent, LeafletMouseEvent} from "leaflet";
import {Button, ButtonGroup, Stack, useMediaQuery} from "@mui/material";
import {toast} from "react-toastify";
import {SalePoint} from "../../../../../global/models/shop";
import theme from "../../../../../global/layout/theme";

interface ShopExtendedLocationPicker {
    onChange: (pickedSalePoint: SalePoint) => void
    existingSalePoints: SalePoint[]
}

function ShopExtendedLocationPicker({onChange, existingSalePoints}: ShopExtendedLocationPicker) {
    const [selectedLocation, setSelectedLocation] = useState<Location | null>(null);
    const [selectedSalePoint, setSelectedSalePoint] = useState<SalePoint | null>(null);
    const [duplicatedWeekDays, setDuplicatedWeekDays] = useState<string[] | null>(null)
    const isMobile = useMediaQuery(theme.breakpoints.down(1920))

    useEffect(() => {
        selectedSalePoint && onChange(selectedSalePoint)
    }, [selectedSalePoint])

    function handleMapClick(salePoint: SalePoint) {
        const longitude = String(salePoint.location.longitude)
        const latitude = String(salePoint.location.latitude)
        setSelectedLocation({latitude: latitude, longitude: longitude, name: salePoint.location.name})

        const duplicatedSalePoints = [...existingSalePoints].filter(sp =>
            sp.location.name === salePoint.location.name && sp.location.latitude === salePoint.location.latitude && sp.location.longitude === salePoint.location.longitude)

        if(duplicatedSalePoints.length > 1){
            setSelectedSalePoint(null)
            setDuplicatedWeekDays(duplicatedSalePoints.map(sp => sp.weekDay.value))
        } else {
            setDuplicatedWeekDays(null)
            setSelectedSalePoint(salePoint)
        }
    }

    const hxw = isMobile ? 300 : 500

    function handleWeekDayChoice(weekDay: string) {
        const duplicatedSalePoints = selectedLocation && [...existingSalePoints].filter(sp =>
            sp.location.name === selectedLocation.name && sp.location.latitude === selectedLocation.latitude && sp.location.longitude === selectedLocation.longitude)
        const uniqueSalePoint = duplicatedSalePoints && duplicatedSalePoints.find(sp => sp.weekDay.value === weekDay)

        uniqueSalePoint && setSelectedSalePoint(uniqueSalePoint)
    }

    return (
        <div>
            <MapContainer
                center={[52.80537130233171, 20.118007397537376]}
                zoom={8}
                style={{ height: hxw, width: hxw, borderRadius: '20px', boxShadow: '0px 0px 7px 0px' }}
            >
                <TileLayer
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />
                {existingSalePoints && existingSalePoints.map((sp, i) => (
                    <Marker key={i} riseOnHover eventHandlers={{
                        click: () => {
                            handleMapClick(sp)
                        }
                    }}
                            position={{lng: Number(sp.location.longitude), lat: Number(sp.location.latitude)}}>
                        {duplicatedWeekDays ?
                            <Popup keepInView>
                                <Stack style={{flexDirection: 'column', gap: '3px', padding: '1px'}}>
                                    {duplicatedWeekDays.map((wd, i) => (
                                        <Button variant={selectedSalePoint?.weekDay.value === wd ? 'contained' : 'outlined'}
                                                key={i} onClick={() => handleWeekDayChoice(wd)} >
                                            {wd}
                                        </Button>
                                    ))}
                                </Stack>
                            </Popup>
                            :
                            <Popup keepInView>
                                {selectedLocation?.name}
                            </Popup>
                        }
                    </Marker>
                ))}
            </MapContainer>
        </div>
    );
}

export default ShopExtendedLocationPicker;
