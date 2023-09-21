const fs = require('fs');

fs.readFile('arquivo.txt', 'utf-8', (err, data) => {
    if(err){
        console.log('erro: ' + err);
        return;
    }

    console.log(data);
});