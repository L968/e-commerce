import { ApolloClient, InMemoryCache } from "@apollo/client";
import { RestLink } from "apollo-link-rest";

const link = new RestLink({ uri: "https://localhost:7283/" });

export const client = new ApolloClient({
  cache: new InMemoryCache(),
  link,
});
