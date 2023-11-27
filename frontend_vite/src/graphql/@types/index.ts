export interface CreateAuthProps {
  authenticate: Authenticate;
}

export interface ValidateAuthProps {
  authorized: boolean;
}

interface Authenticate {
  message: string;
}

