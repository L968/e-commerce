import FilterParams from './FilterParams';
import SortParams from './SortParams';

export default interface GridParams {
    page?: number
    pageSize?: number
    filters?: FilterParams[]
    sorters?: SortParams[]
}
