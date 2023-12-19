//import * as React from "react";

import Button from "@mui/material/Button";
//import CameraIcon from "@mui/icons-material/PhotoCamera";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import CssBaseline from "@mui/material/CssBaseline";
import Grid from "@mui/material/Grid";
import Stack from "@mui/material/Stack";
import Box from "@mui/material/Box";

import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";

import { useState, useEffect } from "react";
import RenderedBooks from "../Components/RenderedBooks";
import { FormControl, Input, InputLabel } from "@mui/material";

import { Book } from "../Types.tsx";

export default function Books() {
	const [books, setBooks] = useState([]);

	async function resultedBookData() {
		try {
			console.log("Func Reached \n");
			const response = await fetch("http://localhost:5000/Book/Books/0/None");
			const booksRes = await response.json();
			setBooks(booksRes);
		} catch (error) {
			console.error("Error fetching data:", error);
		}
	}
	async function searchBooks(id: number = 0, searchParam: string = "None") {
		try {
			if (searchParam.length == 0) {
				searchParam = "None";
			}
			console.log("Func Reached \n");
			const response = await fetch(`http://localhost:5000/Book/Books/${id}/${searchParam}`);
			const booksRes = await response.json();
			setBooks(booksRes);
		} catch (error) {
			console.error("Error fetching data:", error);
		}
	}

	const keyPress = (e: React.ChangeEvent<HTMLInputElement>) => {
		if (e.keyCode == 13) {
			console.log("value", e.target.value);
			searchBooks(0, e.target.value);
			// put the login here
		}
	};

	useEffect(() => {
		resultedBookData();
	}, []);
	return (
		<>
			<CssBaseline />
			<main>
				{/* Hero unit */}
				<Box
					sx={{
						bgcolor: "background.paper",
						pt: 8,
						pb: 6,
					}}
				>
					<Container maxWidth="sm">
						<Typography component="h1" variant="h2" align="center" color="text.primary" gutterBottom>
							Books
						</Typography>
						<Typography variant="h5" align="center" color="text.secondary" paragraph>
							Something short and leading about the collection below—its contents, the creator, etc. Make it short and sweet, but not too short so folks don&apos;t simply skip over it
							entirely.
						</Typography>
						<Stack sx={{ pt: 4 }} direction="row" spacing={2} justifyContent="center">
							<Button variant="contained">Main call to action</Button>
							<Button variant="outlined">Secondary action</Button>
						</Stack>
					</Container>
				</Box>
				<Container sx={{ py: 8 }} maxWidth="md">
					{/* End hero unit */}
					<FormControl variant="standard">
						<InputLabel htmlFor="component-simple">Find Books</InputLabel>
						<Input onKeyDown={keyPress} id="component-simple" defaultValue="Search" />
					</FormControl>
					<Grid container spacing={4}>
						{books.map((book: Book) => (
							<Grid item key={book.bookId} xs={12} sm={6} md={4}>
								<Card sx={{ height: "100%", display: "flex", flexDirection: "column" }}>
									<CardMedia
										component="div"
										sx={{
											// 16:9
											pt: "56.25%",
										}}
										image={book.bookImg!.length > 10 ? book.bookImg : "https://source.unsplash.com/random?wallpapers"}
										//image="https://source.unsplash.com/random?wallpapers"
									/>
									<CardContent sx={{ flexGrow: 1 }}>
										<Typography gutterBottom variant="h5" component="h2">
											Heading
										</Typography>
										<RenderedBooks bookTitle={book.bookTitle} />
									</CardContent>
									<CardActions>
										<Button size="small">View</Button>
										<Button size="small">Edit</Button>
									</CardActions>
								</Card>
							</Grid>
						))}
					</Grid>
				</Container>
			</main>
		</>
	);
}
