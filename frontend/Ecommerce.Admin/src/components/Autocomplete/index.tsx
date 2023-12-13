import { CircularProgress, TextField } from '@mui/material';
import MuiAutocomplete, { AutocompleteProps } from '@mui/material/Autocomplete';

export interface CustomAutocompleteProps extends Omit<AutocompleteProps<any, boolean, boolean, boolean, any>, 'renderInput'> {
    label: string
    required?: boolean
}

export default function Autocomplete({ label, required, value, loading, ...rest }: CustomAutocompleteProps) {
    return (
        <MuiAutocomplete
            {...rest}
            getOptionLabel={option => option.name}
            value={value || null}
            renderInput={(params =>
                <TextField
                    {...params}
                    label={label}
                    required={required}
                    InputProps={{
                        ...params.InputProps,
                        endAdornment: (
                            <>
                                {loading && <CircularProgress color='inherit' size={20} />}
                                {params.InputProps.endAdornment}
                            </>
                        )
                    }}
                />
            )}
        />
    )
}
