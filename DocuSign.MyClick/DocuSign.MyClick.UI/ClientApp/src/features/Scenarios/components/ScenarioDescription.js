import React from "react";
import parse from "html-react-parser";
import { Tab } from "react-bootstrap";
import { ScenarioButton } from "./ScenarioButton";

export const ScenarioDescription = (props) => {
  return (
      <Tab.Pane eventKey={props.id}>
        <div className="scenario-link" role="tab" id={"heading-" + props.id}>
          <i className="scenario-link-icon">
            <img src={props.image} alt="Terms" />
          </i>
          <div className="scenario-link-body">
            <a
              className="scenario-link-title collapsed"
              data-toggle="collapse"
              href={"#collapse-" + props.id}
              data-parent="#content"
              aria-expanded="false"
              aria-controls={"collapse-" + props.id}
            >
              {props.name}
              <i className="chevron"></i>
            </a>
            <p className="scenario-link-text">{props.description}</p>
            <ScenarioButton link={props.link} />
          </div>
        </div>
        <div
          id={"collapse-" + props.id}
          className="collapse"
          role="tabpanel"
          aria-labelledby={"heading-" + props.id}
        >
          <div className="card">
            <div className="card-header">{props.name}</div>
            <div className="card-body">
              <div className="scroll">
                {props.intro ? (
                  <div className="alert alert-primary" role="alert">
                    {props.intro}
                  </div>
                ) : (
                  ""
                )}

                {parse(props.codeFlow)}
              </div>
            </div>
          </div>
        </div>
      </Tab.Pane>
  );
};
