FROM node:16

EXPOSE 3000

WORKDIR /app/BE

COPY package.json package-lock.json ./

run npm install

COPY . .

CMD ["node", "server.js"]