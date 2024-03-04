import React from 'react';
import { Modal, Form, FormControl, Button, InputGroup, DropdownButton, Dropdown } from 'react-bootstrap';
import axios from 'axios';
import { useState, useEffect } from "react";
import { useParams } from 'react-router-dom';

const MakeCarTest = (props) => {
    const { show, onHide, CarID, } = props;

    
    const [testDate, settestDate] = useState('');

    const [dealer, setDealer] = useState('');



    let test = localStorage.getItem('user-info');
    let UserID = null;
    if (test) {
        test = JSON.parse(test);
        UserID = test.id;


    }

    useEffect(() => {

        axios.get("https://localhost:44341/Dealer/GetAllDealers")
            .then(res => {
                console.log(res)
                setDealer(res.data)
            })

            .catch(err => {
                console.log(err)
            })
    }, [])

    console.log(dealer);


    async function testdrive(testDate, UserID) {
        console.log(testDate);


        if ( !testDate || !UserID  ) {
            console.error("Nedostaju neke od obaveznih varijabli");
            return;
        }

        try {
            const response = await axios.post(`https://localhost:44341//​/TestDrive​/MakeTestDrive​/${testDate}/${CarID}/${props.Dealer}/${UserID}`);


            if (response.status !== 200) {
                console.error(`API odgovor nije uspeo: ${response.status}`);
                return;
            }

            const data = response.data;



            alert("Narudzbina je poslata dealeru");
            console.log(response);
        } catch (error) {
            console.error(error);
        }
    }







    return (
        <Modal {...props} size="lg" centered>
            <Modal.Header closeButton>
                <Modal.Title><strong>{props.name}</strong></Modal.Title>
            </Modal.Header>
            <Modal.Body>

                <Form>

                    <Form.Group>
                        <Form.Label><strong>Popunite </strong></Form.Label>
                    </Form.Group>
                    <Form.Group>
                        <Form.Label><strong>TestDate</strong></Form.Label>
                        <Form.Control type="date" value={testDate} onChange={e => settestDate(e.target.value)} />
                    </Form.Group>
                    



                    {<Button onClick={() => testdrive(testDate, UserID)}>Rezervisi</Button> }

                </Form>
            </Modal.Body>
        </Modal>
    )
}
export default MakeCarTest;