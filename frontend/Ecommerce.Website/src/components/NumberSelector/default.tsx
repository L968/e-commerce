import { IconButton } from '@mui/material';
import { Container, Input, Value } from './styles';
import AddIcon from '@mui/icons-material/Add';
import RemoveIcon from '@mui/icons-material/Remove';

interface NumberSelectorProps {
    value: number
    setValue: (newQuantity: number) => void
}

export default function NumberSelector({ value, setValue }: NumberSelectorProps) {
    function handleIncrement() {
        setValue(value + 1);
    }

    function handleDecrement() {
        setValue(Math.max(value - 1, 0));
    }

    function handleInputChange(event: React.ChangeEvent<HTMLInputElement>) {
        const newValue = parseInt(event.target.value, 10) || 1;
        setValue(newValue);
    }

    return (
        <Container>
            <div>
                <IconButton
                    size='small'
                    onClick={handleDecrement}
                    disabled={value <= 1}
                >
                    <RemoveIcon fontSize='small' style={{ color: value === 1 ? '#BDBDBD' : '#3483FA' }} />
                </IconButton>
            </div>
            <Value>
                <Input
                    type="number"
                    value={value}
                    onChange={handleInputChange}
                    disableUnderline
                    inputProps={{ min: 0, style: { textAlign: 'center' } }}
                />
            </Value>
            <div>
                <IconButton
                    size='small'
                    onClick={handleIncrement}
                >
                    <AddIcon fontSize='small' style={{ color: '#3483FA' }} />
                </IconButton>
            </div>
        </Container>
    )
}
