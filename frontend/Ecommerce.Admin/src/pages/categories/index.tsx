import api from '@/services/api';
import { toast } from 'react-toastify';
import { useEffect, useState } from 'react';
import EditIcon from '@mui/icons-material/Edit';
import { Container, Main, Title } from './styles';
import { Chip, IconButton, Stack } from '@mui/material';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import GetCategoryResponse from '@/interfaces/api/responses/GetCategoriesResponse';

const columns: GridColDef[] = [
    {
        field: 'edit',
        headerName: 'Edit',
        flex: .2,
        disableReorder: true,
        disableColumnMenu: true,
        sortable: false,
        renderCell: params =>
        <IconButton>
            <EditIcon />
        </IconButton>
    },
    { field: 'name', headerName: 'Name', flex: 1 },
    { field: 'description', headerName: 'Description', flex: 1 },
    {
        field: 'variants',
        headerName: 'Variants',
        flex: 1,
        renderCell: params =>
            <Stack direction='row' spacing={1}>
                {params.value.map((variantName: string) =>
                    <Chip label={variantName} />
                )}
            </Stack>
    },
];

export default function Categories() {
    const [categories, setCategories] = useState<GetCategoryResponse[]>([]);
    const [loading, setLoading] = useState<boolean>(false);

    useEffect(() => {
        setLoading(true);

        api.get<GetCategoryResponse[]>('/productCategory')
            .then(response => setCategories(response.data))
            .catch(error => toast.error('Error 500'))
            .finally(() => setLoading(false));
    }, []);

    return (
        <Main>
            <Title variant='h1'>Product Categories</Title>

            <Container>
                <DataGrid
                    rows={categories}
                    columns={columns}
                    loading={loading}
                    disableRowSelectionOnClick
                />
            </Container>
        </Main>
    )
}
