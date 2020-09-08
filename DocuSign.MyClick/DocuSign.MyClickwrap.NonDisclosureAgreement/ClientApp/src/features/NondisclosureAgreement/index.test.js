import React from "react";
import ReactDOM from "react-dom";
import { NondisclosureAgreement } from "./index";
import { MemoryRouter } from "react-router-dom";

jest.mock("jquery", () => {
  const m$ = {
    modal: jest.fn(),
  };
  return jest.fn(() => m$);
});

describe("NondisclosureAgreement", () => {
  it("renders without crashing", () => {
    const div = document.createElement("div");

    ReactDOM.render(
      <MemoryRouter>
        <NondisclosureAgreement />
      </MemoryRouter>,
      div
    );
    ReactDOM.unmountComponentAtNode(div);
  });
});
