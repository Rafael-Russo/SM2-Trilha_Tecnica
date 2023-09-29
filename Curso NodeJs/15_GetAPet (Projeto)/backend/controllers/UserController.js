const bcrypt = require('bcrypt');
const mongoose = require('mongoose');
const jwt = require('jsonwebtoken');

const User = require('../models/User');

//helpers
const createUserToken = require('../helpers/create-user-token');
const getToken = require('../helpers/get-token');
const getUserByToken = require('../helpers/get-user-by-token');

module.exports = class UserController {
    static async register(req, res) {
        const { name, email, password, confirmpassword, image, phone } = req.body;

        //required validations
        if (!name) {
            res.status(422).json({ message: 'O nome é obrigatório!' });
            return;
        }
        if (!email) {
            res.status(422).json({ message: 'O email é obrigatório!' });
            return;
        }
        if (!password) {
            res.status(422).json({ message: 'A senha é obrigatória!' });
            return;
        }
        if (!confirmpassword) {
            res.status(422).json({ message: 'Confirme a senha!' });
            return;
        }
        if (!phone) {
            res.status(422).json({ message: 'O telefone é obrigatório!' });
            return;
        }

        //match password validation
        if (password !== confirmpassword) {
            res.status(422).json({ message: 'A senha e sua confirmação não coincidem!' });
            return;
        }

        //check if user exists
        const userExists = await User.findOne({ email: email });

        if (userExists) {
            res.status(422).json({ message: 'Esse email já está cadastrado!' });
            return;
        }

        //encrypt password
        const salt = await bcrypt.genSalt(12);
        const passwordHash = await bcrypt.hash(password, salt);

        //User creation
        const user = new User({
            name,
            email,
            password: passwordHash,
            image,
            phone
        });

        try {
            const newUser = await user.save();

            await createUserToken(newUser, req, res);
        } catch (err) {
            res.status(500).json({ message: `Algo deu errado! Erro: ${err}` });
        }
    }

    static async login(req, res) {
        const { email, password } = req.body;

        //required validations
        if (!email) {
            res.status(422).json({ message: 'O email é obrigatório!' });
            return;
        }
        if (!password) {
            res.status(422).json({ message: 'A senha é obrigatória!' });
            return;
        }

        //check if user exists
        const user = await User.findOne({ email: email });

        if (!user) {
            res.status(422).json({ message: 'Esse email não está cadastrado!' });
            return;
        }

        //check if password match with db
        const checkPassword = await bcrypt.compare(password, user.password);

        if (!checkPassword) {
            res.status(422).json({ message: 'Senha inválida!' });
            return;
        }

        await createUserToken(user, req, res);
    }

    static async checkUser(req, res) {
        let currentUser;

        if (req.headers.authorization) {
            const token = getToken(req);
            const decoded = jwt.verify(token, 'nossosegredo');

            currentUser = await User.findById(decoded.id);
            currentUser.password = undefined;
        } else {
            currentUser = null;
        }

        res.status(200).send(currentUser);
    }

    static async getUserById(req, res) {
        const id = req.params.id;
        const user = await User.findById(id).select("-password");

        if (!user) {
            res.status(422).json({ message: 'Usuário não encontrado' });
            return;
        }

        res.status(200).json({ user })
    }

    static async editUser(req, res) {
        const id = req.params.id;

        const token = getToken(req);
        const user = await getUserByToken(token);

        const { name, email, password, confirmpassword, phone } = req.body;

        if(req.file){
            user.image = req.file.filename;
        }

        //required validations
        if (!name) {
            return res.status(422).json({ message: 'O nome é obrigatório!' });
        }
        user.name = name;
        if (!email) {
            return res.status(422).json({ message: 'O email é obrigatório!' });
        }
        //check if email has already taken
        const userExists = await User.findOne({ email: email });
        if (user.email != email && userExists) {
            return res.status(422).json({ message: 'Esse email está sendo usado por outra conta!' });
        }
        user.email = email;
        if (!phone) {
            return res.status(422).json({ message: 'O telefone é obrigatório!' });
        }
        user.phone = phone;

        //match password validation
        if (password !== confirmpassword) {
            return res.status(422).json({ message: 'A senha e sua confirmação não coincidem!' });
        } else if (password === confirmpassword && password != null) {
            const salt = await bcrypt.genSalt(12);
            const passwordHash = await bcrypt.hash(password, salt);

            user.password = passwordHash;
        }

        try {
            //return user updated data
            await User.findOneAndUpdate(
                { _id: user.id },
                { $set: user },
                { new: true }
            );
            res.status(200).json({ message: 'Usuário atualizado com sucesso!' });
        } catch (err) {
            return res.status(500).json({ message: `Algo deu errado ao editar o usuário! Erro: ${err}` });
        }
    }
}