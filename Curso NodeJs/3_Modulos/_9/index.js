const fs = require('fs');

fs.rename('arquvio.txt', 'novoarquivo.txt', function(err){
    if(err){
        console.log(err);
        return;
    }

    console.log('Arquivo renomeado!');
});