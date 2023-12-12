import { Outlet } from "react-router-dom";
import "./App.css";
import Home from "./Pages/Home";
import Navbar from "./Components/Navbar";
import Footer from "./Components/Footer";
import { ThemeProvider } from "@emotion/react";
import { createTheme } from "@mui/material";

const defaultTheme = createTheme();
function App() {
	return (
		<ThemeProvider theme={defaultTheme}>
			<main>
				<Navbar />
				<Outlet />
				{/*</main>
			</ThemeProvider>*/}
				<Footer />
			</main>
		</ThemeProvider>
	);
}

export default App;
