import React from "react";
import { useTranslation } from "react-i18next";
import { Scenario } from "./Scenario";
import { ScenarioDescription } from "./ScenarioDescription";
import svgScenario1 from "../../../assets/img/ico-covid.svg";
import svgScenario2 from "../../../assets/img/ico-nda.svg";
import svgScenario3 from "../../../assets/img/ico-terms.svg";
import { Tab, Nav } from "react-bootstrap";

export function ScenarioList(props) {
  const { t } = useTranslation("Scenarios");

  return (
    <Tab.Container id="left-tabs-example" defaultActiveKey="scenario1">
      <div className="col-lg-6 col-xl-6">
        <h2 className="h2 text-center text-md-left"> {t("Title")}</h2>
        <Nav className="scenario-tabs  flex-column">
          <Scenario
            id="scenario1"
            name={t("Scenario1.Title")}
            link={t("Scenario1.Link")}
            description={t("Scenario1.Description")}
            codeFlow={t("Scenario1.Description")}
            image={svgScenario1}
          />
          <Scenario
            id="scenario2"
            name={t("Scenario2.Title")}
            link={t("Scenario2.Link")}
            description={t("Scenario2.Description")}
            codeFlow={t("Scenario2.Description")}
            image={svgScenario2}
          />
          <Scenario
            id="scenario3"
            name={t("Scenario3.Title")}
            link={t("Scenario3.Link")}
            description={t("Scenario3.Description")}
            image={svgScenario3}
          />
        </Nav>
      </div>
      <div className="col-lg-6 col-xl-6">
        <Tab.Content id="content">
          <ScenarioDescription
            id="scenario1"
            description={t("Scenario1.Description")}
            intro={t("Scenario1.Intro")}
            name={t("Scenario1.Title")}
            codeFlow={t("Scenario1.CodeFlow")}
            image={svgScenario1}
            link={t("Scenario1.Link")}
          />
          <ScenarioDescription
            id="scenario2"
            description={t("Scenario2.Description")}
            intro={t("Scenario2.Intro")}
            name={t("Scenario2.Title")}
            codeFlow={t("Scenario2.CodeFlow")}
            image={svgScenario2}
            link={t("Scenario2.Link")}
          />
          <ScenarioDescription
            id="scenario3"
            description={t("Scenario3.Description")}
            intro={t("Scenario3.Intro")}
            name={t("Scenario3.Title")}
            codeFlow={t("Scenario3.CodeFlow")}
            image={svgScenario3}
            link={t("Scenario3.Link")}
          />
        </Tab.Content>
      </div>
    </Tab.Container>
  );
}
