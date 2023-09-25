const express = require('express');
const exphbs = require('express-handlebars');
const mysql = require('mysql');

const app = express();

app.use(
    express.urlencoded({
        extended: true
    })
);
app.use(express.json());

app.engine('handlebars', exphbs.engine());
app.set('view engine', 'handlebars');

app.use(express.static('public'));

app.get('/', (req, res) => {
    res.render('home');
});

app.post('/books/insert', (req, res) => {
    const title = req.body.title;
    const pageqtd = req.body.pageqtd;

    const sql = `INSERT INTO books (title, pageqtd) VALUES ('${title}', ${pageqtd})`;

    conn.query(sql, function (err) {
        if (err) {
            console.log(err);
            return;
        }

        res.redirect('/books');
    });
});

app.get('/books', (req, res) => {
    const sql = `SELECT * FROM books`;

    conn.query(sql, function (err, data) {
        if (err) {
            console.log(err);
            return;
        }

        const books = data;

        console.log(books);

        res.render('books', { books })
    });
});

app.get('/book/:id', (req, res) => {
    const id = req.params.id;
    const sql = `SELECT * FROM books WHERE idbooks = ${id}`

    conn.query(sql, function (err, data) {
        if (err) {
            console.log(err);
            return;
        }

        const book = data[0];

        console.log(book);

        res.render('book', { book })
    });
});

app.get('/book/edit/:id', (req, res) => {
    const id = req.params.id;
    const sql = `SELECT * FROM books WHERE idbooks = ${id}`;

    conn.query(sql, function(err, data){
        if(err){
            console.log(err);
            return;
        }

        const book = data[0];

        res.render('editbook', { book })
    })
});

app.post('/book/update', (req, res) => {
    const id = req.body.idbooks;
    const title = req.body.title;
    const pageqtd = req.body.pageqtd;

    const sql = `UPDATE books SET title = '${title}', pageqtd = ${pageqtd} WHERE idbooks = ${id}`;
    conn.query(sql, function(err){
        if(err){
            console.log(err);
            return;
        }

        res.redirect('/books');
    });
})

const conn = mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: '',
    database: 'nodemysql'
});

conn.connect(function (err) {
    if (err) {
        console.log(err);
        return;
    }

    console.log('Conectou ao BD!');

    app.listen(3000);
})