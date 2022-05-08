const mongoose = require('mongoose');
const User = mongoose.model('User');
const jwt = require('jsonwebtoken');

module.exports = {

    async create(request, response) {
        let { email, password } = request.body;

        email = email.toLowerCase();

        const user = await User.findOne({ email: email }).select('_id password');

        if (!user || (user.password !== password)) {
            return response.status(403).json({ message: "Your login credentials don't match an account in our system" });
        }

        const token = jwt.sign({ user_id: user.id }, process.env.SECRET, { expiresIn: 86400 });

        return response.status(201).json({
            user_id: user.id,
            token: token
        });
    },

}