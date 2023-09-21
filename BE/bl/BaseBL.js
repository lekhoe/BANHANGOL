const { v4: uuidv4 } = require('uuid');
const express = require('express');
var mongodb = require("mongodb");
const { RESULT_CODE_API } = require("../constant/resultCode");
const { Category } = require("../model/CategoryModel");
const BSONRegExp = mongodb.BSONRegExp;

class BaseBL {
    constructor(model) {
        this.model = model;
    };

    getById = async (req, res) => {
        // var modelID = req.params.categoryID;
        // await this.model.findById({ categoryID: categoryID }).then((res) => {
        //     if (category == null) {
        //         return res.status(RESULT_CODE_API.RESULT_404).json({
        //             errorCode: RESULT_CODE_API.RESULT_404,
        //             messageUser: "Not Found"
        //         });
        //     } else {
        //         res.send(category);
        //     }
        // }, (e) => {
        //     res.status(RESULT_CODE_API.RESULT_400).send({
        //         errorCode: RESULT_CODE_API.RESULT_400,
        //         messageUser: "Có lỗi xảy ra trong quá trình xử lý",
        //         messageDev: e
        //     });
        // });
    }

    async create() {
        return this.model.create();
    }

    async update(entityId, entityData) {
        return this.model.findByIdAndUpdate(entityId, entityData, { new: true });
    }

    async delete(entityId) {
        return this.model.findByIdAndDelete(entityId);
    }
}

module.exports = new BaseBL;