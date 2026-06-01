console.log('Hello from JavaScript')
let todos = [
    {
        id: 1,
        order: 1,
        title: 'Learn HTML',
        isCompleted: true,
    },
    {
        id: 2,
        order: 2,
        title: 'Learn CSS',
        isCompleted: false,
    },
    {
        id: 3,
        order: 3,
        title: 'Learn JavaScript',
        isCompleted: false,
    },
]
console.log('Initial todos:', todos)

function sayHello() {
    console.log('Hello!')
}

sayHello()

function printTodos(todos) {
    todos.forEach((todo) => {
        console.log('Todo: ' + todo.title)
    })
}
printTodos(todos)

todos.forEach((todo) => {
    if (todo.isCompleted) {
        console.log(todo.title + 'This todo is completed')
    } else {
        console.log(todo.title + 'This todo is not completed')
    }
})

let todoStatus = todos.map((todo) => {
    return {
        title: todo.title,
        isCompleted: todo.isCompleted,
    }
})
console.log('Todo status:', todoStatus)

for (let i = 0; i < 5; i++) {
    console.log(i)
}

for (let i = 0; i < todos.length; i++) {
    console.log(`${i}: ${todos[i].title}`)
}

for (let i = 0; i < todos.length; i++) {
    if (todos[i].isCompleted === true) {
        console.log(`${todos[i].title} is completed`)
    }
}

function getTodoStatus(todo) {
    if (todo.isCompleted) {
        return 'completed'
    } else {
        return 'not completed'
    }
}

todos.forEach((todo) => {
    console.log(getTodoStatus(todo))
})

const addButton = document.getElementById('add-todo-button')
addButton.addEventListener('click', function () {
    console.log('Add button clicked!')
    alert('Todo added!')
})

const todoList = document.getElementById('todo-list')

function renderTodos() {
    todoList.innerHTML = ''

    for (let todo of todos) {
        let todoHtml = `
            <li class="${todo.isCompleted ? 'completed' : ''}">
                <strong>${todo.order}. ${todo.title}</strong>
                <span>${todo.isCompleted ? 'Completed' : 'Not completed'}</span>
            </li>
        `

        todoList.innerHTML += todoHtml
    }
}

renderTodos()
