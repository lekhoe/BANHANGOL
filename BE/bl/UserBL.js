const { User } = require("../model/UserModel");
const { v4: uuidv4 } = require('uuid');
const express = require('express');
var mongodb = require("mongodb");
const { RESULT_CODE_API } = require("../constant/resultCode");
const BSONRegExp = mongodb.BSONRegExp;

//Hàm lấy danh sách
const getListUser = async (req, res) => {
    try {
        const pageIndex = req.body.pageIndex;
        const pageSize = req.body.pageSize;
        const textSearch = req.body.textSearch;
        const filter = req.body.filter;
        const sort = req.body.sort;
        const skip = (pageIndex - 1) * pageSize;
        const limit = pageSize;
        const buildFilter = JSON.parse(atob(filter) || '[]');


        let query = User.find();
        let queryFilter = {
            isDeleted: false
        };
        let totalFilter;
        query = query.find({ "isDeleted": false });
        // Áp dụng điều kiện lọc theo textSearch (tìm kiếm theo tên người dùng)
        if (textSearch) {
            query = query.regex('phoneNumber', new RegExp(textSearch, 'i'));
            queryFilter.phoneNumber = new BSONRegExp(textSearch, "i");
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
        const users = await query.exec();

        await User.find(queryFilter).then((user) => {
            totalFilter = user?.length
        });

        res.send({
            Data: users,
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

//Hàm thêm mới
const addUser = async (req, res) => {
    var user = new User({
        userID: uuidv4(),
        email: req.body.email,
        phoneNumber: req.body.phoneNumber,
        password: req.body.password,
        role: req.body.role,
        userName: req.body.userName,
        isDeleted: false
    });
    await user.save().then((user) => {
        res.send(user.userID?.toString());
    }, (e) => {
        res.status(RESULT_CODE_API.RESULT_400).send({
            errorCode: RESULT_CODE_API.RESULT_400,
            messageUser: "Có lỗi xảy ra trong quá trình xử lý",
            messageDev: e
        });
    });
}

//Hàm lấy detail
const getUserByID = async (req, res) => {
    var userID = req.params.userID;
    await User.findOne({ userID: userID }).then((user) => {
        if (user == null) {
            return res.status(RESULT_CODE_API.RESULT_404).json({
                errorCode: RESULT_CODE_API.RESULT_404,
                messageUser: "Not Found"
            });
        } else {
            res.send(user);
        }
    }, (e) => {
        res.status(RESULT_CODE_API.RESULT_400).send({
            errorCode: RESULT_CODE_API.RESULT_400,
            messageUser: "Có lỗi xảy ra trong quá trình xử lý",
            messageDev: e
        });
    });
}

//Hàm sửa
const updateUser = async (req, res) => {
    var query = { userID: req.params.userID };
    var user = {
        email: req.body.email,
        phoneNumber: req.body.phoneNumber,
        password: req.body.password,
        role: req.body.role,
        userName: req.body.userName
    };
    await User.findOneAndUpdate(query, user, { upsert: false }).then((user) => {
        if (user == null) {
            return res.status(RESULT_CODE_API.RESULT_404).json({
                errorCode: RESULT_CODE_API.RESULT_404,
                messageUser: "Not Found"
            });
        } else {

            res.send(user.userID);
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
const deleteUser = async (req, res) => {
    var query = { userID: req.params.userID };
    await User.findOne(query).then((user) => {
        if (user == null) {
            return res.status(RESULT_CODE_API.RESULT_404).json({
                errorCode: RESULT_CODE_API.RESULT_404,
                messageUser: "Not Found"
            });
        } else {
            user.isDeleted = true;
            User.replaceOne(query, user).then((user1) => {
                return res.send(user.userID);
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

//Hàm login
const loginAccount = async (req, res) => {
    const account = req.body.account;
    const data = JSON.parse(atob(account));
    const phoneNumber = data.userName;
    const password = data.password;
    var query = { phoneNumber: phoneNumber };
    await User.findOne(query).then((user) => {
        if (user == null) {
            return res.status(RESULT_CODE_API.RESULT_200).json({
                errorCode: RESULT_CODE_API.RESULT_FAILED_ACCOUNT,
                messageUser: "Sai số điện thoại đăng nhập"
            });
        } else {
            if (user?.password.toString() === password?.toString()) {
                return res.status(RESULT_CODE_API.RESULT_200).send({
                    messageUser: "Đăng nhập thành công"
                });
            }
            else {
                return res.status(RESULT_CODE_API.RESULT_200).json({
                    errorCode: RESULT_CODE_API.RESULT_FAILED_PASSWORD,
                    messageUser: "Sai số điện thoại đăng nhập"
                });
            }
        }
    }, (e) => {
        res.status(RESULT_CODE_API.RESULT_400).send({
            errorCode: RESULT_CODE_API.RESULT_400,
            messageUser: "Có lỗi xảy ra trong quá trình xử lý",
            messageDev: e
        });
    });
}


module.exports = {
    getListUser,
    addUser,
    getUserByID,
    updateUser,
    deleteUser,
    loginAccount
}