import {
    getGridSingleSelectOperators as MuiGetGridSingleSelectOperators,
    getGridDateOperators as MuiGetGridDateOperators
} from '@mui/x-data-grid';

const validSingleSelectOperators = ['is'];
const validDateOperators = ['is', 'after', 'before', 'onOrAfter', 'onOrBefore'];

export const getGridSingleSelectOperators = () =>
    MuiGetGridSingleSelectOperators()
        .filter(({ value }) => validSingleSelectOperators.includes(value));

export const getGridDateOperators = () =>
    MuiGetGridDateOperators()
        .filter(({ value }) => validDateOperators.includes(value));
