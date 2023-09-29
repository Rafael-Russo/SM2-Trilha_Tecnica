const express = require('express');
const app = express();

app.use(
    express.urlencoded({
        urlencoded: true
    })
)
app.use(express.json());

//rotas - endpoints
app.post('/createproduct', (req, res) => {
    const {name, price} = req.body;

    if(!name || !price){
        res.status(422).json({message: 'Os campos são obrigatórios'});
        return;
    }

    console.log(name);
    console.log(price);

    res.status(201).json({message: `Produto ${name} criado com sucesso!`});
});
app.get('/', (req, res) => {

    res.status(200).json({
        message: "Primeira rota criada com sucesso!"
    });
});

app.listen(3000);