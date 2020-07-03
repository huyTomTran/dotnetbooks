import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { FetchItem } from './components/FetchItem';
import { FetchAuthor } from './components/FetchAuthor';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/fetch-data' component={FetchData} />
        <Route path='/fetch-item' component={FetchItem} />
        <Route path='/fetch-author' component={FetchAuthor} />
      </Layout>
    );
  }
}
