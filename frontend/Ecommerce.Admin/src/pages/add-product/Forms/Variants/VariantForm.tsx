import { useEffect, useState } from 'react';
import { Container, Header } from './styles';
import { IconButton } from '@mui/material';
import SelectChip from '@/components/SelectChip';
import CloseIcon from '@mui/icons-material/Close';
import Autocomplete from '@/components/Autocomplete';
import GetVariantsResponse, { Option } from '@/interfaces/api/responses/GetVariantsResponse';

interface VariantFormProps {
    formKey: string
    variants: GetVariantsResponse[];
    onChange: (variantId: number, options: Option[]) => void;
    onRemove: (formKey: string, variantId?: number) => void,
}

export default function VariantForm({ formKey, variants, onChange, onRemove }: VariantFormProps) {
    const [selectedVariant, setSelectedVariant] = useState<GetVariantsResponse>();
    const [selectedOptions, setSelectedOptions] = useState<Option[]>([]);

    useEffect(() => {
        setSelectedOptions([]);
    }, [selectedVariant]);

    useEffect(() => {
        if (selectedVariant) {
            onChange(selectedVariant.id, selectedOptions);
        }
    }, [selectedOptions]);

    return (
        <Container>
            <Header>
                {selectedVariant && <span>Variant: {selectedVariant.name}</span>}

                <IconButton onClick={() => onRemove(formKey, selectedVariant?.id)} sx={{ marginLeft: 'auto' }}>
                    <CloseIcon />
                </IconButton>
            </Header>

            <Autocomplete
                label='Variant'
                required
                options={variants}
                value={selectedVariant}
                onChange={(_, newValue) => setSelectedVariant(newValue)}
            />

            {selectedVariant &&
                <SelectChip
                    label='Options'
                    required
                    labelProperty='name'
                    options={selectedVariant.options}
                    selectedValues={selectedOptions}
                    onChange={setSelectedOptions}
                />
            }
        </Container>
    )
}
