import Link from 'next/link';
import api from '@/services/api';
import { toast } from 'react-toastify';
import { useRouter } from 'next/router';
import { LoadingButton } from '@mui/lab';
import Address from '@/interfaces/Address';
import { useEffect, useState } from 'react';
import HomeIcon from '@mui/icons-material/Home';
import PrivateRoute from '@/components/PrivateRoute';
import MoreVertIcon from '@mui/icons-material/MoreVert';
import apiAuthorization from '@/services/apiAuthorization';
import { Container, List, ListItem, ListItemContent, ListItemDescription, Main, Title } from './styles';
import { Avatar, Button, CircularProgress, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, IconButton, Menu, MenuItem, Stack, Tooltip } from '@mui/material';
import StarIcon from '@mui/icons-material/Star';

function Addresses() {
    const router = useRouter();

    const [fetchLoading, setFetchLoading] = useState<boolean>(true);
    const [deleteLoading, setDeleteLoading] = useState<boolean>(false);
    const [addresses, setAddresses] = useState<Address[]>([]);
    const [selectedAddress, setSelectedAddress] = useState<Address>();
    const [defaultAddressId, setDefaultAddressId] = useState<string | null>(null);
    const [openDeleteDialog, setOpenDeleteDialog] = useState<boolean>(false);

    const [anchorEl, setAnchorEl] = useState<HTMLElement | null>(null);
    const [openMenus, setOpenMenus] = useState<{ [key: string]: boolean }>({});

    useEffect(() => {
        fetchAddresses();
        fetchDefaultAddressId();
    }, []);

    function fetchAddresses(): void {
        api.get<Address[]>('/address')
            .then(res => setAddresses(res.data))
            .catch(err => toast.error('Error 500'))
            .finally(() => setFetchLoading(false));
    }

    function fetchDefaultAddressId() {
        apiAuthorization.get<string>('/user/defaultAddressId')
            .then(res => setDefaultAddressId(res.data))
            .catch(err => {
                if (err.response?.status !== 404) {
                    toast.error('Error 500');
                }
            });
    }

    function handleEdit(addressId: string): void {
        router.push(`/account/addresses/update/${addressId}`);
    }

    function handleOpenDeleteDialog(address: Address): void {
        handleCloseMenu(address.id);
        setOpenDeleteDialog(true);
        setSelectedAddress(address);
    }

    function handleDelete(addressId: string): void {
        setDeleteLoading(true);

        api.delete(`/address/${addressId}`)
            .then(res => {
                fetchAddresses();
                handleCloseMenu(addressId);
                setOpenDeleteDialog(false);
                toast.success('Address deleted successfully');
            })
            .catch(err => toast.error('Error 500'))
            .finally(() => setDeleteLoading(false));
    }

    function handleSetAsDefault(addressId: string): void {
        apiAuthorization.patch('/user/defaultAddressId/' + addressId)
            .then(res => {
                fetchDefaultAddressId();
                handleCloseMenu(addressId);
                toast.success('Address updated successfully');
            })
            .catch(err => toast.error('Error 500'))
    }

    function handleOpenMenu(event: React.MouseEvent<HTMLElement>, addressId: string): void {
        setAnchorEl(event.currentTarget);
        setOpenMenus(prevState => ({
            ...prevState,
            [addressId]: true,
        }));
    }

    function handleCloseMenu(addressId: string): void {
        setAnchorEl(null);
        setOpenMenus(prevState => ({
            ...prevState,
            [addressId]: false,
        }));
    }

    return (
        <Main>
            <Title variant='h1'>Addresses</Title>

            <Container>
                <List>
                    {fetchLoading
                        ? <CircularProgress />
                        : <>
                            {addresses.map(address => (
                                <ListItem key={address.id}>
                                    <Avatar sx={{ bgcolor: 'primary.main' }}>
                                        <HomeIcon />
                                    </Avatar>

                                    <ListItemContent>
                                        <Stack
                                            alignItems='center'
                                            direction='row'
                                            gap={1}
                                            minHeight={'24px'}
                                        >
                                            {`${address.streetName} ${address.buildingNumber} ${address.complement}`}

                                            {address.id === defaultAddressId && (
                                                <Tooltip title='Default Address' arrow>
                                                    <StarIcon sx={{ color: 'gold' }} />
                                                </Tooltip>
                                            )}
                                        </Stack>

                                        <ListItemDescription>{`Zip Code ${address.postalCode} - ${address.state} - ${address.city}`}</ListItemDescription>
                                        <ListItemDescription>{address.recipientFullName} - {address.recipientPhoneNumber}</ListItemDescription>
                                    </ListItemContent>

                                    <IconButton
                                        onClick={e => handleOpenMenu(e, address.id)}
                                        sx={{ marginLeft: 'auto' }}
                                    >
                                        <MoreVertIcon />
                                    </IconButton>

                                    <Menu
                                        anchorEl={anchorEl}
                                        open={openMenus[address.id] || false}
                                        onClose={() => handleCloseMenu(address.id)}
                                    >
                                        <MenuItem onClick={() => handleEdit(address.id)}>Edit</MenuItem>
                                        <MenuItem onClick={() => handleOpenDeleteDialog(address)}>Delete</MenuItem>
                                        <MenuItem onClick={() => handleSetAsDefault(address.id)}>Set as Default</MenuItem>
                                    </Menu>
                                </ListItem>
                            ))}
                        </>
                    }

                    <Link href='/account/addresses/create'>
                        <Button fullWidth>
                            Add Address
                        </Button>
                    </Link>
                </List>
            </Container>

            {selectedAddress &&
                <Dialog
                    open={openDeleteDialog}
                    onClose={() => setOpenDeleteDialog(false)}
                >
                    <DialogTitle>
                        Are you sure you want to delete this address?
                    </DialogTitle>
                    <DialogContent>
                        <DialogContentText>
                            {`${selectedAddress.streetName} ${selectedAddress.buildingNumber} ${selectedAddress.complement}`}
                        </DialogContentText>
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={() => setOpenDeleteDialog(false)}>Cancel</Button>
                        <LoadingButton
                            onClick={() => handleDelete(selectedAddress.id)}
                            variant='contained'
                            color='error'
                            loading={deleteLoading}
                        >
                            Delete
                        </LoadingButton>
                    </DialogActions>
                </Dialog>
            }
        </Main>
    )
}

export default function Private() {
    return (
        <PrivateRoute>
            <Addresses />
        </PrivateRoute>
    )
}
