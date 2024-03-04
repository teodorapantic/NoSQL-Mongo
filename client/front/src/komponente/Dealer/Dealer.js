import React, {useState, useEffect} from 'react';
import *  as signalR from '@microsoft/signalr';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import {
  MDBCard,
  MDBCardBody,
  MDBCardTitle,
  MDBCardText,
  MDBCardHeader,
  MDBCardFooter,
  MDBBtn,
  MDBCol,
  MDBRow,
  MDBContainer
} from 'mdb-react-ui-kit';
import axios from 'axios';



function Dealer()
{
    const[message , setMessage] =useState(false);
    const[rentals, setRentals] = useState("");
    const[tests, setTests] = useState("");

    let messages =[];
          let deliver = JSON.parse(localStorage.getItem("dealer-info"));

    async function Dozvoli(id)
    {
      await fetch("https://localhost:44341/RentCar/AllowRental/"+id).then(s=>{
        s.ok(alert("Dozolili ste ovaj RentCar"))
      })
    }

    async function Odzumi(id)
    {
      await fetch("https://localhost:44341/RentCar/FrobidRental/"+id).then(s=>{
        s.ok(alert("Oduzeli ste dozvolu za ovaj RentCar"))
      })
    }
    async function dohvati()
    {

      fetch("https://localhost:44341/Dealer/GetDealersTestDrives/"+deliver.id+"/true"
      ).then((p)=>{return p.json();}
      ).then((data)=>{
        let testov = [];
        for(const key in data) {
          
          const ret = {
            id: key,
            ...data[key]
          }
          testov.push(ret);
        }
        setTests(testov);
      })

      fetch("https://localhost:44341/Dealer/GetRentCars/"+deliver.id+"/true"
      ).then((p)=>{return p.json();}
      ).then((data)=>{
        let retov = [];
        for(const key in data) {
          
          const ret = {
            id: key,
            ...data[key]
          }
          retov.push(ret);
        }
        setRentals(retov);
      })
      
      
      //console.log(data);
      //setRentals(data);
      
    }
    useEffect(()=>
    {
      dohvati()
    },[])

      return ( <div>{rentals ? (<>{rentals.map(m=>{<div><MDBRow>
        { 
          <MDBCol >
            <MDBCard className='text-center mb-3'>
                 <MDBCardHeader><MDBCardTitle style={{ fontSize: '25px' }}>Automobil: {m.car.carModel}</MDBCardTitle></MDBCardHeader>
                 <MDBCardBody>
                
      
                   <p style={{ margin: 0 }}><b>Zauzet od: {m.occupiedFrom}</b> </p>
                   <p style={{ margin: 0 }}><b> Zauzet do: {m.occupiedUntill}</b> </p>
                   <p style={{ margin: 0 }}><b>Korisnik: {m.user.userName}</b> </p>
                   <p style={{ margin: 0 }}><b>{m.allowed?(<><MDBBtn className="mb-4 px-5"  size='lg'  onClick={()=>{Dozvoli(m.id)}}>Dozvoli</MDBBtn></>):(<><MDBBtn className="mb-4 px-5"  size='lg'  onClick={()=>{Odzumi(m.id)}}>Oduzmi</MDBBtn></>)}</b> </p>
                  
                 </MDBCardBody>
                 <MDBCardFooter className='text-muted'></MDBCardFooter>
               </MDBCard>
          </MDBCol>
        }
      </MDBRow></div>})}</>):(<><div>Trenutno nemate RentCar koje trebate da odobrite</div></>)} 
      {tests?(<><MDBCol >
            <MDBCard className='text-center mb-3'>
                 <MDBCardHeader><MDBCardTitle style={{ fontSize: '25px' }}>Automobil: {m.car.carModel}</MDBCardTitle></MDBCardHeader>
                 <MDBCardBody>
                
      
                   <p style={{ margin: 0 }}><b>Datum Testiranja: {m.testDate}</b> </p>
                   <p style={{ margin: 0 }}><b>Korisnik: {m.user.userName}</b> </p>
                
                  
                 </MDBCardBody>
                 <MDBCardFooter className='text-muted'></MDBCardFooter>
               </MDBCard>
          </MDBCol></>):(<><div>Nemate testove</div></>)}</div>


          
           );
     
           
     
}
     export default Dealer;

