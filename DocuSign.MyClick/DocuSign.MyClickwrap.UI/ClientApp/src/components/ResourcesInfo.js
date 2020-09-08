import React from "react";
import { useTranslation } from "react-i18next";

export const ResourcesInfo = () => {
  const { t } = useTranslation("ResourcesInfo");
  return (
    <section className="resources-section">
      <div className="container">
        <h3 className="h3">{t("Title")}</h3>
        <ul className="list-inline">
          <li className="list-inline-item">
            <a href={t("Link1.URL")} rel="noopener noreferrer" target="_blank">
              {t("Link1.Title")}
            </a>
          </li>
          <li className="list-inline-item">
            <a href={t("Link2.URL")} rel="noopener noreferrer" target="_blank">
              {t("Link2.Title")}
            </a>
          </li>
          <li className="list-inline-item">
            <a href={t("Link3.URL")} rel="noopener noreferrer" target="_blank">
              {t("Link3.Title")}
            </a>
          </li>
        </ul>
      </div>
    </section>
  );
};
