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
      collectiveId: null
    };
  }
  handleChange = (e) => {
    this.setState({ value: e.target.value });
  };
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

  static renderCreateTransaction(collectives, amount, description, collectiveId) {
    if (collectives.length > 0) {
      return (
        <form>
          <div class="form-group">

            <input class="form-control" type="number" name="Amount" value={amount} onChange={this.handleChange} />
          </div>
          <div class="form-group">
            <input class="form-control" type="text" name="Description" value={description} onChange={this.handleChange} />
          </div>
          <div class="form-group">
            <select name="Collective" class="form-control" value={collectiveId} onChange={this.handleChange}>
              {collectives.map(x => {
                <option selected value={x.id}>{x.name}</option>
              })}
            </select>
          </div>

          <button onClick={() => { let transactionClient = new TransactionsClient(); transactionClient.createTransaction() }}>
            Ausgabe vergesellschaften
          </button>
        </form>
      )
    }
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
      <>
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

        {FeministOverview.renderCreateTransaction(this.state.collectives_data, this.state.amount, this.state.description, this.state.collectiveId)}


      </>
    );
  }

  async populateData() {


    let expenseClient = new ExpensesClient();
    const expenseData = await expenseClient.getExpenses(1, 1000);
    this.setState({ expenses_data: expenseData.items, expenses_loading: false });

    let collectivesClient = new CollectivesClient();
    const collectivesData = await collectivesClient.getCollectives(1, 1000);
    this.setState({ collectives_data: collectivesData.items, collectives_loading: false });

    let feministClient = new FeministsClient();
    const balanceData = await feministClient.getBalance({});
    this.setState({ balance_data: balanceData, balance_loading: false });
  }

}
