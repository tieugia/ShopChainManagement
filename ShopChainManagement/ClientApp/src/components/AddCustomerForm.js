import React, { Component } from 'react';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import CustomerApi from './api/CustomerApi';

export class AddCustomerForm extends Component {
    constructor(props) {
        super(props);
        this.state = {
            customer: {
                name: '',
                dateOfBirth: '',
                email: ''
            },
            customers: []
        };
    }

    componentDidMount() {
        this.loadCustomers();
    }

    async loadCustomers() {
        const customers = await CustomerApi.getAllCustomers();
        this.setState({ customers });
    }

    handleChange = (event) => {
        const { name, value } = event.target;
        this.setState(prevState => ({
            customer: {
                ...prevState.customer,
                [name]: value
            }
        }));
    }

    handleSubmit = async (event) => {
        event.preventDefault();
        const { customer } = this.state;
        try {
            var response = await CustomerApi.createCustomer(customer);
            if (response.status === 400) {
                this.notify('Email existed, failed to add customer', 'error')
            }
            else {
                this.notify('Customer added successfully');
                this.setState({
                    customer: {
                        name: '',
                        dateOfBirth: '',
                        email: ''
                    }
                });
            }
        } catch (error) {
            this.notify('Failed to add customer', 'error');
        }
    }

    notify = (message, type = 'success') => {
        const toastConfig = {
            position: 'bottom-right',
            autoClose: 5000,
            closeOnClick: true,
            pauseOnHover: true
        };
        switch (type) {
            case 'success':
                toast.success(message, toastConfig);
                break;
            case 'error':
                toast.error(message, toastConfig);
                break;
            default:
                toast(message, toastConfig);
        }
    }

    render() {
        const { customer, customers } = this.state;
        return (
            <div>
                <h2>Add customer</h2>
                <form onSubmit={this.handleSubmit}>
                    <div className="form-group">
                        <label htmlFor="Name">Name</label>
                        <input
                            required
                            type="text"
                            name="Name"
                            id="Name"
                            value={customer.Name}
                            onChange={this.handleChange}
                            className="form-control"
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="dateOfBirth">Date Of Birth</label>
                        <input
                            required
                            type="date"
                            name="dateOfBirth"
                            id="dateOfBirth"
                            value={customer.dateOfBirth}
                            onChange={this.handleChange}
                            className="form-control"
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="email">Email</label>
                        <input
                            required
                            type="email"
                            name="email"
                            id="email"
                            value={customer.email}
                            onChange={this.handleChange}
                            className="form-control"
                        />
                    </div>
                    <button type="submit" className="btn btn-primary">
                        Add customer
                    </button>
                </form>
                <ToastContainer />
            </div>
        );
    }
}

export default AddCustomerForm;
