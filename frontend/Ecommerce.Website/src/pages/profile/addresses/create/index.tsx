import PrivateRoute from '@/components/PrivateRoute';
import { Main} from './styles';

function CreateAddress() {
    return (
        <Main>
            Create
        </Main>
    )
}

export default function Private() {
    return (
        <PrivateRoute>
            <CreateAddress />
        </PrivateRoute>
    )
}
