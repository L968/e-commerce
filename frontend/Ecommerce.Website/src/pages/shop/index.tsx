import { useEffect, useState } from 'react';
import Image from 'next/image';
import Grid from '@mui/material/Grid';
import { toast } from 'react-toastify';
import MenuItem from '@mui/material/MenuItem';
import InputLabel from '@mui/material/InputLabel';
import WindowIcon from '@mui/icons-material/Window';
import FormControl from '@mui/material/FormControl';
import FormatListBulletedIcon from '@mui/icons-material/FormatListBulleted';

import api from '@/services/api';
import Pagination from '@/components/Pagination';
import Breadcrumbs from '@/components/Breadcrumbs';
import ProductCard from '@/components/ProductCard';
import ProductListResponse from '@/interfaces/api/responses/ProductListResponse';
import { Card, CategoriesCardsArea, CardQuantity, CardTextContainer, CardTitle, Header, Title, FilterArea, FilterResultsText, FilterContainer, FilterSort, FilterIconContainer, FilterInputContainer, PopularitySelect, FilterButton, FilterViewsText, ProductCardsArea, ProductCardsContainer, ProductCardsRow } from './styles';

export default function Shop() {
    const [products, setProducts] = useState<ProductListResponse>();
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [loading, setLoading] = useState<boolean>(false);

    useEffect(() => {
        setLoading(true);

        api.get<ProductListResponse>(`/product?page=${currentPage}&pageSize=16`)
            .then(response => setProducts(response.data))
            .catch(error => toast.error('Error 500'))
            .finally(() => setLoading(false));
    }, [currentPage]);

    function handlePageChange(newPage: number) {
        setCurrentPage(newPage);
    }

    return (
        <main>
            <Header>
                <Title variant='h6'>Shop</Title>
                <Breadcrumbs
                    items={[
                        { href: '/home', text: 'Home' },
                        { href: '/shop', text: 'Shop' }
                    ]}
                />
            </Header>
            <CategoriesCardsArea>
                <Card>
                    <Image
                        src='/assets/card-1.png'
                        alt='card-1'
                        width={205}
                        height={223}
                    />
                    <CardTextContainer>
                        <CardTitle>CLOTHS</CardTitle>
                        <CardQuantity>5 Items</CardQuantity>
                    </CardTextContainer>
                </Card>
                <Card>
                    <Image
                        src='/assets/card-2.png'
                        alt='card-2'
                        width={205}
                        height={223}
                    />
                    <CardTextContainer>
                        <CardTitle>CLOTHS</CardTitle>
                        <CardQuantity>5 Items</CardQuantity>
                    </CardTextContainer>
                </Card>
                <Card>
                    <Image
                        src='/assets/card-3.png'
                        alt='card-3'
                        width={205}
                        height={223}
                    />
                    <CardTextContainer>
                        <CardTitle>CLOTHS</CardTitle>
                        <CardQuantity>5 Items</CardQuantity>
                    </CardTextContainer>
                </Card>
                <Card>
                    <Image
                        src='/assets/card-4.png'
                        alt='card-4'
                        width={205}
                        height={223}
                    />
                    <CardTextContainer>
                        <CardTitle>CLOTHS</CardTitle>
                        <CardQuantity>5 Items</CardQuantity>
                    </CardTextContainer>
                </Card>
                <Card>
                    <Image
                        src='/assets/card-5.png'
                        alt='card-5'
                        width={205}
                        height={223}
                    />
                    <CardTextContainer>
                        <CardTitle>CLOTHS</CardTitle>
                        <CardQuantity>5 Items</CardQuantity>
                    </CardTextContainer>
                </Card>
            </CategoriesCardsArea>
            <FilterArea>
                <FilterContainer>
                    <FilterResultsText>Showing all 12 results</FilterResultsText>

                    <FilterSort>
                        <FilterViewsText>Views:</FilterViewsText>
                        <FilterIconContainer>
                            <WindowIcon />
                        </FilterIconContainer>
                        <FilterIconContainer>
                            <FormatListBulletedIcon />
                        </FilterIconContainer>
                    </FilterSort>

                    <FilterInputContainer>
                        <FormControl>
                            <InputLabel>Popularity</InputLabel>
                            <PopularitySelect label='Popularity'>
                                <MenuItem>FILTER 1</MenuItem>
                                <MenuItem>FILTER 2</MenuItem>
                                <MenuItem>FILTER 3</MenuItem>
                            </PopularitySelect>
                        </FormControl>

                        <FilterButton>Filter</FilterButton>
                    </FilterInputContainer>
                </FilterContainer>
            </FilterArea>
            <ProductCardsArea>
                {!loading && products && (
                    <ProductCardsContainer>
                        <ProductCardsRow container columnSpacing={6} rowSpacing={9}>
                            {products.items.map(product => (
                                <Grid key={product.id} item xs={3}>
                                    <ProductCard {...product} />
                                </Grid>
                            ))}
                        </ProductCardsRow>

                        <Pagination
                            page={currentPage}
                            totalPages={products.totalPages}
                            onChangePage={handlePageChange}
                        />
                    </ProductCardsContainer>
                )}
            </ProductCardsArea>
        </main>
    )
}
