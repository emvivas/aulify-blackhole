import express from "express";
export var router = express.Router();
import {videogame} from './videogame.js';

  router.use('/videogame', videogame);

  router.get('/', function (req, res) {
    res.status(200).json({ message: 'Welcome to BlackHole API' });
  })