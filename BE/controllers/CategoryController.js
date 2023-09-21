const express = require('express');
const { addCategory, updateCategory, getCategoryByID, deleteCategory, getListCategory } = require('../bl/CategoryBL');
const BaseController = require('./BaseController');
const router = express.Router();

class CategoryController extends BaseController {
    constructor() {
        super('user');
    }
}




module.exports = new CategoryController();