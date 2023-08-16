import React, { useState } from "react";
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import { Paper, Typography, Button, Stack, AppBar, Toolbar } from "@mui/material";
import { observer } from "mobx-react-lite";
import { Link, useNavigate } from "react-router-dom";
import { useStore } from "../../../global/stores/store";
import { DateTimeRange, DeliveryInfo, Location } from "../../../global/models/common";
import 'leaflet/dist/leaflet.css';
import 'leaflet-defaulticon-compatibility/dist/leaflet-defaulticon-compatibility.webpack.css'; 
import 'leaflet-defaulticon-compatibility';
import { getPolishDayOfWeek, getNextDay } from "./temp";
import { usePreventNavigation } from "../../../global/router/routeProtection";


const DeliveryInfoStep: React.FC = () => {
  const { shoppingCartStore } = useStore();
  const [selectedDeliveryPoint, setSelectedDeliveryPoint] = useState<DeliveryInfo | null>(null);
  const navigate = useNavigate();

  usePreventNavigation([
    shoppingCartStore.shoppingCart, shoppingCartStore.personalInfo
  ], '/koszyk')

  const exampleLocations: Location[] = [
    { name: "Unieck", latitude: "52.86934621851329", longitude: "20.199904860257817" },
    { name: "Krzeczanowo", latitude: "52.854646063625495", longitude: "20.104278953042556" },
    { name: "Jeżewo-Wesel", latitude: "52.87918981616971", longitude: "20.162419484104024" },
  ];

  const exampleDates: DateTimeRange[] = [
    {start: getNextDay('Środa', 8), end: getNextDay('Środa', 9, 20)},
    {start: getNextDay('Środa', 11), end: getNextDay('Środa', 11, 40)},
    {start: getNextDay('Środa', 9, 30), end: getNextDay('Środa', 10, 40)},
  ]

  const exampleDeliveryPoints: DeliveryInfo[] = [
    {deliveryDate: exampleDates[0], deliveryLocation: exampleLocations[0]},
    {deliveryDate: exampleDates[1], deliveryLocation: exampleLocations[1]},
    {deliveryDate: exampleDates[2], deliveryLocation: exampleLocations[2]},
  ]

  const handleMarkerClick = (location: Location, date: DateTimeRange) => {
    setSelectedDeliveryPoint({deliveryLocation:location, deliveryDate: date});
  };

  function handleSubmit() {
    if (selectedDeliveryPoint) {
      shoppingCartStore.setDeliveryInfo(selectedDeliveryPoint);
      navigate('/koszyk/zamówienie')
    }
  };

  return (
    <div style={{ display: 'flex', height: '100vh', justifyContent: 'center', alignItems: 'center' }}>
      <Paper style={{ padding: 50, margin: 'auto', maxWidth: 500 }}>
        <AppBar position="static" style={{ marginBottom: '20px' }}>
          <Toolbar>
            <Typography textAlign={'center'} width={'100%'} variant="h5">
              Dane Wysyłki
            </Typography>
          </Toolbar>
        </AppBar>
        <div>
          <MapContainer center={[52.80537130233171, 20.118007397537376]} zoom={11} style={{height: 300, width: 350  }}>
            <TileLayer
              url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            />
            {exampleDeliveryPoints.map(({deliveryLocation: l, deliveryDate: d}) => (
              <Marker
                key={l.name}
                position={[parseFloat(l.latitude), parseFloat(l.longitude)]}
                eventHandlers={{
                  click: () => handleMarkerClick(l, d),
                }}
              >
                <Popup>{l.name}</Popup>
              </Marker>
            ))}
          </MapContainer>
            {selectedDeliveryPoint ? 
              <Paper style={{textAlign: 'center', marginTop: '0px', padding: '10px 0px 10px 0px'}}>
                <div style={{ marginTop: 10 }}>
                  <Typography textAlign={'center'} fontWeight={'bold'}>{selectedDeliveryPoint.deliveryLocation.name}</Typography>
                  <Typography>
                    Czas odbioru: {}
                    {getPolishDayOfWeek(selectedDeliveryPoint.deliveryDate.start)} {}  
                    {selectedDeliveryPoint.deliveryDate.start.toLocaleTimeString().slice(0,5)}  
                    {} - {}
                    {getPolishDayOfWeek(selectedDeliveryPoint.deliveryDate.end)} {}  
                    {selectedDeliveryPoint.deliveryDate.end.toLocaleTimeString().slice(0,5)} 
                    </Typography>
                </div>
              </Paper>
              :
              <Paper style={{textAlign: 'center', marginTop: '0px', padding: '10px 0px 10px 0px'}}>
                <Typography variant="h6">Proszę wybrać punkt odbioru</Typography>
              </Paper>
            }
        </div>
        <Stack width={'100%'} justifyContent={'space-between'} direction={'row'}>
          <Link to={'/koszyk/dane-osobowe'} >
            <Button variant="outlined" color="primary" style={{ marginTop: 16 }}>Wróć</Button>
          </Link>
          <Button onClick={() => handleSubmit()} disabled={selectedDeliveryPoint === null} variant="contained" color="primary" style={{ marginTop: 16 }}>
            Wyślij zamówienie
          </Button>
        </Stack>
      </Paper>
    </div>
  );
};

export default observer(DeliveryInfoStep);
