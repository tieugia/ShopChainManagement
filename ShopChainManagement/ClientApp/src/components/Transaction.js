import React, { Component } from 'react';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

export class Transaction extends Component {
    static displayName = Transaction.name;

    constructor(props) {
        super(props);
        this.state = { transactions: [], enoughData: true, loading: false };
    }

    componentDidMount() {
        this.populateData();
    }

    componentDidUpdate(prevProps, prevState) {
        if (prevState.enoughData !== this.state.enoughData) {
            this.render();
            const message = 'Not Enough Data, require at least 30 distinct Customers, 3 distinct Shops, 30 distinct Product';
            this.notify(message, 'error');
        }
    }

    static renderTable(transactions, enoughData) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Customer Name</th>
                        <th>Customer Email</th>
                        <th>Shop Name</th>
                        <th>Product Name</th>
                    </tr>
                </thead>
                <tbody>
                    {transactions.map((transaction) => (
                        <tr key={transaction.transactionId}>
                            <td>{transaction.customerName}</td>
                            <td>{transaction.customerEmail}</td>
                            <td>{transaction.shopName}</td>
                            <td>{transaction.productName}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        );
    }

    notify = (message, type = 'success') => {
        const toastConfig = {
            position: 'bottom-right',
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
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Transaction.renderTable(this.state.transactions, this.state.enoughData);

        return (
            <div>
                <h1 id="tabelLabel">Transactions</h1>
                {contents}
                <ToastContainer />
            </div>
        );
    }

    async populateData() {
        const response = await fetch('api/transaction/GetAllTransactionsAsync');
        const data = await response.json();

        const customerIds = new Set(
            data.map((transaction) => transaction.customerId)
        );
        const shopIds = new Set(
            data.map((transaction) => transaction.shopId)
        );
        const productIds = new Set(
            data.map((transaction) => transaction.productId)
        );

        const enoughData =
            customerIds.size >= 30 && shopIds.size >= 3 && productIds.size >= 30;

        this.setState({ transactions: data, enoughData: enoughData, loading: false });
    }
}

