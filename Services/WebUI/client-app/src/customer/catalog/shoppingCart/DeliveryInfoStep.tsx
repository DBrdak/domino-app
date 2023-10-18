import React, {useEffect, useState} from "react";
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
import {DeliveryPoint} from "../../../global/models/shop";


const DeliveryInfoStep: React.FC = () => {
  const { shoppingCartStore, shopStore } = useStore();
  const [selectedDeliveryPoint, setSelectedDeliveryPoint] = useState<DeliveryPoint | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    shopStore.loadDeliveryPoints()
  }, [])

  usePreventNavigation([
    shoppingCartStore.shoppingCart, shoppingCartStore.personalInfo
  ], '/koszyk')

  function handleSubmit() {
    if (selectedDeliveryPoint) {
      shoppingCartStore.setDeliveryPoint(selectedDeliveryPoint);
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
            {shopStore.deliveryPoints && shopStore.deliveryPoints.map((d,i) => (
              <Marker
                key={i}
                position={[Number(d.location.latitude), Number(d.location.longitude)]}
                eventHandlers={{
                  click: () => setSelectedDeliveryPoint(d),
                }}
              >
                <Popup>{d.location.name}</Popup>
              </Marker>
            ))}
          </MapContainer>
            {selectedDeliveryPoint ? 
              <Paper style={{textAlign: 'center', marginTop: '0px', padding: '10px 0px 10px 0px'}}>
                <div style={{ marginTop: 10 }}>
                  <Typography textAlign={'center'} fontWeight={'bold'}>{selectedDeliveryPoint.location.name}</Typography>
                  <Typography>
                    {shopStore.deliveryPoints.flatMap(dp => dp.possiblePickupDate).map(d => `${d.start} - ${d.end}`)}
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
