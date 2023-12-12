import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./index.css";
import { createBrowserRouter, RouterProvider, Route, Link } from "react-router-dom";
import RenderedBooks from "./Components/RenderedBooks.tsx";
import Home from "./Pages/Home.tsx";
import Books from "./Pages/Books.tsx";
import MyRentals from "./Pages/MyRentals.tsx";

const router = createBrowserRouter([
	{
		path: "/",
		element: <App />,
		children: [
			{
				path: "/Home",
				element: <Home />,
			},
			{
				path: "/Books",
				element: <Books />,
			},
			{
				path: "/MyRentals",
				element: <MyRentals />,
			},
		],
	},
]);

ReactDOM.createRoot(document.getElementById("root")!).render(
	<React.StrictMode>
		<RouterProvider router={router} />
	</React.StrictMode>
);
