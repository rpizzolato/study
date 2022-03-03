## Rocketseat Skylab ES6 JavaScript Course

#### This is a repository for main annotations of Rocketseat ES6 JavaScript Course, in order to consult and understand the ES6 main concepts.

## Contents
- [ES6+](#ES6+)
- [Babel](#Babel)
- [Destructuring](#Destructuring)
- [rest/spread operators](#restspread-operators)
- [Webpack](#Webpack)
- [webpack server](#webpack-server)
- [async/await](#asyncawait)
- [references](#references)

### ES6+
- ES stands for _Ecma Script_, and the number 6 is its version, and it means the year plus one. For instance, ES6+ means the year of 2017, the ES7+, 2018, and go on;
- every year are added new rules for JavaScript sintax (or new ways of coding in JavaScript).

### Babel
- Considering the new features of JavaScript are added every year, the Web Browsers are not able to get updated with the new features, so Babel is able transpile this in a way the Web Browsers understand;
- when using `Class` in JavaScript, for instance, Babel transpile the class code into function code;
- installing Babel is not so tricky:
    1. init the `package.json` file:
        ```js
        yarn init
        ```
        it's not necessary to fill every field, just press Enter in the keyboard.
    2. in the terminal, add the following packages:
        ```js
        yarn add @babel/cli @babel/core @babel/preset-env
        ```
    3. create a new file named `.babelrc`, and fill it with:
        ```js
        {
            "presets": ["@babel/preset-env"]
        }
        ```
        regardless what is being developed, this file will basically detect it (a web browser, node, a react-native project, etc) and it will transpile to the correct code;
    4. for testing purposes, create a new file named `main.js` and add an `alert('test')` method inside of it. Now on `package.json` file, add the `scripts` property:
        ```js
        {
            //name, version, etc properties
        },
        "scripts": {
            "dev": "babel ./main.js -o .bundle.js"
            // -w flag on previous command detects any
            // change on main.js
        }
        ```
        in the terminal, run the command: `yarn dev`
        note the file `main.js`, it didn't change anything. Now let's try with something different. Inside of the `main.js` file, add the following:
        ```js
        class Test {
            method() {

            }
        }
        ```
        run again `yarn dev`. Now it has a huge change on `bundle.js` file, because this is a new feature of JavaScript, and Babel transpile the `Class` into `Function` code, allowing the web browser understand it;
    5. finally, in order to use the code, add it in your `index.html` file, right before the closing `</body>` tag:
        ```html
        <script src="./bundle.js"></script>
        ```

### Destructuring
- Let's assume the following object:
    ```js
    const user = {
        name: 'Rodrigo',
        age: 33,
        address: {
            city: 'Piracicaba',
            state: 'SP'
        }
    };
    ```
    if it is necessary to access `name`, `age` and `city` properties, it would be made by something like this:
    ```js
    const name = user.name;
    const age = user.age;
    const city = user.address.city;
    ```
    in this way, it is repeated a lot of information. Using ES6, it is possible to destructure in this way:
    ```js
    const {name, age, address: { city } } = user;
    ```
    yeah, in just one line!
    this way can also be done with function params, like this:
    ```js
    function showName({ name, age }) {
        console.log(name, age);
    }
    showName(user);
    ```
    it also can be done by using arrays:
    ```js
    const numbers = [1, 2, 3, 4, 5, 6];
    const [ a, b ];
    console.log(a); // 1
    console.log(a); // 2
    ```
### rest/spread operators
- In order to this operator works, it is necessary to add a plugin:
    ```js
    yarn add @babel/plugin-proposal-object-rest-spread
    ```
    after installed, add another line in the `.babelrc` file:
    ```js
    //presets info
    "plugins": ["@babel/plugin-proposal-object-rest-spread"]
    ```
- `rest` is used for destructuring a whole part of an object or an array:
    object example:
    ```js
    //considering the object user seen previously
    const { name, ...allRestOfTheObj } = user;
    console.log(name); //Rodrigo
    console.log(allRestOfTheObj); //prints: { age: 33, address: { city: 'Piracicaba', state: 'SP' } }
    ```
    array example:
    ```js
    const numbers = [1, 2, 3, 4, 5, 6];
    const [a, b, ...c ] = numbers;

    console.log(a); //1
    console.log(b); //2
    console.log(c); //[3, 4, 5, 6]
    ```

    ### Webpack
- A way to work with several `.js` files, as well as other formats like images, json files, etc;
- in order to make it works, add through yarn the `webpack-cli`:
    ```js
    yarn add webpack-cli
    ```
    it is also necessary to create a configuration file, in this case, named `webpack.config.js`:
    ```js
    module.exports = {
	entry: './main.js',
	output: {
		path: __dirname, //__diname is equal to current location on filesystem
		filename: 'bundle.js',
	},
	module: {
		rules: [
			    {
                test: /\.js$/, 
                //regular expression for all .js files. $ means where expression ends
                exclude: /node_modules/, 
                //avoid .js files inside node_modules
				use: {
                    loader: 'babel-loader', 
                    //this is necessary to install (yarn add babel-loader -D)
				    }
                }
            ],
        },
    };
    ```
    in order to execute the webpack, change the `package.json` file, more precisely in the scripts line:
    ```js
    "scripts": {
        "dev": "webpack --mode=development -w"
    }
    ```
    and now run `yarn dev` command and webpack will be working.
- in order to test if it really worked, create a new file named `functions.js` and fill it with:
    ```js
    export function sum(a, b) {
        return a + b;
    }
    ```
    in `main.js`, let's import this function from `functions.js` file:
    ```js
    import { sum } from './functions';
    console.log(sum(1, 2)); //it returns 3
    ```
    if it is necessary to change the name `sum` when importing, use:
    ```js
    import { sum as functionSum } from './functions';
    console.log(functionSum(1, 2)); //it returns 3
    ```
    if in the `sum.js` has an **export default expression**, like this:
    ```js
    export default sum(a, b) {
        return a + b;
    }
    ```
    import it in this way:
    ```js
    import sum from './functions';
    console.log(sum(1, 2)); //it returns 3
    ```
    note: use export default **once** in each `.js` file;
- import of more than one`functions` in a `.js` file. (Let's assume the `functions.js` now has this content)
    ```js
    export function sum(a, b) {
        return a + b;
    }
    export function sub(a, b) {
        return a - b;
    }
    export function mult(a, b) {
        return a * b;
    }
    ```
    it is possible to import them as an object using this:
    ```js
    import * as Allfunctions from './functions';

    console.log(Allfunctions); //it returns an object of all functions (sum, sub and mult)
    
    console.log(Allfunctions.sum(1, 2)); //it returns 3
    console.log(Allfunctions.sub(4, 2)); //it returns 2
    console.log(Allfunctions.mult(2, 2)); //it returns 4
    ```

    ### webpack server
- in order to organize the project, let's create two folders on the root filesystem: `public` and `src`. Move `main.js` file to `src`. Usually `src` folder will store all the `.js` files, while `public` folder will store all files that is not beeing listened to the webpack. In the webpack config file (`webpack.config.js`), it is necessary to change the `entry` and `path` properties to the related new folders and add a new property named `devServer`:
    ```js
    module.exports = {
        entry: './src/main.js',
        output: {
            path: __dirname + '/public',
            filename: 'bundle.js',
        },
        devServer: {
		contentBase: __dirname + '/public'
	    },
        //the remainy of the file is the same
    ```
    it is necessary to install the webpack server:
    ```js
    yarn add webpack-dev-server -D
    ```
    and finally change the scripts property of the `package.json` file:
    ```js
    "scripts": {
        "dev": "webpack-dev-server --mode=development"
    }
    ```
    run `yarn dev` command in your terminal and in the web browser open the address `localhost:8080`
    note: one interesting thing that happens is that is not necessary to create the `bundle.js` file, webpack server does that by its own. It is possible to inspect the page to realize that it's imported as `bundle.js` close to `</body>` closure tag, although in the `index.html` file is imported as `main.js`.
    This happens because the project is on development mode, in case it is needed to build the project to host it, so create another property in `package.json` file like this:
    ```js
    "scripts": {
        "dev": "webpack-dev-server --mode=development",
        "build": "webpack --mode=production"
    }
    ```
    and then run `yarn build` command. Now the `bundle.js` file is created and visible in the project.

    ### async/await
- In order to use it, install two packages:
    ```js
    yarn add @babel/plugin-transform-async-to-generator @babel/polyfill -D
    ```
    add the `@babel/plugin-transform-async-to-generator` on `plugins` property in your `.babelrc` file:
    ```js
    {
    "presets": ["@babel/preset-env"],
    "plugins": [
        "@babel/plugin-proposal-object-rest-spread",
        "@babel/plugin-transform-async-to-generator"
        ]
    }
    ```
    change the property `entry` of the `webpack.config.js` file:
    ```js
    entry: ['@babel/polyfill', './src/main.js'],
    //the remainy of the file is the same
    ```
    note: when using _async/await_ instead of directly `Promises`, use _try/catch_ for error treatment.

    ### references
    - the own website from Rocketseat, course can be found at [https://app.rocketseat.com.br/](https://app.rocketseat.com.br/)

    #### Found some issue? Please report it to me, I'm in a working progress of learning new technologies, please be patient, I appreciate that!



