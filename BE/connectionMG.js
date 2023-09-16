require('dotenv').config();
const mongoose = require('mongoose');
const ConnectDB = async () => {
    // Kết nối tới MongoDB
    await mongoose.connect("mongodb+srv://lekhoedev:lekhoe1212@cluster0.u3olq4g.mongodb.net/lkkhoe_thoitrang?retryWrites=true&w=majority", { useNewUrlParser: true, useUnifiedTopology: true })
        .then(() => {
            console.log('Connected to MongoDB');
            // Thực hiện các thao tác với cơ sở dữ liệu ở đây
        })
        .catch((error) => {
            console.error('Failed to connect to MongoDB:', error);
        });
}
module.exports = ConnectDB

//mongodb+srv://lekhoeth:lekhoe2512@cluster0.u3olq4g.mongodb.net/lkkhoe_thoitrang?retryWrites=true&w=majority