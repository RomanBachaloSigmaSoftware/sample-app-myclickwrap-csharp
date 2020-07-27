import React from 'react';
import { useTranslation } from 'react-i18next';

export const ResourcesInfo = () => {
  const { t } = useTranslation('ResourcesInfo');
  return <p>{t('Title')}</p>;
};
