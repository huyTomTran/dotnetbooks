// this example shows how to fetch data by using 'Axios'
import React, {Component} from 'react';
import axios from 'axios';

export class FetchAuthor extends Component {
  state = {
    authors: []
  }
  componentDidMount() {
    // axios.get('http://localhost:5000/api/Authors').then((response)=> {
      axios.get(`${process.env.REACT_APP_API_URL}/api/Authors`).then((response)=> {
      
      this.setState({
        authors: response.data
      })
    })
  }
  render() {
      return (
    <div className="FetchAuthor">
      <header className="FetchAuthor-header">
        <h1>List of Authors</h1>
        <ul>
          {this.state.authors.map((author) => (
            // elements authorID, title, and descripton can NOT be CAPITALIZED
            <li key="{author.authorID}" >{ author.title } - { author.description } </li>
          ))}
        </ul>
      </header>
    </div>
  );
  }
}