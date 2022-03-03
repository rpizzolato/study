const express = require('express');
const mongoose = require('mongoose');
const cors = require('cors');
const requireDir = require('require-dir')

//Init the App
const app = express();
app.use(express.json());
app.use(cors());

//Init the DB
mongoose.connect('mongodb://localhost:27017/nodeapi', { useNewUrlParser: true});

requireDir('./src/models/');

const Product = mongoose.model('Product');

app.use('/api', require('./src/routes'));

//creates a fake data to test
// app.get("/", (req, res) => {
//     Product.create({
//         title: "React Native",
//         description: "Nuild native apps with React",
//         url: "http://github.com/facebook/react-native"
//     });

//     return res.send("Hello Rockeseat");
// });

app.listen(3001);