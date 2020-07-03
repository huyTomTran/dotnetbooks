import React, { Component } from 'react';

export class FetchItem extends Component {
  static displayName = FetchItem.name;

  constructor(props) {
    super(props);
    this.state = { items: [], loading: true };
  }

  componentDidMount() {
    this.populateItemsData();
  }

  static renderItemsTable(items) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Id</th>
            <th>Name</th>
          </tr>
        </thead>
        <tbody>
          {items.map(item =>
            <tr key={item.id}>
              <td>{item.id}</td>
              <td>{item.name}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchItem.renderItemsTable(this.state.items);

    return (
      <div>
        <h1 id="tabelLabel" >To Do Items</h1>
        <p>This component demonstrates fetching items from the server.</p>
        {contents}
      </div>
    );
  }

  async populateItemsData() {
    // const response = await fetch('http://localhost:5000/api/todoitems');
    const response = await fetch(`${process.env.REACT_APP_API_URL}/api/todoitems`);

    const data = await response.json();
    this.setState({ items: data, loading: false });
  }
}
