import styled from 'styled-components';
import { mobile } from '../../responsive';
import { IconButton } from '@mui/material';
import Footer from '../../components/Footer';
import Navbar from '../../components/Navbar';
import { useNavigate } from 'react-router-dom';
import { Add, Remove } from '@mui/icons-material';
import { Fragment, useEffect, useState } from 'react';
import Announcement from '../../components/Announcement';
import { cart, orderSummary as orderSummaryData} from '../../services/data';

export default function Cart() {
    const navigate = useNavigate();

    return (
        <>
            <Navbar />
            <Announcement />
            <Wrapper>
                
            </Wrapper>
            <Footer />
        </>
    );
};

const Wrapper = styled.div`
    padding: 20px;
    ${mobile({ padding: '10px' })}
`;