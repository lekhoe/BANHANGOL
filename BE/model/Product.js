const mongoose = require('mongoose');
const Schema = mongoose.Schema;

var productSchema = new Schema({
    productID: { // ID
        type: String,
        required: true,
        unique: true
    },
    productName: {
        type: String,
        required: true,
        unique: true
    },
    categoryID: {
        type: String,
        required: true,
        unique: true
    },
    trademark: { //thương hiệu
        type: String,
        required: false,
        unique: true
    },
    images: { //list hình ảnh
        type: String,
        required: true,
        unique: false
    },
    audio: { //list video
        type: String,
        required: false,
        unique: false
    },
    isDisplay: { //có hiển thị sản phẩm lên không
        type: Boolean,
        required: false,
        default: true
    },
    weight: { //trọng lượng
        type: String,
        required: false,
        unique: false
    },
    weightUnit: { //đơn vị trọng lượng
        type: String,
        required: false,
        unique: false
    },
    productItems: { //danh sách loại sản phẩm
        type: String,
        required: true,
        unique: false,
        default: ''
    },
    countLike: { //lượt thích
        type: Number,
        required: true,
        unique: false,
        default: 0
    },
    evaluate: { //đánh giá
        type: String,
        required: false,
        unique: false
    },
    description: { //Mô tả
        type: String,
        required: false,
        unique: false
    },
    isDeleted: {
        type: Boolean,
        required: false,
        default: false
    },
    dateAdded: { type: Date, default: Date.now },
    dateChanged: { type: Date, default: Date.now },
})

const Category = mongoose.model('Category', productSchema, "product");

module.exports = {
    Category
};