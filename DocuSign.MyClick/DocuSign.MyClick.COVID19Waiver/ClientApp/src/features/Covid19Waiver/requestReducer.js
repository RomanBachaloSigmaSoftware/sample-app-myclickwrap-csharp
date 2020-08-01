import * as types from './actionTypes';

export function reducer(
  state = { clickWrap: {}, accountId: '', userEmail: '', baseUrl: '' },
  action
) {
  switch (action.type) {
    case types.GET_CLICKWRAP_SUCCESS: {
      const { clickWrap, accountId, userEmail, baseUrl } = action.payload;      
      return { ...state, clickWrap,accountId, userEmail, baseUrl };
    }
    default:
      return state;
  }
}
