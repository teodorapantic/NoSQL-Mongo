import React, { useEffect, useState } from 'react';

import axios from "axios";

import {
  MDBContainer,
  MDBRow,
  MDBCol,
  MDBCard,
  MDBCardBody,
  MDBCardImage,
  MDBIcon,
  MDBRipple,
  MDBBtn,
  MDBDropdownToggle,
  MDBDropdownMenu,
  MDBDropdownItem,
  MDBDropdown,
  MDBInput,
  MDBSwitch 
} from "mdb-react-ui-kit";




function Pocetna() {

    const [marks, setMarks] = useState();
    const [models, setModels] = useState(false);
    const [price, setPrice] = useState(false);
    const [fuelTypes, setFuelTypes] = useState(
      [{name: "Dizel"},
       {name: "Benzin"},
       {name: "Hibridni pogon"},
       {name: "Elekricni pogon"}
    ]


      
    );
    const [cars, setCars] = useState(false);
    const [mark, setMark] = useState(" ");
    const [model, setModel] = useState(" ");
    const [fuel, setFuel] = useState(" ");
    const [minPrice, setMinPrice] = useState(-1);
    const [maxPrice, setMaxPrice] = useState(-1);
    const [rentSale, setRentSale] = useState(true);



    


    function changeMark(m)
    {
      setMark(m.name);
    }

    function changeModel(m)
    {
      setModel(m.name);
    }
    function changeFuel(m)
    {
      setFuel(m.name);
    }

    
    const options = [
      {
          label: <span>Iznajmi</span>,
          value: {
               foo: true
          },
          selectedBackgroundColor: "#0097e6",
      },
      {
          label: "Kupi",
          value: {
            bar: false
          },
          selectedBackgroundColor: "#fbc531"
      }
   ];

   const changeRentSale = () => {
      
      setRentSale(!rentSale);
   }
  

   const initialSelectedIndex = options.findIndex(({value}) => value === "bar");

    
    console.log(maxPrice, minPrice);

    useEffect(()=>{
   
  
      
  
  
      axios.get("https://localhost:44341/Car/GetMarka")
      .then(res => {
        console.log(res)
        setMarks(res.data)
      })
      .catch(err => {
        console.log(err)
      })
      
      console.log(marks);

      if (mark != false) {
        
       console.log(mark);
        axios.get("https://localhost:44341/Car/GetModelsFromMark/"+mark.id)
        .then(res => {
          //console.log(res)
          setModels(res.data)
          
        })
        .catch(err => {
          console.log(err)
        })

        console.log(models);
      }
      
  
      
    },[mark])

    const getCars = () => {

      console.log(mark,model,fuel,maxPrice, minPrice);

      axios.get("https://localhost:44341/Car/GetCarsWithFilters/"+mark+"/"+model+"/"+minPrice+"/"+maxPrice+"/"+fuel+"/"+rentSale)
        .then(res => {
          //console.log(res)
          setCars(res.data)
          
        })
        .catch(err => {
          console.log(err)
        })

        console.log(cars);

    }

    
    // useEffect(()=>{
      

    // },[mark.id])

    
    

    
    return (
        <>
      <div style={{ textAlign: 'center', fontWeight: 'bold', fontSize: 'large' }}>
       
      </div>
     
      <MDBContainer fluid>
       <MDBRow className="justify-content-center mb-0">
        <MDBCol md="12" xl="10">
          <MDBCard className="shadow-0 border rounded-3 mt-5 mb-3">
            <MDBCardBody className='justify-content-row'>
            <div style={{ textAlign: 'center', fontWeight: 'bold', fontSize: 'large' }}>
                Pronadjite zeljeni automobil
              </div>

              <MDBDropdown>
                <MDBDropdownToggle tag='a' className='nav-link' role='button'>
                  Marka
                </MDBDropdownToggle>
                <MDBDropdownMenu>
                  {marks ? marks.map( m => (
                     <MDBDropdownItem key={m.id}  onClick={() => changeMark(m)}>  
                     {m.name}
                   </MDBDropdownItem>

                  )): <div>Loading...</div>}
                </MDBDropdownMenu>
              </MDBDropdown>


              <MDBDropdown className='justify-contetnt-center'>
                <MDBDropdownToggle tag='a' className='nav-link' role='button'>
                  Model
                </MDBDropdownToggle>
                <MDBDropdownMenu>
                  {models ? models.map( m => (
                     <MDBDropdownItem  key={m.id} onClick={() => changeModel(m)}>  
                     {m.name}
                   </MDBDropdownItem>

                  )): <div>Loading...</div>}
                </MDBDropdownMenu>
              </MDBDropdown>

              <MDBDropdown className='justify-contetnt-center'>
                <MDBDropdownToggle tag='a' className='nav-link' role='button'>
                  Gorivo
                </MDBDropdownToggle>
                <MDBDropdownMenu>
                  {fuelTypes? fuelTypes.map( m => (
                     <MDBDropdownItem onClick={() => changeFuel(m)}  >  
                     {m.name}
                   </MDBDropdownItem>

                  )): <div>Loading...</div>}
                </MDBDropdownMenu>
              </MDBDropdown>


    
              <div className='mb-4'>
                <label>Kupi</label>
                  <MDBSwitch  id='flexSwitchCheckDefault' label='Iznajmi' onChange={() => changeRentSale()} />
                </div>
          
 



              <MDBInput wrapperClass='mb-4' label='cena od' id='formControlLg'  size="lg" type="number"  onChange={(e)=>setMinPrice(e.target.value)}/>
              <MDBInput wrapperClass='mb-4' label='cena do' id='formControlLg'  size="lg" type="number"  onChange={(e)=>setMaxPrice(e.target.value)}/>

              <MDBBtn className="mb-4 px-5"  size='lg'  onClick={getCars}>Pretrazi</MDBBtn>


            </MDBCardBody>
          </MDBCard>

          
        </MDBCol>
          
        <MDBCol md="12" xl="10">
          {cars ? cars.map(c => (

          <MDBCard className="shadow-0 border rounded-3 mt-5 mb-3">
           <MDBCardBody className='justify-content-row'>
            <div style={{ textAlign: 'center', fontWeight: 'bold', fontSize: 'large' }}>
             {c.mark.name}
             {c.carModel.name}


              </div>
            </MDBCardBody>
            <a href={`/car/${c.id}`}>
            <MDBBtn className="mb-4 px-5"  size='lg'  >Prikazi vise</MDBBtn>
            </a>
           </MDBCard>

          )): <div>Nema vozila</div>}
          
        </MDBCol>
      </MDBRow>
    </MDBContainer>
     
   
      </>
    );
  }
  
  export default Pocetna;
  