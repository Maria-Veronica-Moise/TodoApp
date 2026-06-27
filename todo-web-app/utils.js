export class TodoClient {
    apiBaseUrl = 'https://localhost:7017'

    getTodos(callback) {
        fetch(`${this.apiBaseUrl}/api/todo`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        })
            .then((response) => response.json())
            .then((data) => callback(data))
    }

    getActiveTodos(callback) {
        fetch(`${this.apiBaseUrl}/api/todo/active`)
            .then((response) => response.json())
            .then((data) => callback(data))
    }

    getCompletedTodos(callback) {
        fetch(`${this.apiBaseUrl}/api/todo/completed`)
            .then((response) => response.json())
            .then((data) => callback(data))
    }

    saveTodo(title, categoryId, callback) {
        fetch(`${this.apiBaseUrl}/api/todo`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                title: title,
                categoryId: categoryId,
            }),
        }).then(() => callback())
    }

    completeTodo(id, callback) {
        fetch(`${this.apiBaseUrl}/api/todo/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
        }).then(() => callback())
    }

    deleteTodo(id, callback) {
        fetch(`${this.apiBaseUrl}/api/todo/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        }).then(() => callback())
    }
    getCategories(callback) {
        fetch(`${this.apiBaseUrl}/api/category`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        })
            .then((response) => response.json())
            .then((data) => callback(data))
    }
}
