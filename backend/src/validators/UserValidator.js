const Joi = require('@hapi/joi');

module.exports = {
    detail: (request, response, next) => {
        const validation = options.detail.validate(request.params);

        if (validation.error) {
            return response.status(400).json({ message: validation.error.message });
        }

        return next();
    },

    create: (request, response, next) => {
        const validation = options.create.validate(request.body);

        if (validation.error) {
            return response.status(400).json({ message: validation.error.message });
        }

        return next();
    }
}

const options = {
    detail: Joi.object().keys({
        id: Joi.string().required(),
    }),

    create: Joi.object().keys({
        name:          Joi.string().required(),
        email:         Joi.string().required().email(),
        phone_number:  Joi.string().min(6),
        password:      Joi.string().required().min(6),
    }),
}