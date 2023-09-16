const express = require('express');
const { getListUser, addUser, getUserByID, updateUser, deleteUser, loginAccount } = require('../bl/UserBL');
const router = express.Router();


router.post('/api/users', getListUser);
router.post('/api/user', addUser);
router.get('/api/user/:userID', getUserByID);
router.put('/api/user/:userID', updateUser);
router.delete('/api/user/:userID', deleteUser);
router.post('/api/login', loginAccount);


module.exports = router;