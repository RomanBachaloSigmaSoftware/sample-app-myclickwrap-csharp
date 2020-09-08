import React from "react";
import { Link } from "react-router-dom";
import svg from "../assets/img/logo.svg";
import { useTranslation } from "react-i18next";

export const Header = () => {
  const { t } = useTranslation("Header");
  function refreshPage() {
    window.location.reload();
  }
  function navigate(link) {
    window.location.href = link;
  }
  return (
    <nav className="navbar navbar-expand-md navbar-light">
      <Link
        className="navbar-brand"
        to="#"
        onClick={() => {
          navigate(t("MyClickwrapLink"));
        }}
      >
        <img
          src={svg}
          className="d-inline-block align-top"
          alt={t("ApplicationName")}
          loading="lazy"
        />
        <strong>{t("ApplicationName")}</strong>
      </Link>
      <button
        className="navbar-toggler"
        type="button"
        data-toggle="collapse"
        data-target="#navbarsExample09"
        aria-controls="navbarsExample09"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span className="navbar-toggler-icon"></span>
      </button>

      <div className="collapse navbar-collapse" id="navbarsExample09">
        <ul className="navbar-nav ml-auto">
          <li className="nav-item">
            <a
              className="nav-link"
              href="#"
              target="blank"
              onClick={() => {
                refreshPage();
              }}
            >
              {t("RefreshSite")}{" "}
            </a>
          </li>
          <li className="nav-item">
            <a
              className="nav-link nav-link-external"
              href={t("SourceLink")}
              target="blank"
            >
              {t("SourceLinkName")} <span className="sr-only">(current)</span>
            </a>
          </li>
        </ul>
      </div>
    </nav>
  );
};
