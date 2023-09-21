const x = '10';

if(!Number.isInteger(x)){
    throw new Error('O Valor de X não é um número inteiro!');
}

console.log('Fim');