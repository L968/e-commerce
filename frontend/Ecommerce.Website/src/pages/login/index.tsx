import Image from "next/image";
import Grid from "@mui/material/Grid";
import { toast } from "react-toastify";
import { useRouter } from 'next/navigation';
import { useState, FormEvent } from "react";
import TextField from "@mui/material/TextField";
import api from '../../services/apiAuthorization';
import LoadingButton from "@/components/Button/LoadingButton";
import { Container, LoginArea, LoginFrame, Form, Main, Paragraph, WelcomeLabel, BrandName } from "./styles";

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
            router.push('/home');
        })
        .catch(error => {
            if (error.response.status === 401) {
                toast.warning(error.response.data.message);
                return;
            }

            toast.error('Error 500');
        })
        .finally(() => setLoading(false));
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
                    </LoginFrame>
                </LoginArea>
                <Grid item xs={6}>
                    <Image
                        src='/assets/login.png'
                        alt='login-image'
                        width={0}
                        height={0}
                        sizes="100vw"
                        style={{ width: '100%', height: 'auto' }}
                    />
                </Grid>
            </Container>
        </Main>
    )
}
