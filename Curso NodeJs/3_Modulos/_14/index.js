const fs = require('fs');

if(!fs.existsSync('./pasta')){
    console.log('Não Existe!');
    fs.mkdirSync('pasta');
} else if(fs.existsSync('./pasta')){
    console.log('Existe!');
}