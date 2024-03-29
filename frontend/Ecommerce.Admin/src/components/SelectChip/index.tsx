import { ChipsBox } from './styles';
import { FormControl, Select, OutlinedInput, Chip, MenuItem, SelectProps, InputLabel, SelectChangeEvent } from '@mui/material';

export interface SelectChipProps extends Omit<SelectProps, 'onChange'> {
    labelProperty: string
    options: any[]
    selectedValues: any
    onChange: (e: SelectChangeEvent<unknown>) => void;
}

export default function SelectChip({ label, labelProperty, options, selectedValues, onChange, ...rest }: SelectChipProps) {
    return (
        <FormControl fullWidth>
            <InputLabel>{label}</InputLabel>
            <Select
                {...rest}
                multiple
                value={selectedValues}
                onChange={onChange}
                input={<OutlinedInput label={label} />}
                renderValue={(selected) => (
                    <ChipsBox>
                        {(selected as any[]).map((value, i) =>
                            <Chip key={i} label={value[labelProperty]} />
                        )}
                    </ChipsBox>
                )}
            >
                {options.map((option, i) => (
                    <MenuItem key={i} value={option}>
                        {option[labelProperty]}
                    </MenuItem>
                ))}
            </Select>
        </FormControl>
    );
}
