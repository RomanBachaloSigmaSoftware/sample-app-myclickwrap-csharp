import React, { useState, useReducer, useEffect } from "react";
import { reducer } from "./reducer";
import * as api from "../../api/api";
import * as Actions from "./actionTypes";
import { ClickWrap } from "./components/Clickwrap";
import { RecipientForm } from "./components/RecipientForm";
import { LoadClickwrapApi } from "./components/LoadClickwrapApi";
import { Loader } from "../../components/Loader";

const initialState = {
  clickWrap: null,
  accountId: "",
  userEmail: "",
  baseUrl: "",
};

export const Covid19Waiver = () => {
  const [state, dispatch] = useReducer(reducer, initialState);
  const [clickApiReady, setClickApiReady] = useState(false);
  const [isClickWrapLoading, setIsClickWrapLoading] = useState(false);

  useEffect(() => {
    if (state.baseUrl) {
      LoadClickwrapApi(state.baseUrl, () => {
        setClickApiReady(true);
      });
    }
  }, [state.baseUrl]);

  async function handleSave(userData) {
    setIsClickWrapLoading(true);
    try {
      const response = await api.getClickwrap();
      dispatch({
        type: Actions.GET_CLICKWRAP_SUCCESS,
        payload: {
          clickWrap: response.clickWrap,
          accountId: response.accountId,
          userEmail: userData.email,
          baseUrl: response.docuSignBaseUrl,
        },
      });
    } catch (error) {
      throw error;
    } finally {
      setIsClickWrapLoading(false);
    }
  }

  return (
    <>
      <RecipientForm onSave={handleSave} />
      <div id="ds-clickWrap"></div>
      {state.clickWrap && clickApiReady && state.userEmail ? (
        <ClickWrap
          accountId={state.accountId}
          clickWrapId={state.clickWrap.clickwrapId}
          userEmail={state.userEmail}
          baseUrl={state.baseUrl}
        />
      ) : 
        isClickWrapLoading && <Loader />
      }
    </>
  );
};
