import MuiPagination from '@mui/material/Pagination';

interface PaginationProps {
    page: number
    totalPages: number
    onChangePage: (newPage: number) => void
}

export default function Pagination({ page, totalPages, onChangePage }: PaginationProps) {
    return (
        <MuiPagination
            page={page}
            count={totalPages}
            onChange={(_, value) => onChangePage(value)}
        />
    )
}
