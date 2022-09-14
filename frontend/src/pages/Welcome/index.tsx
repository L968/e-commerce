import { FormEvent, useState } from "react";
import { Container, ContentContainer, LoginContainer } from "./styles";

export const Welcome = (): JSX.Element => {
  const [isSingUp, setIsSignUp] = useState(false);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setconfirmPassword] = useState("");

  const handleEmail = (value: string) => setEmail(value);
  const handlePassword = (value: string) => setPassword(value);
  const handleConfirmPassword = (value: string) => setconfirmPassword(value);

  function handleSignUp(event: FormEvent) {
    event.preventDefault();
    console.log(email, password, confirmPassword);
  }

  function handleLogin(event: FormEvent) {
    event.preventDefault();
    console.log(email, password);
  }

  return (
    <Container>
      <ContentContainer>
        <div className="welcome-content-title">
          <h1>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas quis cursus nibh. Donec et viverra nunc.</h1>
          <h3>
            Ut in tincidunt odio. Fusce non velit euismod turpis eleifend vestibulum non eu nunc.
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
                  value={email}
                  onChange={(event) => handleEmail(event.target.value)}
                />
              </label>
              <label>
                <p>Password</p>
                <input
                  type="password"
                  name="password"
                  value={password}
                  onChange={(event) => handlePassword(event.target.value)}
                />
              </label>
              <label>
                <p>Confirm password</p>
                <input
                  type="password"
                  name="confirmPassword"
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

            <button className="welcome-login-form-loginbutton" type="submit">
              Create account
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
                  value={email}
                  onChange={(event) => handleEmail(event.target.value)}
                />
              </label>
              <label>
                <p>Password</p>
                <input
                  type="password"
                  name="password"
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
              Login
            </button>
          </form>
        )}
      </LoginContainer>
    </Container>
  );
};
