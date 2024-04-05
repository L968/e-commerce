import api from '@/services/api';
import { toast } from 'react-toastify';
import { useRouter } from 'next/router';
import Address from '@/interfaces/Address';
import BaseForm from '@/interfaces/BaseForm';
import { Form, Main, Title } from './styles';
import { Grid, TextField } from '@mui/material';
import LoadingButton from '@mui/lab/LoadingButton';
import PrivateRoute from '@/components/PrivateRoute';
import { FormEvent, useEffect, useState } from 'react';

type AddressFormData = Omit<Address, 'id'>;

export interface AddressFormProps extends BaseForm {
    onClose?: () => void
}

function AddressForm({ crudType, onClose }: AddressFormProps) {
    const router = useRouter();

    const [loadingSubmit, setLoadingSubmit] = useState<boolean>(false);
    const [addressData, setAddressData] = useState<AddressFormData>({
        recipientFullName: '',
        recipientPhoneNumber: '',
        postalCode: '',
        streetName: '',
        buildingNumber: '',
        complement: '',
        neighborhood: '',
        city: '',
        state: '',
        country: '',
        additionalInformation: '',
    });

    useEffect(() => {
        if (crudType === 'Create') return;

        const addressId = router.query.id;
        if (!addressId) return;

        api.get<Address>(`/address/${addressId}`)
            .then(res => setAddressData(res.data))
            .catch(err => toast.error('Error 500'));
    }, [router.query]);

    function updateAddressState(name: string, value: any): void {
        setAddressData(prev => ({
            ...prev,
            [name]: value,
        }));
    }

    function handleOnSubmit(e: FormEvent<HTMLFormElement>): void {
        e.preventDefault();
        setLoadingSubmit(true);

        const onSuccess = () => {
            toast.success('Address saved successfully');
            if (onClose) {
                onClose();
            } else {
                router.push('/profile/addresses');
            }
        }

        const onError = () => {
            toast.error('Error 500');
        }

        if (crudType === 'Create') {
            api.post('/address', addressData)
                .then(res => onSuccess())
                .catch(err => onError())
                .finally(() => setLoadingSubmit(false));
        } else if (crudType === 'Update') {
            const addressId = router.query.id;
            api.put('/address/' + addressId, addressData)
                .then(res => onSuccess())
                .catch(err => onError())
                .finally(() => setLoadingSubmit(false));
        }
    }

    function getTitleText(): string {
        switch (crudType) {
            case 'Create': return 'Create new Address';
            case 'Update': return 'Edit Address';
            default: return '';
        }
    }

    return (
        <Main>
            <Title variant='h1'>{getTitleText()}</Title>

            <Form onSubmit={handleOnSubmit}>
                <Grid container spacing={3.5}>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            label="Recipient's Full Name"
                            required
                            fullWidth
                            value={addressData.recipientFullName}
                            onChange={e => updateAddressState('recipientFullName', e.target.value)}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            label="Recipient's Phone Number"
                            required
                            fullWidth
                            value={addressData.recipientPhoneNumber}
                            onChange={e => {
                                const onlyNums = e.target.value.replace(/[^0-9]/g, '');
                                updateAddressState('recipientPhoneNumber', onlyNums)
                            }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            label='Postal Code'
                            required
                            fullWidth
                            value={addressData.postalCode}
                            onChange={e => updateAddressState('postalCode', e.target.value)}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            label='Street Name'
                            required
                            fullWidth
                            value={addressData.streetName}
                            onChange={e => updateAddressState('streetName', e.target.value)}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            label='Building Number'
                            required
                            fullWidth
                            value={addressData.buildingNumber}
                            onChange={e => updateAddressState('buildingNumber', e.target.value)}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            label='Complement'
                            fullWidth
                            value={addressData.complement}
                            onChange={e => updateAddressState('complement', e.target.value)}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            label='Neighborhood'
                            fullWidth
                            value={addressData.neighborhood}
                            onChange={e => updateAddressState('neighborhood', e.target.value)}
                        />
                    </Grid>
                    <Grid item xs={12} sm={4}>
                        <TextField
                            label='City'
                            required
                            fullWidth
                            value={addressData.city}
                            onChange={e => updateAddressState('city', e.target.value)}
                        />
                    </Grid>
                    <Grid item xs={12} sm={4}>
                        <TextField
                            label='State'
                            required
                            fullWidth
                            value={addressData.state}
                            onChange={e => updateAddressState('state', e.target.value)}
                        />
                    </Grid>
                    <Grid item xs={12} sm={4}>
                        <TextField
                            label='Country'
                            required
                            fullWidth
                            value={addressData.country}
                            onChange={e => updateAddressState('country', e.target.value)}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            label='Additional Information'
                            fullWidth
                            value={addressData.additionalInformation}
                            placeholder='Facade description, landmarks, security information, etc.'
                            onChange={e => updateAddressState('additionalInformation', e.target.value)}
                        />
                    </Grid>

                    <Grid item xs={12}>
                        <LoadingButton
                            type='submit'
                            variant='contained'
                            fullWidth
                            loading={loadingSubmit}
                        >
                            Save
                        </LoadingButton>
                    </Grid>
                </Grid>
            </Form>
        </Main>
    )
}

export default function Private({ crudType, onClose }: AddressFormProps) {
    return (
        <PrivateRoute>
            <AddressForm crudType={crudType} onClose={onClose} />
        </PrivateRoute>
    )
}
