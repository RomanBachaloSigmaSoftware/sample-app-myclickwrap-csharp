import React from 'react';
import { useTranslation } from 'react-i18next';

export const PageCaption = () => {
  const { t } = useTranslation('PageCaption');
  return <h1>{t('Title')}</h1>;
};
