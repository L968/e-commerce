import api from '@/services/api';
import { Form } from '../../styles';
import { BaseFormProps } from '../..';
import { toast } from 'react-toastify';
import { LoadingButton } from '@mui/lab';
import Autocomplete from '@/components/Autocomplete';
import { TextField, FormControlLabel, Checkbox } from '@mui/material';
import { Dispatch, FormEvent, SetStateAction, useEffect, useState } from 'react';
import CreateProductRequest from '@/interfaces/api/requests/CreateProductRequest';
import GetCategoryResponse from '@/interfaces/api/responses/GetCategoriesResponse';

interface BasicDataProps extends BaseFormProps {
    setProductId: Dispatch<SetStateAction<string>>
}

interface ProductFormData {
    name: string
    description: string
    category: GetCategoryResponse
    active: boolean
    visible: boolean
}

export default function BasicData({ next, setProductId }: BasicDataProps) {
    const [categories, setCategories] = useState<GetCategoryResponse[]>([]);
    const [loadingCategories, setLoadingCategories] = useState<boolean>(false);
    const [loadingCreateProduct, setLoadingCreatingProduct] = useState<boolean>(false);
    const [productData, setProductData] = useState<ProductFormData>({ name: '', description: '', category: { id: '', name: '', description: '', variants: [] }, active: false, visible: false });

    useEffect(() => {
        setLoadingCategories(true);

        api.get<GetCategoryResponse[]>('/productCategory')
            .then(response => setCategories(response.data))
            .catch(error => toast.error('Error 500'))
            .finally(() => setLoadingCategories(false));
    }, []);

    function updateProductState(name: string, value: any): void {
        setProductData(prev => ({
            ...prev,
            [name]: value,
        }));
    }

    function handleCreateProduct(e: FormEvent<HTMLFormElement>): void {
        e.preventDefault();

        const data: CreateProductRequest = {
            name: productData.name,
            description: productData.description,
            productCategoryId: productData.category.id,
            active: true,
            visible: true,
        }

        setLoadingCreatingProduct(true);

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
            .finally(() => setLoadingCreatingProduct(false));
    }

    return (
        <Form onSubmit={handleCreateProduct}>
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
                loading={loadingCreateProduct}
            >
                Create
            </LoadingButton>
        </Form>
    )
}
