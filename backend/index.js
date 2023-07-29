import express from "express";
import cors from "cors";
const app = express();
import morgan from 'morgan';
import {router} from "./routes/index.js";
import {PORT} from './config.js';

let port = process.env.PORT || PORT;

app.use(cors());
app.use(express.json());
app.use(morgan('tiny'));
app.use('/api', router);

app.listen(port);
console.log('BlackHole Server running on ' + port);