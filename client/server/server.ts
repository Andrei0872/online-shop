import * as jsonServer from 'json-server';
import * as fs from 'fs';
import db from './db.json'; 

const server = jsonServer.create();
const router = jsonServer.router('server/db.json');
const middlewares = jsonServer.defaults();

const PORT = 3000;

server.use(middlewares);
server.use(jsonServer.bodyParser);

server.get('/orders/:id', (req, res) => {
  const orderId = req.params.id;
  const products = db.products.slice(0, 5).map(p => ({ ...p, quantity: Math.random() > 0.5 ? 1 : 2 }));

  const data = {
    id: orderId,
    products,
  };

  res.json({ data });
});

server.use(router);

server.listen(PORT, () => console.log(`JSON server running at port ${PORT}.`));