const express = require('express');
const app = express();
const port = 3000;

const path = require('path');

//ler body
app.use(
    express.urlencoded({
        extended: true
    })
)
app.use(express.json);

const basePath = path.join(__dirname, 'templates');

app.get('/users/create', (req, res) => {
    res.sendFile(`${basePath}/usersform.html`);
});

app.post('/users/save', (req, res) =>{
    console.log(req.body);

    const name = req.body.name;
    const age = req.body.age;

    console.log(`O nome do usuário é ${name} e ele tem ${age} anos`);

    res.sendFile(`${basePath}/usersform.html`);
});

app.get('/users/:id', (req, res) => {
    const id = req.params.id;
    
    console.log(`Estamos buscando o usuário: ${id}`);

    res.sendFile(`${basePath}/users.html`);
});

app.get('/', (req, res) => {
    res.sendFile(`${basePath}/index.html`);
});

app.listen(port, () => {
    console.log(`App rodando na porta: ${port}`);
});
