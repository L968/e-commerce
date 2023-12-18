import api from '@/services/api';;
import { Button } from '@mui/material';
import { toast } from 'react-toastify';
import { LoadingButton } from '@mui/lab';
import { Form, CombinationsContainer } from './styles';
import { FormEvent, useEffect, useState } from 'react';
import VariantForm, { CombinationFormData } from './VariantsForm';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline'
import { useRouter } from 'next/router';

interface VariantsProps {
    productId: string
    productCategoryId: string
}

export default function Variants({ productId, productCategoryId }: VariantsProps) {
    const router = useRouter();
    const [combinationForms, setCombinationForms] = useState<{ key: string; form: JSX.Element }[]>([]);
    const [combinationsData, setCombinationsData] = useState<{ [key: string]: CombinationFormData }>({});

    useEffect(() => {
        handleAddVariantForm();
    }, []);

    function handleAddVariantForm(): void {
        const formKey = String(Date.now());

        const newForm = (
            <VariantForm
                key={formKey}
                formKey={formKey}
                productCategoryId={productCategoryId}
                onDataChange={onDataChange}
                onRemove={handleRemoveCombinationForm}
            />
        );

        setCombinationForms(prevForms => [...prevForms, { key: formKey, form: newForm }]);
    }

    function onDataChange(formKey: string, data: CombinationFormData) {
        setCombinationsData(prevData => ({
            ...prevData,
            [formKey]: data,
        }));
    }

    function handleRemoveCombinationForm(formKey: string): void {
        setCombinationsData(prevData => {
            const { [formKey]: _, ...newData } = prevData;
            return newData;
        });

        setCombinationForms(prevForms =>
            prevForms.length > 1
                ? prevForms.filter(form => form.key !== formKey)
                : prevForms
        );
    }

    async function handleOnSubmit(e: FormEvent<HTMLFormElement>): Promise<void> {
        e.preventDefault();

        const combinationArray = Object.values(combinationsData);

        if (combinationArray.some(c => c.images.length === 0)) {
            toast.warning('At least one variant has no images');
            return;
        }

        try {
            await Promise.all(combinationArray.map(sendCombination));
            router.push('/products');
        } catch (error) {
            console.error('Error during combination submission:', error);
            toast.error('Error submitting combinations');
        }
    }

    async function sendCombination(combination: CombinationFormData) {
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

        try {
            await api.post(`/product/${productId}/add-combination`, formData);
        } catch (error) {
            console.error('Error during combination submission:', error);
            throw new Error('Error 500');
        }
    }

    return (
        <Form onSubmit={handleOnSubmit}>
            <CombinationsContainer>
                {combinationForms.map(form => form.form)}
            </CombinationsContainer>


            <Button
                variant='outlined'
                color='info'
                onClick={handleAddVariantForm}
                endIcon={<AddCircleOutlineIcon />}
            >
                Add Combination
            </Button>

            <LoadingButton type='submit' variant='contained'>
                Next
            </LoadingButton>
        </Form>
    )
}
