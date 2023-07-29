import React, { useState, useEffect } from 'react';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Table from 'react-bootstrap/Table';
import FormControl from 'react-bootstrap/FormControl';
import Button from 'react-bootstrap/Button';
import * as XLSX from 'xlsx';
import {getTotalStatisticsRequest} from './api/dashboard.js';

let result;
getData();

async function getData(){
  result = (await getTotalStatisticsRequest()).data;
}

export const Stats = () => {
  const [players, setPlayers] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');

  useEffect( () => {
    const data = result[0];
    data.sort((a, b) => a.Jugador.localeCompare(b.Jugador));
    setPlayers(data);
  }, []);

  const filteredData = players.filter((player) =>
    player.Jugador.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const exportToExcel = () => {
    const ws = XLSX.utils.json_to_sheet(filteredData);
    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Player stats');
    XLSX.writeFile(wb, 'player-stats.xlsx');
  };

  return (
    <div>
      <Container fluid>
        <Row>
          <Col>
          <h1 style={{ marginTop: '60px', marginBottom: '50px' }}>Estadísticas generales</h1>
          </Col>
          <Col md={{ span: 4, offset: 4 }} style={{  display: 'flex', justifyContent: 'flex-end' }}>
            <Button
              variant="primary"
              onClick={exportToExcel}
              style={{ marginRight: '8px', marginTop: '60px', marginBottom: '50px', height: '48px', width: '140px' }}
            >Exportar</Button>
            <FormControl
              placeholder="Buscar"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              style={{ marginBottom: '50px', marginTop: '60px'}}
            />
          </Col>
        </Row>
        <Row>
          <Col>
            <div style={{ height: 'calc(100vh - 236px)', overflowY: 'auto' }}>
              <Table striped bordered hover>
                <thead>
                  <tr style={{ backgroundColor: "#1c5fb0", color: "#FFFFFF" }}>
                    <th>Jugador</th>
                    <th>Partidas totales</th>
                    <th>Victorias</th>
                    <th>Derrotas</th>
                    <th>Equivocaciones</th>
                    <th>Partidas sin guía</th>
                    <th>Partidas con guía</th>
                    <th>Mejor tiempo</th>
                    <th>Logros obtenidos</th>
                    <th>Última partida jugada</th>
                  </tr>
                </thead>
                <tbody>
                  {filteredData.map((player, index) => (
                    <tr key={index}>
                      <td>{player.Jugador}</td>
                      <td>{player.Partidas}</td>
                      <td>{player.Victorias}</td>
                      <td>{player.Derrotas}</td>
                      <td>{player.Equivocaciones}</td>
                      <td>{player.SinGuia}</td>
                      <td>{player.ConGuia}</td>
                      <td>{player.MejorTiempo}</td>
                      <td>{player.Logros}</td>
                      <td>{player.UltimaPartida}</td>
                    </tr>
                  ))}
                </tbody>
              </Table>
            </div>
          </Col>
        </Row>
      </Container>
    </div>
  );
};
