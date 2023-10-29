import React, { useEffect, useRef } from 'react';
import L from 'leaflet';

function Map() {
  const mapRef = useRef<HTMLDivElement | null>(null);

  useEffect(() => {
    if (!mapRef.current) {
      return;
    }

    const map = L.map(mapRef.current).setView([52.805642320027076, 20.119060046456283], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);

    const marker = L.marker([52.805642320027076, 20.119060046456283]).addTo(map);
    marker.bindPopup('Pólka-Raciąż 75A');

    return () => {
      map.remove();
    };
  }, []);

  return <div ref={mapRef} style={{ width: '400px', height: '400px', borderRadius: '20px', boxShadow: '0px 0px 7px 0px' }} />;
}

export default Map;