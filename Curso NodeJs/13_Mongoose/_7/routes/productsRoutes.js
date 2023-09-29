const express = require('express');
const router = express.Router();

const ProductController = require('../controllers/ProductController');

router.get('/create', ProductController.createProduct);
router.post('/create', ProductController.createProductSave);
router.post('/remove/:id', ProductController.deleteProduct);
router.get('/:id', ProductController.getProduct);
router.get('/edit/:id', ProductController.editProduct);
router.post('/edit', ProductController.editProductSave);
router.get('/', ProductController.showProducts);

module.exports = router;