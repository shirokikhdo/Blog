import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h1>It's SWITTER web application</h1>
        <p>Created by shirokikhdo</p>
        <a href='https://github.com/shirokikhdo/Blog'>GutHub</a>
      </div>
    );
  }
}