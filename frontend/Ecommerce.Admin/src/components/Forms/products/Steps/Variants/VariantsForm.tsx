import api from '@/services/api';
import { toast } from 'react-toastify';
import { useEffect, useState } from 'react';
import { Container, Header } from './styles';
import Dropzone from '@/components/Dropzone';
import CloseIcon from '@mui/icons-material/Close';
import { useProductContext } from '../../ProductContext';
import DeleteConfirmationDialog from './DeleteConfirmationDialog';
import { Combination } from '@/interfaces/api/responses/GetProductAdminResponse';
import { Divider, FormControl, Grid, IconButton, InputLabel, MenuItem, Select, TextField } from '@mui/material';
import GetProductCategoryVariantsResponse, { VariantOption } from '@/interfaces/api/responses/GetProductCategoryVariantsResponse';

interface VariantFormProps {
    formKey: string
    defaultData?: Combination
    onDataChange: (formKey: string, data: CombinationFormData) => void
    onRemove: (formKey: string, variantId?: number) => void
}

export interface CombinationFormData {
    id?: string
    sku: string
    price: string
    stock: string
    images: File[]
    variants: { [key: string]: VariantOption }
    length: string
    width: string
    height: string
    weight: string
}

export default function VariantForm({ formKey, defaultData, onDataChange, onRemove }: VariantFormProps) {
    const { productCategoryId } = useProductContext();
    const [deleteConfirmationOpen, setDeleteConfirmationOpen] = useState(false);
    const [variants, setVariants] = useState<GetProductCategoryVariantsResponse[]>([]);
    const [combinationData, setCombinationData] = useState<CombinationFormData>({
        sku: '',
        price: '',
        stock: '',
        images: [],
        variants: {},
        length: '',
        width: '',
        height: '',
        weight: ''
    });

    useEffect(() => {
        api.get<GetProductCategoryVariantsResponse[]>(`/productCategory/${productCategoryId}/variants`)
            .then(response => setVariants(response.data))
            .catch(error => toast.error('Error 500'));
    }, []);

    useEffect(() => {
        setDefaultData();
    }, [variants]);

    useEffect(() => {
        onDataChange(formKey, combinationData);
    }, [combinationData]);

    function updateCombinationState(name: string, value: any): void {
        setCombinationData(prev => ({
            ...prev,
            [name]: value,
        }));
    }

    async function setDefaultData(): Promise<void> {
        if (!defaultData) return;
        if (variants.length === 0) return;

        const files: File[] = [];

        for (const image of defaultData.images) {
            files.push(await createFile(image))
        }

        setCombinationData({
            id: defaultData.id,
            sku: defaultData.sku,
            price: defaultData.price.toString(),
            stock: defaultData.stock.toString(),
            images: files,
            variants: parseCombinationString(defaultData.combinationString, variants),
            length: defaultData.length.toString(),
            width: defaultData.width.toString(),
            height: defaultData.height.toString(),
            weight: defaultData.weight.toString()
        })
    }

    function setFiles(files: File[]) {
        updateCombinationState('images', files);
    }

    function handleVariantChange(key: string, value: VariantOption[]): void {
        updateCombinationState('variants', {
            ...combinationData.variants,
            [key]: value,
        });
    }

    function handleOnDropImage(files: File[]): void {
        setCombinationData(prev => ({
            ...prev,
            'images': [
                ...prev.images,
                ...files
            ]
        }));
    }

    function handleOnRemove(): void {
        if (combinationData.id) {
            setDeleteConfirmationOpen(true);
        } else {
            onRemove(formKey);
        }
    }

    function handleConfirmDelete(): void {
        api.delete(`/productCombination/${combinationData.id}`)
            .then(response => {
                toast.success('Variant deleted successfully');
                onRemove(formKey);
            })
            .catch(error => toast.error('Error 500'));
    }

    function parseCombinationString(combinationString: string, variants: GetProductCategoryVariantsResponse[]): { [key: string]: VariantOption } {
        const variantOptions = combinationString.split('/').map(option => option.trim());

        const parsedVariants: { [key: string]: VariantOption } = {};
        variantOptions.forEach(option => {
            const [variantName, variantValue] = option.split('=').map(part => part.trim());

            const foundVariant = variants.find(v => v.name === variantName);

            if (foundVariant) {
                const foundOption = foundVariant.options.find(o => o.name === variantValue);
                if (foundOption) {
                    parsedVariants[variantName] = foundOption;
                }
            }
        });

        return parsedVariants;
    }

    async function createFile(url: string): Promise<File> {
        const extension = url.split('.').pop();
        const response = await fetch(url);
        const data = await response.blob();
        const metadata = { type: 'image/*' };
        const fileName = `test.${extension}`;

        return new File([data], fileName, metadata);
    }

    function getTitleText(): string {
        if (defaultData) {
            return 'Edit Variant';
        } else {
            return 'New Variant';
        }
    }

    return (
        <Container>
            <Header>
                <h1>Variant</h1>

                <IconButton onClick={handleOnRemove} sx={{ marginLeft: 'auto' }}>
                    <CloseIcon />
                </IconButton>
            </Header>

            <Grid container spacing={3} sx={{ width: '570px' }}>
                <Grid item xs={12}>
                    <h3>{getTitleText()}</h3>
                </Grid>

                <Grid container item xs={12} spacing={3} marginBottom={5}>
                    {variants.map(variant =>
                        <Grid key={variant.name} item xs={6}>
                            <FormControl fullWidth>
                                <InputLabel>{variant.name}</InputLabel>
                                <Select
                                    value={combinationData.variants[variant.name] || ''}
                                    label={variant.name}
                                    required
                                    onChange={e => handleVariantChange(variant.name, e.target.value as any)}
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
                        disabled={!!defaultData}
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

            <DeleteConfirmationDialog
                open={deleteConfirmationOpen}
                onClose={() => setDeleteConfirmationOpen(false)}
                onConfirm={handleConfirmDelete}
            />
        </Container>
    )
}
