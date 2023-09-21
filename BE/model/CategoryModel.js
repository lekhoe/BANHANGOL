const mongoose = require('mongoose');
const { baseModel } = require('./BaseModel');
const Schema = mongoose.Schema;
const util = require('util');

const CategorySchema = new Schema({
    categoryID: {
        type: String,
        required: true,
        unique: true
    },
    categoryName: {
        type: String,
        required: true,
        unique: true
    },
    dateAdded: { type: Date, default: Date.now },
    dateChanged: { type: Date, default: Date.now },
    isDeleted: {
        type: Boolean,
        required: false,
        default: false
    },
})

// class CategoryModel extends baseModel {
//     constructor() {
//         super(); // Gọi constructor của BaseModel để kết nối với MongoDB
//     };
// }
// util.inherits(CategorySchema, CategoryModel);
const Category = baseModel.discriminator('Category', CategorySchema, "category");
module.exports = {
    Category
};