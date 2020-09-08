import React from "react";
import ReactDOM from "react-dom";
import { Covid19Waiver } from "./index";
import { MemoryRouter } from "react-router-dom";

jest.mock("jquery", () => {
  const m$ = {
    modal: jest.fn(),
  };
  return jest.fn(() => m$);
});

describe("Covid19Waiver", () => {
  it("renders without crashing", () => {
    const div = document.createElement("div");

    ReactDOM.render(
      <MemoryRouter>
        <Covid19Waiver />
      </MemoryRouter>,
      div
    );
    ReactDOM.unmountComponentAtNode(div);
  });
});
