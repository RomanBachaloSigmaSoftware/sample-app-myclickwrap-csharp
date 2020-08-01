import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route path='/login' component={() => {
                    window.location.href = 'Account/Login';
                    return null;
                }} />
                <Route exact path='/' component={Home} />
                <Route path='/NDA' component={() => {
                    window.location.href = 'api/ClickWrap';
                    return null;
                }} />
            </Layout>
        );
    }
}