import Operator from './Operator';

export default interface FilterParams {
    property: string
    operator: Operator
    value: string
}
