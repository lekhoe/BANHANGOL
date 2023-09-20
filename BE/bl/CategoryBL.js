const BaseBL = require('./BaseBL');
const { CategoryModel } = require('../model/CategoryModel');

class CategoryBL extends BaseBL {
    constructor() {
        super(CategoryModel);
    }
}

module.exports = CategoryBL;