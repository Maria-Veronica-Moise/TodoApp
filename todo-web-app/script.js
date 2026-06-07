import { TodoClient } from './utils.js'
const client = new TodoClient()
client.getTodos(getTodosCallback)

function getTodosCallback(data) {
    renderTodos(data)
}

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

    client.saveTodo(text, () => {
        newTodoInput.value = ''
        client.getTodos(getTodosCallback)
    })
})

function renderTodos(todos) {
    todoList.innerHTML = ''
    todos.forEach((todo, index) => {
        let todoHtml = `
        <li>
            <strong>${index + 1}. ${todo.title}</strong>
            <span>${todo.completed ? ' (Completed)' : ' (Not Completed)'}</span>
        </li>
        `
        todoList.innerHTML += todoHtml
    })
}
