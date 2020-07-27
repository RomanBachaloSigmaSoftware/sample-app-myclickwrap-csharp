import React from 'react';
import { useTranslation } from 'react-i18next';

export const About = () => {
  const { t } = useTranslation('About');
  return (
    <div className='container'>
      <br></br>
      <h2 id='sample-app'>{t('Title')}</h2>

      <p>
        <a href='http://www.docusign.com/'> {t('DocuSignLink')}</a>
      </p>
    </div>
  );
};
