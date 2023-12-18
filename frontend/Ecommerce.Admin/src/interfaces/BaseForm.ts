import { CrudType } from "./CrudType";

export default interface BaseForm {
    crudType: CrudType
    next?: () => void
}
