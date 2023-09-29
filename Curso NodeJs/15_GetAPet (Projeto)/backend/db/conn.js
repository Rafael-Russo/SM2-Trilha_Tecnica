const { default: mongoose } = require('mongoose');
const mangoose = require('mongoose');

async function main(){
    await mangoose.connect('mongodb://127.0.0.1:27017/getapet');
    console.log('Conectado com o DB');
}

main().catch((err) => console.log(`Não foi possivel conectar ao BD! Erro: ${err}`));

module.exports = mongoose;