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

    saveTodo(todo, callback = null) {
        fetch(`${this.apiBaseUrl}/api/todo`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(todo),
        }).then(() => callback(data))
    }
}
