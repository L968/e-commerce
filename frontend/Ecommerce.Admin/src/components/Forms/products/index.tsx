import { useEffect } from 'react';
import Variants from './Steps/Variants';
import BasicData from './Steps/BasicData';
import { Container, Main } from './styles';
import BaseForm from '@/interfaces/BaseForm';
import { Step, StepLabel, Stepper, Typography } from '@mui/material';
import { ProductProvider, useProductContext } from './ProductContext';

const steps = ['Basic Data', 'Variants'];

export default function ProductForm({ crudType }: BaseForm) {
    return (
        <ProductProvider>
            <Component crudType={crudType} />
        </ProductProvider>
    )
}

function Component({ crudType }: BaseForm) {
    const { activeStep, setCrudType, productId } = useProductContext();

    useEffect(() => {
        setCrudType(crudType);
    }, []);

    function renderStepContent(): JSX.Element {
        switch (activeStep) {
            case 0: return <BasicData />
            case 1:
                if (!productId) return <div>Product Id not found</div>;

                return <Variants />
            default:
                return <div>Not Found</div>
        }
    }

    function getTitleText(): string {
        switch (crudType) {
            case 'Create': return 'Create Product'
            case 'Update': return 'Edit Product'
            default: return '';
        }
    }

    return (
        <Main>
            <Typography variant='h1'>{getTitleText()}</Typography>

            <Container>
                <Stepper activeStep={activeStep} sx={{ marginBottom: '30px' }}>
                    {steps.map((step, i) =>
                        <Step key={i}>
                            <StepLabel>{step}</StepLabel>
                        </Step>
                    )}
                </Stepper>

                {renderStepContent()}
            </Container>
        </Main>
    )
}
