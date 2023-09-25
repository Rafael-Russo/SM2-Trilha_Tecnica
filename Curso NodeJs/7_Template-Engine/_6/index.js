const express = require('express');
const exphbs = require('express-handlebars');

const app = express();

app.engine('handlebars', exphbs.engine());
app.set('view engine', 'handlebars');

app.get('/dashboard', (req, res) =>{
    const itens = ['item1', 'item2', 'item3', 'item4', 'item5'];

    res.render('dashboard', {itens});
});

app.get('/', (req, res) => {
    const user = {
        name : "Rafael",
        surname: "Soares"
    }

    const palavra = 'Teste';

    const auth = true;

    const approved = true;

    res.render('home',{user: user, palavra, auth, approved});
});

app.listen(3000, () => {
    console.log('App funcionando!');
});