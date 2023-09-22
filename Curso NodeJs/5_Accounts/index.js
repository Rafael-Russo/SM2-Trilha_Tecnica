//modulos externos
const inquirer = require('inquirer');
const chalk = require('chalk');

//modulos internos
const fs = require('fs');

operation();

function operation() {
    inquirer.prompt([
        {
            type: 'list',
            name: 'action',
            message: 'O que você deseja fazer?',
            choices: [
                'Criar conta',
                'Consultar saldo',
                'Depositar',
                'Sacar',
                'Sair'
            ]
        }
    ]).then((answer) => {
        const action = answer['action'];

        if (action === 'Criar conta') {
            createAccount();
        } else if (action === 'Consultar saldo') {
            getBalance();
        } else if (action === 'Depositar') {
            deposit();
        } else if (action === 'Sacar') {
            withdraw();
        } else if (action === 'Sair') {
            console.log(chalk.bgBlue.black('Obrigado por usar o Accounts!'));
            process.exit();
        }
    }).catch((err) => console.log(err));
}

//criar conta
function createAccount() {
    console.log(chalk.bgGreen.black('Parabéns por escolher o nosso banco!'));
    console.log(chalk.green('Defina as opções da sua conta a seguir'));

    buildAccount();
}

function buildAccount() {
    inquirer.prompt([
        {
            name: 'accountName',
            message: 'Digite um nome para sua conta: '
        }
    ]).then((answer) => {
        const accountName = answer['accountName'];

        console.info(accountName);

        if (!fs.existsSync('accounts')) {
            fs.mkdir('accounts', (err) => {
                if(err){
                    console.log(err);
                }
            });
        }

        if (fs.existsSync(`accounts/${accountName}.json`)) {
            console.log(chalk.bgRed.black('Essa conta ja existe, escolha outro nome!'));
            buildAccount();
            return;
        }

        fs.writeFileSync(
            `accounts/${accountName}.json`,
            '{"balance": 0}',
            function (err) {
                console.log(err);
            }
        );

        console.log(chalk.green('Parabéns, a sua conta foi criada!'));
        operation();
    }).catch((err) => console.log(err));
}

//depositar
function deposit() {
    inquirer.prompt([
        {
            name: 'accountName',
            message: 'Qual o nome da sua conta?'
        }
    ]).then((answer) => {
        const accountName = answer['accountName'];

        if (!checkAccount(accountName)) {
            return deposit();
        }

        inquirer.prompt([
            {
                name: 'amount',
                message: 'Quanto você deseja depositar?'
            }
        ]).then((answer) => {
            const amount = answer['amount'];

            addAmount(accountName, amount);
            operation();
        }).catch((err) => console.log(err));
    }).catch((err) => console.log(err));
}

//verifica se conta existe
function checkAccount(accountName) {
    if (!fs.existsSync(`./accounts/${accountName}.json`)) {
        console.log(chalk.bgRed.black('Essa conta não existe, tente novamente'));
        return false;
    }
    return true;
}

//adiciona quantidade
function addAmount(accountName, amount) {
    const accountData = getAccount(accountName);

    if (!amount) {
        console.log(chalk.bgYellow.black('Ocorreu um erro, tente novamente mais tarde!'));
        return deposit();
    }

    accountData.balance = parseFloat(amount) + parseFloat(accountData.balance);

    fs.writeFileSync(
        `./accounts/${accountName}.json`,
        JSON.stringify(accountData),
        function (err) {
            console.log(err);
        }
    )

    console.log(chalk.green(`Foi depositado o valor de R$${amount} na conta: ${accountName}`));
}

function getAccount(accountName) {
    const accountJSON = fs.readFileSync(`./accounts/${accountName}.json`, {
        encoding: 'utf8',
        flag: 'r'
    });

    return JSON.parse(accountJSON);
}

//verificar saldo
function getBalance(){
    inquirer.prompt([
        {
            name: 'accountName',
            message: 'Qual o nome da sua conta? '
        }
    ]).then((answer) => {
        const accountName = answer['accountName'];

        if(!checkAccount(accountName)){
            return getBalance();
        }

        const accountData = getAccount(accountName);

        console.log(chalk.bgBlue.black(
            `O saldo da sua conta é R$${accountData.balance}`
        ));
    }).catch((err) => console.log(err));
}

//sacar
function withdraw(){
    inquirer.prompt([
        {
            name: 'accountName',
            message: 'Qual o nome da sua conta? '
        }
    ]).then((answer) => {
        const accountName = answer['accountName'];

        if(!checkAccount(accountName)){
            return withdraw();
        }

        inquirer.prompt([
            {
                name: 'amount',
                message: 'Quanto você deseja sacar? '
            }
        ]).then((answer) => {
            const amount = answer['amount'];
            
            removeAmount(accountName, amount);
            operation();
        }).catch((err) => console.log(err));
    }).catch((err) => console.log(err));
}

//retira quantidade
function removeAmount(accountName, amount){
    const accountData = getAccount(accountName);

    if(!amount){
        console.log(chalk.bgYellow.black('Ocorreu um erro, tente novamente mais tarde!'));
        return withdraw();
    }

    if(accountData.balance < amount){
        console.log(chalk.bgRed.black('Valor indisponivel!'));
        return withdraw();
    }

    accountData.balance = parseFloat(accountData.balance) - parseFloat(amount);

    fs.writeFileSync(
        `./accounts/${accountName}.json`,
        JSON.stringify(accountData),
        function (err){
            console.log(err);
        }
    )

    console.log(chalk.green(`Saque de R$${amount} na conta: ${accountName}`));
    operation();
}