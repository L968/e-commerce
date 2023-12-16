import api from '@/services/api';
import { toast } from 'react-toastify';
import { useRouter } from 'next/router';
import { useEffect, useState } from 'react';
import EditIcon from '@mui/icons-material/Edit';
import { Container, Main, Title } from './styles';
import { Chip, IconButton, Stack } from '@mui/material';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import GetVariantsResponse, { Option } from '@/interfaces/api/responses/GetVariantsResponse';

export default function Variants() {
    const router = useRouter();

    const [loading, setLoading] = useState<boolean>(false);
    const [variants, setVariants] = useState<GetVariantsResponse[]>([]);

    useEffect(() => {
        setLoading(true);

        api.get<GetVariantsResponse[]>('/variant')
            .then(response => setVariants(response.data))
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
                <IconButton onClick={() => router.push(`/variants/${params.row.id}/edit`)}>
                    <EditIcon />
                </IconButton>
        },
        { field: 'name', headerName: 'Name', flex: .5 },
        {
            field: 'options',
            headerName: 'Options',
            flex: 2,
            renderCell: params =>
                <Stack direction='row' spacing={1}>
                    {params.value.map((option: Option) =>
                        <Chip key={option.id} label={option.name} />
                    )}
                </Stack>
        },
    ];

    return (
        <Main>
            <Title variant='h1'>Variants</Title>

            <Container>
                <DataGrid
                    rows={variants}
                    columns={columns}
                    loading={loading}
                    disableRowSelectionOnClick
                    autoHeight
                />
            </Container>
        </Main>
    )
}
