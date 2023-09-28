const conn = require('../db/conn');
const mongo = require('mongodb');

class Product {
    constructor(name, image, price, description) {
        this.name = name;
        this.image = image;
        this.price = price;
        this.description = description;
    }

    save() {
        const product = conn.db().collection('products').insertOne({
            name: this.name,
            image: this.image,
            price: this.price,
            description: this.description
        })

        return product;
    }

    static getProducts() {
        const products = conn.db().collection('products').find().toArray();

        return products;
    }
    static async getProductById(id) {
        const product = await conn.db().collection('products').findOne({ _id: new mongo.ObjectId(id) });

        return product;
    }
    static async deleteProductById(id){
        await conn.db().collection('products').deleteOne({_id: new mongo.ObjectId(id)});
        return;
    }
}

module.exports = Product;