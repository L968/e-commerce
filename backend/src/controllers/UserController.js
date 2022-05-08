const mongoose = require('mongoose');
const User = mongoose.model('User');

module.exports = {

    async index (request, response) {
        const users = await User.find();

        if (users.length == 0) {
            return response.status(404).json({ message: 'There are no users registered in the system' });
        }

        return response.json(users);
    },

    async detail(request, response) {
        const { id } = request.params;

        if (!mongoose.Types.ObjectId.isValid(id)) {
            return response.status(400).json({ message: 'Invalid id' });
        }

        const user = await User.findById(id);

        if (!user) {
            return response.status(404).json({ message: 'User not found' });
        }

        return response.json(user);
    },

    async create(request, response) {
        let { name, email, phone_number, password } = request.body;

        name = name.toUpperCase();
        email = email.toLowerCase();

        const userExists = await User.findOne({ email });

        if (userExists) {
            return response.json(userExists);
        }

        const user = await User.create({
            name,
            email,
            phone_number,
            password
        });

        return response.status(201).json(user);
    },

}