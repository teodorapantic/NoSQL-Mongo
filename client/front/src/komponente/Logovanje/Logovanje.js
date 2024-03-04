

import {useState} from "react";
import {useHistory} from"react-router-dom";
import React from "react";
import {Form, Button, CloseButton} from "react-bootstrap";
import { Modal, ModalBody } from "react-bootstrap";

import {
  MDBBtn,
  MDBContainer,
  MDBCard,
  MDBCardBody,
  MDBCardImage,
  MDBRow,
  MDBCol,
  MDBIcon,
  MDBInput,
  MDBModal,
  MDBModalBody,
  MDBModalHeader,
  MDBModalTitle,
  MDBModalFooter,
  MDBSwitch
}
from 'mdb-react-ui-kit';




let Korisnikk =[];
let obj;
function Logovanje(props)
{
    const{
        show,
        onHide
    }= props;
    const[username, setUserName]=useState("");
    const[password, setPassword]=useState("");
    



    async function login()
    {
      console.log(username, password);
        let item = {username, password};
        var heders = {};
      console.log(document.getElementById('flexSwitchCheckDefault2'));

      if ((document.getElementById('flexSwitchCheckDefault2')).checked)
      {
        console.log("CEKIRANO");
        let result = await fetch("https://localhost:44341/Dealer/LogInDealer/"+username+"/"+password,
        {method: 'GET',
        headers: {

            "Content-type": "application/json;charset=UTF-8",

        }});
        console.log(result);
        let data = await result.text();
        console.log(data);
        localStorage.setItem("dealer-info",data);
        window.location.href='dealer';
      }
      else{
        let result = await fetch("https://localhost:44341/User/LogIn/"+username+"/"+password,
        {method: 'GET',
        headers: {

            "Content-type": "application/json;charset=UTF-8",

        }});

        let data = await result.json();
        console.log(data);
        let userInfo = JSON.stringify(data);
        localStorage.setItem("user-info", userInfo);
        window.location.href='/';
     }
        
      //localStorage.setItem("user-info",JSON.stringify(data));
       
    }
  

    return(
        <Modal
          {...props}
       
          
        
        centered
      >
         
        <Modal.Header closeButton >
        
        </Modal.Header>
        <Modal.Body className="py-0">
        <MDBContainer className="py-0" >

      <MDBCard>
         <MDBRow className='g-0' >

   
             <MDBCardBody className='d-flex flex-column'>

              <div className='d-flex flex-row mt-2'>
                
                 <span className="h1 fw-bold mb-0 mt-3">Uloguj se!</span>
               </div>

               <h5 className="fw-normal my-4 pb-3" style={{letterSpacing: '1px'}}>Prijavi se na svoj profil</h5>

                 <MDBInput wrapperClass='mb-4' label='Korisničko ime' id='formControlLg' type='username' size="lg" onChange={(e)=>setUserName(e.target.value)}/>
                 <MDBInput wrapperClass='mb-4' label='Lozinka' id='formControlLg' type='password' size="lg"  onChange={(e)=>setPassword(e.target.value)}/>

                <div className='mb-4'>
                  <MDBSwitch id='flexSwitchCheckDefault2' label='Prijavi se kao dealer' />
                </div>

               <MDBBtn className="mb-4 px-5"  size='lg'  onClick={login}>Prijavi se</MDBBtn>
              
               <p className="mb-5 pb-lg-2" style={{color: '#393f81'}}>Nemas nalog? <a href="/registracija" style={{color: '#393f81'}}>Registruj se ovde.</a></p>

              

             </MDBCardBody>
         

         </MDBRow>
       </MDBCard>

     </MDBContainer>
        </Modal.Body>
      
  
        
        {/* <Form.Group style={{marginLeft:'40px', marginRight:'100px'}} >
                        <Button   style={{marginLeft:'40px', marginRight:'100px', border:'none'}}
                        onClick={login}>Prijavi se</Button>
                        
                        </Form.Group >
                        <Form.Group style={{marginRight:'150px'}}><a href ="/registracija" style={{color:"gray"}}>Nemaš nalog? Registruj se!</a>
                        
                        </Form.Group> */}
        
                        

                        
                   
        
      </Modal>
        
    );
    }
export default Logovanje;







