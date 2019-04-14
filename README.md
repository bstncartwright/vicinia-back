# Vicinia
 The serverless backend of the [Vicinia](https://vicinia.net) service written in 24 hours at [PackHacks 2019](http://ncsupackhacks.org/).

## Team Members

- [Francesco Hayes](https://github.com/francesco-hayes): Web Developer
- [Boston Cartwright](https://github.com/munkurious): Mobile App Developer
- [Tyler Smith](https://github.com/tylerssmith): Backend Developer
- [Michelle Charette](https://github.com/theManMitch): "Math Guy" on Backend Developer

## Description



The back end of this project was written within 12 hours and entirely in C#. We developed a CRUD API that allows the app to create and retrieve messages from the serverless database through Azure Functions. We created a database using Cosmos DB and using the SQL API, we were able to query entries in real time, limiting them by location that we recieved from the app using GPS coordinates, and sorting them based on the time the message was posted. The limitation radius is scaleable and all math was done by us. The API is based on authenticated http triggers and automatically scales based on traffic and is available globally.