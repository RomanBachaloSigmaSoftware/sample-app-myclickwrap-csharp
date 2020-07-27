import React, { Component } from 'react';
import { PageCaption } from '../../components/PageCaption';
import { Scenarios } from '../Scenarios';
import { ResourcesInfo } from '../../components/ResourcesInfo';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <>
        <PageCaption />
        <Scenarios />
        <ResourcesInfo />
      </>
    );
  }
}
