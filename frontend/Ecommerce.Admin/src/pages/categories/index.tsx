import api from '@/services/api';
import { toast } from 'react-toastify';
import { useRouter } from 'next/router';
import { useEffect, useState } from 'react';
import EditIcon from '@mui/icons-material/Edit';
import { Container, Main, Title } from './styles';
import { Chip, IconButton, Stack } from '@mui/material';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import GetVariantsResponse from '@/interfaces/api/responses/GetVariantsResponse';
import GetCategoryResponse from '@/interfaces/api/responses/GetCategoriesResponse';

export default function Categories() {
    const router = useRouter();

    const [loading, setLoading] = useState<boolean>(false);
    const [categories, setCategories] = useState<GetCategoryResponse[]>([]);

    useEffect(() => {
        setLoading(true);

        api.get<GetCategoryResponse[]>('/productCategory')
            .then(response => setCategories(response.data))
            .catch(error => toast.error('Error 500'))
            .finally(() => setLoading(false));
    }, []);

    const columns: GridColDef[] = [
        {
            field: 'edit',
            headerName: 'Edit',
            flex: .2,
            disableReorder: true,
            disableColumnMenu: true,
            sortable: false,
            renderCell: params =>
                <IconButton onClick={() => router.push(`/categories/${params.row.id}/edit`)}>
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
                    {params.value.map((variant: GetVariantsResponse) =>
                        <Chip key={variant.id} label={variant.name} />
                    )}
                </Stack>
        },
    ];

    return (
        <Main>
            <Title variant='h1'>Product Categories</Title>

            <Container>
                <DataGrid
                    rows={categories}
                    columns={columns}
                    loading={loading}
                    disableRowSelectionOnClick
                    autoHeight
                />
            </Container>
        </Main>
    )
}
