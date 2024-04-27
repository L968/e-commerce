import { GridSortItem } from '@mui/x-data-grid';
import Direction from '@/interfaces/gridParams/Direction';
import SortParams from '@/interfaces/gridParams/SortParams';

export default function convertToSortParams(gridSortItems: GridSortItem[]): SortParams[] {
    return gridSortItems.map(({ field, sort }) => ({
        property: field,
        direction: sort as Direction
    }));
}
