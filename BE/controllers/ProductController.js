const express = require('express');
const { addCategory, updateCategory, getCategoryByID, deleteCategory, getListCategory } = require('../bl/CategoryBL');
const router = express.Router();


router.post('/api/category', addCategory);
router.put('/api/category/:categoryID', updateCategory);
router.get('/api/category/:categoryID', getCategoryByID);
router.delete('/api/category/:categoryID', deleteCategory);
router.post('/api/categorys', getListCategory);


module.exports = router;