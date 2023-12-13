import { useState } from 'react';
import Variants from './Forms/Variants';
import BasicData from './Forms/BasicData';
import { Container, Main, Title } from './styles';
import { Step, StepLabel, Stepper } from '@mui/material';

const steps = ['Basic Data', 'Variants']

export interface BaseFormProps {
    next: () => void
}

export default function AddProduct() {
    const [activeStep, setActiveStep] = useState<number>(0);
    const [productId, setProductId] = useState<string>('');

    function next(): void {
        setActiveStep(activeStep + 1);
    }

    function renderStepContent(): JSX.Element {
        switch (activeStep) {
            case 0:
                return <BasicData next={next} setProductId={setProductId} />
            case 1:
                if (!productId) return <div>Product Id not found</div>;
                return <Variants productId={productId} />
            default:
                return <div>Not Found</div>
        }
    }

    return (
        <Main>
            <Title variant='h1'>Create Product</Title>

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
