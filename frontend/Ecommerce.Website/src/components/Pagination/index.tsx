import MuiPagination, { PaginationProps } from '@mui/material/Pagination';

interface CustomPaginationProps extends PaginationProps {
    totalPages: number
    onChangePage: (newPage: number) => void
}

export default function Pagination({ page, totalPages, onChangePage, ...rest }: CustomPaginationProps) {
    return (
        <MuiPagination
            page={page}
            count={totalPages}
            onChange={(_, value) => onChangePage(value)}
            {...rest}
        />
    )
}
