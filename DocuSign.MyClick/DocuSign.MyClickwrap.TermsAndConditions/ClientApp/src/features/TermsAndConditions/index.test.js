import React from "react";
import ReactDOM from "react-dom";
import { TermsAndConditions } from "./index";
import { MemoryRouter } from "react-router-dom";

jest.mock("jquery", () => {
  const m$ = {
    modal: jest.fn(),
  };
  return jest.fn(() => m$);
});

describe("TermsAndConditions", () => {
  it("renders without crashing", () => {
    const div = document.createElement("div");

    ReactDOM.render(
      <MemoryRouter>
        <TermsAndConditions />
      </MemoryRouter>,
      div
    );
    ReactDOM.unmountComponentAtNode(div);
  });
});
