import React from "react";
import { ClickWrap } from "./ClickWrap";
import { render } from "@testing-library/react";

describe("ClickWrap", () => {
  beforeEach(() => {
    Object.defineProperty(window, "docuSignClick", {
      writable: true,
      value: {
        Clickwrap: {
          render: jest.fn().mockImplementation((options, selector) => "clickWrap"),
        },
      },
    });
  });

  it("renders without crashing", () => {
    const div = document.createElement("div");

    render(
      <ClickWrap
        accountId="testAccountId"
        clickWrapId="testClickWrapId"
        userEmail="testUserEmail"
        baseUrl="testBaseUrl"
      />,
      div
    );

    expect(window.docuSignClick.Clickwrap.render).toHaveBeenCalledWith(
      {
        environment: "testBaseUrl",
        accountId: "testAccountId",
        clickwrapId: "testClickWrapId",
        clientUserId: "testUserEmail",
      },
      "#ds-clickWrap"
    );
  });
});
