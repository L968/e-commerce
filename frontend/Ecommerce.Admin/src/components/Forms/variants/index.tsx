import api from '@/services/api';
import { toast } from 'react-toastify';
import { useRouter } from 'next/router';
import BaseForm from '@/interfaces/BaseForm';
import AddIcon from '@mui/icons-material/Add';
import { CrudType } from '@/interfaces/CrudType';
import RemoveIcon from '@mui/icons-material/Remove';
import { FormEvent, useEffect, useState, ChangeEvent } from 'react';
import { Container, Form, Main, StyledList, StyledListItem } from './styles';
import GetVariantsResponse from '@/interfaces/api/responses/GetVariantsResponse';
import UpdateVariantRequest from '@/interfaces/api/requests/variants/UpdateVariantRequest';
import { Button, IconButton, InputAdornment, ListItemText, TextField } from '@mui/material';
import CreateVariantRequest from '@/interfaces/api/requests/variants/CreateVariantRequest';

export default function VariantForm({ crudType }: BaseForm) {
    const router = useRouter();
    const [newOption, setNewOption] = useState<string>('');
    const [variant, setVariant] = useState<GetVariantsResponse>({ id: 0, name: '', options: [] });

    useEffect(() => {
        if (crudType === CrudType.Create) return;

        const variantId = router.query.id;

        if (!variantId) return;

        api.get<GetVariantsResponse>(`/variant/${variantId}`)
            .then(response => setVariant(response.data))
            .catch(error => toast.error('Error 500'));
    }, [router.query]);

    function handleOnSubmit(e: FormEvent<HTMLFormElement>): void {
        e.preventDefault();

        if (variant.options.length === 0) {
            toast.warning('At least one option is required');
            return;
        }

        if (crudType === CrudType.Update) {
            const data: UpdateVariantRequest = {
                name: variant.name,
                options: variant.options.map(o => o.name),
            }

            api.put(`/variant/${variant.id}`, data)
                .then(_ => {
                    toast.success('Variant updated successfully')
                    router.push('/variants');
                })
                .catch(error => {
                    if (error.response?.status === 400) {
                        toast.warning(error.response.data);
                        return;
                    }

                    toast.error('Error 500')
                });
        } else if (crudType === CrudType.Create) {
            const data: CreateVariantRequest = {
                name: variant.name,
                options: variant.options.map(o => o.name),
            }

            api.post('/variant/', data)
                .then(_ => {
                    toast.success('Variant created successfully')
                    router.push('/variants');
                })
                .catch(error => {
                    if (error.response?.status === 400) {
                        toast.warning(error.response.data);
                        return;
                    }

                    toast.error('Error 500')
                });
        }
    }

    function handleCategoryOnChange(name: string, value: any): void {
        setVariant(prev => ({
            ...prev,
            [name]: value
        }));
    }

    function handleAddOption(): void {
        if (newOption.trim() === '') return;

        const isOptionExists = variant.options.some((option) => option.name.toLowerCase() === newOption.trim().toLowerCase());

        if (isOptionExists) {
            toast.warning('Option with the same text already exists');
            return;
        }

        setVariant((prev) => ({
            ...prev,
            options: [...prev.options, { id: Date.now(), name: newOption.trim() }],
        }));

        setNewOption('');
    }

    function handleRemoveOption(optionId: number): void {
        setVariant(prev => ({
            ...prev,
            options: prev.options.filter(option => option.id !== optionId),
        }));
    }

    function renderTitle(): string {
        switch (crudType) {
            case CrudType.Create: return'Create Variant'
            case CrudType.Update: return'Edit Variant'
            default: return '';
        }
    }

    return (
        <Main>
            <Container>
                <Form onSubmit={handleOnSubmit}>
                    <h1>{renderTitle()}</h1>

                    <TextField
                        label='Name'
                        value={variant?.name}
                        onChange={e => handleCategoryOnChange('name', e.target.value)}
                        required
                    />

                    <h2>Options</h2>

                    <TextField
                        label='New Option'
                        value={newOption}
                        onChange={(e: ChangeEvent<HTMLInputElement>) => setNewOption(e.target.value)}
                        InputProps={{
                            endAdornment: (
                                <InputAdornment position="end">
                                    <IconButton
                                        aria-label='add'
                                        onClick={handleAddOption}
                                    >
                                        <AddIcon />
                                    </IconButton>
                                </InputAdornment>
                            ),
                        }}
                    />

                    <StyledList disablePadding>
                        {variant.options.map(option => (
                            <StyledListItem key={option.id}>
                                <ListItemText primary={option.name} />

                                <IconButton
                                    edge='end'
                                    color='error'
                                    aria-label='delete'
                                    onClick={() => handleRemoveOption(option.id)}
                                >
                                    <RemoveIcon />
                                </IconButton>
                            </StyledListItem>
                        ))}
                    </StyledList>

                    <Button type='submit' variant='contained'>
                        Save
                    </Button>
                </Form>
            </Container>
        </Main>
    )
}
