import Option from '@/interfaces/Option';
import Variant from '@/interfaces/Variant';
import { Stack } from '@mui/material';
import { Chip, Title, TitleContainer } from './styles';

interface VariantSelectorProps {
    variant: Variant
    selectedVariant?: Option
    onSelectVariant: (variant: Option) => void
    validOptions: Option[];
}

export default function VariantSelector({ variant, selectedVariant, onSelectVariant, validOptions, }: VariantSelectorProps) {
    return (
        <div>
            <TitleContainer>
                <Title>{variant.name}:</Title> {selectedVariant?.name}
            </TitleContainer>
            <Stack direction="row" spacing={1}>
                {variant.options.map((option) => {
                    const isSelected = selectedVariant?.id === option.id;
                    const isValidOption = validOptions.some((validOption) => validOption.id === option.id);

                    return (
                        <Chip
                            key={option.id}
                            label={option.name}
                            onClick={() => isValidOption && onSelectVariant(option)}
                            variant={isSelected ? 'filled' : 'outlined'}
                            isValidOption={isValidOption}
                            isSelected={isSelected}
                        />
                    );
                })}
            </Stack>
        </div>
    )
}
