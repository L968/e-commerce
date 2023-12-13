import api from '@/services/api';
import { Form } from '../../styles';
import { toast } from 'react-toastify';
import { Button } from '@mui/material';
import VariantForm from './VariantForm';
import { LoadingButton } from '@mui/lab';
import { VariantsContainer } from './styles';
import { FormEvent, useEffect, useState } from 'react'
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import GetVariantsResponse, { Option } from '@/interfaces/api/responses/GetVariantsResponse'

interface VariantsProps {
    productId: string
}

interface Variant {
    variantId: number
    options: number[]
}

export default function Variants({ productId }: VariantsProps) {
    const [variants, setVariants] = useState<GetVariantsResponse[]>([]);
    const [variantForms, setVariantForms] = useState<{ key: string; form: JSX.Element }[]>([]);
    const [selectedVariantOptionIds, setSelectedVariantOptionIds] = useState<Variant[]>([]);

    useEffect(() => {
        api.get<GetVariantsResponse[]>('/variant')
            .then(response => setVariants(response.data))
            .catch(error => toast.error('Error 500'));
    }, []);

    function handleAddSelectedVariantOptionIds(variantId: number, options: Option[]): void {
        setSelectedVariantOptionIds(prev => {
            const index = prev.findIndex(v => v.variantId === variantId);

            if (index === -1) {
                return [
                    ...prev,
                    {
                        variantId,
                        options: options.map(o => o.id)
                    }
                ]
            } else {
                return [
                    ...prev.slice(0, index),
                    {
                        variantId,
                        options: options.map(o => o.id),
                    },
                    ...prev.slice(index + 1),
                ]
            }
        })
    }

    function handleAddVariantForm(): void {
        const formKey = String(Date.now());

        const newForm = (
            <VariantForm
                key={formKey}
                formKey={formKey}
                onChange={handleAddSelectedVariantOptionIds}
                onRemove={handleRemoveVariantForm}
                variants={variants.filter(v => !selectedVariantOptionIds.some(s => s.variantId === v.id))}
            />
        );

        setVariantForms(prevForms => [...prevForms, { key: formKey, form: newForm }]);
    }

    function handleRemoveVariantForm(formKey: string, variantId?: number): void {
        setVariantForms(prevForms => {
            if (prevForms.length > 1) {
                return prevForms.filter(form => form.key !== formKey)
            } else {
                return prevForms
            }
        });

        setSelectedVariantOptionIds(prev => prev.filter(v => v.variantId !== variantId));
    }

    async function handleOnSubmit(e: FormEvent<HTMLFormElement>): Promise<void> {
        e.preventDefault();
        console.log(selectedVariantOptionIds.map(s => s.options));
    }

    return (
        <Form onSubmit={handleOnSubmit}>
            <VariantsContainer>
                {variantForms.map(form => form.form)}
            </VariantsContainer>

            <Button
                variant='outlined'
                color='info'
                onClick={handleAddVariantForm}
                endIcon={<AddCircleOutlineIcon />}
            >
                Add Variant
            </Button>

            <LoadingButton type='submit' variant='contained'>
                Next
            </LoadingButton>
        </Form>
    )
}
