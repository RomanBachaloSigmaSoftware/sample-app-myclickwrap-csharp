import React, { Component } from "react";
import { Header } from "./Header";
import { Footer } from "./Footer";
import { ResourcesInfo } from "./ResourcesInfo";

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
      <div className="app">
        <Header />
        <main>
          <section className="scenario-section">
            <div className="container">{this.props.children}</div>
          </section>
          <ResourcesInfo />
        </main>
        <Footer />
      </div>
    );
  }
}
