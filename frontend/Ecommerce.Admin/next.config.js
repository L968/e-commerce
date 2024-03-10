/** @type {import('next').NextConfig} */

const nextConfig = {
    images:{
        remotePatterns: [{
            protocol: 'https',
            hostname: process.env.IMAGE_HOSTNAME
        }]
    },
    async redirects() {
        return [
            {
                source: '/',
                destination: '/dashboard',
                permanent: true
            }
        ]
    }
}

module.exports = nextConfig
