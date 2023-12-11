import { theme } from '@/themes/theme'
import type { AppProps } from 'next/app'
import { ToastContainer } from 'react-toastify';
import { ThemeProvider } from '@mui/material/styles'

import '../themes/global.css'
import Sidebar from '@/components/Sidebar';
import 'react-toastify/dist/ReactToastify.css';
import CssBaseline from '@mui/material/CssBaseline';

export default function MyApp({ Component, pageProps }: AppProps) {
    return (
        <ThemeProvider theme={theme}>
            <div style={{ display: 'flex' }}>
                <Sidebar />
                <Component {...pageProps} />
            </div>
            <CssBaseline />
            <ToastContainer />
        </ThemeProvider>
    )
}
