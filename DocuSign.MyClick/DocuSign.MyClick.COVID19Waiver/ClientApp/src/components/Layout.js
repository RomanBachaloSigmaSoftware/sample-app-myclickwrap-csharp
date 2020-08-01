import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { Header } from './Header';
import { Footer } from './Footer';

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
      <>
        <div className="app">
          <Header />
          <Container className="content">{this.props.children}</Container>
          <Footer />
        </div>
      </>
    );
  }
}