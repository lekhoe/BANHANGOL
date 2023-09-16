const mongoose = require('mongoose');
const Schema = mongoose.Schema;

var userSchema = new Schema({
    userID: {
        type: String,
        required: true,
        unique: true
    },
    userName: {
        type: String,
        required: true,
        unique: true
    },
    phoneNumber: {
        type: String,
        required: true,
        unique: true
    },
    email: {
        type: String,
        required: true,
        unique: true
    },
    password: {
        type: String,
        required: true
    },
    role: {
        type: String,
        required: true,
        default: 'user'
    },
    dateAdded: { type: Date, default: Date.now },
    isDeleted: {
        type: Boolean,
        required: false,
        default: false
    },
})

const User = mongoose.model('User', userSchema, "user");

module.exports = {
    User
};
