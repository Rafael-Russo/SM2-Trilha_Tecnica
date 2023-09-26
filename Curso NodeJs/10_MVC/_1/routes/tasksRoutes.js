const express = require('express');
const router = express.Router();

const TaskController = require('../controllers/TaskController');
const Task = require('../models/Task');

router.get('/add', TaskController.createTask);
router.post('/add', TaskController.createTaskSave);
router.post('/delete', TaskController.removeTask);
router.get('/edit/:id', TaskController.updateTask);
router.post('/edit', TaskController.updateTaskPost);
router.post('/updatestatus', TaskController.toggleTaskStatus);
router.get('/', TaskController.showTasks);

module.exports = router;