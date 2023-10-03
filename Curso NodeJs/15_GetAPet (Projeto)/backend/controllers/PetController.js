const Pet = require('../models/Pet');

//helpers
const getToken = require('../helpers/get-token');
const getUserByToken = require('../helpers/get-user-by-token');
const ObjectId = require('mongoose').Types.ObjectId;

module.exports = class PetController {
    //create a pet
    static async create(req, res) {
        const { name, age, weight, color } = req.body;
        const images = req.files;
        const available = true;

        //images upload

        //validations
        if (!name) {
            return res.status(422).json({ message: 'O nome é obrigatório!' });
        }
        if (!age) {
            return res.status(422).json({ message: 'A idade é obrigatória!' });
        }
        if (!weight) {
            return res.status(422).json({ message: 'O peso é obrigatório!' });
        }
        if (!color) {
            return res.status(422).json({ message: 'A cor é obrigatória!' });
        }
        if (!images) {
            return res.status(422).json({ message: 'Envie pelo menos uma imagem!' });
        }


        //get pet owner
        const token = getToken(req);
        const user = await getUserByToken(token);
        console.log(token);

        //create a pet
        const pet = new Pet({
            name,
            age,
            weight,
            color,
            available,
            images: [],
            user: {
                _id: user.id,
                name: user.name,
                image: user.image,
                phone: user.phone
            }
        });

        images.map((image) => {
            pet.images.push(image.filename);
        })

        try {
            const newPet = await pet.save();
            res.status(201).json({
                message: 'Pet cadastrado com sucesso!',
                newPet
            });
        } catch (err) {
            res.status(500).json({ message: `Algo deu errado ao criar o pet! Erro: ${err}` });
        }
    }
    static async getAll(req, res) {
        const pets = await Pet.find().sort('-createdAt');

        return res.status(200).json({ pets: pets });
    }
    static async getUserPets(req, res) {
        //get user from token
        const token = getToken(req);
        const user = await getUserByToken(token);
        const userId = user._id.toString();

        const pets = await Pet.find({ 'user._id': userId }).sort('-createdAt');

        return res.status(200).json({ pets });
    }
    static async getUserAdoptions(req, res) {
        //get user from token
        const token = getToken(req);
        const user = await getUserByToken(token);
        //const userId = user._id.toString();

        const pets = await Pet.find({ 'adopter._id': user._id }).sort('-createdAt');

        return res.status(200).json({ pets });
    }
    static async getPetById(req, res) {
        const id = req.params.id;

        //check if ID is valid
        if (!ObjectId.isValid(id)) {
            return res.status(422).json({ message: 'ID do pet inválido!' });
        }
        //check if pet exists
        const pet = await Pet.findOne({ _id: id });

        if (!pet) {
            return res.status(404).json({ message: 'Pet não encontado!' });
        }

        return res.status(200).json({ pet: pet });
    }
    static async removePetById(req, res) {
        const id = req.params.id;

        //check if ID is valid
        if (!ObjectId.isValid(id)) {
            return res.status(422).json({ message: 'ID do pet inválido!' });
        }
        //check if pet exists
        const pet = await Pet.findOne({ _id: id });

        if (!pet) {
            return res.status(404).json({ message: 'Pet não encontado!' });
        }
        //check if logged in user registered the pet
        const token = getToken(req);
        const user = await getUserByToken(token);

        if (pet.user._id.toString() !== user._id.toString()) {
            return res.status(422).json({ message: 'Você não tem autorização para remover esse pet!' });
        }

        await Pet.findByIdAndRemove(id);

        return res.status(200).json({ message: 'Pet removido com sucesso!' });
    }
    static async updatePet(req, res) {
        const id = req.params.id;
        const { name, age, weight, color, available } = req.body;
        const images = req.files;
        const updatedData = {};

        //check if ID is valid
        if (!ObjectId.isValid(id)) {
            return res.status(422).json({ message: 'ID do pet inválido!' });
        }
        //check if pet exists
        const pet = await Pet.findOne({ _id: id });

        if (!pet) {
            return res.status(404).json({ message: 'Pet não encontado!' });
        }
        //check if logged in user registered the pet
        const token = getToken(req);
        const user = await getUserByToken(token);

        if (pet.user._id.toString() !== user._id.toString()) {
            return res.status(422).json({ message: 'Você não tem autorização para remover esse pet!' });
        }

        //validations
        if (!name) {
            return res.status(422).json({ message: 'O nome é obrigatório!' });
        }else{
            updatedData.name = name;
        }
        if (!age) {
            return res.status(422).json({ message: 'A idade é obrigatória!' });
        }else{
            updatedData.age = age;
        }
        if (!weight) {
            return res.status(422).json({ message: 'O peso é obrigatório!' });
        }else{
            updatedData.weight = weight;
        }
        if (!color) {
            return res.status(422).json({ message: 'A cor é obrigatória!' });
        }else{
            updatedData.color = color;
        }
        if (images.length > 0){
            updatedData.images = [];
            images.map((image) => {
                updatedData.images.push (image.filename);
            });
        }

        await Pet.findByIdAndUpdate(id, updatedData);
        res.status(200).json({message: 'Pet atualizado com sucesso!'});
    }
    static async schedule(req, res){
        const id = req.params.id;

        //check if ID is valid
        if (!ObjectId.isValid(id)) {
            return res.status(422).json({ message: 'ID do pet inválido!' });
        }
        //check if pet exists
        const pet = await Pet.findOne({ _id: id });

        if (!pet) {
            return res.status(404).json({ message: 'Pet não encontado!' });
        }

        //check if logged in user registered the pet
        const token = getToken(req);
        const user = await getUserByToken(token);

        if (pet.user._id.toString() === user._id.toString()) {
            return res.status(422).json({ message: 'Você não pode agendar uma visita com seu próprio Pet!' });
        }

        //check if user has already scheduled a visit
        if(pet.adopter){
            if(pet.adopter._id.equals(user._id)){
                return res.status(422).json({ message: 'Você ja agendou uma visita para esse pet!' });
            }
        }

        //add user to pet
        pet.adopter = {
            _id: user._id,
            name: user.name,
            image: user.image
        };

        await Pet.findByIdAndUpdate(id, pet);
        return res.status(200).json({message: `A visita  foi agendada com sucesso! Entre em contato com ${pet.user.name} pelo telefone ${pet.user.phone}`});
    }
    static async concludeAdoption(req, res){
        const id = req.params.id;

        //check if ID is valid
        if (!ObjectId.isValid(id)) {
            return res.status(422).json({ message: 'ID do pet inválido!' });
        }
        //check if pet exists
        const pet = await Pet.findOne({ _id: id });

        if (!pet) {
            return res.status(404).json({ message: 'Pet não encontado!' });
        }
        //check if logged in user registered the pet
        const token = getToken(req);
        const user = await getUserByToken(token);

        if (pet.user._id.toString() !== user._id.toString()) {
            return res.status(422).json({ message: 'Você não tem autorização para concluir essa solicitação!' });
        }

        pet.available = false;

        await Pet.findByIdAndUpdate(id, pet);
        return res.status(200).json({message: 'Parabéns, o ciclo de adoção foi finalizado com sucesso!'});
    }
}