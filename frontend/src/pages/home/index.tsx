import Image from 'next/image'
import { Banner, BannerContent, BannerText, BannerTitle, BannerHeader, ShopButton, Clients } from "./styles";

export default function Home() {
    return (
        <main>
            <Banner>
                <BannerContent>
                    <BannerHeader>SUMMER 2024</BannerHeader>
                    <BannerTitle>NEW COLLECTION</BannerTitle>
                    <BannerText>We know how large objects will act,<br />but things on a small scale.<br/></BannerText>
                    <ShopButton variant='contained'>
                        SHOP NOW
                    </ShopButton>
                </BannerContent>

                <Image
                    alt='banner'
                    src='/assets/banner.png'
                    width={697}
                    height={619}
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
