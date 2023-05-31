class ProductApi {
    static async getAllProducts() {
        const response = await fetch("/api/product/GetAllProductsAsync");
        const data = await response.json();
        return data;
    }

    static async searchProductByName(searchTerm) {
        const response = await fetch(`/api/product/GetProductsBySearchTermAsync?searchTerm=${searchTerm}`);
        const data = await response.json();
        return data;
    }

    static async createProduct(product) {
        const response = await fetch("/api/product/CreateProductAsync", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(product),
        });
        const data = await response.json();
        return data;
    }
}

export default ProductApi;
