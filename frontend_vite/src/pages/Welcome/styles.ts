import styled from "styled-components";

export const Container = styled.div`
  display: flex;
`;

export const ContentContainer = styled.div`
  width: 100%;
  height: 100vh;

  margin-right: auto;
  padding: 1rem;

  display: flex;

  background: linear-gradient(to right, var(--purple-600), var(--blue-200));

  .welcome-content-title {
    height: 5rem;

    margin: auto;

    align-items: center;
  }
`;

export const LoginContainer = styled.div`
  height: 100vh;

  margin-left: auto;
  padding: 1rem;

  display: flex;

  align-items: center;

  .welcome-login-form {
    margin: auto;

    width: 30rem;
    padding: 1rem;

    display: flex;
    flex-direction: column;

    gap: 1rem;

    .welcome-login-form-header {
      height: 3rem;
    }

    .welcome-login-form-content {
      margin: 10% 0;

      gap: 2rem;
      display: flex;
      flex-direction: column;

      label {
        p {
          margin-bottom: 0.5rem;
          font-size: 12px;
        }
      }

      input {
        width: 100%;
        height: 1.5rem;
        padding-bottom: 4px;

        border-style: none;
        border-bottom: 1px solid #d1d1d1;

        background: var(--white-800);

        outline: none;
      }

      p {
        font-size: 14px;
        color: #8d8daa;

        transition: filter 0.2s;

        &:hover {
          filter: brightness(0.9);
        }
      }

      .welcome-login-form-actions {
        display: flex;
        flex-direction: column;

        gap: 0.5rem;

        p {
          transition: filter 0.2s;

          cursor: pointer;

          &:hover {
            filter: brightness(0.8);
          }
        }
      }
    }

    .welcome-login-form-loginbutton {
      height: 3rem;

      border: none;
      border-radius: 4px;

      font-weight: 800;
      color: var(--white-800);

      background: var(--purple-600);

      transition: filter 0.2s;

      &:hover {
        filter: brightness(0.9);
      }
    }
  }
`;
