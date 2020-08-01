import axios from "./interceptors";
import { handleResponse, handleError } from "./apiHelpers";

export async function isAuthenticated() {
  try {
    const response = await axios.get(
      'api/IsAuthenticated'
    );
    return handleResponse(response);
  } catch (error) {
    handleError(error);
  }
}

export async function getClickwrap() {
    try {
      const response = await axios.get(
        'api/ClickWrap'
      );
      return handleResponse(response);
    } catch (error) {
      handleError(error);
    }
  }
  
  
  