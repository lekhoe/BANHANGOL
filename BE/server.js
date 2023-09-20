require('dotenv').config();
const express = require('express');
const app = express();
const hostname = process.env.HOST_NAME;
const port = process.env.PORT;
const userController = require('./controllers/UserControllers');
const categoryController = require('./controllers/CategoryController');
const mongoDb = require('./connectionMG');


mongoDb();

//user 
app.use(express.json()); // Đặt middleware express.json() trước
app.use(express.urlencoded({ extended: true }));
app.use('/', userController);

//category
app.use('/', categoryController);

//product
// app.use('/', categoryController);

app.listen(port, hostname, () => {
    console.log(`Server running at http://${hostname}:${port}/`);
});
