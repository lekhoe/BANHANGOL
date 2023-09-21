const express = require('express');
const router = express.Router();
class BaseController {
    constructor(getById, BaseBL) {
        // router.post('/api/category', addCategory);
        // router.put('/api/category/:categoryID', updateCategory);
        router.get('/api/category/:categoryID', BaseBL.getById());
        // router.delete('/api/category/:categoryID', deleteCategory);
        // router.post('/api/categorys', getListCategory);
    }

    // async getById(req, res) {
    //     const entityId = req.params.id;
    //     try {
    //         const entity = await this.baseBL.getById(entityId);
    //         res.json(entity);
    //     } catch (error) {
    //         res.status(500).json({ error: error.message });
    //     }
    // }

    // async create(req, res) {
    //     res.send(`Create ${this.entity}`);
    // }

    // async update(req, res) {
    //     const entityId = req.params.id;
    //     res.send(`Update ${this.entity} with ID: ${entityId}`);
    // }

    // async delete(req, res) {
    //     const entityId = req.params.id;
    //     res.send(`Delete ${this.entity} with ID: ${entityId}`);
    // }


}

module.exports = BaseController;