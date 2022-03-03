export function addTodo(text) {
	return {
		type: 'ADD_TODO',
		text
		//em ES6, o trecho acima é a mesma coisa que text: text
	}
}