const Joi = require('@hapi/joi');

module.exports = {
    create: (request, response, next) => {
        const validation = options.create.validate(request.body);

        if (validation.error) {
            return response.status(400).json({ message: validation.error.message });
        }

        return next();
    },
}

const options = {
    create: Joi.object().keys({
        email:    Joi.string().required(),
        password: Joi.string().required(),
    }),
}