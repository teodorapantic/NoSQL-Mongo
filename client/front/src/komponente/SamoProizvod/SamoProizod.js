import React from "react";

import MakeCarRental from "../Narudzbina/MakeCarRental";
import MakeCarTest from "../Narudzbina/MakeCarTest";
import Recenzija from "../Recenzija/Recenzija";
import Komentari from "../Komentari/Komentari";
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
} from "mdb-react-ui-kit";
import { useState, useEffect } from "react";

import axios from "axios";
import { useParams } from 'react-router-dom';


function SamoProizvod() {

  const [car, setCar] = useState();
  const [order, setOrder] = useState("");
  const [order2, setOrder2] = useState("");

  const { CarID } = useParams();


  const [loading, setLoading] = useState(true);

  // const [following, setFollowing] = useState(localStorage.getItem(`following-${CarID}`) || false);
  const [user_info, setUserinfo] = useState(JSON.parse(localStorage.getItem('user-info')));

  const [showModal, setShowModal] = useState(false);
  const [showModal2, setShowModal2] = useState(false);

  const handleOpenModal = () => {
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
  };
  function ordershow() {
    setOrder(true);
  }
  function orderhide() {
    setOrder(false);
  }
  function ordershow2() {
    setOrder2(true);
  }
  function orderhide2() {
    setOrder2(false);
  }



  const handleOpenModal2 = () => {
    setShowModal2(true);
  };

  const handleCloseModal2 = () => {
    setShowModal2(false);
  };

  // let test = localStorage.getItem('user-info');
  // let IDUser = null;
  // if (test) {
  //   test = JSON.parse(test);
  //   IDUser = test.returnID;
  // }

  // const Follow = async () => {
  //   setFollowing(true);
  //   localStorage.setItem(`following-${CarID}`, true);
  //   try {
  //     const response = await axios.put(`https://localhost:44332/User/FollowProduct/${IDUser}/${CarID}`);
  //     console.log(response.data);
  //     alert(`Uspesno ste zapratili -${car.nameProduct}`);
  //   } catch (error) {
  //     console.error(error);
  //   }
  // };

  // const Unfollow = async () => {
  //   setFollowing(false);
  //   localStorage.removeItem(`following-${CarID}`);
  //   try {
  //     const response = await axios.delete(`https://localhost:44332/User/UnFollowProduct/${IDUser}/${CarID}`);
  //     console.log(response.data);
  //     alert("Uspeno ste odpratili proizovd");
  //   } catch (error) {
  //     console.error(error);
  //   }
  // };


  const fetchData = async () => {
    try {
      const res = await axios.get(`https://localhost:44341/Car/GetMoreDetails/${CarID}`);
      setCar(res.data);
      setLoading(false);
    } catch (err) {
      console.error(err);
    }
  };

  useEffect(() => {
    fetchData();
  }, [CarID]);

  console.log(car)
  if (loading) return <p>Loading...</p>;

  return (

    <MDBContainer fluid>
      <MDBRow className="justify-content-center mb-0">
        <MDBCol md="12" xl="10">
          <MDBCard className="shadow-0 border rounded-3 mt-5 mb-3">
            <MDBCardBody>
              <MDBRow>
                <MDBCol md="12" lg="3" className="mb-4 mb-lg-0">
                  <MDBRipple
                    rippleColor="light"
                    rippleTag="div"
                    className="bg-image rounded hover-zoom hover-overlay"
                  >
                    <MDBCardImage
                      src={"https://localhost:44341/CarsPictures/" + car.pictureProduct}
                      fluid
                      className="w-100"
                    />
                    <a href="#!">
                      <div
                        className="mask"
                        style={{ backgroundColor: "rgba(251, 251, 251, 0.15)" }}
                      ></div>
                    </a>
                  </MDBRipple>
                </MDBCol>
                <MDBCol md="6">
                  <h5>{car.mark.name} - {car.carModel.name} </h5>
                  <div className="d-flex flex-row">

                  </div>
                  <div className="mt-1 mb-0 text-muted small">
                    <span>origin:</span>
                    <span className="text-primary"> • </span>
                    <span>{car.mark.origin}</span>
                    {/* <span className="text-primary">  </span>
                    <span>Description:</span>
                    <span className="text-primary"> • </span>
                    <span>{car.description}</span>
                    <span className="text-primary">  </span> */}
                  </div>

                  <div className="mt-1 mb-0 text-muted small">
                    <span>year:</span>
                    <span className="text-primary"> • </span>
                    <span>  {car.year}</span>

                  </div>

                  <div className="mt-1 mb-0 text-muted small">
                    <span>price:</span>
                    <span className="text-primary"> • </span>
                    <span>  {car.price} € </span>

                  </div>



                  <div className="mt-1 mb-0 text-muted small">
                    <span>color:</span>
                    <span className="text-primary"> • </span>
                    <span> {car.exteriorColor}</span>
                  </div>

                  <div className="mt-1 mb-0 text-muted small">
                    <span>engineType:</span>
                    <span className="text-primary"> • </span>
                    <span> {car.engineType.fuelType}</span>
                    <span className="text-primary"> • </span>
                    <span>displacement - {car.engineType.displacement}</span>
                    <span className="text-primary"> • </span>
                    <span>power - {car.engineType.power}</span>
                  </div>



                </MDBCol>


                <MDBCol md="6" lg="3" className="border-sm-start-none border-start">
                  <div>
                    <div className="mt-1 mb-0 text-muted small">
                      <span>Reviews</span>
                      <span className="text-primary"> • </span>

                    </div>

                    <div>
                      <MDBBtn color="primary" size="sm" onClick={handleOpenModal2}>Prikazi Recenzije</MDBBtn>
                      <Komentari show={showModal2} onHide={handleCloseModal2} name={car.carModel.name} CarID={car.CarID} />
                    </div>


                    {user_info ? (<> <div>
                      <MDBBtn color="primary" size="sm" onClick={handleOpenModal}>Dodaj recenziju</MDBBtn>
                      <Recenzija show={showModal} onHide={handleCloseModal} name={car.carModel.name} CarID={car.CarID} />
                    </div></>) :
                      (<></>)}

                  


                  </div>




                  {user_info ? (<>

                    {car.rentOrSale ? <MDBBtn color="primary" size="sm" onClick={ordershow}>
                      "Make a Rent"
                    </MDBBtn> : <MDBBtn color="primary" size="sm" onClick={ordershow2}>
                      "Make a Test Drive"
                    </MDBBtn>}
                    

                    <MakeCarRental
                      show={order}
                      onHide={orderhide}
                      CarID={car.CarID}
                      Dealer={car.Dealer}
                    >
                    </MakeCarRental>

                    <MakeCarTest
                    show={order2}
                    onHide={orderhide2}
                    CarID={car.CarID}
                    Dealer={car.Dealer}></MakeCarTest>

                  </>)

                    : (<></>)}


                </MDBCol>

              </MDBRow>
            </MDBCardBody>
          </MDBCard>
        </MDBCol>

      </MDBRow>


    </MDBContainer>
  )
}



export default SamoProizvod;
