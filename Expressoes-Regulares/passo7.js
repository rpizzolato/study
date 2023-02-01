var regExp = /^\([0-9]{2}\) [0-9]{4,5}-?[0-9]{4}$/;
var telefone1 = "(80) 99876-1234";
var telefone2 = "(80) 998761234";

console.log(regExp.test(telefone1));
console.log(regExp.test(telefone2));