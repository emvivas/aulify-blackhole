import express from "express";
import {createPool} from 'mysql2/promise';
import HttpStatus from 'http-status-codes';
import {MYSQL_HOST, MYSQL_PORT, MYSQL_USER, MYSQL_PASSWORD, MYSQL_DATABASE} from '../config.js';

export const videogame = express.Router();

const pool = createPool({
  host: MYSQL_HOST,
  port: MYSQL_PORT,
  user: MYSQL_USER,
  password: MYSQL_PASSWORD,
  database: MYSQL_DATABASE,
  multipleStatements: true
});

  videogame.get('/', async (req, res) => {
    try{
    let [rows] = await pool.query("CALL getAvailableUserAchievements(?);", [req.body.userIdentificator]);
    res.set('Access-Control-Allow-Origin', '*');
    res.status(HttpStatus.OK).json(rows[0]);
    }catch(error){
      return res.status(500).json({message: error});
    }
  });
  
  videogame.post('/', async (req, res) => {
    try{
    let time = req.body.time.split(":");
    let [rows] = await pool.query("CALL registerGame(@id, ?, ?, ?, ?, ?); SELECT @id;", ["00:"+time[0]+":"+time[1], req.body.wrongAnswers_, req.body.mode, req.body.modulesCompleted_, req.body.userIdentificator]);
    let gameIdentificator = rows[1][0]["@id"];
    await req.body.achievements.forEach(achievement => pool.query("CALL registerUserAchievement(?, ?, ?)", [achievement, req.body.userIdentificator, gameIdentificator]));
    res.set('Access-Control-Allow-Origin', '*');
    res.sendStatus(HttpStatus.OK);
  }catch(error){
    return res.status(500).json({message: error});
  }
  });
  
  videogame.get('/stats', async (req, res) => {
    try{
    let [rows] = await pool.query("CALL getUserStatistics(?, 10);", [req.query.userIdentificator]);
    res.set('Access-Control-Allow-Origin', '*');
    res.status(HttpStatus.OK).json(rows);
  }catch(error){
    return res.status(500).json({message: error});
  }
  });

  videogame.get('/statistics', async (req, res) => {
    try{
    let [rows] = await pool.query("CALL getStatistics();");
    res.set('Access-Control-Allow-Origin', '*');
    res.status(HttpStatus.OK).json(rows);
  }catch(error){
    return res.status(500).json({message: error});
  }
  });

  videogame.post('/user', async (req, res) => {
      try{
      let [rows] = await pool.query("CALL registerUser(?, ?, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);", [req.body.userIdentificator, req.body.username]);
      res.set('Access-Control-Allow-Origin', '*');
      res.sendStatus(HttpStatus.OK);
    }catch(error){
      return res.sendStatus(HttpStatus.OK);
    }
    });