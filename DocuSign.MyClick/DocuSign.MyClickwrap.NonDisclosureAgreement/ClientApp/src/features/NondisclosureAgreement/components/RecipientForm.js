import React, { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import $ from "jquery";
import { InputText } from "../../../components/InputText";

const initialState = {
  fullName: "",
  email: "",
  company: "",
};

export const RecipientForm = ({ onSave }) => {
  const { t } = useTranslation("RecipientForm");
  const [submitted, setSubmitted] = useState(false);
  const [userData, setUserData] = useState({ ...initialState });
  const [errors, setErrors] = useState({});

  useEffect(() => {
    $("#modal").modal("show");
  }, []);

  function handleChange(event) {
    const { name } = event.target;
    const { [name]: removed, ...updatedErrors } = errors;
    const value = event.target.value;
    setErrors(updatedErrors);
    setUserData((userData) => ({
      ...userData,
      [name]: value,
    }));
  }

  function closeModal() {
    $("#modal").modal("hide");
  }

  function handleSave(event) {
    event.preventDefault();
    if (!formIsValid()) {
      return;
    }
    onSave(userData);
    setSubmitted(true);
    closeModal();
  }

  function formIsValid() {
    const { fullName, email, company } = userData;
    const errors = {};
    if (!fullName) {
      errors.fullName = t("Error.FullName");
    }
    if (!email) {
      errors.email = t("Error.Email");
    }
    if (!company) {
      errors.email = t("Error.Company");
    }
    setErrors(errors);
    return Object.keys(errors).length === 0;
  }

  return (
    <>
      <div
        className="modal show"
        id="modal"
        role="dialog"
        aria-labelledby="modalLabel-1"
        aria-hidden="true"
        data-backdrop="static"
        data-keyboard="false"
      >
        <div className="modal-dialog modal-dialog-centered" role="document">
          <div className="modal-content">
            <button
              type="button"
              className="close"
              data-dismiss="modal"
              aria-label="Close"
            >
              <span aria-hidden="true">&times;</span>
            </button>
            <div className="modal-body">
              <h4 className="modal-title" id="modalLabel-1">
                {t("Title")}
              </h4>
              <span className="modal-subtitle">{t("Description")}</span>

              <form
                action=""
                onSubmit={(event) => {
                  handleSave(event);
                }}
                className={submitted ? "was-validated" : ""}
                noValidate
              >
                {errors.onSave && (
                  <div className="alert alert-danger mt-2">{errors.onSave}</div>
                )}
                <div className="form-group">
                  <InputText
                    name="fullName"
                    label={t("FullName")}
                    value={userData.fullName}
                    onChange={handleChange}
                    error={errors.fullName}
                  />
                </div>
                <div className="form-group">
                  <InputText
                    name="email"
                    label={t("Email")}
                    value={userData.email}
                    onChange={handleChange}
                    error={errors.email}
                  />
                </div>
                <div className="form-group">
                  <InputText
                    name="company"
                    label={t("Company")}
                    value={userData.company}
                    onChange={handleChange}
                    error={errors.company}
                  />
                </div>
                <div className="buttons-group d-flex flex-wrap justify-content-end pt-3">
                <button className='btn btn-primary btn-sm' name='button' type='submit'>
                    {t('SubmitButton')}
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};
