import { reducer } from "./reducer";
import * as t from "./actionTypes";

describe("reducer", () => {
  const initialState = {
    clickWrap: {},
    accountId: "",
    userEmail: "",
    baseUrl: "",
  };

  it("GET_CLICKWRAP_SUCCESS", () => {
    const action = {
      type: t.GET_CLICKWRAP_SUCCESS,
      payload: {
        clickWrap: {
          clickwrapId: "testClickwrapId",
          clickwrapName: "clickwrapNameTest",
          status: "active",
        },
        accountId: "testAccountId",
        userEmail: "testUserEmail",
        baseUrl: "testBaseUrl",
      },
    };

    expect(reducer(initialState, action)).toEqual({
      ...initialState,
      clickWrap: action.payload.clickWrap,
      accountId: action.payload.accountId,
      userEmail: action.payload.userEmail,
      baseUrl: action.payload.baseUrl,
    });
  });
});
