import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./index.css";
import { createBrowserRouter, RouterProvider, Route, Link } from "react-router-dom";
import RenderedBooks from "./Components/RenderedBooks.tsx";

const router = createBrowserRouter([
	{
		path: "/",
		element: (
			<div>
				<App />
			</div>
		),
	},
	{
		path: "/Book",
		element: <RenderedBooks />,
	},
]);

ReactDOM.createRoot(document.getElementById("root")!).render(
	<React.StrictMode>
		<RouterProvider router={router} />
	</React.StrictMode>
);
