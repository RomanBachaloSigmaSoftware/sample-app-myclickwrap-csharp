import React from 'react';
import { ScenarioList } from './components/ScenarioList';
import { ScenarioDescription } from './components/ScenarioDescription';

export const Scenarios = () => {
  return (
    <section className='container content-section'>
      <div className='row'>
        <ScenarioList />
        <ScenarioDescription />
      </div>
    </section>
  );
};
