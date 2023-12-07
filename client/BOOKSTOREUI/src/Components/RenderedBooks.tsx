type Book = {
	bookId?: number;
	bookTitle?: string;
	bookAuthorFirstName?: string;
	bookAuthorLastName?: string;
	genre?: string;
	bookImg?: string;
	publishedYear?: string;
};
function RenderedBooks(props: Book) {
	return (
		<div>
			<h2>{props.bookTitle}</h2>
		</div>
	);
}

export default RenderedBooks;
