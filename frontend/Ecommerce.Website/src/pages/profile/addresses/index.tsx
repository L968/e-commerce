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
import { Container, List, ListItem, ListItemContent, ListItemDescription, Main, Title } from './styles';
import { Avatar, Button, CircularProgress, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, IconButton, Menu, MenuItem } from '@mui/material';

function Addresses() {
    const router = useRouter();

    const [fetchLoading, setFetchLoading] = useState<boolean>(true);
    const [deleteLoading, setDeleteLoading] = useState<boolean>(false);
    const [addresses, setAddresses] = useState<Address[]>([]);
    const [selectedAddress, setSelectedAddress] = useState<Address>();
    const [openDeleteDialog, setOpenDeleteDialog] = useState<boolean>(false);

    const [anchorEl, setAnchorEl] = useState<HTMLElement | null>(null);
    const openMenu = Boolean(anchorEl);

    useEffect(() => {
        fetchAddresses();
    }, []);

    function fetchAddresses() {
        api.get<Address[]>('/address')
            .then(res => setAddresses(res.data))
            .catch(err => toast.error('Error 500'))
            .finally(() => setFetchLoading(false));
    }

    function handleEdit(addressId: number): void {
        router.push(`/profile/addresses/${addressId}`);
    }

    function handleOpenDeleteDialog(address: Address) {
        setOpenDeleteDialog(true);
        setSelectedAddress(address);
    }

    function handleDelete(addressId: number): void {
        setDeleteLoading(true);

        api.delete(`/address/${addressId}`)
            .then(res => {
                fetchAddresses();
                setOpenDeleteDialog(false);
                toast.success('Address deleted successfully');
            })
            .catch(err => toast.error('Error 500'))
            .finally(() => setDeleteLoading(false));
    }

    return (
        <Main>
            <Title variant='h2'>Addresses</Title>

            <Container>
                <List>
                    {fetchLoading
                        ? <CircularProgress />
                        : <>
                            {addresses.map(address => (
                                <ListItem>
                                    <Avatar sx={{ bgcolor: 'primary.main' }}>
                                        <HomeIcon />
                                    </Avatar>

                                    <ListItemContent>
                                        <p>{`${address.streetName} ${address.buildingNumber} ${address.complement}`}</p>
                                        <ListItemDescription>{`Zip Code ${address.postalCode} - ${address.state} - ${address.city}`}</ListItemDescription>
                                        <ListItemDescription>James Bond - 13999998888</ListItemDescription>
                                    </ListItemContent>

                                    <IconButton
                                        onClick={e => setAnchorEl(e.currentTarget)}
                                        sx={{ marginLeft: 'auto' }}
                                    >
                                        <MoreVertIcon />
                                    </IconButton>

                                    <Menu
                                        anchorEl={anchorEl}
                                        open={openMenu}
                                        onClose={() => setAnchorEl(null)}
                                    >
                                        <MenuItem onClick={() => handleEdit(address.id)}>Edit</MenuItem>
                                        <MenuItem onClick={() => handleOpenDeleteDialog(address)}>Delete</MenuItem>
                                    </Menu>
                                </ListItem>
                            ))}
                        </>
                    }

                    <Link href='/profile/addresses/create'>
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
