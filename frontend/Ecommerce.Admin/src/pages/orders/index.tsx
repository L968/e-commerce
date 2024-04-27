import moment from 'moment';
import { toast } from 'react-toastify';
import Order from '@/interfaces/Order';
import { Container, Main } from './styles';
import apiOrder from '@/services/apiOrder';
import { FormEvent, useEffect, useState } from 'react';
import GridParams from '@/interfaces/gridParams/GridParams';
import getOrderStatusColor from '@/utils/getOrderStatusColor';
import convertToSortParams from '@/utils/datagrid/convertToSortParams';
import convertToFilterParams from '@/utils/datagrid/convertToFilterParams';
import GetAllOrdersResponse from '@/interfaces/api/responses/GetAllOrdersResponse';
import { Button, Chip, LinearProgress, Stack, TextField, Typography } from '@mui/material';
import { getGridDateOperators, getGridSingleSelectOperators } from '@/utils/datagrid/getGridOperators';
import { DataGrid, GridColDef, GridFilterModel, GridPaginationModel, GridSortModel } from '@mui/x-data-grid';

const columns: GridColDef[] = [
    {
        field: 'id',
        headerName: 'Id',
        sortable: false,
        filterable: false,
        flex: 1,
    },
    {
        field: 'paymentMethod',
        headerName: 'Payment Method',
        type: 'singleSelect',
        valueOptions: ['PayPal'],
        filterOperators: getGridSingleSelectOperators(),
        flex: 1,
        renderCell: params => <Chip label={params.value} />
    },
    {
        field: 'status',
        headerName: 'Status',
        type: 'singleSelect',
        valueOptions: [
            'Pending Payment',
            'Processing',
            'Shipped',
            'Delivered',
            'Cancelled',
            'Refunded',
            'Returned'
        ],
        flex: 1,
        renderCell: params => <Chip label={params.value} style={{ backgroundColor: getOrderStatusColor(params.value) }} />
    },
    {
        field: 'createdAt',
        headerName: 'Date',
        type: 'date',
        flex: 1,
        filterOperators: getGridDateOperators(),
        valueFormatter: (params) => moment(params.value).format('DD/MM/YYYY HH:mm:ss'),
        valueGetter: (params) => new Date(params.value)
    },
];

export default function Orders() {
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
                    pagination
                    paginationMode='server'
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
