import React, { useRef } from 'react';
import { useNavigate } from 'react-router-dom';
import Container from 'react-bootstrap/Container';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';
import './Login.css';

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
  flexDirection: 'row',
  justifyContent: 'space-between',
  alignItems: 'center',
  width: 'auto',
  height: '175px',
  backgroundImage: 'url(https://res.cloudinary.com/dt5oxu5y7/image/upload/v1682089934/elementos-y-logotipo-02-1_sei4nx.png)',
  backgroundSize: 'cover',
  borderRadius: '0 0 25px 25px',
  padding: '0 20px',
  position: 'relative',
}

const bottomRightImageStyle = {
  alignSelf: 'flex-end',
  width: '200px',
  height: 'auto',
}

const bottomLeftImageStyle = {
  width: '250px',
  height: 'auto',
  position: 'absolute',
  left: '0px',
  bottom: '0px',
}

const containerStyle = {
  width: '100%',
  padding: '0',
  overflowX: 'hidden',
}

const customForm = {
  color: '#135fb6',
 };

export const PasswordRecovery = () => {
  const navigate = useNavigate();

  const redirectToHome = () => {
    navigate('/home');
  };
  const form = useRef();
  const recuperarContraseña = async (event) => {
    event.preventDefault();
    const formData = new FormData(form.current)

    const uri = "http://localhost:3001/login/recover"
    const response = await fetch(uri, {
        method: 'POST',
        body: formData
    })
    const data = response.text();
  }

  return (
    <div style={containerStyle}>
      <div style={navbarStyle}>
        <div style={{ display: 'flex', alignItems: 'flex-end' }}>
          <img
            src="https://res.cloudinary.com/dt5oxu5y7/image/upload/v1682089958/mocks-display-aulify-23-1_udhfu0.png"
            alt=""
            style={bottomLeftImageStyle}
          />
       </div>
        <div style={{ display: 'flex', alignItems: 'flex-end' }}>
          <img
            src="https://res.cloudinary.com/dt5oxu5y7/image/upload/v1682093608/mocks-display-aulify-20-1_zerycx.png"
            alt=""
            style={bottomRightImageStyle}
          />
        </div>
      </div>
      <Row>
        <Col sm={4}></Col>
        <Col sm={4}>
        <div onClick={redirectToHome} style={{display: 'flex', flexDirection: 'column', alignItems: 'center'}}>
          <img src="https://res.cloudinary.com/dt5oxu5y7/image/upload/v1681941763/logo_gkh03a.png"
          width="150px" alt='' style={{ marginTop: '200px'}}/>
        </div>
          <Form ref={form} onSubmit={recuperarContraseña}>
              <Form.Group className="mb-2">
                  <Form.Label htmlFor="email">Usuario</Form.Label>
                  <Form.Control type="email" id="email" placeholder="ejemplo@gmail.com"name="email" style={customForm}/>
              </Form.Group>
              <div className="ctr">
                <Button type="submit" style={customButtonStyle}>Recuperar contraseña</Button>
              </div>
          </Form>
        </Col>
        <Col sm={4}></Col>
      </Row>
    </div>
  )
}
