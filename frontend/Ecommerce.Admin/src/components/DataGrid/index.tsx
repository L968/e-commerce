import { DataGrid as MuiDataGrid, DataGridProps, GridColDef, GridColType, getGridBooleanOperators } from '@mui/x-data-grid';
import { getGridStringOperators, getGridNumericOperators, getGridDateOperators, getGridSingleSelectOperators, getGridDateTimeOperators } from '@/utils/datagrid';

export default function DataGrid({ columns, ...props }: DataGridProps) {
    const columnsWithCustomFilterOperators: GridColDef[] = columns.map(column => ({
        ...column,
        filterOperators: column.filterOperators || getFilterOperatorsByType(column.type),
    }));

    return (
        <MuiDataGrid
            {...props}
            pagination
            paginationMode='server'
            filterMode='server'
            sortingMode='server'
            columns={columnsWithCustomFilterOperators}
        />
    )
}

function getFilterOperatorsByType(type: GridColType | undefined): any[] {
    switch (type) {
        case 'string': return getGridStringOperators();
        case 'number': return getGridNumericOperators();
        case 'boolean': return getGridBooleanOperators();
        case 'date': return getGridDateOperators();
        case 'dateTime': return getGridDateTimeOperators();
        case 'singleSelect': return getGridSingleSelectOperators();
        case 'actions': getGridStringOperators();
        default: return getGridStringOperators();
    }
}
