import Navbar from '@/components/Navbar'
import type { AppProps } from 'next/app'
import '../themes/global.css'
import Footer from '@/components/Footer'
import { ThemeProvider } from '@mui/material/styles'
import { theme } from '@/themes/theme'

export default function MyApp({ Component, pageProps }: AppProps) {
    return (
        <ThemeProvider theme={theme}>
            <Navbar />
            <Component {...pageProps} />
            <Footer />
        </ThemeProvider>
    )
}