import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Logovanje from '../Logovanje/Logovanje';

import { useState, useEffect } from "react";
import axios from "axios";

import './Navigacija.css'






import {
  MDBContainer,
  MDBNavbar,
  MDBNavbarBrand,
  MDBNavbarToggler,
  MDBIcon,
  MDBNavbarNav,
  MDBNavbarItem,
  MDBNavbarLink,
  MDBBtn,
  MDBDropdown,
  MDBDropdownToggle,
  MDBDropdownMenu,
  MDBDropdownItem,
  MDBCollapse,
  MDBBadge,
  MDBPopover,
  MDBPopoverBody,
  MDBPopoverHeader,
 
} from 'mdb-react-ui-kit';
import { Button } from 'react-bootstrap';

const Navigacija = (props) =>
{
  const [popoverOpen, setPopoverOpen] = useState(false);
  const[login, setlogin]= useState("");
  const[notifications, setNotifications]= useState(false);
  const [categories, setCategories] = useState();
  const [markets, setMarkets] = useState();
  const[user_info, setUserinfo]=useState("");
  const[dealer_info, setDealerinfo]=useState("");
  const[dealer_name, setDealerName]=useState("");
  const[search, setSearch] = useState("");
  const[param, setparam] = useState("")
  const user = JSON.parse(localStorage.getItem('user-info'));
  const del=localStorage.getItem('dealer-info');
  const [message, setMessage] = useState('');
  
 

  


 

  console.log(user);
  useEffect(()=>{
   
    
    console.log(user);
    if (user!=null)
     setUserinfo(user);
    // console.log(user_info);
    //console.log(del);

    if (del!=null)
    {
      setDealerinfo(del);
      const dealerInfo = JSON.parse(del);
      const name = dealerInfo.name;
      setDealerName(name);
    }
    
  },[]
  )
  
  
  function notificationsShow()
  {
    setNotifications(true);
  }
  function notificationsHide()
  {
    setNotifications(false);
  }

  

  function loginshow() {
    setlogin(true);
  }
  function loginhide() {
    setlogin(false);
  }

  function logout()
    {
      if(localStorage.getItem('user-info')) 
       // localStorage.removeItem('user-info');
       localStorage.clear();
      //history.push("/");
      else 
        localStorage.removeItem('dealer-info');
      //window.location.reload();
      window.location.href='/';
    }
    
    
    
    
    
     
    
 

  return (
    

       

    <MDBNavbar expand='lg'  bgColor='light'>
      <MDBContainer fluid>
   
      
      { dealer_info ? (<><MDBNavbarBrand > {dealer_name} </MDBNavbarBrand></>) : (<> <MDBNavbarBrand ><img src="https://cdn-icons-png.flaticon.com/512/2156/2156021.png" style={{ height: '30px', objectFit: 'cover' }} ></img></MDBNavbarBrand></>) }

         

        <MDBNavbarToggler
          aria-controls='navbarSupportedContent'
          aria-expanded='false'
          aria-label='Toggle navigation'


        >
          <MDBIcon icon='bars' fas />
        </MDBNavbarToggler>


        
        <MDBCollapse navbar >
        <MDBNavbarNav className='mr-auto mb-2 mb-lg-0'>


        {dealer_info ? (<>
          </>)
          : (<>
          <MDBNavbarItem>
              <MDBNavbarLink active aria-current='page' href='/'>
                Početna
              </MDBNavbarLink>
            </MDBNavbarItem>


        </>)}
          
            


          </MDBNavbarNav>

          
          
          

          

          


          <Logovanje show={login} onHide={loginhide}></Logovanje>
          
          {/* <MDBNavbarItem> <MDBNavbarLink onClick={notificationsShow} eventkey={2}>Notifikacije </MDBNavbarLink></MDBNavbarItem>
          <Notifikacije show={notifications} onHide={notificationsHide}></Notifikacije> */}
          

          {/* <MDBPopover color='secondary' btnChildren='Popover on bottom' placement='bottom' onClick={notificationsShow}></MDBPopover>
          <Notifikacije show={notifications} onHide={notificationsHide}></Notifikacije> */}


      {user_info ? (<> 
          
       

          {/* <div>   
            {setNotifications && <Notifikacije {...user_info} onClose={() => setNotifications(false)}  />}
          </div> */}

         
        </>  ) : (<>
          </>)
      }

          
    {user_info || dealer_info ? (<>        
            <MDBNavbarLink onClick={logout} eventkey={2} style={{ whiteSpace: 'nowrap' }}>Odjavi se</MDBNavbarLink>
       
        </>  ) : (<>
        
            <MDBNavbarLink onClick={loginshow} eventkey={2} style={{ whiteSpace: 'nowrap' }}>Prijavi se</MDBNavbarLink>
            <MDBNavbarLink href="/registracija" eventkey={2} style={{ whiteSpace: 'nowrap' }}>Registruj se</MDBNavbarLink>

          </>)
    }

   



         

        </MDBCollapse>
      </MDBContainer>
    </MDBNavbar>

  
  );
}
export default Navigacija;