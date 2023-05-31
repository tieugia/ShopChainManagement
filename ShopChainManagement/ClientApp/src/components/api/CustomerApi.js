class CustomerApi {
    static async getAllCustomers() {
        const response = await fetch("/api/Customer/GetAllCustomersAsync");
        const data = await response.json();
        return data;
    }

    static async createCustomer(customer) {
        const response = await fetch("/api/Customer/CreateCustomerAsync", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(customer),
        });
        const data = await response.json();
        return data;
    }
}

export default CustomerApi;
