import React from "react";
import ReactDOM from "react-dom";
import { RecipientForm } from "./RecipientForm";
import { MemoryRouter } from "react-router-dom";
import { act } from "react-dom/test-utils";
import { fireEvent, render } from "@testing-library/react";

jest.mock("jquery", () => {
  const m$ = {
    modal: jest.fn(),
  };
  return jest.fn(() => m$);
});

describe("RecipientForm", () => {
  it("renders without crashing", () => {
    const div = document.createElement("div");

    ReactDOM.render(
      <MemoryRouter>
        <RecipientForm />
      </MemoryRouter>,
      div
    );
    ReactDOM.unmountComponentAtNode(div);
  });

  it("on valid form calls handleSave function", async () => {
    const mockHandleSave = jest.fn();
    const { getByLabelText, getByText } = render(
      <RecipientForm onSave={mockHandleSave} />
    );
    await act(async () => {
      fireEvent.change(getByLabelText("FullName"), {
        target: { value: "testFullName" },
      });
      fireEvent.change(getByLabelText("Email"), {
        target: { value: "test@email.com" },
      });
    });

    await act(async () => {
      fireEvent.click(getByText("SubmitButton"));
    });

    expect(mockHandleSave).toHaveBeenCalled();
  });

  it("on error data handleSave function has not been called", async () => {
    const mockHandleSave = jest.fn();
    const { getByLabelText, getByText } = render(
      <RecipientForm onSave={mockHandleSave} />
    );
    await act(async () => {
      fireEvent.change(getByLabelText("FullName"), {
        target: { value: "" },
      });
      fireEvent.change(getByLabelText("Email"), {
        target: { value: "" },
      });
    });

    await act(async () => {
      fireEvent.click(getByText("SubmitButton"));
    });

    expect(mockHandleSave).not.toHaveBeenCalled();
  });

  it("renders email validation error", async () => {
    const mockHandleSave = jest.fn();
    const { getByLabelText, getByText, container } = render(
      <RecipientForm onSave={mockHandleSave} />
    );
    await act(async () => {
      const emailInput = getByLabelText("Email");
      fireEvent.change(emailInput, {
        target: { value: "" },
      });
      fireEvent.blur(emailInput);
    });

    await act(async () => {
      fireEvent.click(getByText("SubmitButton"));
    });

    expect(container.innerHTML).toMatch("Error.Email");
  });

  it("renders full name validation error", async () => {
    const mockHandleSave = jest.fn();
    const { getByLabelText, getByText, container } = render(
      <RecipientForm onSave={mockHandleSave} />
    );
    await act(async () => {
      const fullNameInput = getByLabelText("FullName");
      fireEvent.change(fullNameInput, {
        target: { value: "" },
      });
      fireEvent.blur(fullNameInput);
    });

    await act(async () => {
      fireEvent.click(getByText("SubmitButton"));
    });

    expect(container.innerHTML).toMatch("Error.FullName");
  });
});
