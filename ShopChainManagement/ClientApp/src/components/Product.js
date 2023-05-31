import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { NavLink } from 'reactstrap';
import ProductApi from "./api/ProductApi";

export class Product extends Component {
    static displayName = Product.name;

    constructor(props) {
        super(props);
        this.state = { products: [], loading: false };
    }

    componentDidMount() {
        this.populateData();
        this.render();
    }

    static renderTable(products) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Price</th>
                    </tr>
                </thead>
                <tbody>
                    {products.map((product) => (
                        <tr key={product.id}>
                            <td>{product.name}</td>
                            <td>{product.price}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        );
    }

    handleSearchChange = async (e) => {
        const keyword = e.target.value;
        this.setState({ loading: true });
        const data = await ProductApi.searchProductByName(keyword);
        this.setState({ products: data, loading: false });
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Product.renderTable(this.state.products);

        return (
            <div>
                <NavLink tag={Link} className="btn btn-primary text-white" to="/add-product">Add product</NavLink>
                <h1 id="tabelLabel">Product</h1>
                <input className="form-control" type="text" placeholder="Search by name" onChange={this.handleSearchChange} />
                {contents}
            </div>
        );
    }

    async populateData() {
        var data = await ProductApi.getAllProducts();
        this.setState({ products: data, loading: false });
    }
}
