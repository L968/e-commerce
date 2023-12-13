import { theme } from '@/themes/theme'
import type { AppProps } from 'next/app'
import { useRouter } from 'next/router';
import { ToastContainer } from 'react-toastify';
import { ThemeProvider } from '@mui/material/styles'

import '../themes/global.css'
import Sidebar from '@/components/Sidebar';
import 'react-toastify/dist/ReactToastify.css';
import CssBaseline from '@mui/material/CssBaseline';

export default function MyApp({ Component, pageProps }: AppProps) {
    const router = useRouter();

    const routesWithoutSidebar = ['/login'];
    const hideSidebar = routesWithoutSidebar.includes(router.pathname);

    return (
        <ThemeProvider theme={theme}>
            <div style={{ display: 'flex' }}>
                {!hideSidebar && <Sidebar />}
                <Component {...pageProps} />
            </div>
            <CssBaseline />
            <ToastContainer />
        </ThemeProvider>
    )
}
