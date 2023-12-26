import api from '@/services/api';
import { toast } from 'react-toastify';
import { useRouter } from 'next/router';
import { LoadingButton } from '@mui/lab';
import CrudType from '@/interfaces/CrudType';
import { Button, IconButton } from '@mui/material';
import { Form, CombinationsContainer } from './styles';
import { FormEvent, useEffect, useState } from 'react';
import { useProductContext } from '../../ProductContext';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import VariantForm, { CombinationFormData } from './VariantsForm';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import { Combination } from '@/interfaces/api/responses/GetProductAdminResponse';

export default function Variants() {
    const router = useRouter();
    const [variantForms, setVariantForms] = useState<{ key: string; crudType: CrudType; form: JSX.Element }[]>([]);
    const [combinationsData, setCombinationsData] = useState<{ [key: string]: CombinationFormData }>({});

    const {
        back,
        crudType,
        setCrudType,
        productId,
        originalProductData,
    } = useProductContext();

    useEffect(() => {
        if (!crudType) return;

        if (crudType === 'Create') {
            handleAddVariantForm(crudType);
        } else if (crudType === 'Update') {
            const { combinations } = originalProductData!;

            if (combinations.length === 0) {
                handleAddVariantForm('Create');
                return;
            }

            combinations.map(combination => handleAddVariantForm('Update', combination));
        }
    }, []);

    function onDataChange(formKey: string, data: CombinationFormData): void {
        setCombinationsData(prevData => ({
            ...prevData,
            [formKey]: data,
        }));
    }

    function getFormData(combination: CombinationFormData): FormData {
        const formData = new FormData();

        Object.entries(combination).map(([key, value]) => {
            switch (key) {
                case 'variants':
                    Object.values(value).map((variant: any) => formData.append('variantOptionIds', variant.id));
                    break;
                case 'images':
                    (value as File[]).map(file => formData.append(key, file));
                    break;
                default:
                    formData.append(key, value);
            }
        })

        return formData;
    }

    function getButtonText(): string {
        switch (crudType) {
            case 'Create': return 'Create';
            case 'Update': return 'Save';
            default: return '';
        }
    }

    function generateUniqueKey(): string {
        const timestamp = Date.now().toString(36);
        const randomString = Math.random().toString(36).substr(2, 5);
        return `${timestamp}${randomString}`;
    }

    function handleAddVariantForm(crudType: CrudType, combination?: Combination): void {
        const formKey = generateUniqueKey();

        const newForm = (
            <VariantForm
                key={formKey}
                defaultData={combination}
                formKey={formKey}
                onDataChange={onDataChange}
                onRemove={handleRemoveVariantForm}
            />
        );

        setVariantForms(prevForms =>
            [...prevForms, {
                key: formKey,
                crudType: crudType,
                form: newForm
            }]);
    }

    function handleRemoveVariantForm(formKey: string): void {
        setCombinationsData(prevData => {
            const { [formKey]: _, ...newData } = prevData;
            return newData;
        });

        setVariantForms(prevForms => prevForms.filter(form => form.key !== formKey));
    }

    async function handleOnSubmit(e: FormEvent<HTMLFormElement>): Promise<void> {
        e.preventDefault();

        const combinationArray = Object.entries(combinationsData);

        if (combinationArray.length === 0) {
            toast.warning('At least one variant is required');
            return;
        }

        if (combinationArray.some(([key, c]) => c.images.length === 0)) {
            toast.warning('At least one variant has no images');
            return;
        }

        const results = await Promise.allSettled(combinationArray.map(async ([key, combination]) => {
            const formData = getFormData(combination);
            const combinationForm = variantForms.find(form => form.key === key)!;
            await sendCombination(formData, combinationForm.crudType);
        }));

        if (!results.some(result => result.status === 'rejected')) {
            toast.success('Product saved succesfully');
            router.push('/products');
        }
    }

    async function sendCombination(formData: FormData, crudType: CrudType): Promise<void> {
        try {
            if (crudType === 'Create') {
                formData.append('productId', productId);
                await api.post('/productCombination', formData);
            } else if (crudType === 'Update') {
                const productCombinationId = formData.get('id');
                await api.put(`/productCombination/${productCombinationId}`, formData);
            }
        } catch (error) {
            console.error('Error during combination submission:', error);
            toast.error('Error 500');
            throw error;
        }
    }

    return (
        <Form onSubmit={handleOnSubmit}>
            <div>
                <IconButton onClick={() => {
                    back();
                    setCrudType('Update');
                }}>
                    <ChevronLeftIcon />
                </IconButton>
            </div>

            <CombinationsContainer>
                {variantForms.map(form => form.form)}
            </CombinationsContainer>

            <Button
                variant='outlined'
                color='info'
                onClick={() => handleAddVariantForm('Create')}
                endIcon={<AddCircleOutlineIcon />}
            >
                Add Combination
            </Button>

            <LoadingButton type='submit' variant='contained'>
                {getButtonText()}
            </LoadingButton>
        </Form>
    )
}
