import { theme } from '@/themes/theme'
import Navbar from '@/components/Navbar'
import type { AppProps } from 'next/app'
import Footer from '@/components/Footer'
import { ToastContainer } from 'react-toastify';
import { ThemeProvider } from '@mui/material/styles'
import { AuthProvider } from '@/contexts/authContext';
import { OrderCheckoutProvider } from '@/contexts/orderCheckoutContext';

import '../themes/global.css'
import 'react-toastify/dist/ReactToastify.css';

export default function MyApp({ Component, pageProps }: AppProps) {
    return (
        <ThemeProvider theme={theme}>
            <AuthProvider>
                <OrderCheckoutProvider>
                    <Navbar />
                    <Component {...pageProps} />
                    <Footer />
                    <ToastContainer />
                </OrderCheckoutProvider>
            </AuthProvider>
        </ThemeProvider>
    )
}
