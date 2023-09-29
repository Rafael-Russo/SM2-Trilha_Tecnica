const express = require('express');
const cors = require('cors');

const app = express();

//config JSON
app.use(express.json());

//solve CORS
app.use(cors({
    credentials: true,
    origin: 'http://127.0.0.1:3000'
}));

//public folder for images
app.use(express.static('public'));

//routes
const userRoutes = require('./routes/UserRoutes');
app.use('/users', userRoutes);

app.listen(5000);