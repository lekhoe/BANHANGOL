const mongoose = require('mongoose');
const Schema = mongoose.Schema;

var categorySchema = new Schema({
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

const Category = mongoose.model('Category', categorySchema, "category");

module.exports = {
    Category
};