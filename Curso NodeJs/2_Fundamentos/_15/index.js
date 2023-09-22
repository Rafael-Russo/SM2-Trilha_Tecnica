const chalk = require('chalk');
const inquirer = require('inquirer');

try{
    inquirer.prompt([
        {
            name: 'nome', message: 'Qual o seu nome?'
        },
        {
            name: 'idade', message: 'Qual a sua idade?'
        }
    ]).then(answers => {
        console.log(chalk.bgYellow.black(`O seu nome é ${answers.nome} e você tem ${answers.idade} anos`));
    }).catch(err => console.log(err));
}catch(err){
    console.log(`Erro: ${err}`);
}