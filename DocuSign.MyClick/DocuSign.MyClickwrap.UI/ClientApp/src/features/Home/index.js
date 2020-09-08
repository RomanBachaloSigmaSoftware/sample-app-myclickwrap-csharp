import React from "react";
import { Scenarios } from "../Scenarios";
import { useTranslation } from "react-i18next";

export const Home = () => {
  const { t } = useTranslation("Home");

  return (
    <>
      <div className="myclick-header text-center">
        <h1 className="h1">{t("Introduction1")}</h1>
        <p className="lead">{t("Introduction2")}</p>
      </div>
      <div className="row">
        <Scenarios />
      </div>
    </>
  );
};
