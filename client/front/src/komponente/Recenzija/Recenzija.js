import React from 'react';
import { Modal, Form, FormControl, Button, InputGroup, DropdownButton, Dropdown } from 'react-bootstrap';
import { MDBBtn } from 'mdb-react-ui-kit';
import { MDBIcon } from 'mdb-react-ui-kit';
import { useState, useEffect } from "react";
import { useParams } from 'react-router-dom';
import axios from 'axios';




const Recenzija = (props) => {
    const [text, setText] = useState('');
   
    const [carID, setCarId] = useState('');
    let { CarID } = useParams();

    const [errorMessage, setErrorMessage] = useState('');
    let test = localStorage.getItem('user-info');
    let userId = null;

    if (test) {
        test = JSON.parse(test);
        userId = test.id;
     
    }
    const addComment = async() => {
        if (!text) {
            setErrorMessage('Molim Vas unesite komentar.');
            return;
          }
      
      

        try {
            const response = await axios.post(`https://localhost:44341/Car/AddReview/${userId}/${CarID}/${text}`);


            if (response.status !== 200) {
                console.error(`API odgovor nije uspeo: ${response.status}`);
                return;
            }

            const data = response.data;
            alert("Dodali ste recenziju");
            console.log(data);
        } catch (error) {
            console.error(error);
        }
    }

    return (
        <Modal {...props} size="lg" centered>
            <Modal.Header closeButton>
                <Modal.Title><strong>{props.name} *RECENZIJE*</strong></Modal.Title>
            </Modal.Header>
            <Modal.Body>
            {errorMessage && <div style={{ color: 'red' }}>{errorMessage}</div>}
                <Form>
                    <Form.Group>
                        <Form.Label><strong>komentar:</strong></Form.Label>
                        <Form.Control as="textarea" style={{ height: '100%' }} placeholder="Unesite neku recenziju"
                            onChange={e => setText(e.target.value)} />

                        <Form.Group className="d-flex">

                        </Form.Group>
                    </Form.Group>
                    <Form.Group>
                        <Button color="primary" onClick={addComment}>Dodaj komentar</Button>
                    </Form.Group>
                </Form>
            </Modal.Body>
        </Modal>
        
       
    )
};

export default Recenzija;
