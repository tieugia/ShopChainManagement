import React, { useState, useEffect } from "react";
import { Table } from "react-bootstrap";

const TransactionTable = () => {
    const [transactions, setTransactions] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        const fetchTransactions = async () => {
            try {
                const response = await fetch('api/customer/GetAllTransactionsAsync');
                const data = await response.json();
                setTransactions(data);
                setIsLoading(false);
            } catch (error) {
                setError("Can't get data from server");
                setIsLoading(false);
            }
        };

        fetchTransactions();
    }, []);

    const renderTable = () => {
        if (isLoading) {
            return <p>Loading...</p>;
        } else if (error) {
            return <p>{error}</p>;
        } else if (transactions.length < 30) {
            return <p>Not Enough Data</p>;
        } else {
            return (
                <Table striped bordered hover>
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
                            <tr key={transaction.id}>
                                <td>{transaction.customerName}</td>
                                <td>{transaction.customerEmail}</td>
                                <td>{transaction.shopName}</td>
                                <td>{transaction.productName}</td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
            );
        }
    };

    return (
        <div>
            <h1>Transaction </h1>
            {renderTable()}
        </div>
    );
};

export default TransactionTable;
