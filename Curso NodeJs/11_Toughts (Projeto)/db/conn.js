const { Sequelize } = require('sequelize');
const sequelize = new Sequelize('toughts', 'root', '', {
    host: 'localhost',
    dialect: 'mysql'
});

try{
    sequelize.authenticate();
    console.log('Conectado ao BD!');
}catch(err){
    console.log(`Erro ao conectar ao BD: ${err}`);
}

module.exports = sequelize;