import React from 'react';
import { useTranslation } from 'react-i18next';

export const ScenarioList = () => {
  const { t } = useTranslation('Scenarios');
  return (
    <div className='col-lg-6'>
      <p> {t('Title')}</p>
      <ul>
        <li>
          <a href='https://localhost:7001/'> {t('TermsAndConditions')}</a>
        </li>
        <li>
          <a href='https://localhost:8001/'> {t('NDA')}</a>
        </li>
        <li>
          <a href='https://localhost:6001/'> {t('COVID19Waiver')}</a>
        </li>
      </ul>
    </div>
  );
};
