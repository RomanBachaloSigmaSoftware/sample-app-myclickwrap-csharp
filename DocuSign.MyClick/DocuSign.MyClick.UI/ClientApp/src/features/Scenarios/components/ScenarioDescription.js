import React from 'react';
import { useTranslation } from 'react-i18next';

export const ScenarioDescription = () => {
  const { t } = useTranslation('Scenarios');
  return (
    <div className='col-lg-6'>
      <p> {t('Title')}</p>
    </div>
  );
};
