import { gql } from "@apollo/client";

export const CREATE_AUTH = gql`
  mutation Authenticate($email: Email!, $password: Password!) {
    authenticate(input: { email: $email, password: $password })
      @rest(type: "create-auth", path: "Login/", method: "POST") {
      message
    }
  }
`;

export const VALIDATE_AUTH = gql`
  mutation Validate($token: Token!) {
    validate(input: { token: $token })
      @rest(type: "validate-auth", path: "is-authorized/", method: "POST") {
      authorized
    }
  }
`;
