import React from "react";
import { useTranslation } from "react-i18next";
import parse from "html-react-parser";

export const About = () => {
  const { t } = useTranslation("About");
  return (
    <>
      <br></br>
      <h2 id="sample-app">{t("Title")}</h2>
      <blockquote>{parse(t("Description"))}</blockquote>

      <div className="row">
        <div className="col-md-6">
          <p>
            <strong>
              {t("SourceButton")}
              <a target="_blank" href={t("GitHubLink")} rel="noopener noreferrer">
                {t("GitHubLink")}
              </a>
            </strong>
          </p>
          <br></br>
          <br></br>
          {parse(t("ScenariosList"))}
        </div>
      </div>
      {parse(t("AboutDocuSign"))}
      <br></br>
    </>
  );
};
