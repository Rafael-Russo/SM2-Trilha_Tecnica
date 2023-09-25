const express = require('express');
const exphbs = require('express-handlebars');
const pool = require('./db/conn');

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

    const sql = `INSERT INTO books (??, ??) VALUES (?, ?)`;
    const data = ['title', 'pageqtd', title, pageqtd];

    pool.query(sql, data, function (err) {
        if (err) {
            console.log(err);
            return;
        }

        res.redirect('/books');
    });
});

app.get('/books', (req, res) => {
    const sql = `SELECT * FROM books`;

    pool.query(sql, function (err, data) {
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
    const sql = `SELECT * FROM books WHERE ?? = ?`
    const data = ['idbooks', id]

    pool.query(sql, data, function (err, data) {
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
    const sql = `SELECT * FROM books WHERE ?? = ?`;
    const data = ['idbooks', id];

    pool.query(sql, data, function (err, data) {
        if (err) {
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

    const sql = `UPDATE books SET ?? = ?, ?? = ? WHERE ?? = ?`;
    const data = ['title', title, 'pageqtd', pageqtd, 'idbooks', id];

    pool.query(sql, data, function (err) {
        if (err) {
            console.log(err);
            return;
        }

        res.redirect('/books');
    });
});

app.post('/book/delete/:id', (req, res) => {
    const id = req.params.id;
    const sql = `DELETE FROM books WHERE ?? = ?`;
    const data = ['idbooks', id]

    pool.query(sql, data, function (err) {
        if (err) {
            console.log(err);
            return;
        }

        res.redirect('/books');
    })
});

app.listen(3000);