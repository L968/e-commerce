const log4js = require('log4js');
const path = require('path');

log4js.configure({
    appenders: {
        "file-appender": {
            type: "file",
            filename: path.join(__dirname, "./error.log"),
            layout: {
                type: "pattern",
                pattern: "%d{dd/MM/yyyy hh\:mm\:ss} %p at %f:%l:%o%n %m%n"
            }
        }
    },
    categories: {
        default: {
            appenders: ["file-appender"],
            enableCallStack: true,
            level: "info"
        }
    }
});

module.exports = log4js.getLogger();;