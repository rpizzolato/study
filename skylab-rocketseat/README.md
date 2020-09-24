## Rocketseat Skylab Node, React and React Native Courses
---

#### This is a repository for main annotations of Rocketseat Skylab Node, React and React Native Courses, in order to consult and understand the main concepts.

## Contents
- [1. NodeJS](#1.-NodeJS)
  - [a basic routing](#a-basic-routing)
  - [nodemon](#nodemon)
  - [MongoDB](#MongoDB)
  - [mongoose](#mongoose)
  - [require-dir](#require-dir)
  - [insomnia](#insomnia)
  - [POST](#POST)
  - [Pagination](#Pagination)
  - [CORS](#CORS)
- [2. REACT](#2.-REACT)
  - [accessing API](#accessing-API)
  - [React state](#React-state)
  - [onClick](#onClick)
  - [disabled button](#disabled-button)
  - [page navigation](#page-navigation)
- [3. REACT NATIVE](#3.-REACT-NATIVE)
  - [creating the first project](#creating-the-first-project)
  - [react-navigation](#react-navigation)
  - [styling status bar](#styling-status-bar)
  - [fetching data from back-end](#fetching-data-from-back-end)
- [references](#references)

## 1. NodeJS
- it is a platform that uses the v8 engine of the Google Chrome, used to treat requests on a back-end server API Rest, like accessing data bases and etc;
- it is strongly recommended to install NodeJS through package manager (like [Chocolatey](https://chocolatey.org/)), in case of you need to unistall or upgrade it in tests;
- to test it, type:
    ```sh
    node -v //it brings the node version
    npm -v //it brings the npm version
    ```
- in order to start the project, use `npm init -y`. It'll create a new file named `package.json`. This file will basically store all dependencies that the project will use;
- let's install the first dependency on this project, using the following command on terminal:
    ```sh
    npm install express
    ```
    note that now there's a new property named `dependencies` and the `express` is on it;
- `package-lock.json` file manages the cache. For example, `express` depends on a lot of dependencies in order to work, in case of a new dependency is installed,  it can be got from cache automatically instead of downloading all over again;

### a basic routing
- for a basic route, use the following code:
```js
const express = require("express");
const app = express();

app.get('/', (req, res) => {
    res.send("Hello Rocketseat");
});

app.listen(3001);
```
- `req` param stands for the request to server. It contains all details of the request, like *params*, *body*, *header*, *user*, *auth* and etc.
- `res` is the response provided by the server. The most simple response is using the method `send()`.

### nodemon
- every change it's made in the code, it's necessary to kill NodeJS process and run it again. In order to avoid that, and have live refresh, the use of the nodemon is recommended. Install it as a developer dependency:
```sh
npm install nodemon -D
```
- considering the main file is named as `server.js`, it's necessary to create an entry in the `package.json` to run the script:
```js
...
"scripts": {
    "dev": "nodemon server.js"
}
...
```
now run the command in the terminal: `npm run dev`.

### MongoDB
- in order to avoid directlly install on the OS, it's going to be used [Docker](https://www.docker.com/get-started).
- assuming Docker is installed, use the following command to download the mongoDB image:
```sh
docker pull mongo
```
- **docker pull**: download the required image from Docker Hub.
it's not specified the version, so docker will assume the lastest version.
- now it's time to up the container, using the command:
```sh
docker run --name mongodb -p 27017:27017 -d mongo
```
- **docker run**: command to run the container;
- **--name**: the name to identify the container;
- **-p**: stands for the container's port/publish. First it's the local machine's port, and the second is the container's (27017 it's the mongoDB's default port);
- **-d**: is to indicate the container to run in background (detach)
- after -d, it's indicated the imagem, in this case, the one it was downloaded by docker pull.

- in order to verify which docker images are running, use the command: `docker ps`;
- to stop it: `docker stop mongodb`;
- to start it: `docker start mongodb`;

### mongoose
- mongoose is an ORM (Object-Relational Mapping), that is a technique of developing in POO to get closer to relational data base paradigm. In short words, is using JavaScript to do inserts, deletes, updates, etc;
- in order to install it, use:
```sh
npm install mongoose
```
- now it's time to import the mongoose and init the database:
```js
const mongoose = require('mongoose');

mongoose.connect('mongodb://localhost:27017/nodeapi', { useNewUrlParser: true});
```

### require-dir
- whenever it is necessary to import files from a model, instead of using one by one (with a lot of requires), use require-dir, to install it:
```sh
npm install require-dir
```
after installed it, use:
```js
require-dir('src/modes');
```
everything inside `src/models` will be imported.

### insomnia
- in order to test others route's type, like _POST_, _DELETE_, _PUT_ and etc, it's used this software;
- when creating an environment, go to _Manage Environments_ option, and inside the object `{}`, use the `base_url` property:
```js
{
    "base_url": "http://localhost:3001/api"
}
```
using this, it is possible to create routes just using slash (`base_url/product`, for instance).

### POST
- in order to insert registers on DB, it is going to be used the JSON format. To do so, it is necessary to add the following line in `server.js`:
```js
app.use(express.json)
```
### Pagination
- it is used when the DB constains several registers, so instead of returning all of them, the recommendation is using pagination, for avoinding performance issues. In order to use it, install through npm: `npm install mongoose-paginate`;
- on the Product.js file, import the `mongoose-paginate` and use:
```js
import mongoosePaginate = require('mongoose-paginate');
...
ProductSchema.plugin(mongoosePaginate);
```
- in the `ProductController.js` file, change the _async index_ method to:
```js
const { page = 1 } = req.query;
const products = await Product.paginate({}, { page, limit: 10 });
```

### CORS
- [CORS](https://expressjs.com/en/resources/middleware/cors.html) is a way of allowing the request of the API from selected addresses, to use it, installing through npm: `npm install cors`;
- the most simple configurations is enabling the request to all:
```js
app.use(cors());
```
---

## 2. REACT
- in order to install the React lib and create the first app, install through npm: `npm install -g create-react-app`. Now create the first app: `create-react-app huntweb`. After installing it, go to app directory using `cd .\huntweb\` and open it with the preffered code editor;
- run `npm start` and the project will open in the `http://localhost:3000`;
- struture of the App.js file:
    - it is a _class_, where extends from _Component_. Inside this _class_ there's a method called `render()`, this method returns a JSX (mix of **J**ava**S**cript and **X**ML), that renders to the user screen.

### accessing API
- in order to access the API built in NodeJS previously, it is going to be used the lib axios. Install it by using the command: `yarn add axios` or `npm install axios`;
- this access to the API is considered an external access (both to retrieve or send data) from the app, so it is a best practice to create a folder `src/services`, and a new file named `api.js` to deal with it:
```js
import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:3001/api/'
});

export default api;
```
- in this case, it is necessary to create a new page to retrieve the API information, so create a new folder `src/pages/main` and a `index.js` file:
```js
import React, { Component } from 'react'
import api from '../../services/api'

export default class Main extends Component {
  componentDidMount() {
    this.loadProduct();
  }

  loadProduct = async () => {
    const response = await api.get(`/products`);

    //here is logged the products from the API
    console.log(response.data.docs);
  }

  render() {
    return <h1>Hello Rocketseat</h1>
  }
}
```
the `componentDidMout()` is where the data from API is retrieved, just after the whole Component is successfully loaded on the user's screen. It is one of the React's lifecycle methods;

**important**: the `loadProduct()` method should be in arrow function model, because the object `this` can't be seen by the scope when using a named function. In other words, `this` couldn't be used.

### React state
- React has a _state_ variable, that stays listening for changing. Everytime _state_ has any change, the `render()` method is called. In order to use _state_, type:
```js
state = {
    products: []
};
//products is receiving an empty array
```
- By changing the state, use the `setState()` method:
```js
this.setState({
    products: response.data.docs
})
```
- and finally for using the state, use braces `{ this.state.products }`
- in this case, listing the products, it would be somethig like:
```js
render() {
    return (
      <div className="product-list">
        {this.state.products.map(product => (
          <article key={product._id}>
            <strong>{product.title}</strong>
            <p>{product.description}</p>
            <a href="">Acessar</a>
          </article>
        ))}
      </div>
    );
  }
```

### onClick
- when it is necessary to use an event, the correct way is:
```js
nextPage = () => {
    //do something
  }
render() {
  return(
//some code
<button onClick={this.nextPage}>Próximo</button>
//some code
  )
}
```

### disabled button
- when using disabled for buttons in React, it is necessary to white a CSS code for disabled property:
```js
<button disabled={page === 1} onClick={this.prevPage}>Anterior</button>
```
so the `style.css` will be something like:
```css
button[disabled] {
    opacity: 0.5;
    cursor: default;
}

button[disabled]:hover {
    opacity: 0.5;
}
```
*this is one of the several ways of doing that.

### page navigation
- in order to navigate from one page to another, install the package react-router-dom by using: `npm install react-router-dom` or `yarn add react-router-dom`;
- create a file `src/routes.js` and fill it with:
```js
import React from 'react';
import { BrowserRouter, Switch, Route } from 'react-router-dom';

import Main from './pages/main';
import Product from './pages/product';

const Routes = () => (
    <BrowserRouter>
    <Switch>
        <Route exact path="/" component={Main} />
        <Route path="/products/:id" component={Product} />
    </Switch>
    </BrowserRouter>
);

export default Routes;
```
**exact** is used because when using `/products/:id` react router reads the first route, it starts with `/`, so using exact makes it unique;
- in `App.js` file, switch the component `<Main />` to `<Routes />`. Before that, do not forget to import it by using `import Routes from './routes';`
- in order to link two pages, without refreshing, in `pages/main/index.js` file, import the Link component, by using: `import Link from 'react-router-dom';`
- now, switch the `<a href=""></a>` HTML element to `<Link to={''}>` component, like:
```js
 <Link to={`/product/${product._id}`}>Access</Link>
```
---

## 3. REACT NATIVE

- it is a lib/tool that allows to use JavaScript for mobile development. It renders the native interface both _Android_ or _iOS_ (it does not work using webview concept). The whole code in JavaScrip is not converted to Java/Kotlin or Objective-C, it is used a dependency called JavaScriptCore used within native device.

### creating the first project
- in order to create the React Native project, use the command: `npx react-native init app_name`. This is from a repo, available at [here](https://github.com/react-native-community/cli). It is also possible to install react-native-cli, by using the command `npm install –g react-native-cli`. The second option stands out because when running the applications, the command is going to be from react-native-cli, something like this:
  - for Android: `react-native run-android`;
  - for iOS: `react-native run-ios --simulator="iPhone X"`.
  - some issues I had (using Microsoft Windows):
  **'adb' was not recognized as a intern command**: this issue is because of the SDK's path is not set on environment, so go to environment configuration and add it (usually the SDK is located at `C:\Users\user_name\AppData\Local\Android\Sdk\`). I Also needed to create a environment variable called `ANDROID_SDK_ROOT` pointing to `C:\Users\user_name\AppData\Local\Android\Sdk\`. In order to run `react-native run-android`, I had to enable the scripts execution by using the command `Set-ExecutionPolicy Unrestricted` on PowerShell as Administrator rights.
- in case of closing the project, it is not necessary run the command again, just run `react-native start` and open the app in the emulator.

### react-navigation
- in order to navigate throughout the application, it is going to be used the navigation from react. Install it using the command: `npm install react-navigation@2.18.3` or `yarn add react-navigation@2.18.3`.

### styling status bar
- create a file `config/StatusBarConfig.js` and fill it:
```js
import { StatusBar } from 'react-native';
//styling for Android Status Bar
StatusBar.setBackgroundColor('#DA552F');
//styling for iOS
StatusBar.setBarStyle("light-contet");
```
import this file in the `index.js` by using: `import './config/StatusBarConfig';`

### fetching data from back-end
- it is going to be used the `axios` for fetching data from back-end, so install it using the command: `yarn add axios` or `npm install axios`;
- when retrieving or doing any action with data, is a good practice to separate it from a different folder, in this case will be created a folder named `services`, and a file named `api.js`, which will import `axios` and a _base_url_ will be set:
```js
import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:3001/api'
});

export default api;
```


### references
- the own [Rocketseat Skylab Course](https://app.rocketseat.com.br/);
- [treinaweb](https://www.treinaweb.com.br/blog/o-que-e-orm/);
- [React Native CLI explained for beginners @mohitaunni](https://medium.com/@mohitaunni/react-native-cli-explained-for-beginners-4725a271c30d)