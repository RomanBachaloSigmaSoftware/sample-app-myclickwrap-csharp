import React from "react";
import ReactDOM from "react-dom";
import { Layout } from "./Layout";
import { MemoryRouter } from "react-router-dom";

describe("Layout", () => {
  it("renders without crashing", () => {
    const div = document.createElement("div");
    ReactDOM.render(
      <MemoryRouter>
        <Layout />
      </MemoryRouter>,
      div
    );
    ReactDOM.unmountComponentAtNode(div);
  });
});
