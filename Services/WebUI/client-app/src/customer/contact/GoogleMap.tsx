import { useEffect } from 'react';

interface GoogleMapProps {
    apiKey: string;
    lat: number;
    lng: number;
}

declare global {
  interface Window { 
      initMap: () => void;
  }
}


const GoogleMap: React.FC<GoogleMapProps> = ({ apiKey, lat, lng }) => {
    useEffect(() => {
        const script = document.createElement('script');
        script.src = `https://maps.googleapis.com/maps/api/js?key=${apiKey}&callback=initMap`;
        script.async = true;
        window.initMap = () => {
          const map = new google.maps.Map(document.getElementById("map") as HTMLElement, {
              center: { lat, lng },
              zoom: 12,
          });
          new google.maps.Marker({
            position: { lat, lng },
            map,
          });
        };
        document.head.appendChild(script);
    }, [apiKey, lat, lng]);

    return <div id="map" style={{ width: '100%', height: '300px' }} />;
}

export default GoogleMap;
