import { Button } from '@mui/material'
import { LoadingButton } from '@mui/lab'
import { useEffect, useState } from 'react';
import CombinationForm from './VariantsForm';
import { Form, CombinationsContainer } from './styles'
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';

interface VariantsProps {
    productId: string
    productCategoryId: string
}

export default function Variants({ productId, productCategoryId }: VariantsProps) {
    const [combinationForms, setCombinationForms] = useState<{ key: string; form: JSX.Element }[]>([]);

    useEffect(() => {
        handleAddVariantForm();
    }, []);

    function handleAddVariantForm(): void {
        const formKey = String(Date.now());

        const newForm = (
            <CombinationForm
                key={formKey}
                formKey={formKey}
                productCategoryId={productCategoryId}
                onRemove={handleRemoveCombinationForm}
            />
        );

        setCombinationForms(prevForms => [...prevForms, { key: formKey, form: newForm }]);
    }

    function handleRemoveCombinationForm(formKey: string): void {
        setCombinationForms(prevForms =>
            prevForms.length > 1
                ? prevForms.filter(form => form.key !== formKey)
                : prevForms
        );
    }

    return (
        <Form>
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
