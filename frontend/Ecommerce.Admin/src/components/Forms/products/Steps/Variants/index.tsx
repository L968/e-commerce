import api from '@/services/api';;
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
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline'

export default function Variants() {
    const router = useRouter();
    const [combinationForms, setCombinationForms] = useState<{ key: string; crudType: CrudType; form: JSX.Element }[]>([]);
    const [combinationsData, setCombinationsData] = useState<{ [key: string]: CombinationFormData }>({});

    const {
        back,
        crudType,
        setCrudType,
        productId,
    } = useProductContext();

    useEffect(() => {
        if (!crudType) return;

        if (crudType === 'Create') {
            handleAddVariantForm(crudType);
        } else if (crudType === 'Update') {

        }
    }, []);

    function handleAddVariantForm(crudType: CrudType): void {
        const formKey = String(Date.now());

        const newForm = (
            <VariantForm
                key={formKey}
                formKey={formKey}
                onDataChange={onDataChange}
                onRemove={handleRemoveCombinationForm}
            />
        );

        setCombinationForms(prevForms =>
            [...prevForms, {
                key: formKey,
                crudType: crudType,
                form: newForm
            }]);
    }

    function onDataChange(formKey: string, data: CombinationFormData): void {
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

    async function handleOnSubmit(e: FormEvent<HTMLFormElement>): Promise<void> {
        e.preventDefault();

        const combinationArray = Object.entries(combinationsData);

        if (combinationArray.some(([key, c]) => c.images.length === 0)) {
            toast.warning('At least one variant has no images');
            return;
        }

        try {
            await Promise.all(combinationArray.map(([key, combination]) => {
                const formData = getFormData(combination);
                const combinationForm = combinationForms.find(form => form.key === key)!;
                sendCombination(formData, combinationForm.crudType);
            }));

            router.push('/products');
        } catch (error) {
            console.error('Error during combination submission:', error);
            toast.error('Error 500');
        }
    }

    async function sendCombination(formData: FormData, crudType: CrudType): Promise<void> {
        try {
            if (crudType === 'Create') {
                await api.post(`/product/${productId}/add-combination`, formData);
            } else if (crudType === 'Update') {
                await api.put(`/product/${productId}/add-combination`, formData);
            }
        } catch (error) {
            console.error('Error during combination submission: ', error);
            throw new Error();
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
                {combinationForms.map(form => form.form)}
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
