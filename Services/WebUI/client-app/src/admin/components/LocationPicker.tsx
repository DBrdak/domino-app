import React, {useEffect, useState} from 'react';
import {MapContainer, TileLayer, Marker, Popup, useMapEvents, useMapEvent} from 'react-leaflet';
import { Location } from '../../global/models/common'
import {LatLng, LeafletEvent, LeafletMouseEvent} from "leaflet";
import {useMediaQuery} from "@mui/material";
import theme from "../../global/layout/theme";
import {toast} from "react-toastify";
import {SalePoint} from "../../global/models/shop";
import {bool} from "yup";
import dismiss = toast.dismiss;

interface LocationPickerProps {
    locationName: string
    onChange: (pickedLocation: Location) => void
    existingSalePoints?: SalePoint[]
    setNameEdition?: (state: boolean) => void
    newMode?: boolean
}

function LocationPicker({locationName, onChange, existingSalePoints, setNameEdition, newMode}: LocationPickerProps) {
    const [selectedLocation, setSelectedLocation] = useState<Location | null>(null);
    const isMobile = useMediaQuery(theme.breakpoints.down(1920))

    useEffect(() => {
        selectedLocation && onChange(selectedLocation)
    }, [selectedLocation])

    function handleMapClick(lat: number | string, lng: number | string, name?: string) {
        if(locationName.length < 1 && !name) {
            toast.dismiss()
            toast.error('Podaj nazwę punktu')
            return
        }


        if(setNameEdition && !name && !locationName ){
            console.log('hi')
            setNameEdition(true)
            setSelectedLocation({latitude: '', longitude: '', name: ''})
            return;
        }

        setNameEdition && name && setNameEdition(false)

        const longitude = String(lng)
        const latitude = String(lat)
        setSelectedLocation({latitude: latitude, longitude: longitude, name: name ? name : locationName})
    }

    const hxw = isMobile ? 300 : 500

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
                {newMode && <ClickLocator onMapClick={(e) => handleMapClick(e.latlng.lat, e.latlng.lng)}/>}
                {selectedLocation && (
                    <Marker position={{lng: Number(selectedLocation.longitude), lat: Number(selectedLocation.latitude)}}>
                        <Popup keepInView>
                            {locationName}
                        </Popup>
                    </Marker>
                )}
                {existingSalePoints && existingSalePoints.map((sp, i) => (
                    <Marker key={i} riseOnHover eventHandlers={{
                        click: () => {
                            handleMapClick(sp.location.latitude, sp.location.longitude, sp.location.name)
                        }
                    }}
                            position={{lng: Number(sp.location.longitude), lat: Number(sp.location.latitude)}}>
                        <Popup keepInView>
                            {sp.location.name}
                        </Popup>
                    </Marker>
                ))}
            </MapContainer>
        </div>
    );
}

export default LocationPicker;

interface ClickLocatorProps {
    onMapClick: (e: LeafletMouseEvent) => void
}

function ClickLocator({onMapClick}: ClickLocatorProps) {
    useMapEvent('click', (event) => {
        onMapClick(event)
    })
    return null
}
