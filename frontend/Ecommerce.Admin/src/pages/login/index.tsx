import { toast } from 'react-toastify';
import { Main, Form } from './styles';
import { LoadingButton } from '@mui/lab';
import { useRouter } from 'next/navigation';
import { useState, FormEvent } from 'react';
import TextField from '@mui/material/TextField';
import api from '../../services/apiAuthorization';

export default function Login() {
    const router = useRouter();

    const [emailOrPhoneNumber, setEmailOrPhoneNumber] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(false);

    function handleSubmit(e: FormEvent<HTMLFormElement>): void {
        e.preventDefault();

        const data = { emailOrPhoneNumber, password };

        setLoading(true);

        api.post('/login', data)
            .then(response => {
                localStorage.setItem('SESSIONJWT', response.data.message);
                router.push('/');
            })
            .catch(error => {
                if (error.response?.status === 401) {
                    toast.warning(error.response.data.message);
                    return;
                }

                toast.error('Error 500');
            })
            .finally(() => setLoading(false));
    }

    return (
        <Main>
            <Form onSubmit={handleSubmit}>
                <TextField
                    label='Email'
                    required
                    fullWidth
                    value={emailOrPhoneNumber}
                    onChange={e => setEmailOrPhoneNumber(e.target.value)}
                />
                <TextField
                    label='Password'
                    type='password'
                    required
                    fullWidth
                    value={password}
                    onChange={e => setPassword(e.target.value)}
                />

                <LoadingButton loading={loading} type='submit' fullWidth>Login</LoadingButton>
            </Form>
        </Main>
    )
}
