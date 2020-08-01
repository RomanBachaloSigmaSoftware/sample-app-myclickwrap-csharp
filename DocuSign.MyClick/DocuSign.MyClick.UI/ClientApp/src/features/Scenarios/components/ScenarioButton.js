import PropTypes from "prop-types";
import React from "react";
import { useTranslation } from "react-i18next";

export function ScenarioButton(props) {
  const { t } = useTranslation("Scenarios");
  function navigate(link) {
    window.location.href = link;
  }

  return (
    <button
      type="button"
      className="btn btn-primary"
      onClick={() => navigate(props.link)}
    >
      {t("TryMeButton")}
    </button>
  );
}

ScenarioButton.propTypes = {
  link: PropTypes.string.isRequired,
};
