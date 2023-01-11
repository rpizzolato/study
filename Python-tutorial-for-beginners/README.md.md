# Python Tutorial for Beginners

This is a repository with annotations from the Course **Python Tutorial for Beginners - Learn Python in 5 Hours [FULL COURSE]** of the YouTube channel **TechWorld with Nana**. The Course is available here 
[https://www.youtube.com/watch?v=t8pPdKYpowI](https://www.youtube.com/watch?v=t8pPdKYpowI). I'd like to Thank Nana Janashia so much for the channel for providing free content with so much quality!

## Data Types
- Text Type: String (str)
- Number Types:
  - Integer (int): whole number, positive or negative, **without decimals**
  - Floating Point Number (float): number, positive or negative, containing **1 or more decimals**
- Boolean: **True** and **False**

## String concatenation
- using `+`: `print("20 days are" + str(20 * 24 * 60) + "minutes")`. We use `str()` because all of the output must be string. This is not a fancy way of using concatenation.
- using `{}`: `print(f"20 days are {20 * 24 * 60} minutes")` This is a cooler way of using concatenation. The `f` used at the beginning stands for "format". Also, this is a feature of updated version of Python, so keep it updated!

## Function
- A function is defined using the `def` keyword. Function is a block of code which only runs when it is called.
  - Advantages:
    - less code;
    - reduce code duplication;
    - more descriptive.

## Scope
A Variable is only available from inside the region it is created.
- **Global Scope**: variables available from within any scope
- **Local Scope**: variables created inside function

## User input
- `input()`: to ask the user for an input. Python stops executing when it comes to the `input()`
- `input()` is a Built-In Function provided by Python language itself
- `input("Enter something")`: shows the message described.

>**Warning**
> 
> `input()` always returns a **string**. If you want to return a number, for example, you have to **cast** the value using `int()`. `int()` is a built-in Python function that converts the specific value into and integer number

>**Tip**
> 
> use `\n` for breaking a line

## Function with return value
A function can return data as a result. For example, a function that prints out something, if you want to assign this value (printed) to a variable, you should use `return` statement for the function, something like this:
```python
calculation_to_hours = 24
name_of_unit = "hours"


def days_to_units(num_of_days, custom_message):
    # originally print() is removed and add the return statement
    # print(f"{num_of_days} days are {num_of_days * calculation_to_hours} {name_of_unit}")
    return f"{num_of_days} days are {num_of_days * calculation_to_hours} {name_of_unit}"


my_return = days_to_units(20)
```

### Here's an example, with full code containing Function and User input:
```python
calculation_to_hours = 24
name_of_unit = "hours"


def days_to_units(num_of_days):
    # originally print() is removed and add the return statement
    # print(f"{num_of_days} days are {num_of_days * calculation_to_hours} {name_of_unit}")
    return f"{num_of_days} days are {num_of_days * calculation_to_hours} {name_of_unit}"


user_input = input("Enter a number of days and I will convert it to hours!\n")
user_input_number = int(user_input)
calculated_value = days_to_units(user_input_number)
print(calculated_value)
```

## Conditionals (if / else) and Boolean Data Type
We can use it for **Validate User Input**, which:
- doesn't make sense;
- that could crash our program
- could even be a security risk

### Expressions that evaluate to either **true** or **false**
Comparison operators are used to compare two values:

| Operator | Name                      | Example  |
|----------|---------------------------|----------|
| `==`       | Equals                    | `a == b` |
| `!=`       | Not Equals                | `a != b` |
| `>`        | Less than                 | `a < b`  |
| `<=`       | Less than or equals to    | `a <= b` |
| `>`        | Greater than              | `a > b`  |
| `>=`       | Greater than or equals to | `a >= b` |

### Python Atithmetic Operators
Arithmetic operators are used with numeric values to perform common mathematical operations:

| Operator | Name           | Example  |
|----------|----------------|----------|
| `+`      | Addition       | `x + y`  |
| `-`      | Subtraction    | `x - y`  |
| `*`      | Multiplication | `x * y`  |
| `/`      | Division       | `x / y`  |
| `%`      | Modulus        | `x % y`  |
| `**`     | Exponentiation | `x ** y` |
| `//`     | Floor division | `x // y` |

### Boolean
Boolean Data Type can only have 2 values: **true** or **false**

For a condition check, for example, like `num_of_days > 0`, it should return true for number greater than 0 (zero), and false for a negative number, like -9.

We can use a function `type()` to return the type o a variable. For example, if we use the condition `num_of_days > 0`, and store it in a variable, like this: `condition_check = num_of_days >0` and print it `print(type(condition_check))`, will output `<class 'bool'>`, informing the type of it.

Another example would be something like this: `print(type("this should be a string"))`, will output `<class 'str'>`. A float example would be like this: `print(type(30.00))`, will output `<class 'float'>`

### Clean Up The Code
Related to the following code:
```python
calculation_to_hours = 24
name_of_unit = "hours"


def days_to_units(num_of_days):
    if num_of_days > 0:
        return f"{num_of_days} days are {num_of_days * calculation_to_hours} {name_of_unit}"
    elif num_of_days == 0:
        return "you entered a 0, please enter a valid positive number"


user_input = input("Enter a number of days and I will convert it to hours!\n")


if user_input.isdigit():
    user_input_number = int(user_input)
    calculated_value = days_to_units(user_input_number)
    print(calculated_value)
else:
    print("Your input is not a valid number.")
```
It's necessary to clean up, for example encapsulating most of the logic in functions, being a best practices of your code.

We could turn it to the following code (**we used nested if too**):
```python
calculation_to_hours = 24
name_of_unit = "hours"


def days_to_units(num_of_days):
    return f"{num_of_days} days are {num_of_days * calculation_to_hours} {name_of_unit}"


def validade_and_execute():
    if user_input.isdigit():
        user_input_number = int(user_input)
        if user_input_number > 0:
            calculated_value = days_to_units(user_input_number)
            print(calculated_value)
        elif user_input_number == 0:
            print("you entered a 0, please enter a valid positive number")
    else:
        print("Your input is not a valid number.")


user_input = input("Enter a number of days and I will convert it to hours!\n")
validade_and_execute()
```

### Nested Function Execution
Let's say we have the following code:
```python
print(type(set(list_of_days)))
```
The sequence of execution is:
1. `set(list_of_days)`
  - **Input**: the user input array
  - **Output**: Returns the converted Set
2. `type(return_value_of_previous_func)`
  - **Input**: the converted Set
  - **Output** Returns the data
3. `print(return_value_of_previous_func)`
  - **Input**: the data type
  - **Output**: Prints the value to console

Which means the execution is from inner functions to outer functions.

## Error Handling with try/except
The **try** block lets you "test" a block of code for errors. The **except** block catches the error and lets you handle it.

We could rewrite the conversion program by using the following code:
```python
calculation_to_hours = 24
name_of_unit = "hours"


def days_to_units(num_of_days):
    return f"{num_of_days} days are {num_of_days * calculation_to_hours} {name_of_unit}"


def validade_and_execute():
    try:

        user_input_number = int(user_input)
        if user_input_number > 0:
            calculated_value = days_to_units(user_input_number)
            print(calculated_value)
        elif user_input_number == 0:
            print("you entered a 0, please enter a valid positive number")
        else:
            print("You entered a negative number, no conversion for you")
    except ValueError:
        print("Your input is not a valid number.")


user_input = input("Enter a number of days and I will convert it to hours!\n")
validade_and_execute()
```
The advantage of using try/except is that we could test a block of codes, without worrying about each if/else statement, and if there's a break on the code, the whole program doesn't stop. It's like try/catch in others programming languages.

## While Loops
Looping is used pretty much:
- to execute logic multiple times
- Python has 2 loops commands
This logic is applied in conditions or until something happen.<br />
Conditions:
- evaluate to true or false
- are used most commonly in "if statements" and "loops"

A single looping can be stated in this format:
```python
while True:
    user_input = input("Enter a number of days and I will convert it to hours!\n")
    validade_and_execute()
```
In this way, the looping will be infinite, because the condition is always **True** value. This is not good of doing, and it's not a good practice.

### Let user exit the program

>**Information**
>
>Python is a Case-Sensitive Language<br />
>True: logic value<br />
>true: is just a string<br />

## Lists and For Loop
- **Lists**: used to store multiple items in a single variable
- **For Loop**: is used iterating over a sequence (like a list), so we can execute something for each item in a list

To turn a string into a slit, we need to "split" it, using the method `split()`. We need to provide a list without commas, because `split()` changes the spaces by default. If you want to use comma, for example, we need to declare split method in the following way: `split(",")`

For example, let's say a string value equals to `"10, 22, 50, 100"` (**comma** separated, or it could be **space** separated). Split this list is equals to `[10, 22, 50, 100]`

The program to convert to hours would be like this:
```python
calculation_to_hours = 24
name_of_unit = "hours"


def days_to_units(num_of_days):
    return f"{num_of_days} days are {num_of_days * calculation_to_hours} {name_of_unit}"


def validate_and_execute():
    try:
        user_input_number = int(num_of_days_element)
        if user_input_number > 0:
            calculated_value = days_to_units(user_input_number)
            print(calculated_value)
        elif user_input_number == 0:
            print("you entered a 0, please enter a valid positive number")
        else:
            print("You entered a negative number, no conversion for you")
    except ValueError:
        print("Your input is not a valid number.")


user_input = ""
while user_input != "exit":
    user_input = input("Enter a number of days as a comma separated list and I will convert it to hours!\n")
    print(type(user_input.split(", ")))
    print(user_input.split(", "))
    for num_of_days_element in user_input.split(", "):
        validate_and_execute()
```
On the code example above, we still print the type of the list, with the command: `print(type(user_input.split(",")))` and the list itself, with the command: `print(user_input.split(","))`

### Basic List Operations
- **Create** a list
- **Add an item** to the list
- **Remove an item** from the list
- **Change items** in the list
- **Access items** of the list

Let's see some examples:
```python
my_list = ["January", "February", "March"]
# print a specific element from the list
print(my_list[2])

# add value to the next index
my_list.append("April")
print(my_list)

# print an element that doesn't exist, returns IndexError: list index out of the range
print(my_list[4])
```

## Comments in Python

- **single comment**: `# this is a single comment`
- **multi line comment**:
```python
#This is a comment
#written in
#more than just one line
```
- **multi line comment (with triple quotes)**: 
```python
"""
This is a comment
written in
more than just one line
"""
```

## Set
Is another data type built-in of Python. It's like a List, but it only allows unique values inside.
- another built-in data type of Python
- as with Lists, used to store multiple items of data
- **does NOT allow duplicate values**

In the same program written previously, we are going to print the type of the List and Set, and their values, just to clarify.

```python
user_input = ""
while user_input != "exit":
    user_input = input("Enter a number of days as a comma separated list and I will convert it to hours!\n")
    list_of_days = user_input.split(", ")

    print(list_of_days)
    print(set(list_of_days))

    print(type(list_of_days))
    print(type(set(list_of_days)))

    for num_of_days_element in set(user_input.split(", ")):
        validate_and_execute()

```
The output must be something like this, considering the input: `10, 15, 10, 5`:
```
['10', '15', '10', '5']
{'5', '15', '10'}
<class 'list'>
<class 'set'>
5 days are 120 hours
15 days are 360 hours
10 days are 240 hours
```
Note the difference when is List and Set. List represents **square bracket** `[]` and Set represents **curly braces** `{}`.

### Basic Set Operation and Syntax
- **Create** a set
- **Access items** only via loop!
- **Add an item** to the set
- **Remove an item** from the set

#### Unordered and unchangeable
- ❌Items in a set do **NOT** have a defined order! (it changes every time you execute the program)
- ❌Items **CANNOT be referred** to by index!
- ❌Items **CANNOT be changed**, only added/removed!

> **Observation**
>
> In List, if we have the following list `my_list = ["January", "February", "March", "January"]` and remove the information "January", using the command: `my_list.remove("January")` it will **remove only the first occurrence** of that value.

Here's some examples in code:
```python
# remember to use curly braces {} instead of square bracket []
my_set = {"January", "February", "March"}

# to print a set, use for in
for element in my_set:
    print(element)

# to add an element in the set
my_set.add("April")
print(my_set)

# to remove an element in the set or list
my_set.remove("January")
print(my_set)

my_list = ["January", "February", "March"]
my_list.remove("January")
print(my_list)
```

The output is:
```
March
January
February

{'March', 'January', 'April', 'February'}
{'March', 'April', 'February'}

['February', 'March']
```

## Built-In Functions
| Function    | Description               | Example                  |
|-------------|---------------------------|--------------------------|
| ``print()`` | prints to the standard output device                    | `print("some text")`     |
| `input()`   | asks user for input                | `input("enter a value")` |
| `set()`     | returns a new set                | `set([1, 3, 4])`         |
| ``int()``   | converts value into an integer number                 | `int("20")`              |
Some of we've seen throughout this course. See all of them [here](https://www.w3schools.com/python/python_ref_functions.asp) or in [official documentation](https://docs.python.org/3/library/functions.html).

### Built-In Functions on Data Types
- each data type has its own built-in functions
- which are useful/makes sense only for this specific data type

| Function      | Description                                                             | Example            |
|---------------|-------------------------------------------------------------------------|--------------------|
| ``split()``   | Split a string into a list where each word is a list item               | `"2, 3".split()`   |
| ``isdigit()`` | checks if the string is a representation of a digit or a regular string | `"text".isdigit()` |

> **Observation**
> 
> If you use a string, you can type dot (.) and the IDE will provide a list of function you can use with a string (`"2, 3".`). Or if you use a list, and again type (.), the IDE will provide a list of functions to use for a list (`[1, 3, 4].`).
 
## Dictionary Data Type
- Are used to store values in **key:value pairs**
- Is a collection, which does not allow duplicate values

In order to access items in a **List**, we need to use their **index** (just like accessing a specific item of a list), and the first item index is 0 (zero). For example `days_and_unit_dictionary = {"days": days_and_unit[0], "unit": days_and_unit[1]}`. In this case, the variable `days_and_unit` is a **List**.

In the example of converting numbers of days to hours, we are going to sophisticate our application, now it'll accept inputs of the days and the conversion, like for example `20:hours` or `45:minutes` and so on.

The programming code will be something like this:
```python
user_input = ""
while user_input != "exit":
    user_input = input("Enter a number of days and conversion unit!\n")

    # this line converts the format 45:hours to a list ["45", "hours"]
    days_and_unit = user_input.split(":")

    print(days_and_unit)

    days_and_unit_dictionary = {"days": days_and_unit[0], "unit": days_and_unit[1]}
    
    print(days_and_unit_dictionary)
```

Just to exemplify, the output of an input `20:hours` will be:
```
['20', 'hours']
{'days': '20', 'unit': 'hours'}
```

And the output of an input `45:minutes` will be:
```
['45', 'minutes']
{'days': '45', 'unit': 'minutes'}
```

### Accessing Items in a Dictionary
- Items can be accessed by its **key name**

An upgraded version of the program using list and dictionary is:
```python
def days_to_units(num_of_days, conversion_unit):
    if conversion_unit == "hours":
        return f"{num_of_days} days are {num_of_days * 24} hours"
    elif conversion_unit == "minutes":
        return f"{num_of_days} days are {num_of_days * 24 * 60} minutes"
    else:
        return "Unsupported unit"


def validate_and_execute():
    try:
        user_input_number = int(days_and_unit_dictionary["days"])
        if user_input_number > 0:
            calculated_value = days_to_units(user_input_number, days_and_unit_dictionary["unit"])
            print(calculated_value)
        elif user_input_number == 0:
            print("you entered a 0, please enter a valid positive number")
        else:
            print("You entered a negative number, no conversion for you")
    except ValueError:
        print("Your input is not a valid number.")


user_input = ""
while user_input != "exit":
    user_input = input("Enter a number of days and conversion unit!\n")

    # this line converts the format 45:hours to a list ["45", "hours"]
    days_and_unit = user_input.split(":")

    print(days_and_unit)

    days_and_unit_dictionary = {"days": days_and_unit[0], "unit": days_and_unit[1]}
    print(days_and_unit_dictionary)

    print(type(days_and_unit_dictionary))

    validate_and_execute()
```

### Summary of Data Types
- string: `message= "enter some value"`
- integer: `days = 20`
- boolean: `valid_number = True`
- list with integer: `list_of_days = [20, 40, 20, 100]`
- list with string: `list_of_months = ["January", "February", "June"]`
- set: `set_of_days = {20, 45, 100}`
- dictionary: `days_and_unit = {"days": 10, "unit": "hours"}`

#### Why do we need all these data types?
- Depending on what you try to achieve in your program
- you need a different data type to achieve exactly that

## Modules
- Logically organize your Python code
- Module should contain related code
- is just a `.py` file

### import statement
Let's copy the below code from our program to a separated file called `helper.py`:
```python
user_input = ""
while user_input != "exit":
    user_input = input("Enter a number of days and conversion unit!\n")

    # this line converts the format 45:hours to a list ["45", "hours"]
    days_and_unit = user_input.split(":")

    print(days_and_unit)

    days_and_unit_dictionary = {"days": days_and_unit[0], "unit": days_and_unit[1]}
    print(days_and_unit_dictionary)

    print(type(days_and_unit_dictionary))

    validate_and_execute()
```

Now in our `main.py` file, let's import the `helper.py` by using the `import helper` statement. Remembering the `main.py` will contain just the the following code:
```python
import helper

user_input = ""
while user_input != "exit":
    user_input = input("Enter a number of days and conversion unit!\n")

    # this line converts the format 45:hours to a list ["45", "hours"]
    days_and_unit = user_input.split(":")

    print(days_and_unit)

    days_and_unit_dictionary = {"days": days_and_unit[0], "unit": days_and_unit[1]}
    print(days_and_unit_dictionary)

    print(type(days_and_unit_dictionary))

    helper.validate_and_execute()
```
Now we can call the `validate_and_execute()` method using `helper` import statement.

In case you need only one function to import from another file, you can import it by using: `from helper import validate_and_execute` and do not use `helper.validate_and_execute(days_and_unit_dictionary)` before the statement.
Others ways:
- `from helper import some_function, some_variable`: imports just a function and a variable
- `from helper import *`: imports everything
- `from helper as h`: in this way you kind of rename, and use it like this: `h.validate_and_execute(days_and_unit_dictionary)`

## Built-In Python Module
You can use many existing Python Modules, like Math (pow, sqrt, floor, ceil, etc), datetime modules, etc. For example in Datetime module, it provides many functions that make it easier for you to work with **dates and times**! There's a bunch of built-in python modules, for another example, there's a module called `os`, where we can retrieve information regarding the Operating System, and so on. [Here](https://docs.python.org/3/py-modindex.html) is a complete list from officil website

## Projetc: Countdown App
It is going to be used the datetime module:

```python
from datetime import datetime

user_input = input("enter your goal with a deadline separated by colon\n")
input_list = user_input.split(":")

goal = input_list[0]
deadline = input_list[1]

deadline_date = datetime.strptime(deadline, "%d.%m.%Y")

# calculate how many das from now till deadline
today_date = datetime.today()

time_till = deadline_date - today_date
hours = int(time_till.total_seconds() / 60 / 60)

print(f"Dear user! Time remaining for your goal: {goal} is {hours} hours")

```

## Build-In vs. Third-Party
- Python comes only with a set of built-in modules
- Many more modules out there, which are NOT part of the Python Installation
- You need to install these third-party packages
- Built-In Modules and Packages are the most common ones
- Depending on what application you write, add specific ones
If you need a package for Web Application, django for example, you'll need to install them through **PyPi** (The Python Package Index)

### PyPI
- is a repository (storage) for third-party Python packages
- People can publish their packages to this repository
- So it becomes available for everyone to use
- A large community means, many people are creating useful modules and make them available for others

### Module vs Package
- Any Python file is a module, for example the Python modules we already used before, like `datetime.py`, `os.py` and etc.
- Package is a collection of Python modules, structured divided. Package must include an **\__init__.py** file. This **\__init__** file distinguishes a package from a directory

### Pip
In order to install a package, we use the package manager tool called **pip**.

Pip is a package manager for Python. It is used to install packages from the Python Package Index, but also other indexes.

In Python 3, pip is included in the Python Installation. When you install Python, you install pip as well.

Let's test it, trying to install Django package. Access the address https://pypi.org/project/Django/ or search for "Django" in pypi website. There will be a command to copy and paste in the terminal: `pip install Django`. After installing it, you can check it accessing the folder `my-python-project\venv\Lib\site-packages`, and you'll see the folder containing the Django files. You can uninstall packages using the commando `unistall`, for example, `pip uninstall django`

You can check if it is really installed, by using the `import django` statement.

## Project: Automation with Python

The project will consist in a program that reads a spreadsheets file, with information regarding a company. It will be created some calculus of the company things value and so on.

### Different ways to work with files
- Python has several built-in functions for handling files in general
- **io module**: create, read, write

Here's an example:

```python
f = open("demofile.txt", "r")

# read
f.read()

# write
f.write("Now the file has more content!")

# close the file
f.close()
```

However, there's a Python package to work with spreadsheets specifically with the following advantages:
- more practical functions for spreadsheets specifically
- easier to use, compared to the built-in modules

The package we're going to use is called **openpyxl**, available in the PyPI website, [here](https://pypi.org/project/openpyxl/). Let's run the command `pip install openpyxl` to install this module/package.

- **Module** = `.py` file
- **Package** = collection of modules must contain `init.py` file
- **Library** = collection of packages

### Implementation

-`range()`: creates a sequence of number. We cannot use, for example, `for product_row in product_list.max_row` (`product_list.max_row` in this case will return the number 75), we need to create a range of numbers, a sequence. And fixing the previews example, it'd be like this `for product_row in range(product_list.max_row)`.
- `range()` also starts from 0 by default, and creates a list:
- it stays like this: `range(75)` ➡️`[0, 1, 2, 3 ... 74]`
- **in a valid loop** we can iterate over and do something for each item in this list `[0, 1, 2, 3 ... 74]`
- we can also use `range()` with a parameter informing the starting number we want, in this case will be tue number 2: `for product_row in range(2, product_list.max_row)`. This will start the list in this way: `[2, 3, 4...]`

### Select the value in a cell

In order to select a value in a cell, we are going to use a function called `cell(row, column)`. For the project, we're going to use like this:
```python
for product_row in range(2, product_list.max_row + 1):
    supplier_name = product_list.cell(product_row, 4)
```

But in this way is going to return only the object. In order to get the actual value of the cell, we need to use `.value` statement, like this: `supplier_name = product_list.cell(product_row, 4).value`

Now it's going to be necessary to add a value to the dictionary (variable called `products_per_supplier`)

The dictionary format is: `products_per_supplier = {"AAA company": 20, "BBB Comp"}` 

And the way to add data on in, the format is going to be like this: `products_per_supplier["key"] = "value"`

In this case, it's going to add this entry, and considering we have mode than one company, the entry will appear like this (counting the companies):
`{"AAA company": 1, "BBB company": 1}`

### Exercise: List each company with respective product count
```python
import openpyxl

inv_file = openpyxl.load_workbook("inventory.xlsx")
product_list = inv_file["Sheet1"]

products_per_supplier = {}

print(range(product_list.max_row))


for product_row in range(2, product_list.max_row + 1):
    supplier_name = product_list.cell(product_row, 4).value
    products_per_supplier

    if supplier_name in products_per_supplier:
        current_num_products = products_per_supplier[supplier_name]
        products_per_supplier[supplier_name] = current_num_products + 1
    else:
        print("adding a new supplier")
        products_per_supplier[supplier_name] = 1

print(products_per_supplier)
```
Considering our dictionary is the `products_per_supplier` this variable, to get information about it, we use the key value pair. We can use in two ways:
- `products_per_supplier[supplier_name]` and
- `products_per_supplier.get(supplier_name)` (**Recommended** form)

### Exercise: List each company with respective total inventory value

It's good to say that we just need to add a new calculation to the previous code.
```python
import openpyxl

inv_file = openpyxl.load_workbook("inventory.xlsx")
product_list = inv_file["Sheet1"]

products_per_supplier = {}

total_value_per_supplier = {}

for product_row in range(2, product_list.max_row + 1):
    supplier_name = product_list.cell(product_row, 4).value
    inventory = product_list.cell(product_row, 2).value
    price = product_list.cell(product_row, 3).value

    # calculation number of products per supplier
    if supplier_name in products_per_supplier:
        current_num_products = products_per_supplier.get(supplier_name)
        products_per_supplier[supplier_name] = current_num_products + 1

    else:
        print("adding a new supplier")
        products_per_supplier[supplier_name] = 1

    # calculation total value of inventory per supplier
    if supplier_name in total_value_per_supplier:
        current_total_value = total_value_per_supplier.get(supplier_name)
        total_value_per_supplier[supplier_name] = current_total_value + inventory * price

    else:
        total_value_per_supplier[supplier_name] = inventory * price

print(products_per_supplier)
print(total_value_per_supplier)
```

### Exercise: List products with inventory less than 10

In a real scenario, we use this to order more products, that counting is low.

```python
import openpyxl

inv_file = openpyxl.load_workbook("inventory.xlsx")
product_list = inv_file["Sheet1"]

products_per_supplier = {}
total_value_per_supplier = {}
products_under_10_inv = {}

for product_row in range(2, product_list.max_row + 1):
    supplier_name = product_list.cell(product_row, 4).value
    inventory = product_list.cell(product_row, 2).value
    price = product_list.cell(product_row, 3).value
    product_num = product_list.cell(product_row, 1).value

    # calculation number of products per supplier
    if supplier_name in products_per_supplier:
        current_num_products = products_per_supplier.get(supplier_name)
        products_per_supplier[supplier_name] = current_num_products + 1

    else:
        print("adding a new supplier")
        products_per_supplier[supplier_name] = 1

    # calculation total value of inventory per supplier
    if supplier_name in total_value_per_supplier:
        current_total_value = total_value_per_supplier.get(supplier_name)
        total_value_per_supplier[supplier_name] = current_total_value + inventory * price

    else:
        total_value_per_supplier[supplier_name] = inventory * price

    # logic products with inventory less than 10
    if inventory < 10:
        products_under_10_inv[int(product_num)] = int(inventory)

print(products_per_supplier)
print(total_value_per_supplier)
print(products_under_10_inv)

```

### Exercise: Write to Spreadsheet: Calculate and write inventory value for each product into spreadsheet

