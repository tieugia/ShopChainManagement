import { Shop } from "./components/Shop";
import { Customer } from "./components/Customer";
import { Product } from "./components/Product";
import { Transaction } from "./components/Transaction";
import AddCustomerForm from "./components/AddCustomerForm";
import AddProductForm from "./components/AddProductForm";
import AddShopForm from "./components/AddShopForm";

const AppRoutes = [
    {
        index: true,
        element: <Transaction />
    },
    {
        path: '/customer',
        element: <Customer />
    },
    {
        path: '/add-customer',
        element: <AddCustomerForm />
    },
    {
        path: '/product',
        element: <Product />
    },
    {
        path: '/add-product',
        element: <AddProductForm />
    },
    {
        path: '/shop',
        element: <Shop />
    },
    {
        path: '/add-shop',
        element: <AddShopForm />
    }
];

export default AppRoutes;
