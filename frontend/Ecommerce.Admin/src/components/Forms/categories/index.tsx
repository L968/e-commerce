import api from '@/services/api';
import { toast } from 'react-toastify';
import { useRouter } from 'next/router';
import BaseForm from '@/interfaces/BaseForm';
import { Container, Form, Main } from './styles';
import { Button, TextField, Typography } from '@mui/material';
import Autocomplete from '@/components/Autocomplete';
import { FormEvent, useEffect, useState } from 'react';
import GetVariantsResponse from '@/interfaces/api/responses/GetVariantsResponse';
import GetCategoryResponse from '@/interfaces/api/responses/GetCategoriesResponse';
import UpdateProductCategoryRequest from '@/interfaces/api/requests/categories/UpdateProductCategoryRequest';
import CreateProductCategoryRequest from '@/interfaces/api/requests/categories/CreateProductCategoryRequest';

export default function CategoryForm({ crudType }: BaseForm) {
    const router = useRouter();
    const [variants, setVariants] = useState<GetVariantsResponse[]>([]);
    const [category, setCategory] = useState<GetCategoryResponse>({ id: '', name: '', description: '', variants: [] });

    useEffect(() => {
        if (crudType === 'Create') return;

        const productCategoryId = router.query.id;
        if (!productCategoryId) return;

        api.get<GetCategoryResponse>(`/productCategory/${productCategoryId}`)
            .then(response => setCategory(response.data))
            .catch(error => toast.error('Error 500'));
    }, [router.query]);

    useEffect(() => {
        api.get<GetVariantsResponse[]>('/variant')
            .then(response => setVariants(response.data))
            .catch(error => toast.error('Error 500'));
    }, []);

    function handleOnSubmit(e: FormEvent<HTMLFormElement>): void {
        e.preventDefault();

        if (crudType === 'Create') {
            const data: CreateProductCategoryRequest = {
                name: category.name,
                description: category.description,
                variantIds: category.variants.map(v => v.id)
            }

            api.post('/productCategory', data)
                .then(_ => {
                    toast.success('Category created successfully')
                    router.push('/categories');
                })
                .catch(error => {
                    if (error.response?.status === 400) {
                        toast.warning(error.response.data);
                        return;
                    }

                    toast.error('Error 500')
                });
        } else if (crudType === 'Update') {
            const data: UpdateProductCategoryRequest = {
                name: category.name,
                description: category.description,
                variantIds: category.variants.map(v => v.id)
            }

            api.put(`/productCategory/${category.id}`, data)
                .then(_ => {
                    toast.success('Category updated successfully')
                    router.push('/categories');
                })
                .catch(error => {
                    if (error.response?.status === 400) {
                        toast.warning(error.response.data);
                        return;
                    }

                    toast.error('Error 500')
                });
        }
    }

    function handleCategoryOnChange(name: string, value: any): void {
        setCategory(prev => ({
            ...prev,
            [name]: value
        }));
    }

    function getTitleText(): string {
        switch (crudType) {
            case 'Create': return 'Create Product Category'
            case 'Update': return 'Edit Product Category'
            default: return '';
        }
    }

    return (
        <Main>
            <Container>
                <Form onSubmit={handleOnSubmit}>
                    <Typography variant='h1'>{getTitleText()}</Typography>

                    <TextField
                        label='Name'
                        value={category?.name}
                        onChange={e => handleCategoryOnChange('name', e.target.value)}
                        required
                    />

                    <TextField
                        label='Description'
                        value={category?.description}
                        onChange={e => handleCategoryOnChange('description', e.target.value)}
                        multiline
                        rows={4}
                    />

                    <Autocomplete
                        label='Variants'
                        options={variants}
                        value={category.variants}
                        onChange={(_, newValue) => handleCategoryOnChange('variants', newValue)}
                        multiple
                        required={category.variants.length === 0}
                        disableCloseOnSelect
                        isOptionEqualToValue={(option, value) => option.id === value.id}
                    />

                    <Button type='submit' variant='contained'>
                        Save
                    </Button>
                </Form>
            </Container>
        </Main>
    )
}
