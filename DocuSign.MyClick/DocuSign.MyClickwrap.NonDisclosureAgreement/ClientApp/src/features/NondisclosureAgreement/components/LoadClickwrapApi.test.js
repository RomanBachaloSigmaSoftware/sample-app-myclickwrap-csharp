import { LoadClickwrapApi } from "./LoadClickwrapApi";

describe("LoadClickwrapApi", () => {
  const testCallback = jest.fn();
  const testBaseUrl = "testBaseUrl";

  it("callback function should be called once when script exist", async () => {
    Object.defineProperty(document, "getElementById", {
      writable: true,
      value: jest.fn().mockImplementation((selector) => "test"),
    });

    LoadClickwrapApi(testBaseUrl, testCallback);

    expect(testCallback).toHaveBeenCalledTimes(1);
  });

  it("callback function should be called once when script does not exist", async () => {
    Object.defineProperty(document, "getElementById", {
      writable: true,
      value: jest.fn().mockImplementation((selector) => null),
    });

    LoadClickwrapApi(testBaseUrl, testCallback);

    expect(testCallback).toHaveBeenCalledTimes(1);
  });
});
