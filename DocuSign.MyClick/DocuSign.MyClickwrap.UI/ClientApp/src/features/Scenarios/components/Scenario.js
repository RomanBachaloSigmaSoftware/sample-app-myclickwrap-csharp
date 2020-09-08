import PropTypes from "prop-types";
import React from "react";
import { useTranslation } from "react-i18next";
import { Nav } from "react-bootstrap";
import { ScenarioButton } from "./ScenarioButton";

export function Scenario(props) {
  const { t } = useTranslation("Scenarios");

  return (
    <Nav.Link eventKey={props.id} className="scenario-link ">
      <i className="scenario-link-icon">
        <img src={props.image} alt={t("Title")} />
      </i>
      <div className="scenario-link-body">
        <span className="scenario-link-title">
          {props.name} <i className="chevron"></i>
        </span>
        <p className="scenario-link-text">{props.description}</p>
        <ScenarioButton link={props.link} />
      </div>
    </Nav.Link>
  );
}

Scenario.propTypes = {
  name: PropTypes.string.isRequired,
};
