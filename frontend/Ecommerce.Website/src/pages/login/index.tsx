import Image from 'next/image';
import Grid from '@mui/material/Grid';
import { toast } from 'react-toastify';
import { useState, FormEvent } from 'react';
import TextField from '@mui/material/TextField';
import { useAuth } from '@/contexts/authContext';
import LoadingButton from '@/components/Button/LoadingButton';
import { Container, LoginArea, LoginFrame, Form, Main, Paragraph, WelcomeLabel, BrandName } from './styles';

export default function Login() {
    const { login } = useAuth();

    const [emailOrPhoneNumber, setEmailOrPhoneNumber] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(false);

    async function handleSubmit(e: FormEvent<HTMLFormElement>): Promise<void> {
        e.preventDefault();

        setLoading(true);

        try {
            await login(emailOrPhoneNumber, password);
        } catch (error: any) {
            if (error.response.status === 401) {
                toast.warning(error.response.data.message);
                return;
            }

            toast.error('Error 500');
        }
        finally {
            setLoading(false);
        }
    }

    return (
        <Main>
            <Container container>
                <LoginArea item xs={6}>
                    <LoginFrame>
                        <BrandName variant='h3'>Brand Name</BrandName>
                        <WelcomeLabel variant='h2'>Welcome back</WelcomeLabel>
                        <Paragraph>Welcome back! Please enter your details</Paragraph>
                        <Form onSubmit={handleSubmit}>
                            <TextField
                                label='Email or Phone Number'
                                name='email'
                                required
                                fullWidth
                                value={emailOrPhoneNumber}
                                onChange={e => setEmailOrPhoneNumber(e.target.value)}
                            />
                            <TextField
                                label='Password'
                                type='password'
                                name='password'
                                required
                                fullWidth
                                value={password}
                                onChange={e => setPassword(e.target.value)}
                            />

                            <LoadingButton loading={loading} type='submit' fullWidth>Login</LoadingButton>
                        </Form>
                    </LoginFrame>
                </LoginArea>
                <Grid item xs={6}>
                    <Image
                        src='/assets/login.png'
                        alt='login-image'
                        width={0}
                        height={0}
                        sizes='100vw'
                        style={{ width: '100%', height: 'auto' }}
                    />
                </Grid>
            </Container>
        </Main>
    )
}
