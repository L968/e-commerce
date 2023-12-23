import { Form } from './styles';
import api from '@/services/api';
import { toast } from 'react-toastify';
import { useRouter } from 'next/router';
import { LoadingButton } from '@mui/lab';
import { CrudType } from '@/interfaces/CrudType';
import Autocomplete from '@/components/Autocomplete';
import { FormEvent, useEffect, useState } from 'react';
import { useProductContext } from '../../ProductContext';
import { TextField, FormControlLabel, Checkbox } from '@mui/material';
import GetCategoryResponse from '@/interfaces/api/responses/GetCategoriesResponse';
import GetProductAdminResponse from '@/interfaces/api/responses/GetProductAdminResponse';
import CreateProductRequest from '@/interfaces/api/requests/products/CreateProductRequest';
import UpdateProductRequest from '@/interfaces/api/requests/products/UpdateProductRequest';

interface ProductFormData {
    name: string
    description: string
    category: GetCategoryResponse
    active: boolean
    visible: boolean
}

export default function BasicData() {
    const router = useRouter();
    const {
        crudType,
        next,
        setProductId,
        setProductCategoryId,
    } = useProductContext();

    const [categories, setCategories] = useState<GetCategoryResponse[]>([]);
    const [loadingCategories, setLoadingCategories] = useState<boolean>(false);
    const [loadingSubmit, setLoadingSubmit] = useState<boolean>(false);
    const [productData, setProductData] = useState<ProductFormData>({ name: '', description: '', category: { id: '', name: '', description: '', variants: [] }, active: false, visible: false });

    useEffect(() => {
        setLoadingCategories(true);

        api.get<GetCategoryResponse[]>('/productCategory')
            .then(response => setCategories(response.data))
            .catch(error => toast.error('Error 500'))
            .finally(() => setLoadingCategories(false));
    }, []);

    useEffect(() => {
        if (crudType === 'Create') return;

        const productId = router.query.id;
        if (!productId) return;

        api.get<GetProductAdminResponse>(`/product/${productId}/admin`)
            .then(response => setProductData(response.data))
            .catch(error => toast.error('Error 500'));
    }, [router.query]);

    function updateProductState(name: string, value: any): void {
        setProductData(prev => ({
            ...prev,
            [name]: value,
        }));
    }

    function handleOnSubmit(e: FormEvent<HTMLFormElement>): void {
        e.preventDefault();

        setLoadingSubmit(true);

        setProductCategoryId(productData.category.id);

        if (crudType === 'Create') {
            const data: CreateProductRequest = {
                name: productData.name,
                description: productData.description,
                productCategoryId: productData.category.id,
                active: productData.active,
                visible: productData.visible,
            }

            api.post('/product', data)
                .then(response => {
                    setProductId(response.data.id);
                    toast.success('Product created succesfully');
                    next();
                })
                .catch(error => {
                    if (error.response.status === 400) {
                        toast.warning(error.response.data.message);
                        return;
                    }

                    toast.error('Error 500');
                })
                .finally(() => setLoadingSubmit(false));
        } else if (crudType === 'Update') {
            const data: UpdateProductRequest = {
                name: productData.name,
                description: productData.description,
                productCategoryId: productData.category.id,
                active: productData.active,
                visible: productData.visible,
            }

            const productId = router.query.id;

            api.put(`/product/${productId}`, data)
                .then(response => {
                    setProductId(productId as any);
                    toast.success('Product saved succesfully');
                    next();
                })
                .catch(error => {
                    if (error.response.status === 400) {
                        toast.warning(error.response.data.message);
                        return;
                    }

                    toast.error('Error 500');
                })
                .finally(() => setLoadingSubmit(false));
        }
    }

    function getButtonText(): string {
        switch (crudType) {
            case 'Create': return 'Create';
            case 'Update': return 'Save';
            default: return '';
        }
    }

    return (
        <Form onSubmit={handleOnSubmit}>
            <TextField
                label='Product Title'
                required
                value={productData?.name}
                onChange={e => updateProductState('name', e.target.value)}
            />

            <TextField
                label='Description'
                multiline
                rows={5}
                required
                value={productData?.description}
                onChange={e => updateProductState('description', e.target.value)}
            />

            <Autocomplete
                label='Category'
                required
                options={categories}
                getOptionLabel={option => option.name}
                loading={loadingCategories}
                value={productData.category}
                onChange={(_, newValue) => updateProductState('category', newValue)}
            />

            <div>
                <FormControlLabel
                    label='Active'
                    control={
                        <Checkbox
                            checked={productData.active}
                            onChange={e => updateProductState('active', e.target.checked)}
                        />
                    }
                />

                <FormControlLabel
                    label='Visible'
                    control={
                        <Checkbox
                            checked={productData.visible}
                            onChange={e => updateProductState('visible', e.target.checked)}
                        />
                    }
                />
            </div>

            <LoadingButton
                type='submit'
                variant='contained'
                loading={loadingSubmit}
            >
                {getButtonText()}
            </LoadingButton>
        </Form>
    )
}
