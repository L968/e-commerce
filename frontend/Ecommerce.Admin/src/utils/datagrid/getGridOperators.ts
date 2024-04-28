import {
    getGridStringOperators as MuiGetGridStringOperators,
    getGridNumericOperators as MuiGetGridNumericOperators,
    getGridDateOperators as MuiGetGridDateOperators,
    getGridSingleSelectOperators as MuiGetGridSingleSelectOperators,
} from '@mui/x-data-grid';

const validStringOperators = ['equals', 'contains'];
const validSingleSelectOperators = ['is'];
const validDateOperators = ['is', 'after', 'onOrAfter', 'before', 'onOrBefore'];
const validNumericOperators = ['=', '>', '>=', '<', '<='];

export const getGridStringOperators = () =>
    MuiGetGridStringOperators()
        .filter(({ value }) => validStringOperators.includes(value));

export const getGridNumericOperators = () =>
    MuiGetGridNumericOperators()
        .filter(({ value }) => validNumericOperators.includes(value));

export const getGridDateOperators = () =>
    MuiGetGridDateOperators()
        .filter(({ value }) => validDateOperators.includes(value));

export const getGridDateTimeOperators = () =>
    MuiGetGridDateOperators(true)
        .filter(({ value }) => validDateOperators.includes(value));

export const getGridSingleSelectOperators = () =>
    MuiGetGridSingleSelectOperators()
        .filter(({ value }) => validSingleSelectOperators.includes(value));
