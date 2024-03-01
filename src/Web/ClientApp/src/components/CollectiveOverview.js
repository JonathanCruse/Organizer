import React, { Component } from 'react';
import { CollectivesClient } from '../web-api-client.ts';

export class CollectiveOverview extends Component {
  static displayName = CollectiveOverview.name;

  constructor(props) {
    super(props);
    this.state = {
      collectives: [],
      loading: true,
      name: null
    };
  }

  componentDidMount() {
    this.polupateData();
  }

  static renderCollectives(collectives) {
    return (
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>Name</th>
            <ht>Erstellungsdatum</ht>
          </tr>
        </thead>
        <tbody>
          {collectives.map(collective =>
            <tr key={collective.id}>
              <td>{collective.name}</td>
              <td>{new Date(collective.lastModified).toLocaleDateString()}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Rufe Daten ab...</em></p>
      : CollectiveOverview.renderCollectives(this.state.collectives);

    return (
      <>
        <div>
          <h1 id="tableLabel">Weather forecast</h1>
          <p>This component demonstrates fetching data from the server.</p>
          {contents}
        </div>

        {this.renderCreateCollective()}
      </>
    );
  }

  renderCreateCollective() {
    return (
      <form>
        <div class="form-group mb-3">
          <label class="form-label">Wie soll das neue Kollektiv heiﬂen?</label>
          <input class="form-control" type="text" name="name" value={this.state.name} onChange={x => this.handleNameChange(x.target.value)} />
        </div>

        <button type="button" class="btn btn-primary" onClick={async () => {
          let transactionClient = new CollectivesClient();
          await transactionClient.createCollective({ name: this.state.name });
          document.location.reload();
        }}>
          Neues Kollektiv erstellen
        </button>
      </form>
      )
  }

  async handleNameChange(value) {
    await this.setState({ name: value});
  };

  async polupateData() {
    let collectivesClient = new CollectivesClient();
    const collectivesData = await collectivesClient.getCollectives(1, 1000);
    this.setState({ collectives: collectivesData.items, loading: false });
  }
}
