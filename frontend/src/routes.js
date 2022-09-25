import React from 'react';
import { BrowserRouter, Routes as Switch, Route } from 'react-router-dom';

import Home from './pages/Home';
import ProductList from './pages/ProductList';
import Product from './pages/Product';
import Cart from './pages/Cart';
import Checkout from './pages/Checkout';
import Login from './pages/Login';
import Register from './pages/Register';

export default function Routes() {
    return (
        <BrowserRouter>
            <Switch>
                <Route exact path="/" element={<Home />} />
                <Route path="/products" element={<ProductList />} />
                <Route path="/product" element={<Product/>} />
                <Route path="/cart" element={<Cart />} />
                <Route path="/checkout" element={<Checkout />} />
                <Route path="/login" element={<Login/>} />
                <Route path="/register" element={<Register/>} />
            </Switch>
        </BrowserRouter>
    );
}