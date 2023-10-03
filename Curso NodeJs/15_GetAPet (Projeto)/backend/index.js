const express = require('express');
const cors = require('cors');

const app = express();

//config JSON
app.use(express.json());

//solve CORS
app.use(cors({
    origin: ['http://127.0.0.1:3000', 'http://localhost:3000']
}));

//public folder for images
app.use(express.static('public'));

//routes
const UserRoutes = require('./routes/UserRoutes');
app.use('/users', UserRoutes);

const PetRoutes = require('./routes/PetRoutes');
app.use('/pets', PetRoutes);

app.listen(5000);