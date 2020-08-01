import React from 'react';
import { Link } from 'react-router-dom';
import svg from  '../assets/img/logo.svg';
import { useTranslation } from 'react-i18next';

export const Header = () => {
  const { t } = useTranslation('Header');
  return (
      <nav className="navbar navbar-expand-md navbar-light">
        <Link className="navbar-brand" to="/">
            <img src={svg}  className="d-inline-block align-top" alt="MyClick" loading="lazy" />
            <strong>{t('ApplicationName')}</strong>
        </Link>
        <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarsExample09" aria-controls="navbarsExample09" aria-expanded="false" aria-label="Toggle navigation">
            <span className="navbar-toggler-icon"></span>
        </button>

        <div className="collapse navbar-collapse" id="navbarsExample09">
            <ul className="navbar-nav ml-auto">
                <li className="nav-item">
                    <a className="nav-link nav-link-external" href='https://github.com/docusign/' target="blank"> {t('GitHubLink')} <span className="sr-only">(current)</span></a>
                </li>
            </ul>
        </div>
    </nav>
  );
};
