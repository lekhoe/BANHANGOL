const { BaseBL } = require('../bl/BaseBL');
const express = require('express');
const router = express.Router();
class BaseController {
    constructor(model) {
        // router.post('/api/users', getListUser);
        // router.post(`/api/${model}`, addUser);
        router.get(`/api/${model}/:${model}ID`, this.getById);
        // router.put(`/api/${model}/:${model}ID`, updateUser);
        // router.delete(`/api/${model}/:${model}ID`, deleteUser);
        // router.post(`/api/login`, loginAccount);

    }

    async getById(req, res) {
        try {
            const entity = await BaseBL.getById(req, res);
            res.json(entity);
        } catch (error) {
            res.status(500).json({ error: error.message });
        }
    }

    async create(req, res) {
        try {
            const entity = await BaseBL.create(req, res);
            res.json(entity);
        } catch (error) {
            res.status(500).json({ error: error.message });
        }
    }

    async update(req, res) {
        try {
            const entity = await BaseBL.update(req, res);
            res.json(entity);
        } catch (error) {
            res.status(500).json({ error: error.message });
        }
    }

    async delete(req, res) {
        try {
            const entity = await BaseBL.delete(req, res);
            res.json(entity);
        } catch (error) {
            res.status(500).json({ error: error.message });
        }
    }


}

module.exports = new BaseController;