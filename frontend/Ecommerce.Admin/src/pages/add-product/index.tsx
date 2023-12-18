import { useState } from 'react';
import BasicData from './Forms/BasicData';
import Variants from './Forms/Variants';
import { Container, Main } from './styles';
import { Step, StepLabel, Stepper, Typography } from '@mui/material';

const steps = ['Basic Data', 'Variants']

export interface BaseFormProps {
    next: () => void
}

export default function AddProduct() {
    const [activeStep, setActiveStep] = useState<number>(0);
    const [productId, setProductId] = useState<string>('');
    const [productCategoryId, setProductCategoryId] = useState<string>('');

    function next(): void {
        setActiveStep(activeStep + 1);
    }

    function renderStepContent(): JSX.Element {
        switch (activeStep) {
            case 0:
                return <BasicData next={next} setProductId={setProductId} setProductCategoryId={setProductCategoryId} />
            case 1:
                if (!productId) return <div>Product Id not found</div>;
                return <Variants productId={productId} productCategoryId={productCategoryId} />
            default:
                return <div>Not Found</div>
        }
    }

    return (
        <Main>
            <Typography variant='h1'>Create Product</Typography>

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
