import React, { Component } from 'react';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import ShopApi from './api/ShopApi';

export class AddShopForm extends Component {
    constructor(props) {
        super(props);
        this.state = {
            shop: {
                name: '',
                location: ''
            },
            shops: []
        };
    }

    componentDidMount() {
        this.loadShops();
    }

    async loadShops() {
        const shops = await ShopApi.getAllShops();
        this.setState({ shops });
    }

    handleChange = (event) => {
        const { name, value } = event.target;
        this.setState(prevState => ({
            shop: {
                ...prevState.shop,
                [name]: value
            }
        }));
    }

    handleSubmit = async (event) => {
        event.preventDefault();
        const { shop } = this.state;
        try {
            await ShopApi.createShop(shop);
            this.loadShops();
            this.notify('Shop added successfully');
            this.setState({
                shop: {
                    name: '',
                    location: ''
                }
            });
        } catch (error) {
            this.notify('Failed to add shop', 'error');
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
        const { shop, shops } = this.state;
        return (
            <div>
                <h2>Add shop</h2>
                <form onSubmit={this.handleSubmit}>
                    <div className="form-group">
                        <label htmlFor="name">Name</label>
                        <input
                            required
                            type="text"
                            name="name"
                            id="name"
                            value={shop.name}
                            onChange={this.handleChange}
                            className="form-control"
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="location">Location</label>
                        <input
                            type="text"
                            name="location"
                            id="location"
                            value={shop.location}
                            onChange={this.handleChange}
                            className="form-control"
                        />
                    </div>
                    <button type="submit" className="btn btn-primary">
                        Add shop
                    </button>
                </form>
                <ToastContainer />
            </div>
        );
    }
}

export default AddShopForm;