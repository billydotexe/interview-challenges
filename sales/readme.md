# BACKEND
- install amazon lambda tools to publish the lambda
`dotnet tool install -g Amazon.Lambda.Tools`                    

- from ./backend/salesLambdaFunction/src/salesLambdaFunction launch the command
`dotnet lambda deploy-function salesLambdaFunction --region <region>`

- publish a rest api (being carefull with cors settings, they must be enabled) from aws and set up a post method that execute the lambda

- add the url of the published api to `./frontend/src/app/config.tsx` as the `apiUrl`

# FRONTEND
- from the `./frontend` folder launch the command
`npm install`

- run the project with `npm run dev`
