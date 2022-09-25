import React from 'react';
import Announcement from '../../components/Announcement';
import Navbar from '../../components/Navbar';
import Slider from '../../components/Slider';
import Categories from '../../components/Categories';
import Products from '../../components/Products';
import Newsletter from '../../components/Newsletter';
import Footer from '../../components/Footer';

export default function Home() {
    return (
        <React.Fragment>
            <Announcement />
            <Navbar />
            <Slider />
            <Categories />
            <Products />
            <Newsletter />
            <Footer />
        </React.Fragment>
    )
}