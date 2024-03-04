import {Routes, Route, BrowserRouter} from 'react-router-dom';
import Logovanje from './komponente/Logovanje/Logovanje';
import Navigacija from './komponente/Navigacija/Navigacija';
import React, { useEffect, useState } from 'react';
import Registracija from './komponente/Registracija/Registracija';

import Pocetna from './Pocetna'
import SamoProizvod from './komponente/SamoProizvod/SamoProizod';
import Dostavljac from './Deliever';
import Pretraga from './komponente/Pretraga';
import {useRef} from 'react';

import { MDBBtn, MDBAlert } from 'mdb-react-ui-kit';


function Rute ()
{


    
    
    return (<div style={{ width:'100%', height:'100vh' }}> 
       
        <Navigacija />
        <div >
        <BrowserRouter>
        <Routes>  
       
            <Route path='/logovanje' element={<Logovanje />} />
            <Route path='/registracija' element={<Registracija/>} />
        
            <Route path='/' element={<Pocetna/>} />
            <Route path='/car/:CarID' element={< SamoProizvod />} />
            <Route path='dealer' element={<Dostavljac/>} />
            <Route path='/pretraga' element = {<Pretraga/>} />
           
        </Routes>
        </BrowserRouter>
        </div>

        </div>
           );
}
export default Rute;