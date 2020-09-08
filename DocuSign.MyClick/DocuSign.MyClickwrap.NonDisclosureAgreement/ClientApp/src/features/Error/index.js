import React from "react";
import { useTranslation } from "react-i18next";

export const Error = () => {
  const { t } = useTranslation("Error");
  return (
    <>
      <div className="myclick-header text-center">
        <h1 className="h1">{t("Title")}</h1>
        <p className="lead">{t("Description")}</p>
      </div>
    </>
  );
};
