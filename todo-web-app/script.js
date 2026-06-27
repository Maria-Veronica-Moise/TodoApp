import { TodoClient } from './utils.js'
const client = new TodoClient()

const todoList = document.getElementById('todo-list')
const newTodoInput = document.getElementById('new-todo-input')
const addTodoButton = document.getElementById('add-todo-button')
const searchInput = document.getElementById('search-input')
const showAllButton = document.getElementById('show-all-button')
const showActiveButton = document.getElementById('show-active-button')
const showCompletedButton = document.getElementById('show-completed-button')
const categorySelect = document.getElementById('category-select')

let todos = []

client.getTodos(getTodosCallback)
client.getCategories(getCategoriesCallback)

client.getCategories((categories) => {
    console.log('categories from backend:', categories)

    categorySelect.innerHTML = '<option value="">Choose category</option>'

    categories.forEach((category) => {
        categorySelect.innerHTML += `
            <option value="${category.id}">
                ${category.name}
            </option>
        `
    })
})

function getTodosCallback(data) {
    todos = data
    renderTodos()
}

function getCategoriesCallback(categories) {
    categorySelect.innerHTML = '<option value="">Choose category</option>'

    categories.forEach((category) => {
        categorySelect.innerHTML += `
            <option value="${category.id}">
                ${category.name}
            </option>
        `
    })
}

// add eventlisteners
addTodoButton.addEventListener('click', () => {
    let text = newTodoInput.value.trim()
    let categoryId = categorySelect.value

    if (text === '') {
        alert('Please enter a todo title')
        return
    }

    if (categoryId === '') {
        alert('Please choose a category')
        return
    }

    client.saveTodo(text, categoryId, () => {
        newTodoInput.value = ''
        categorySelect.value = ''
        client.getTodos(getTodosCallback)
    })
})

showAllButton.addEventListener('click', () => {
    console.log('show all')
    client.getTodos(getTodosCallback)
})

showActiveButton.addEventListener('click', () => {
    console.log('show active')
    client.getActiveTodos(getTodosCallback)
})

showCompletedButton.addEventListener('click', () => {
    console.log('show completed')
    client.getCompletedTodos(getTodosCallback)
})

window.toggleCompleted = function (index) {
    const todo = todos[index]

    client.completeTodo(todo.id, () => {
        client.getTodos(getTodosCallback)
    })
}

window.deleteTodo = function (index) {
    const todo = todos[index]

    client.deleteTodo(todo.id, () => {
        client.getTodos(getTodosCallback)
    })
}

function renderTodos() {
    todoList.innerHTML = ''
    todos.forEach((todo, index) => {
        let todoHtml = `
        <li>
            <input type="checkbox" ${todo.completed ? 'checked' : ''}
            onchange="toggleCompleted(${index})" ${todo.isCompleted ? 'checked' : ''}>
            
            <strong>${index + 1}. ${todo.title}</strong>
            <span>${todo.isCompleted ? ' (Completed)' : ' (Not Completed)'}</span>

            <button class = "delete-btn" onclick="deleteTodo(${index})">Delete</button>
            
        </li>
        `
        todoList.innerHTML += todoHtml
    })
}
