const express = require('express');
const exphbs = require('express-handlebars');

const app = express();

const hbs = exphbs.create({
    partialsDir: ['views/partials']
});

app.engine('handlebars', hbs.engine());
app.set('view engine', 'handlebars');

app.use(express.static('public'));

app.get('/dashboard', (req, res) =>{
    const itens = ['item1', 'item2', 'item3', 'item4', 'item5'];

    res.render('dashboard', {itens});
});

app.get('/post', (req, res) => {
    const post = {
        title: 'Aprender Node.js',
        category: 'JavaScript',
        body: 'Esse artigo vai te ajudar a aprender Node.js...',
        comments: 4
    };

    res.render('blogpost', {post})
});

app.get('/blog', (req, res) => {
    const posts = [
        {
            title: 'Aprender Node.js',
            category: 'JavaScript',
            body: 'teste',
            comments: 4
        },
        {
            title: 'Aprender PHP',
            category: 'PHP',
            body: 'teste',
            comments: 4
        },
        {
            title: 'Aprender C#',
            category: 'C#',
            body: 'teste',
            comments: 4
        },
        {
            title: 'Aprender Python',
            category: 'Python',
            body: 'teste',
            comments: 4
        },
    ]

    res.render('blog', {posts})
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