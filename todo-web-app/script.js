import { TodoClient } from './utils.js'
const client = new TodoClient()
// client.getTodos((todos) => console.log(todos))
let todos = []
const todoList = document.getElementById('todo-list')
const newTodoInput = document.getElementById('new-todo-input')
const addTodoButton = document.getElementById('add-todo-button')
const searchInput = document.getElementById('search-input')
const showAllButton = document.getElementById('show-all-button')
const showActiveButton = document.getElementById('show-active-button')
const showCompletedButton = document.getElementById('show-completed-button')

// add eventlisteners
addTodoButton.addEventListener('click', () => {
    let text = newTodoInput.value.trim()
    todos.push({ text, completed: false })
    newTodoInput.value = ''
    renderTodos()
})

function renderTodos() {
    todoList.innerHTML = ''
    todos.forEach((todo, index) => {
        let todoHtml = `
        <li>
            <strong>${index + 1}. ${todo.text}</strong>
            <span>${todo.completed ? ' (Completed)' : ' (Not Completed)'}</span>
        </li>
        `
        todoList.innerHTML += todoHtml
    })
}
