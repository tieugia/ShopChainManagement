import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { NavLink } from 'reactstrap';
import CustomerApi from "./api/CustomerApi";

export class Customer extends Component {
    static displayName = Customer.name;

    constructor(props) {
        super(props);
        this.state = { customers: [], loading: false };
    }

    componentDidMount() {
        this.populateData();
        this.render();
    }

    static renderTable(customers) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Date Of Birth</th>
                        <th>Email</th>
                    </tr>
                </thead>
                <tbody>
                    {customers.map(customer =>
                        <tr key={customer.id}>
                            <td>{customer.name}</td>
                            <td>{customer.dateOfBirth}</td>
                            <td>{customer.email}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Customer.renderTable(this.state.customers);

        return (
            <div>
                <NavLink tag={Link} className="btn btn-primary text-white" to="/add-customer">Add Customer</NavLink>
                <h1 id="tabelLabel">Customer</h1>
                {contents}
            </div>
        );
    }

    async populateData() {
        var data = await CustomerApi.getAllCustomers();
        this.setState({ customers: data, loading: false });
    }
}
