class ShopApi {
    static async getAllShops() {
        const response = await fetch("/api/Shop/GetAllShopsAsync");
        const data = await response.json();
        return data;
    }

    static async createShop(shop) {
        const response = await fetch("/api/Shop/CreateShopAsync", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(shop),
        });
        const data = await response.json();
        return data;
    }
}

export default ShopApi;
