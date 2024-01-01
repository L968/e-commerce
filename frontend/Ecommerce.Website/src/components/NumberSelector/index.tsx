import AddIcon from '@mui/icons-material/Add';
import RemoveIcon from '@mui/icons-material/Remove';
import { IconButton } from '@mui/material';
import { Container, Input, Value } from './styles';

interface NumberSelectorProps {
    cartItemId: number
    value: number
    setValue: (cartItemId: number, newQuantity: number) => void
}

export default function NumberSelector({ cartItemId, value, setValue }: NumberSelectorProps) {
    function handleIncrement() {
        setValue(cartItemId, value + 1);
    }

    function handleDecrement() {
        setValue(cartItemId, Math.max(value - 1, 0));
    }

    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const newValue = parseInt(event.target.value, 10) || 1;
        setValue(cartItemId, newValue);
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
