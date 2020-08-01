import React from 'react';
import { useTranslation } from 'react-i18next';

export const Footer = () => {
  const { t } = useTranslation('Footer');
  return (
    <footer className='footer border-top'>
      <div className='container text-center p-2'>
        <strong className='copyright'>{t('Copyright')}</strong>
      </div>
    </footer>
  );
};
