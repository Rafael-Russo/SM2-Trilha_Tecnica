const mongoose = require('mongoose');

async function main(){
    await mongoose.connect('mongodb://127.0.0.1:27017/testemongoose');
    console.log('Conectado ao mongoose com sucesso!');
}

main().catch((err) => console.log(`Falha ao conectar ao mongoose! Erro: ${err}`));

module.exports = mongoose;