//import * as React from "react";
import AppBar from "@mui/material/AppBar";
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
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import Link from "@mui/material/Link";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { useState, useEffect } from "react";
import RenderedBooks from "./RenderedBooks";

function Copyright() {
	return (
		<Typography variant="body2" color="text.secondary" align="center">
			{"Copyright © "}
			<Link color="inherit" href="https://mui.com/">
				Your Website
			</Link>{" "}
			{new Date().getFullYear()}
			{"."}
		</Typography>
	);
}

// TODO remove, this demo shouldn't need to reset the theme.
const defaultTheme = createTheme();

export default function Home() {
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
		resultedBookData();
	}, []);
	return (
		<ThemeProvider theme={defaultTheme}>
			<CssBaseline />
			<AppBar position="relative">
				<Toolbar>
					{/*<CameraIcon sx={{ mr: 2 }} />*/}
					<Typography variant="h6" color="inherit" noWrap>
						Oxford's Bookstore
					</Typography>
				</Toolbar>
			</AppBar>
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
							Oxford's Bookstore
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
			{/* Footer */}
			<Box sx={{ bgcolor: "background.paper", p: 6 }} component="footer">
				<Typography variant="h6" align="center" gutterBottom>
					Footer
				</Typography>
				<Typography variant="subtitle1" align="center" color="text.secondary" component="p">
					Something here to give the footer a purpose!
				</Typography>
				<Copyright />
			</Box>
			{/* End footer */}
		</ThemeProvider>
	);
}
