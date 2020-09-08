import axios from "./interceptors";
import * as apiHelpers from "./apiHelpers";
import { getClickwrap } from "./api";
import { isAuthenticated } from "./api";

jest.mock("axios");

describe("getClickwrap", () => {
  const successResponse = {
    docuSignBaseUrl: "https://test.docusign.net",
    clickWrap: {
      clickwrapId: "testClickwrapId",
      clickwrapName: "clickwrapNameTest",
      status: "active",
    },
    accountId: "testAccountId",
    userId: "testUserId",
  };

  it("should call handleResponse on success response", async () => {
    axios.get.mockResolvedValueOnce(successResponse);
    const handleResponseSpy = spyOn(apiHelpers, "handleResponse");
    await getClickwrap();

    expect(handleResponseSpy).toHaveBeenCalled();
  });

  it("should call handleError on error response", async () => {
    const error = new Error();
    axios.get.mockRejectedValueOnce(error);

    const handleErrorSpy = spyOn(apiHelpers, "handleError");

    await getClickwrap();

    expect(handleErrorSpy).toHaveBeenCalled();
  });
});

describe("isAuthenticated", () => {
  const successResponse = true;

  it("should call handleResponse on success response", async () => {
    axios.get.mockResolvedValueOnce(successResponse);
    const handleResponseSpy = spyOn(apiHelpers, "handleResponse");

    await isAuthenticated();

    expect(handleResponseSpy).toHaveBeenCalled();
  });

  it("should call handleError on error response", async () => {
    const error = new Error();
    axios.get.mockRejectedValueOnce(error);

    const handleErrorSpy = spyOn(apiHelpers, "handleError");

    await isAuthenticated();

    expect(handleErrorSpy).toHaveBeenCalled();
  });
});
