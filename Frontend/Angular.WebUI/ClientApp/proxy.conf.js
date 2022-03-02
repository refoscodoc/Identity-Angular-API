const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:5003';

const PROXY_CONFIG = [
  {
    context: [
      "/login",
   ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  },
  {
    "/Auth/login": {
      "target":  {
        "host": "localhost",
        "protocol": "https:",
        "port": 5003
      },
      "secure": false,
      "changeOrigin": true,
      "logLevel": "info"
    }
  }
]

module.exports = PROXY_CONFIG;

