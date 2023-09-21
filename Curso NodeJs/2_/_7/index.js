//imprime mais de um valor
const x = 7;
const y = "texto";
const z = [1, 2];

console.log(x, y, z);

//contagem de impressões
console.count(`O valor de x é ${x}, cotagem: `);
console.count(`O valor de x é ${x}, cotagem: `);
console.count(`O valor de x é ${x}, cotagem: `);
console.count(`O valor de x é ${x}, cotagem: `);
console.count(`O valor de x é ${x}, cotagem: `);
console.count(`O valor de x é ${x}, cotagem: `);
console.count(`O valor de x é ${x}, cotagem: `);
console.count(`O valor de x é ${x}, cotagem: `);
console.count(`O valor de x é ${x}, cotagem: `);

//interpolação diferente
console.log("O nome é %s, e ele é programador", y);

//limpa console
setTimeout(()=>{
    console.clear();
}, 2000);