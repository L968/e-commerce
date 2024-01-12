import PrivateRoute from '@/components/PrivateRoute';

function Orders() {
    return (
        <h1>orders</h1>
    )
}

export default function Private() {
    return (
        <PrivateRoute>
            <Orders />
        </PrivateRoute>
    )
}
