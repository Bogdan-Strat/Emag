import React from "react";
import "./App.css";
import { Routes, Route, BrowserRouter } from "react-router-dom";
import Register from "./pages/Register.tsx";
import Login from "./pages/Login.tsx";
import Home from "./pages/Home.tsx";
import Cart from "./pages/Cart.tsx";
import History1 from "./pages/History.tsx";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        {<Route path="/" element={<Login />} />}
        <Route path="/register" element={<Register />} />
        <Route path="/home" element={<Home />} />
        <Route path="/cart" element={<Cart />} />
        <Route path="/history" element={<History1  />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
