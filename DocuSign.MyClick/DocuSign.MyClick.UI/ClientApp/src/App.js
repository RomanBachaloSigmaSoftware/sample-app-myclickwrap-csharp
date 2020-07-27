import React, { Suspense } from 'react';
import { Route, Switch } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './features/Home/index';
import { About } from './features/About/index';

import './custom.css';

const App = () => {
  const routes = (
    <Switch>
      <Route path='/about' component={About} />
      <Route path='/' exact component={Home} />
    </Switch>
  );
  return (
    <Suspense fallback=''>
      <Layout>{routes}</Layout>
    </Suspense>
  );
};
export default App;
