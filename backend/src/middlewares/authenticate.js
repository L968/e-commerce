const jwt = require('jsonwebtoken');

module.exports = (request, response, next) => {
	try
	{
		const token = request.headers.authorization;

		if (!token){
			return response.status(401).json({
				isAuthenticated: false,
				message: 'No token provided'
			});
		}

		jwt.verify(token, process.env.SECRET, (error, decoded) => {
			if (error){
				return response.status(401).json({
					isAuthenticated: false,
					message: 'Invalid token'
				});
			}

			request.user_id = decoded.user_id;
			request.isAuthenticated = true;

			return next();
		});
	}
	catch (error)
	{
		return response.status(500).json({
			isAuthenticated: false,
			message: 'An unexpected error has occured, please try again later'
		});
	}
}