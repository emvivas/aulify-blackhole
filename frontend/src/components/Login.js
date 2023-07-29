import React, { useRef, useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import Container from 'react-bootstrap/Container';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';
import './Login.css';
import icon from '../assets/exclamation-octagon-fill.svg'
import { InformativeModal } from './InformativeModal';

const customButtonStyle = {
  backgroundColor: '#1766b7',
  color: 'white',
  border: 'none',
  width: '200px',
  height: '50px',
  marginTop: '16px',
  borderRadius: '17px',
};

const navbarStyle = {
  display: 'flex',
  justifyContent: 'space-between',
  alignItems: 'center',
  width: 'auto',
  height: '175px',
  backgroundImage: 'url(https://res.cloudinary.com/dt5oxu5y7/image/upload/v1681950278/fondo-degradado-1-1_birdnz.png)',
  backgroundSize: 'cover',
  borderRadius: '0 0 25px 25px',
  padding: '0 20px',
}

const topLeftImageStyle = {
  width: '175px',
  height: 'auto',
}

const bottomRightImageStyle = {
  alignSelf: 'flex-end',
  width: '175px',
  height: 'auto',
}

const containerStyle = {
  width: '100%',
  padding: '0',
  overflowX: 'hidden',
}

const customForm = {
 color: '#135fb6',
};

export const Login = () => {
  const form = useRef();
  const navigate = useNavigate();
  const [modalState, setModalState] = useState(false);

  const envioDatos = async (event) => {
    event.preventDefault();
    //La constante contiene los datos del formulario
    //usuario y password en formato de objeto
    const formData = new FormData(form.current);
    //const uri = "https://zxdy2p6fv6.execute-api.us-east-1.amazonaws.com/dev/api-login";
    //const uri = "https://046kuhhu7i.execute-api.us-east-1.amazonaws.com/login";
    //API Hugo
    //const uri = "http://apitc2005b23-env.eba-hipqm9w8.us-east-1.elasticbeanstalk.com/login";
    //API Vivas
    //const uri = "http://blackholeapi.us-east-1.elasticbeanstalk.com/api/videogame/statistics";
    //API Plant
    //const uri = "https://jgvuug8gmj.execute-api.us-east-1.amazonaws.com/login";
    const uri = "https://0vnx9rbox9.execute-api.us-east-1.amazonaws.com/login";
    const uriVideogame = "https://0vnx9rbox9.execute-api.us-east-1.amazonaws.com/api/videogame/user";

    //const uri = "http://localhost:3002/login";
    //const r = fetch(uri, { method: "GET", headers: { "x-access-token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MSwidXNlciI6Ikh1Z28iLCJpYXQiOjE2Nzg5MjY3MDgsImV4cCI6MTY3ODkzMzkwOH0.fag66yi7I8Wkp9QgsmpOJosZGc0ekZw2vWkFXDCEwk8" } });
    //console.log(r)
    const response = await fetch(uri, { headers: { 'Content-Type': 'application/json'}, 
                                        method: 'POST',
                                        body: JSON.stringify(
                                        { 
                                          username: document.getElementById("username").value,
                                          password: document.getElementById("password").value
                                        })
                                      });                          
    //const response = await fetch(uri,{method: 'POST', body: formData});                                  
    // data contiene el mensaje y token devueltos por el api
    const data = await response.json();
    if(data.token !== ""){
      //Inicia sesión
      localStorage.setItem('token', data.token);
      localStorage.setItem('userIdentificator', data.idUser);
      localStorage.setItem('username', document.getElementById("username").value);
      await fetch(uriVideogame, { headers: { 'Content-Type': 'application/json'}, 
                                        method: 'POST',
                                        body: JSON.stringify(
                                        { 
                                          userIdentificator: localStorage.getItem('userIdentificator'),
                                          username: localStorage.getItem('username')
                                        })
                                      });
      
      //sessionStorage.setItem('token', data.token);
      /*let fecha = new Date();
      fecha.setTime(fecha.getTime() + (3600*1000));
      document.cookie = `token=${data.token}; expires=${fecha.toUTCString()}`;*/
      navigate('/home');
    }else{
      //Indicar que el usuario no existe o los datos incorrectos
      //alert("Datos incorrectos");
      setModalState(true)
    }
  }

  return (
    <div style={containerStyle}>
      <div style={navbarStyle}>
          <img
            src="https://res.cloudinary.com/dt5oxu5y7/image/upload/v1681951983/mocks-display-aulify-18-1_kt8wwq.png"
            alt=""
            style={topLeftImageStyle}
          />
          <img
          src="https://res.cloudinary.com/dt5oxu5y7/image/upload/v1681951993/mocks-display-aulify-19-1_pjvoit.png"
          alt=""
          style={bottomRightImageStyle}
          />
      </div>
      <Row>
        <Col sm={4}></Col>
        <Col sm={4}>
          <div style={{display: 'flex', flexDirection: 'column', alignItems: 'center'}}>
            <img src="https://res.cloudinary.com/dt5oxu5y7/image/upload/v1681941763/logo_gkh03a.png"
                  width="150px" alt='' style={{ marginTop: '150px'}}/>
          </div>
          <Form ref={form} onSubmit={envioDatos}>
              <Form.Group className="mb-2">
                  <Form.Label htmlFor="username">Usuario</Form.Label>
                  <Form.Control type="text" placeholder="Ejemplo" id="username" name="username" style={customForm}/>
              </Form.Group>
              <Form.Group className="mb-2">
                  <Form.Label htmlFor="password">Contraseña</Form.Label>
                  <Form.Control type="password" placeholder="***********"id="password" name="password" style={customForm}/>
              </Form.Group>
              <div className="ctr">
                <a href="https://main.drvblo32vuwsq.amplifyapp.com/forgotPassword" style={{ marginTop: '8px'}}>Recuperar contraseña</a>
                <Button type="submit" style={customButtonStyle}>Entrar</Button>
              </div>
          </Form>
          <InformativeModal title='Error'
                            icon={icon}
                            message=' El usuario o contraseña es incorrecto'
                            show={modalState}
                            onHide={() => setModalState(false)}/> 
        </Col>
        <Col sm={4}></Col>
      </Row>
    </div>
  )
}
