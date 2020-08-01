import React, { useEffect, useState } from 'react';
import { Route } from 'react-router-dom';
import * as api from '../api/api';
import { useTranslation } from 'react-i18next';

export function PrivateRoute({ children, ...rest }) {
  const { t } = useTranslation('Commmon');
    const [authState, setAuthState] = useState({
      isAuthenticated: false,
      loading: true,
    });
  
    useEffect(() => {
      async function auth() {
        try {
          const isAuthenticated = await api.isAuthenticated();
  
          setAuthState({ isAuthenticated, loading: false });
        } catch {
          setAuthState({ isAuthenticated: false, loading: false });
        }
      }  
      auth();
    }, []);
    return (
      <Route
        {...rest}
        render={() => {
          if (authState.loading) {
            return t('LoadingStatus');
          }
          if (authState.isAuthenticated) {
            return children;
          }
          window.location.href = `Account/Login`;
        }}
      />
    );
  }