import { useEffect, useRef, useState } from "react";
import {
  Route,
  Routes as Router,
  BrowserRouter,
  Navigate,
} from "react-router-dom";

import { useMutation } from "@apollo/client";
import { ValidateAuthProps } from "./graphql/@types";
import { VALIDATE_AUTH } from "./graphql/mutations";

import { Main } from "./pages/Main";

import { Welcome } from "./pages/Welcome";

interface PrivateRouteProps {
  element: JSX.Element;
}

const PrivateRoute = ({ element }: PrivateRouteProps): JSX.Element => {
  const isAuthenticatedRef = useRef(false);
  const [Validate] = useMutation<ValidateAuthProps>(VALIDATE_AUTH);

  useEffect(() => {
    Validate({
      variables: { token: localStorage.getItem("AUTH-TOKEN")?.toString() },
    }).then(
      (response) =>
        (isAuthenticatedRef.current = response.data?.authorized as boolean)
    );
  }, []);

  if (isAuthenticatedRef.current) {
    return element;
  }

  <Navigate to="/" replace />;
  return <Welcome />;
};

const Routes = () => {
  return (
    <BrowserRouter>
      <Router>
        <Route path="/" element={<Welcome />} />
        <Route path="main/" element={<PrivateRoute element={<Main />} />} />
      </Router>
    </BrowserRouter>
  );
};

export default Routes;
