import React from "react";
import ReactDOM from "react-dom";
import { Home } from "./index";
import { MemoryRouter } from "react-router-dom";

jest.mock("jquery", () => {
  const m$ = {
    modal: jest.fn(),
  };
  return jest.fn(() => m$);
});

describe("Home", () => {
  it("renders without crashing", () => {
    const div = document.createElement("div");

    ReactDOM.render(
      <MemoryRouter>
        <Home />
      </MemoryRouter>,
      div
    );
    ReactDOM.unmountComponentAtNode(div);
  });
});
