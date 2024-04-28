import moment from 'moment';
import { toast } from 'react-toastify';
import Order from '@/interfaces/Order';
import { useRouter } from 'next/router';
import { Container, Main } from './styles';
import apiOrder from '@/services/apiOrder';
import DataGrid from '@/components/DataGrid';
import SearchIcon from '@mui/icons-material/Search';
import { FormEvent, useEffect, useState } from 'react';
import GridParams from '@/interfaces/gridParams/GridParams';
import getOrderStatusColor from '@/utils/getOrderStatusColor';
import { paymentMethodValues } from '@/interfaces/PaymentMethod';
import OrderStatus, { orderStatusOptions } from '@/interfaces/OrderStatus';
import { convertToFilterParams, convertToSortParams } from '@/utils/datagrid';
import GetAllOrdersResponse from '@/interfaces/api/responses/GetAllOrdersResponse';
import { Button, Chip, IconButton, LinearProgress, Stack, TextField, Typography } from '@mui/material';
import { GridColDef, GridFilterModel, GridPaginationModel, GridSortModel } from '@mui/x-data-grid';

export default function Orders() {
    const router = useRouter();

    const [loading, setLoading] = useState<boolean>(false);
    const [orders, setOrders] = useState<Order[]>([]);
    const [totalItems, setTotalItems] = useState<number>(0);
    const [searchId, setSearchId] = useState<string>('');
    const [gridParams, setGridParams] = useState<GridParams>({});

    useEffect(() => {
        fetchOrders();
    }, [gridParams]);

    function fetchOrders() {
        setLoading(true);

        apiOrder.get<GetAllOrdersResponse>(`/order/admin?gridParams=${JSON.stringify(gridParams)}`)
            .then(response => {
                setOrders(response.data.items);
                setTotalItems(response.data.totalItems);
            })
            .catch(error => toast.error('Error 500'))
            .finally(() => setLoading(false));
    }

    function handleSearchById(e: FormEvent<HTMLFormElement>): void {
        e.preventDefault();

        if (!searchId) {
            fetchOrders();
            return;
        }

        setLoading(true);

        apiOrder.get<Order>(`/order/admin/${searchId}`)
            .then(response => {
                if (response.data) {
                    setOrders([response.data]);
                    setTotalItems(1);
                } else {
                    setOrders([]);
                    setTotalItems(0);
                }
            })
            .catch(error => {
                setOrders([]);
                setTotalItems(0);
            })
            .finally(() => setLoading(false));
    }

    function handleOnPaginationModelChange(model: GridPaginationModel): void {
        setGridParams(prev => ({
            ...prev,
            page: model.page + 1,
            pageSize: model.pageSize,
        }));
    }

    function handleOnFilterModelChange(model: GridFilterModel): void {
        setGridParams(prev => ({
            ...prev,
            filters: convertToFilterParams(model.items),
        }));
    }

    function handleOnSortModelChange(model: GridSortModel): void {
        setGridParams(prev => ({
            ...prev,
            sorters: convertToSortParams(model),
        }));
    }

    const columns: GridColDef[] = [
        {
            field: 'details',
            headerName: 'Details',
            flex: .3,
            disableReorder: true,
            disableColumnMenu: true,
            sortable: false,
            renderCell: params =>
                <IconButton onClick={() => router.push(`/order/${params.row.id}/edit`)}>
                    <SearchIcon />
                </IconButton>
        },
        {
            field: 'id',
            headerName: 'Id',
            sortable: false,
            flex: 1,
        },
        {
            field: 'paymentMethod',
            headerName: 'Payment Method',
            type: 'singleSelect',
            valueOptions: paymentMethodValues,
            flex: 1,
            renderCell: (params) => <Chip label={params.value} />
        },
        {
            field: 'status',
            headerName: 'Status',
            type: 'singleSelect',
            valueOptions: orderStatusOptions,
            flex: 1,
            renderCell: (params) => <Chip label={params.formattedValue} style={{ backgroundColor: getOrderStatusColor(params.formattedValue) }} />,
            valueGetter: (params) => OrderStatus[params.value.replace(' ', '')]
        },
        {
            field: 'createdAt',
            headerName: 'Date',
            flex: 1,
            valueFormatter: (params) => moment.utc(params.value).local().format('DD/MM/YYYY HH:mm:ss'),
        },
    ];

    return (
        <Main>
            <Typography variant='h1'>Orders</Typography>

            <Container>
                <form onSubmit={handleSearchById}>
                    <Stack direction='row' gap={2}>
                        <TextField
                            label='Search by ID'
                            variant='outlined'
                            value={searchId}
                            size='small'
                            onChange={e => setSearchId(e.target.value)}
                        />

                        <Button variant='contained' type='submit'>Search</Button>
                    </Stack>
                </form>

                <DataGrid
                    rows={orders}
                    columns={columns}
                    loading={loading}
                    onPaginationModelChange={handleOnPaginationModelChange}
                    onFilterModelChange={handleOnFilterModelChange}
                    onSortModelChange={handleOnSortModelChange}
                    rowCount={totalItems}
                    pageSizeOptions={[10, 20, 50, 100]}
                    initialState={{
                        pagination: {
                            paginationModel: { pageSize: 10 }
                        }
                    }}
                    autoHeight
                    disableRowSelectionOnClick
                    slots={{ loadingOverlay: LinearProgress }}
                />
            </Container>
        </Main>
    )
}
