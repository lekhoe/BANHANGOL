const mongoose = require('mongoose');
const Schema = mongoose.Schema;

var model = new Schema({
    isDeleted: {
        type: Boolean,
        required: false,
        default: false
    },
    dateAdded: { type: Date, default: Date.now },
    dateChanged: { type: Date, default: Date.now },
})
class baseModel {
    constructor() {
        this.model = model;
    }
}

module.exports = {
    baseModel
};