import React, {useEffect, useState} from 'react';
import {MapContainer, TileLayer, Marker, Popup, useMapEvents, useMapEvent} from 'react-leaflet';
import { Location } from '../../global/models/common'
import {LatLng, LeafletEvent, LeafletMouseEvent} from "leaflet";
import {useMediaQuery} from "@mui/material";
import theme from "../../global/layout/theme";
import {toast} from "react-toastify";

interface LocationPickerProps {
    locationName: string
    onChange: (pickedLocation: Location) => void
}

function LocationPicker({locationName, onChange}: LocationPickerProps) {
    const [selectedLocation, setSelectedLocation] = useState<Location | null>(null);
    const isMobile = useMediaQuery(theme.breakpoints.down(1920))

    useEffect(() => {
        selectedLocation && onChange(selectedLocation)
    }, [selectedLocation])

    function handleMapClick(coordinates: LatLng) {
        const longitude = String(coordinates.lng)
        const latitude = String(coordinates.lat)
        setSelectedLocation({latitude: latitude, longitude: longitude, name: locationName})
    }

    const hxw = isMobile ? 300 : 500

    return (
        <div>
            <MapContainer
                center={[52.80537130233171, 20.118007397537376]}
                zoom={8}
                style={{ height: hxw, width: hxw }}
            >
                <TileLayer
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />
                <ClickLocator onMapClick={(e) => handleMapClick(e.latlng)} />
                {selectedLocation && (
                    <Marker position={{lng: Number(selectedLocation.longitude), lat: Number(selectedLocation.latitude)}}>
                        <Popup>
                            {locationName}
                        </Popup>
                    </Marker>
                )}
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
