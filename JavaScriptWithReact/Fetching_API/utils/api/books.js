import { BASE_URL } from "./base"

const getBook = (bookId) =>{
    return fetch(`${BASE_URL}/works/${bookId}/editions.json`)
        .then((response)=> {
            return response.json()
        }).then((data)=>{
            return data
        })
}

const getBookTitle = (bookId) =>{
    return fetch(`${BASE_URL}/works/${bookId}.json`)
        .then((response)=> {
            return response.json()
        }).then((data)=>{
            return data
        })
}


export {getBook, getBookTitle}



