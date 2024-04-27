import { GridFilterItem } from '@mui/x-data-grid';
import Operator from '@/interfaces/gridParams/Operator';
import FilterParams from '@/interfaces/gridParams/FilterParams';

const operatorMap: Record<string, Operator> = {
    is: '=',
    equals: '=',
    contains: 'like',
    after: '>',
    before: '<',
    onOrAfter: '>=',
    onOrBefore: '<=',
};

export default function convertToFilterParams(gridFilters: GridFilterItem[]): FilterParams[] {
    return gridFilters
    .filter(({ value }) => !!value)
    .map(({ field, operator, value }) => {
        const mappedOperator = operatorMap[operator];

        if (!mappedOperator) {
            throw new Error(`Unsupported GridFilterItem operator: ${operator}`);
        }

        return {
            property: field,
            operator: mappedOperator,
            value: value.toString(),
        };
    });
}
