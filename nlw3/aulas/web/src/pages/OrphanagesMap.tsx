import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { FiPlus, FiArrowRight } from 'react-icons/fi';
import { Map, TileLayer, Marker, Popup } from 'react-leaflet';

import mapMarkerImg from '../images/map-marker.svg';

import '../styles/pages/orphanagesMap.css';
import mapIcon from '../utils/mapIcon';
import api from '../services/api';

interface Orphanage {
  id: number;
  latitude: number;
  longitude: number;
  name: string;
}

function OrphanagesMap() {
  //inicia o state orphanages com uma lista vazia
  const [orphanages, setOrphanages] = useState<Orphanage[]>([]);

  console.log(orphanages);

  useEffect(() => {
    api.get('orphanages').then(response => {
      //setOrphanages atualiza a variável orphanages (que está desestruturado)
      setOrphanages(response.data);
    });
  }, []);
  
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


      {orphanages.map(orphanage => {
        return(
          <Map
            center={[orphanage.latitude, orphanage.longitude]}
            zoom={15}
            style={{ width: '100%', height: '100%' }}
          >
            <TileLayer url="https://a.tile.openstreetmap.org/{z}/{x}/{y}.png" />

            <Marker
              icon={mapIcon}
              position={[-22.7244976,-47.6381184]}
            >
              <Popup closeButton={false} minWidth={240} maxHeight={240} className="map-popup">
                Lar das meninas
                <Link to="/orphanages/1">
                  <FiArrowRight size={20} color="#fff" />
                </Link>
              </Popup>
            </Marker>

            {/* <TileLayer
              url={`https://api.mapbox.com/styles/v1/mapbox/light-v10/tiles/256/{z}/{x}/{y}@2x?access_token=${process.env.REACT_APP_MAPBOX_TOKEN}`} /> */}
          </Map>
        );
      })}

      <Link to="/orphanages/create" className="create-orphanage" >
        <FiPlus size={32} color="#FFF" />
      </Link>

      
    </div>
  )
}

export default OrphanagesMap;