import React, { useState, useEffect } from 'react'
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Table from 'react-bootstrap/Table';
import {getStatisticsRequest} from './api/dashboard.js';

let result;
getData();

async function getData(){
  result = (await getStatisticsRequest({userIdentificator: localStorage.getItem('userIdentificator')})).data;
}

export const Friends = () => {
  // You'll need to fetch real data for these tables and replace the sample data

  const achievements = result ? result[7] : [];

  const mostGamesPlayed = result ? result[2] : [];

  const mostGamesWon = result ? result[1] : [];

  const leastMistakes = result ? result[3] : [];

  const highScores = result ? result[0] : [];

  const personalStats = [
    { stat: 'Partidas jugadas', value: result && result[4][0] ?  result[4][0].Partidas : 0 },
    { stat: 'Partidas ganadas', value: result && result[5][0] ? result[5][0].Partidas : 0 },
    { stat: 'Mejor tiempo', value: result && result[6][0] ? (result[6][0].Tiempo + "  (" + result[6][0].Modalidad + ").") : "NA" },
    { stat: 'Última partida jugada', value: result && result[4][0] ? result[4][0].Fecha : "NA" },
    { stat: 'Equivocaciones', value: result && result[8][0] ? result[8][0].Equivocaciones : 0 },
    { stat: 'Logros', value: achievements.length + " / 12" },
  ];

  return (
    <div>
      <Container>
        <Row>
          <h1 style={{ marginTop: '60px', marginBottom: '10px' }}>Estas son tus estadísticas, {localStorage.getItem('username')}</h1>
          <Col sm={6}>
            <h3 style={{ marginTop: '60px', marginBottom: '20px' }}>Estadísticas personales</h3>
            <Table striped bordered hover size="sm" style={{ marginTop: '15px', marginBottom: '20px' }}>
              <tbody>
                {personalStats.map((stat, index) => (
                  <tr key={index} style={{ backgroundColor: "#FFFFFF" }}>
                    <td style={{ fontWeight: "bold" }}>{stat.stat}</td>
                    <td>{stat.value}</td>
                  </tr>
                ))}
              </tbody>
            </Table>
          </Col>
          <Col sm={6}>
          <h3 style={{ marginTop: '60px', marginBottom: '20px' }}>Logros</h3>
          <div style={{ maxHeight: '200px', overflowY: 'auto', marginTop: '15px', marginBottom: '20px' }}>
            <Table striped bordered hover size="sm" style={{ backgroundColor: '#FFFFFF' }}>
              <thead>
                <tr style={{ backgroundColor: "#1c5fb0", color: "#FFFFFF" }}>
                  <th>Insignia</th>
                  <th>Descripción</th>
                </tr>
              </thead>
              <tbody>
                {achievements.length > 0 ? achievements.map((achievement) => (
                  <tr key={achievement.id} style={{ backgroundColor: "#FFFFFF"}}>
                    <td>{achievement.Logro}</td>
                    <td>{achievement.Descripcion}</td>
                  </tr>
                )):
                 
                  <tr key={1} style={{ backgroundColor: "#FFFFFF"}}>
                    <td>{"No has conseguido ningún logro"}</td>
                    <td>{"¡Venga! Juega y consíguelos"}</td>
                  </tr>
                }
              </tbody>
            </Table>
          </div>
        </Col>
        <h1 style={{ marginTop: '60px', marginBottom: '10px' }}>Estadísticas globales</h1>
        <Col sm={6}>
          <h3 style={{ marginTop: '60px', marginBottom: '20px' }}>Más partidas jugadas</h3>
          <Table striped bordered hover size="sm" style={{ marginBottom: '20px' }}>
              <thead>
                <tr style={{ backgroundColor: "#1c5fb0", color: "#FFFFFF" }}>
                  <th>Jugador</th>
                  <th>Partidas</th>
                  <th>Fecha</th>
                </tr>
              </thead>
              <tbody>
                {mostGamesPlayed.map((player, index) => (
                  <tr key={index} style={{ backgroundColor: "#FFFFFF"}}>
                    <td>{player.Jugador}</td>
                    <td>{player.Partidas}</td>
                    <td>{player.Fecha}</td>
                  </tr>
                ))}
              </tbody>
            </Table>
            <h3 style={{ marginTop: '60px', marginBottom: '20px' }}>Menores equivocaciones</h3>
          <Table striped bordered hover size="sm" style={{ marginBottom: '20px' }}>
              <thead>
                <tr style={{ backgroundColor: "#1c5fb0", color: "#FFFFFF" }}>
                  <th>Jugador</th>
                  <th>Equivocaciones</th>
                  <th>Fecha</th>
                </tr>
              </thead>
              <tbody>
                {leastMistakes.map((player, index) => (
                  <tr key={index} style={{ backgroundColor: "#FFFFFF"}}>
                    <td>{player.Jugador}</td>
                    <td>{player.Equivocaciones}</td>
                    <td>{player.Fecha}</td>
                  </tr>
                ))}
              </tbody>
            </Table>
          </Col>
          <Col sm={6}>
          <h3 style={{ marginTop: '60px', marginBottom: '20px' }}>Más partidas ganadas</h3>
          <Table striped bordered hover size="sm" style={{ marginBottom: '20px' }}>
              <thead>
                <tr style={{ backgroundColor: "#1c5fb0", color: "#FFFFFF" }}>
                  <th>Jugador</th>
                  <th>Victorias</th>
                  <th>Fecha</th>
                </tr>
              </thead>
              <tbody>
                {mostGamesWon.map((player, index) => (
                  <tr key={index} style={{ backgroundColor: "#FFFFFF"}}>
                    <td>{player.Jugador}</td>
                    <td>{player.Victorias}</td>
                    <td>{player.Fecha}</td>
                  </tr>
                ))}
              </tbody>
            </Table>
            <h3 style={{ marginTop: '60px', marginBottom: '20px' }}>Partidas con mejor tiempo</h3>
          <Table striped bordered hover size="sm" style={{ marginBottom: '20px' }}>
              <thead>
                <tr style={{ backgroundColor: "#1c5fb0", color: "#FFFFFF" }}>
                  <th>Jugador</th>
                  <th>Tiempo</th>
                  <th>Modalidad</th>
                  <th>Fecha</th>
                </tr>
              </thead>
              <tbody>
                {highScores.map((player, index) => (
                  <tr key={index} style={{ backgroundColor: "#FFFFFF"}}>
                    <td>{player.Jugador}</td>
                    <td>{player.Tiempo}</td>
                    <td>{player.Modalidad}</td>
                    <td>{player.Fecha}</td>
                  </tr>
                ))}
              </tbody>
            </Table>
          </Col>
        </Row>
      </Container>
    </div>
  )
}