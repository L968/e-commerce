require('dotenv').config();
require('express-async-errors');
const express = require('express');
const cors = require('cors');
const requireDir = require('require-dir');
requireDir('./models');
const logger = require('./logs/logger');
const routes = require('./routes');
const mongoose = require('mongoose');

const app = express();

mongoose.connect(process.env.CONNECTION_STRING, {
    useNewUrlParser: true,
    useUnifiedTopology: true
});

app.use(cors());
app.use(express.json());
app.use(routes);

app.use((error, request, response, next) => {
    console.log('\x1b[31m%s\x1b[0m', error);
    logger.error(error);

    return response.status(500).json({ message: 'An unexpected error has occured, please try again later' });
});

module.exports = app;