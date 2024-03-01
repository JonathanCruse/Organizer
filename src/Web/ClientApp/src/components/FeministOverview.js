import React, { Component } from 'react';
import { FeministsClient, ExpensesClient, CollectivesClient, TransactionsClient } from '../web-api-client.ts';

export class FeministOverview extends Component {
  static displayName = FeministOverview.name;

  constructor(props) {
    super(props);
    this.state = {
      expenses_data: [],
      expenses_loading: true,
      collectives_data: [],
      collectives_loading: true,
      balance_data: [],
      balance_loading: true,
      amount: 0,
      description: 0,
      collectiveid: null,
      notice: ""
    };
  }

  componentDidMount() {
    this.populateData();
  }

  static renderExpenses(expenses) {
    return (
      <>
        <table className="table table-striped" aria-labelledby="tableLabel">
          <thead>
            <tr>
              <th>Ausgabe</th>
              <th>Datum</th>
            </tr>
          </thead>
          <tbody>
            {expenses.map(expense =>
              <tr key={expense.id}>

                <td>{expense.amount} &euro;</td>
                <td>{new Date(expense.lastModified).toLocaleDateString()}</td>
              </tr>
            )}
          </tbody>
        </table>
      </>
    );
  }

  static renderCollectives(collectives) {
    return (
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>Name</th>
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
  static renderBalance(balance) {

    if (balance > 0) {
      return (
        <div class="balance success info alert alert-success">

          Dein aktueller Stand ist {parseFloat(balance).toFixed(2)}&euro;
        </div>
      );
    } else {
      return (
        <div class="balance warning info alert alert-warning">
          Du hast {((-1) * parseFloat(balance)).toFixed(2)}&euro; Schulden.
        </div>
      )
    }

  }

  renderCreateTransaction() {
    if (this.state.collectives_data.length > 0) {
      return (
        <form>
          <div class="form-group">
            <label class="form-label">Wieviel Geld hast Du ausgegeben?</label>
            <input class="form-control" type="number" name="amount" value={this.state.amount} onChange={x => this.handleChange("amount", x.target.value)} />
          </div>
          <div class="form-group">
            <label class="form-label">Wof&uuml;r hast Du das Geld ausgegeben?</label>
            <input class="form-control" type="text" name="description" value={this.state.description} onChange={x => this.handleChange("description", x.target.value)} />
          </div>
          <div class="form-group mb-3">
            <label class="form-label">F&uuml;r welches Kollektiv war die Ausgabe?</label>
            <select name="collectiveid" class="form-control" value={this.state.collectiveId} onChange={x => this.handleChange("collectiveid", x.target.value)}>
              <option disabled >Bitte ausw&auml;hlen</option>
              {this.state.collectives_data.map(function (x) {
                return <option selected key={x.id} value={x.id}>{x.name}  </option>

              })}
            </select>
          </div>

          <button type="button" class="btn btn-primary" onClick={async () => {
            let transactionClient = new TransactionsClient();
            await transactionClient.createTransaction({ description: this.state.description, amount: this.state.amount, collectiveId: this.state.collectiveid });
            this.setState({ notice: "Bis die Ausgabe im Kollektiv verrechnet ist, kann etwas Zeit vergehen. Wir passen Deine Bilanz zeitnah an." });
          }}>
            Ausgabe vergesellschaften
          </button>
        </form>
      )
    }
  }

  renderNotice() {
    if (this.state.notice !== "")
      return (
        <div class="info alert alert-info" onClick={x => this.setState({ notice: "" })}>
          <div>Ausgabe wurde dokumentiert</div>
          <div>{this.state.notice}</div>
        </div>
      )
    else return <div></div>
  }

  render() {
    let expenseContent = this.state.expenses_loading
      ? <p><em>Loading...</em></p>
      : FeministOverview.renderExpenses(this.state.expenses_data);

    let collectiveContent = this.state.collectives_loading
      ? <p><em>Loading...</em></p>
      : FeministOverview.renderCollectives(this.state.collectives_data);

    let balanceContent = this.state.balance_loading
      ? <p><em>Loading...</em></p>
      : FeministOverview.renderBalance(this.state.balance_data);


    return (
      <div>
        <div>
          <h1 id="tableLabel">Deine Ausgaben</h1>
          <p>Ich suche aktuell alle Infos zu Deinen Ausgaben - einen Moment bitte..</p>
          {expenseContent}
        </div>
        <div>
          <h1 id="tableLabel">Du bist Teil dieser Kollektive</h1>
          <p>Ich suche aktuell alle Infos zu Deinen Collektiven - einen Moment bitte..</p>
          {collectiveContent}
        </div>
        <div>
          <h1 id="tableLabel">Deine Bilanz</h1>
          <p>Ich suche summiere aktuell Deine Ausgaben - einen Moment bitte</p>
          {balanceContent}
        </div>

        {this.renderCreateTransaction()}

        {this.renderNotice()}
      </div>
    );
  }



  async populateData() {


    let expenseClient = new ExpensesClient();
    const expenseData = await expenseClient.getExpenses(1, 1000);
    this.setState({ expenses_data: expenseData.items, expenses_loading: false });

    let collectivesClient = new CollectivesClient();
    const collectivesData = await collectivesClient.getCollectives(1, 1000);
    this.setState({ collectives_data: collectivesData.items, collectives_loading: false, collectiveid: collectivesData.items[0].id });

    let feministClient = new FeministsClient();
    const balanceData = await feministClient.getBalance({});
    this.setState({ balance_data: balanceData, balance_loading: false });
  }

  async handleChange(type, value) {
    if (type === "amount") await this.setState({ amount: value });
    if (type === "description") await this.setState({ description: value });
    if (type === "collectiveid") await this.setState({ collectiveid: value });
  };

}
