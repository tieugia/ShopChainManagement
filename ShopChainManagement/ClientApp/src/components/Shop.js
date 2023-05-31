import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { NavLink } from 'reactstrap';
import ShopApi from "./api/ShopApi";

export class Shop extends Component {
    static displayName = Shop.name;

    constructor(props) {
        super(props);
        this.state = { shops: [], loading: false };
    }

    componentDidMount() {
        this.populateData();
        this.render();
    }

    static renderTable(shops) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Location</th>
                    </tr>
                </thead>
                <tbody>
                    {shops.map((shop) => (
                        <tr key={shop.id}>
                            <td>{shop.name}</td>
                            <td>{shop.location}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Shop.renderTable(this.state.shops);

        return (
            <div>
                <NavLink tag={Link} className="btn btn-primary text-white" to="/add-shop">Add shop</NavLink>
                <h1 id="tabelLabel">Shop</h1>
                {contents}
            </div>
        );
    }

    async populateData() {
        var data = await ShopApi.getAllShops();
        this.setState({ shops: data, loading: false });
    }
}
