import React, { Component } from 'react';
import { CollectivesClient, FeministsClient } from '../web-api-client.ts';

export class CollectiveOverview extends Component {
  static displayName = CollectiveOverview.name;

  constructor(props) {
    super(props);
    this.state = {
      collectives: [],
      loading: true,
      name: null,
      email: null,
      collectiveId: null
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

        <div class="container">
          <div class="row">

            {this.renderCreateCollective()}
            {this.renderInviteFeminist()}
          </div>
        </div>

      </>
    );
  }

  renderCreateCollective() {
    return (
      <form class="col-6">
      <h2>Neues Kollektiv erstellen</h2>
        <div class="form-group mb-3">
          <label class="form-label">Wie soll das neue Kollektiv hei&amp;en?</label>
          <input class="form-control" type="text" name="name" value={this.state.name} onChange={x => this.handleNameChange(x.target.value)} />
        </div>

        <button type="button" class="btn btn-primary" onClick={async () => {
          let transactionClient = new CollectivesClient();
          await transactionClient.createCollective({ name: this.state.name });
          setTimeout(1000, () => window.location.reload());
        }}>
          Neues Kollektiv erstellen
        </button>
      </form>
      )
  }

  renderInviteFeminist() {
    return (
      <form class="col-6">
      <h2>User einladen</h2>
        <div class="form-group mb-3">
          <label class="form-label">E-Mail-Adresse</label>
          <input class="form-control" type="text" name="email" value={this.state.email} onChange={x => this.handleInviteFeminist("email", x.target.value)} />
        </div>
        <div class="form-group mb-3">
          <label class="form-label">F&uuml;r welches Kollektiv war die Ausgabe?</label>
          <select name="collectiveid" class="form-control" value={this.state.collectiveId} onChange={x => this.handleInviteFeminist("collectiveId", x.target.value)}>
            <option disabled >Bitte ausw&auml;hlen</option>
            {this.state.collectives.map(function (x) {
              return <option selected key={x.id} value={x.id}>{x.name}  </option>

            })}
          </select>
        </div>

        <button type="button" class="btn btn-primary" onClick={async () => {
          let feministClient = new FeministsClient();
          await feministClient.createFeminist({ email: this.state.email, collectiveid : this.state.collectiveId });
          window.location.reload();
        }}>
          Neues Kollektiv erstellen
        </button>
      </form>
    )
  }

  async handleNameChange(value) {
    await this.setState({ name: value});
  };
  async handleInviteFeminist(type, value) {
    if (type === "email") await this.setState({ email: value });
    if (type === "collectiveId") await this.setState({ collectiveId: value });
  }

  async polupateData() {
    let collectivesClient = new CollectivesClient();
    const collectivesData = await collectivesClient.getCollectives(1, 1000);
    this.setState({ collectives: collectivesData.items, loading: false });
  }
}
