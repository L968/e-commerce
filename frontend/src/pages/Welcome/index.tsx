import { FormEvent, useEffect, useRef, useState } from "react";
import { Container, ContentContainer, LoginContainer } from "./styles";

import { notify } from "../../notify";
import { Loader } from "../../components/Loader";

import { useMutation } from "@apollo/client";
import { CREATE_AUTH } from "../../graphql/mutations";

import { CreateAuthProps } from "../../graphql/@types";
import { useNavigate } from "react-router-dom";

export const Welcome = (): JSX.Element => {
  const [isSingUp, setIsSignUp] = useState(false);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setconfirmPassword] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);

  const [Authenticate, { loading, error }] =
    useMutation<CreateAuthProps>(CREATE_AUTH);

  const navigate = useNavigate();

  const handleEmail = (value: string) => setEmail(value);
  const handlePassword = (value: string) => setPassword(value);
  const handleConfirmPassword = (value: string) => setconfirmPassword(value);
  const handleIsModalOpen = (value: boolean) => setIsModalOpen(value);

  function handleSignUp(event: FormEvent) {
    event.preventDefault();

    if (password !== confirmPassword) {
      notify.error("Passwords are different", "top-right");
      return;
    }

    authenticate(email, password);
  }

  function handleLogin(event: FormEvent) {
    event.preventDefault();
    authenticate(email, password);

    if (error) {
      handleAuthentionException();
      return;
    }

    navigate("main/");
  }

  async function authenticate(email: string, password: string) {
    await Authenticate({ variables: { email, password } }).then((response) =>
      localStorage.setItem(
        "AUTH-TOKEN",
        response.data?.authenticate.message as string
      )
    );
  }

  function handleAuthentionException() {
    if (error?.message.includes("401")) {
      if (error.message == "User is currently locked out") {
        notify.warning("Too many attemps, try again later", "top-right");
        return;
      }

      notify.warning("Your email or password might be wrong", "top-right");
      return;
    }

    notify.error("Something happened while trying to connect", "top-right");
    return;
  }

  return (
    <Container>
      <ContentContainer>
        <div className="welcome-content-title">
          <h1>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas
            quis cursus nibh. Donec et viverra nunc.
          </h1>
          <h3>
            Ut in tincidunt odio. Fusce non velit euismod turpis eleifend
            vestibulum non eu nunc.
          </h3>
        </div>
      </ContentContainer>
      <LoginContainer>
        {isSingUp ? (
          <form className="welcome-login-form" onSubmit={handleSignUp}>
            <div className="welcome-login-form-header">
              <h1>Sign up</h1>
            </div>

            <div className="welcome-login-form-content">
              <label>
                <p>Email</p>
                <input
                  type="text"
                  name="text"
                  required
                  onInvalid={() => console.log("opa")}
                  value={email}
                  onChange={(event) => handleEmail(event.target.value)}
                />
              </label>
              <label>
                <p>Password</p>
                <input
                  type="password"
                  name="password"
                  required
                  value={password}
                  onChange={(event) => handlePassword(event.target.value)}
                />
              </label>
              <label>
                <p>Confirm password</p>
                <input
                  type="password"
                  name="confirmPassword"
                  required
                  value={confirmPassword}
                  onChange={(event) =>
                    handleConfirmPassword(event.target.value)
                  }
                />
              </label>
              <div className="welcome-login-form-actions">
                <p onClick={() => setIsSignUp(false)}>
                  Already have an account?
                </p>
              </div>
            </div>

            <button
              className="welcome-login-form-loginbutton"
              onClick={() => handleIsModalOpen(!isModalOpen)}
            >
              {loading ? <Loader /> : "Create account"}
            </button>
          </form>
        ) : (
          <form className="welcome-login-form" onSubmit={handleLogin}>
            <div className="welcome-login-form-header">
              <h1>Sign in</h1>
            </div>

            <div className="welcome-login-form-content">
              <label>
                <p>Email</p>
                <input
                  type="text"
                  name="text"
                  required
                  value={email}
                  onChange={(event) => handleEmail(event.target.value)}
                />
              </label>
              <label>
                <p>Password</p>
                <input
                  type="password"
                  name="password"
                  required
                  value={password}
                  onChange={(event) => handlePassword(event.target.value)}
                />
              </label>
              <div className="welcome-login-form-actions">
                <p onClick={() => setIsSignUp(true)}>Create account</p>
                <p>Forgot password?</p>
              </div>
            </div>

            <button className="welcome-login-form-loginbutton" type="submit">
              {loading ? <Loader /> : "Login"}
            </button>
          </form>
        )}
      </LoginContainer>
    </Container>
  );
};
