import Image from "next/image";
import Grid from "@mui/material/Grid";
import { toast } from "react-toastify";
import { useRouter } from 'next/navigation';
import { FormEvent, useState } from "react";
import TextField from "@mui/material/TextField";
import api from '../../services/apiAuthorization';
import LoadingButton from "@/components/Button/LoadingButton";
import { Container, LoginArea, LoginFrame, Form, Main, BrandName } from "./styles";

export default function Register() {
    const router = useRouter();

    const [name, setName] = useState<string>('');
    const [email, setEmail] = useState<string>('');
    const [phoneNumber, setPhoneNumber] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [repassword, setRepassword] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(false);

    function handleSubmit(e: FormEvent<HTMLFormElement>): void {
        e.preventDefault();

        if (password !== repassword) {
            toast.warning('Passwords do not match');
            return;
        }

        const data = { name, email, phoneNumber, password, repassword };

        setLoading(true);

        api.post('/user', data)
        .then(_ =>  {
            toast.success('Account created successfully! Please check your email for verification');
            router.push('/login');
        })
        .catch(error => {
            if (error.response.status === 400) {
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
                        <Form onSubmit={handleSubmit}>
                            <TextField
                                label='Name'
                                placeholder='Name and Surname'
                                required
                                fullWidth
                                value={name}
                                onChange={e => setName(e.target.value)}
                            />
                            <TextField
                                label='Email'
                                placeholder='Your email'
                                type='email'
                                required
                                fullWidth
                                value={email}
                                onChange={e => setEmail(e.target.value)}
                            />
                            <TextField
                                label='Phone Number'
                                placeholder='Your phone number'
                                required
                                fullWidth
                                value={phoneNumber}
                                onChange={e => setPhoneNumber(e.target.value)}
                            />
                            <TextField
                                label='Password'
                                placeholder='Enter password'
                                type='password'
                                required
                                fullWidth
                                value={password}
                                onChange={e => setPassword(e.target.value)}
                            />
                            <TextField
                                type='password'
                                label='Confirm Password'
                                required
                                fullWidth
                                value={repassword}
                                onChange={e => setRepassword(e.target.value)}
                            />

                            <LoadingButton loading={loading} type='submit' fullWidth>Register</LoadingButton>
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
