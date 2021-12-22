import * as jsonServer from 'json-server';
import * as fs from 'fs';
import db from './db.json'; 

const server = jsonServer.create();
const router = jsonServer.router('server/db.json');
const middlewares = jsonServer.defaults();

const PORT = 3000;

server.use(middlewares);
server.use(jsonServer.bodyParser);

server.use(router);

server.listen(PORT, () => console.log(`JSON server running at port ${PORT}.`));