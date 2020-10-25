import React from 'react';
import { Link } from 'react-router-dom';
import mapMarkerImg from '../images/map-marker.svg';
import { FiPlus } from 'react-icons/fi';
import { Map, TileLayer, Marker, Popup } from 'react-leaflet';
import Leaflet from 'leaflet';

import 'leaflet/dist/leaflet.css';

import '../styles/pages/orphanagesMap.css';

const mapIcon = Leaflet.icon({
  iconUrl: mapMarkerImg,
  iconSize: [58, 68],
  iconAnchor: [29, 68],
  popupAnchor: [170, 2]
})

function OrphanagesMap() {
  return (
    <div id="page-map">
      <aside>
        <header>
          <img src={mapMarkerImg} alt="Happy"/>

          <h2>Escola um orfanato no mapa</h2>
          <p>Muitas crianças estão esperando a sua visita :)</p>
        </header>

        <footer>
          <strong>Piracicaba</strong>
          <span>São Paulo</span>
        </footer>
      </aside>

      <Map
        center={[-22.7244976,-47.6352641]}
        zoom={15}
        style={{ width: '100%', height: '100%' }}
      >
        <TileLayer url="https://a.tile.openstreetmap.org/{z}/{x}/{y}.png" />

        <Marker
          icon={mapIcon}
          position={[-22.7244976,-47.6381184]}
        >
          <Popup closeButton={false} minWidth={240} maxHeight={240}>
            Lar das meninas
          </Popup>
        </Marker>

        {/* <TileLayer
          url={`https://api.mapbox.com/styles/v1/mapbox/light-v10/tiles/256/{z}/{x}/{y}@2x?access_token=${process.env.REACT_APP_MAPBOX_TOKEN}`} /> */}
      </Map>

      <Link to="" className="create-orphanage" >
        <FiPlus size={32} color="#FFF" />
      </Link>

      
    </div>
  )
}

export default OrphanagesMap;