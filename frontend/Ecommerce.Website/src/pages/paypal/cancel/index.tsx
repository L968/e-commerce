import { toast } from 'react-toastify';
import { useRouter } from 'next/router';
import apiOrder from '@/services/apiOrder';
import { useEffect, useState } from 'react';
import { CircularProgress } from '@mui/material';
import PrivateRoute from '@/components/PrivateRoute';

function PayPalCancel() {
    const router = useRouter();
    const [loading, setLoading] = useState<boolean>(true);
    const [success, setSuccess] = useState<boolean>(false);

    useEffect(() => {
        const { token } = router.query;
        if (!token) return;

        apiOrder.post('/paypal/cancel', { token: token })
            .then(res =>  setSuccess(true))
            .catch(err => toast.error('Error 500'))
            .finally(() => setLoading(false));
    }, [router.query]);

    function loadContent(): JSX.Element {
        if (loading) {
            return <CircularProgress />
        }

        if (!loading && success) {
            return <h1>Order cancelled successfully.</h1>
        }

        if (!loading && !success) {
            return <h1>Error processing order...</h1>
        }

        return <></>
    }

    return (
        <main>
            {loadContent()}
        </main>
    )
}

export default function Private() {
    return (
        <PrivateRoute>
            <PayPalCancel />
        </PrivateRoute>
    )
}
