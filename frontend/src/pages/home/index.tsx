import Image from 'next/image'
import { Banner, BannerContent, BannerText, BannerTitle, BannerHeader, ShopButton, Clients } from "./styles";

export default function Home() {
    return (
        <main>
            <Banner>
                <BannerContent>
                    <BannerHeader variant='h5'>SUMMER 2024</BannerHeader>
                    <BannerTitle variant='h1'>NEW COLLECTION</BannerTitle>
                    <BannerText>We know how large objects will act,<br />but things on a small scale.<br/></BannerText>
                    <ShopButton variant='contained'>
                        SHOP NOW
                    </ShopButton>
                </BannerContent>

                <Image
                    alt='banner'
                    src='/assets/banner.png'
                    width={700}
                    height={619}
                    quality={100}
                />
            </Banner>
            <Clients>
                <Image
                    alt='clients'
                    src='/assets/clients.png'
                    width={1050}
                    height={175}
                />
            </Clients>
        </main>
    )
}
