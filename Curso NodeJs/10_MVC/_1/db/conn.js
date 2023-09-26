const {Sequelize} = require('sequelize');
const sequelize = new Sequelize('nodemvc', 'root', '', {
    host: 'localhost',
    dialect: 'mysql'
});

try{
    console.log('BD conectado!');
} catch(err){
    console.log('Erro ao conectar ao BD: ' + error);
    return;
}

module.exports = sequelize;