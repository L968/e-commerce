import Image from 'next/image';
import { Swiper, SwiperSlide, SwiperProps } from 'swiper/react';

import 'swiper/css';

export default function ProductCarousel({ ...props }: SwiperProps) {
    return (
        <Swiper
            allowTouchMove={false}
            {...props}
        >
            <SwiperSlide>
                <Image
                    src='/assets/product-detail-1.png'
                    alt='product'
                    width={506}
                    height={450}
                />
            </SwiperSlide>
            <SwiperSlide>
                <Image
                    src='/assets/product-detail-1.png'
                    alt='product'
                    width={506}
                    height={450}
                />
            </SwiperSlide>
        </Swiper>
    );
}
