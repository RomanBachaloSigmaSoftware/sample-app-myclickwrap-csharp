import React, { useEffect, useState } from "react";
import { Route } from "react-router-dom";
import * as api from "../api/api";
import { Loader } from "./Loader";
import { Redirect } from "react-router-dom";

export function PrivateRoute({ children, ...rest }) {
    const [authState, setAuthState] = useState({
        isAuthenticated: false,
        loading: true,
        isError: false,
    });

    useEffect(() => {
        async function auth() {
            try {
                const isAuthenticated = await api.isAuthenticated();
                setAuthState({
                    isAuthenticated,
                    loading: false,
                    isError: false,
                });
            } catch {
                setAuthState({
                    isAuthenticated: false,
                    loading: false,
                    isError: true,
                });
            }
        }
        auth();
    }, []);
    return (
        <Route
            {...rest}
            render={() => {
                if (authState.loading) {
                    return <Loader />;
                }
                if (authState.isError) {
                    return <Redirect to="/error" />;
                }
                if (authState.isAuthenticated) {
                    return children;
                }

                window.location.href = `Account/Login`;
            }}
        />
    );
}
