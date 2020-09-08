import React, { Suspense } from "react";
import { Switch, Route } from "react-router-dom";
import { Layout } from "./components/Layout";
import { PrivateRoute } from "./components/PrivateRoute";
import { Home } from "./features/Home";
import { Error } from "./features/Error";
import "./assets/scss/main.scss";

const App = () => {
  const routes = (
    <Switch>
      <Route path="/error">
        <Error />
      </Route>
      <PrivateRoute path="/">
        <Home />
      </PrivateRoute>
    </Switch>
  );

  return (
    <Suspense fallback="">
      <Layout>{routes}</Layout>
    </Suspense>
  );
};

export default App;
