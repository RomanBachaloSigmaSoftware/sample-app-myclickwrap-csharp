import React from "react";
import { useTranslation } from "react-i18next";
import { TermsAndConditions } from "../TermsAndConditions/index";

export const Home = () => {
  const { t } = useTranslation("Home");
  return (
    <>
      <div className="myclick-header text-center">
        <h1 className="h1">{t("Title")}</h1>
        <p className="lead">{t("Description")}</p>
      </div>
      <TermsAndConditions />
    </>
  );
};
