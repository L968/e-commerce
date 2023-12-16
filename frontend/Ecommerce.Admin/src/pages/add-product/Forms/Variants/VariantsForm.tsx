import api from '@/services/api';
import { toast } from 'react-toastify';
import { useEffect, useState } from 'react';
import { Container, Header } from './styles';
import Dropzone from '@/components/Dropzone';
import SelectChip from '@/components/SelectChip';
import CloseIcon from '@mui/icons-material/Close';
import Autocomplete from '@/components/Autocomplete';
import { Divider, FormControl, Grid, IconButton, InputLabel, MenuItem, Select, TextField } from '@mui/material';
import GetVariantsResponse, { Option } from '@/interfaces/api/responses/GetVariantsResponse';
import GetProductCategoryVariantsResponse, { VariantOption } from '@/interfaces/api/responses/GetProductCategoryVariantsResponse';

interface CombinationFormProps {
    formKey: string
    productCategoryId: string
    //onChange: (variantId: number, options: Option[]) => void
    onRemove: (formKey: string, variantId?: number) => void
}

interface CombinationFormData {
    sku: string
    price: string
    stock: string
    images: File[]
    length: string
    width: string
    height: string
    weight: string
}

export default function CombinationForm({ formKey, productCategoryId, onRemove }: CombinationFormProps) {
    const [variants, setVariants] = useState<GetProductCategoryVariantsResponse[]>([]);
    const [selectedVariantOptions, setSelectedVariantOptions] = useState<{ [key: string]: VariantOption[] }>({});
    const [combinationData, setCombinationData] = useState<CombinationFormData>({ sku: '', price: '', stock: '', images: [], length: '', width: '', height: '', weight: '' });

    useEffect(() => {
        api.get<GetProductCategoryVariantsResponse[]>(`/productCategory/${productCategoryId}/variants`)
            .then(response => setVariants(response.data))
            .catch(error => toast.error('Error 500'));
    }, []);

    function updateCombinationState(name: string, value: any): void {
        setCombinationData(prev => ({
            ...prev,
            [name]: value,
        }));
    }

    function setFiles(files: File[]) {
        updateCombinationState('images', files);
    }

    function handleOptionChange(name: string, value: VariantOption[]) {
        setSelectedVariantOptions((prevOptions) => ({
            ...prevOptions,
            [name]: value,
        }));
    }

    function handleOnDropImage(files: File[]) {
        setCombinationData(prev => ({
            ...prev,
            'images': [
                ...prev.images,
                ...files
            ]
        }));
    }

    return (
        <Container>
            <Header>
                <h1>Variant</h1>

                <IconButton onClick={() => onRemove(formKey)} sx={{ marginLeft: 'auto' }}>
                    <CloseIcon />
                </IconButton>
            </Header>

            <Grid container spacing={3} sx={{ width: '570px' }}>
                <Grid item xs={12}>
                    <h3>New Variant</h3>
                </Grid>

                <Grid container item xs={12} spacing={3} marginBottom={5}>
                    {variants.map(variant =>
                        <Grid key={variant.name} item xs={6}>
                            <FormControl fullWidth>
                                <InputLabel>{variant.name}</InputLabel>
                                <Select
                                    value={selectedVariantOptions[variant.name] || ''}
                                    label={variant.name}
                                    onChange={e => handleOptionChange(variant.name, e.target.value as any)}
                                >
                                    {variant.options.map(option =>
                                        <MenuItem key={option.id} value={option as any}>{option.name}</MenuItem>
                                    )}
                                </Select>
                            </FormControl>
                        </Grid>
                    )}
                </Grid>

                <Grid item xs={6}>
                    <TextField
                        label='SKU'
                        required
                        fullWidth
                        value={combinationData?.sku}
                        onChange={e => updateCombinationState('sku', e.target.value)}
                    />
                </Grid>

                <Grid item xs={6}>
                    <TextField
                        label='Price'
                        required
                        fullWidth
                        value={combinationData?.price}
                        onChange={e => updateCombinationState('price', e.target.value)}
                    />
                </Grid>

                <Grid item xs={6}>
                    <TextField
                        label='Stock'
                        required
                        fullWidth
                        value={combinationData?.stock}
                        onChange={e => updateCombinationState('stock', e.target.value)}
                    />
                </Grid>

                <Grid item xs={12}>
                    <Divider />
                </Grid>

                <Grid item xs={12}>
                    <h3>Images</h3>
                </Grid>

                <Grid item xs={12}>
                    <Dropzone
                        files={combinationData.images}
                        setFiles={setFiles}
                        onDrop={handleOnDropImage}
                    />
                </Grid>

                <Grid item xs={12}>
                    <Divider />
                </Grid>

                <Grid item xs={12}>
                    <h3>Dimensions</h3>
                </Grid>

                <Grid item xs={6}>
                    <TextField
                        label='Length'
                        required
                        value={combinationData?.length}
                        onChange={e => updateCombinationState('length', e.target.value)}
                    />
                </Grid>
                <Grid item xs={6}>
                    <TextField
                        label='Width'
                        required
                        value={combinationData?.width}
                        onChange={e => updateCombinationState('width', e.target.value)}
                    />
                </Grid>
                <Grid item xs={6}>
                    <TextField
                        label='Height'
                        required
                        value={combinationData?.height}
                        onChange={e => updateCombinationState('height', e.target.value)}
                    />
                </Grid>
                <Grid item xs={6}>
                    <TextField
                        label='Weight'
                        required
                        value={combinationData?.weight}
                        onChange={e => updateCombinationState('weight', e.target.value)}
                    />
                </Grid>
            </Grid>
        </Container>
    )
}
