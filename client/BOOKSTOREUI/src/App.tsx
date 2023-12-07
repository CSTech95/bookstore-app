import { useEffect, useState } from "react";
import "./App.css";
import RenderedBooks from "./Components/RenderedBooks";

function App() {
	const [books, setBooks] = useState([]);

	async function resultedData() {
		try {
			console.log("Func Reached \n");
			const response = await fetch("http://localhost:5000/Book/Books/0");
			const booksRes = await response.json();
			setBooks(booksRes);
		} catch (error) {
			console.error("Error fetching data:", error);
		}
	}

	type Book = {
		bookId?: number;
		bookTitle?: string;
		bookAuthorFirstName?: string;
		bookAuthorLastName?: string;
		genre?: string;
		bookImg?: string;
		publishedYear?: string;
	};

	useEffect(() => {
		resultedData();
	}, []);
	return (
		<>
			<div>T</div>
			{books.map((book: Book) => {
				return (
					<div>
						<RenderedBooks bookTitle={book.bookTitle} />
					</div>
				);
			})}
			<button onClick={resultedData}>Click</button>
		</>
	);
}

export default App;
