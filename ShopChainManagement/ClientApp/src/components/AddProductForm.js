import React, { Component } from 'react';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import ProductApi from './api/ProductApi';

export class AddProductForm extends Component {
    constructor(props) {
        super(props);
        this.state = {
            product: {
                name: '',
                price: 0
            },
            products: []
        };
    }

    componentDidMount() {
        this.loadProducts();
    }

    async loadProducts() {
        const products = await ProductApi.getAllProducts();
        this.setState({ products });
    }

    handleChange = (event) => {
        const { name, value } = event.target;
        this.setState(prevState => ({
            product: {
                ...prevState.product,
                [name]: value
            }
        }));
    }

    handleSubmit = async (event) => {
        event.preventDefault();
        const { product } = this.state;
        try {
            await ProductApi.createProduct(product);
            this.loadProducts();
            this.notify('Product added successfully');
            this.setState({
                product: {
                    name: '',
                    price: 0
                }
            });
        } catch (error) {
            this.notify('Failed to add product', 'error');
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
        const { product, products } = this.state;
        return (
            <div>
                <h2>Add product</h2>
                <form onSubmit={this.handleSubmit}>
                    <div className="form-group">
                        <label htmlFor="name">Name</label>
                        <input
                            required
                            type="text"
                            name="name"
                            id="name"
                            value={product.name}
                            onChange={this.handleChange}
                            className="form-control"
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="price">Price</label>
                        <input
                            type="number"
                            name="price"
                            id="price"
                            value={product.price}
                            onChange={this.handleChange}
                            className="form-control"
                        />
                    </div>
                    <button type="submit" className="btn btn-primary">
                        Add product
                    </button>
                </form>
                <ToastContainer />
            </div>
        );
    }
}

export default AddProductForm;