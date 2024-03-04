import React from 'react';
import './Komentari.css';
import { Modal } from 'react-bootstrap';
import { useState, useEffect } from "react";
import axios from 'axios';
import { useParams } from 'react-router-dom';

const Komentari = (props) => {
    const [username, setUsername] = useState('');
    const [text, setText] = useState('');
    const [comment, setComment] = useState();
    const [loading, setLoading] = useState(true);
    let { CarID } = useParams();

    const fetchData = async () => {
        try {
            const res = await  axios.get(`https://localhost:44341/Car/GetReviewsForCar/${CarID}`);
                setComment(res.data);
                setLoading(false)
            
        } 
        catch (err) {
            console.error(err);
        }
    };


    useEffect(() => {
        fetchData();
    }, [CarID]);

    
    if (loading) return <p>Loading...</p>;


    return (
        <Modal {...props} size="lg" centered>
            <Modal.Header closeButton>
                <Modal.Title><strong>{props.name} *KOMENTARI*</strong></Modal.Title>
            </Modal.Header>
            <Modal.Body>
  {comment.map(({ user: { username }, text }) => (
    <p>
      <strong>{username} :: </strong> 
      <span>{text}</span>
    </p>
  ))}
</Modal.Body>
        </Modal>
    )
};

export default Komentari;

