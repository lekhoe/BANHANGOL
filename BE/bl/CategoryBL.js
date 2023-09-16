const { v4: uuidv4 } = require('uuid');
const express = require('express');
var mongodb = require("mongodb");
const { RESULT_CODE_API } = require("../constant/resultCode");
const { Category } = require("../model/CategoryModel");
const BSONRegExp = mongodb.BSONRegExp;


//Hàm thêm mới
const addCategory = async (req, res) => {
    var category = new Category({
        categoryID: uuidv4(),
        categoryName: req.body.categoryName,
    });
    await category.save().then((category) => {
        res.send(category.categoryID?.toString());
    }, (e) => {
        res.status(RESULT_CODE_API.RESULT_400).send({
            errorCode: RESULT_CODE_API.RESULT_400,
            messageUser: "Có lỗi xảy ra trong quá trình xử lý",
            messageDev: e
        });
    });
}

//Hàm sửa
const updateCategory = async (req, res) => {
    var query = { categoryID: req.params.categoryID };
    var category = {
        categoryName: req.body.categoryName,
        dateChanged: new Date()
    };
    await Category.findOneAndUpdate(query, category, { upsert: false }).then((category) => {
        if (category == null) {
            return res.status(RESULT_CODE_API.RESULT_404).json({
                errorCode: RESULT_CODE_API.RESULT_404,
                messageUser: "Not Found"
            });
        } else {

            res.send(category.categoryID);
        }
    }, (e) => {
        res.status(RESULT_CODE_API.RESULT_400).send({
            errorCode: RESULT_CODE_API.RESULT_400,
            messageUser: "Có lỗi xảy ra trong quá trình xử lý",
            messageDev: e
        });
    });
}

//Hàm lấy detail
const getCategoryByID = async (req, res) => {
    var categoryID = req.params.categoryID;
    await Category.findOne({ categoryID: categoryID }).then((category) => {
        if (category == null) {
            return res.status(RESULT_CODE_API.RESULT_404).json({
                errorCode: RESULT_CODE_API.RESULT_404,
                messageUser: "Not Found"
            });
        } else {
            res.send(category);
        }
    }, (e) => {
        res.status(RESULT_CODE_API.RESULT_400).send({
            errorCode: RESULT_CODE_API.RESULT_400,
            messageUser: "Có lỗi xảy ra trong quá trình xử lý",
            messageDev: e
        });
    });
}

//Hàm xóa
const deleteCategory = async (req, res) => {
    var query = { categoryID: req.params.categoryID };
    await Category.findOne(query).then((category) => {
        if (category == null) {
            return res.status(RESULT_CODE_API.RESULT_404).json({
                errorCode: RESULT_CODE_API.RESULT_404,
                messageUser: "Not Found"
            });
        } else {
            category.isDeleted = true;
            Category.replaceOne(query, category).then(() => {
                return res.send(category.categoryID);
            })
        }
    }, (e) => {
        res.status(RESULT_CODE_API.RESULT_400).send({
            errorCode: RESULT_CODE_API.RESULT_400,
            messageUser: "Có lỗi xảy ra trong quá trình xử lý",
            messageDev: e
        });
    });
}

//Hàm lấy danh sách
const getListCategory = async (req, res) => {
    try {
        const pageIndex = req.body.pageIndex;
        const pageSize = req.body.pageSize;
        const textSearch = req.body.textSearch;
        const filter = req.body.filter;
        const sort = req.body.sort;
        const skip = (pageIndex - 1) * pageSize;
        const limit = pageSize;
        const buildFilter = JSON.parse(atob(filter) || '[]');


        let query = Category.find();
        let queryFilter = {
            isDeleted: false
        };
        let totalFilter;
        query = query.find({ "isDeleted": false });
        // Áp dụng điều kiện lọc theo textSearch (tìm kiếm theo tên người dùng)
        if (textSearch) {
            query = query.regex('categoryName', new RegExp(textSearch, 'i'));
            queryFilter.categoryName = new BSONRegExp(textSearch, "i");
        }
        let sortDefault;
        if (sort) {
            const sortType = sort.slice(0, 1) == '-' ? -1.0 : 1.0;
            const sortValue = sort.slice(1).toString();
            sortDefault = [[sortValue, sortType]]
        } else {
            sortDefault = [["dateAdded", -1.0]]
        }

        if (buildFilter?.length > 0) {
            buildFilter.forEach(element => {
                queryFilter[`${element?.key}`] = element?.value;
                query = query.regex(element?.key, element?.value);
            });
        }
        // Áp dụng phân trang
        query = query.sort(sortDefault).skip(skip).limit(limit);
        const categorys = await query.exec();

        await Category.find(queryFilter).then((category) => {
            totalFilter = category?.length
        });

        res.send({
            Data: categorys,
            Total: totalFilter,
        })
    } catch (error) {
        res.status(RESULT_CODE_API.RESULT_400).send({
            errorCode: RESULT_CODE_API.RESULT_400,
            messageUser: "Có lỗi xảy ra trong quá trình xử lý",
            messageDev: error
        })
    }
}

module.exports = {
    addCategory,
    updateCategory,
    getCategoryByID,
    deleteCategory,
    getListCategory
}