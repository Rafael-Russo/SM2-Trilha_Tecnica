const { Sequelize } = require('sequelize');
const sequelize = new Sequelize('nodesequelize', 'root', '', {
    host: 'localhost',
    dialect: 'mysql'
});

try{
    sequelize.authenticate();
    console.log('Conectado com o BD pelo Sequelize!');
}catch(err){
    console.log('Erro de conexão com o BD: ' + error);
}

module.exports = sequelize;