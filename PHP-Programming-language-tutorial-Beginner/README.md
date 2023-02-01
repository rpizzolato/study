# PHP Programming Language Tutorial - Full Course

This is a repository with annotations from the Course **PHP Programming Language Tutorial** of the YouTube channel **freeCodeCamp.org**. The Course is available here
[https://www.youtube.com/watch?v=OK_JCtrrv-c](https://www.youtube.com/watch?v=t8pPdKYpowI). I'd like to Thank Mike Dane so much for providing free content with so much quality!

## Working with Strings

- **text in upper case**: `strtoupper("text in upper case");`

- **text in lower case**: `strtolower("text in upper case");`

- **count character number**: `strlen("count how many character there's in the phrase");`

We can access text by index, for example:

```php
  $phrase = "Giraffe Academy";
 
  // prints G
  echo $phrase[0];
```

We can also replace character using index:

```php
  $phrase = "Giraffe Academy";

  $phrase[0] = "B";
 
  // prints Biraffe Academy
  echo $phrase;
```

It is possible to replace a string to some other string:

```php
  $phrase = "Giraffe Academy";

  // prints Panda Academy
  echo str_replace("Giraffe", "Panda", $phrase);

  // prints GiraPanda Academy
  echo str_replace("ffe", "Panda", $phrase);
```

It's possible to grab part of a string:
- `substr(string $string, int $start, int $length = ?)`. See more [here](https://www.php.net/manual/pt_BR/function.substr.php)

```php
  $phrase = "Giraffe Academy";

  // grabs and prints "Aca"
  echo substr($phrase, 8, 3);
```

## Working with numbers
```php
$num = 10;

// the second is a shorthand of the first
$num = $num + 25;
$num += 25;

// can be used with other arithmetic operators
// $num -= 25;
// $num *= 25;
// and so on
```

- `abs(-100)`: prints absolute value of a number, in this case prints `100`
- `pow(2, 4)`: the power of 4 (2^4), and prints `16`
- `sqrt(144)`: prints the square root of a number. In this case prints `12`
- `max(2, 10)`: tells which number is bigger, in this case prints `10`
- `min(2, 10)`: the opposite of `max()`, prints `2`
- `round(3.2)`: rounds a number, in this case prints `3`. If `round(3.7)`, rounds to `4`
- `ceil(3.3)`: no matter the decimal point is, it ALWAYS will round up. In this case prints `4`
- `floor(3.9)`: the opposite of `ceil()`, it rounds down. In this case will print `3`

## Getting User Input

In order to get information from a form, we use `$_GET["name_from_field"]`, like this example:
```php
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Document</title>
</head>
<body>
  <form action="site.php" method="get">
    Name: <input type="text" name="name"><br />
    Age: <input type="text" name="age">
    <input type="submit">
  </form>
  <br />
  Your name is <?php echo $_GET["name"] ?><br />
  And your you are <?php echo $_GET["age"] ?> year(s) old.
</body>
</html>
```

## Building a basic (very simple) calculator
```php
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Document</title>
</head>
<body>
  <form action="site.php" method="get">
    <input type="number" name="num1">
    <br />
    <input type="number" name="num2">
    <input type="submit">
  </form>
  <br />
  Answer: <?php echo $_GET["num1"] + $_GET["num2"] ?>
</body>
</html>
```
## Building a Mad Libs Game
```php
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Document</title>
</head>
<body>
  <form action="site.php" method="get">
    Color: <input type="text" name="color"><br>
    Plural Noum: <input type="text" name="PluralNoum"><br>
    Celebrity: <input type="text" name="celebrity"><br>
    <input type="submit" value="submit">
  </form>
  <br />

  <?php
    $color = $_GET["color"];
    $PluralNoum = $_GET["PluralNoum"];
    $celebrity = $_GET["celebrity"];
    echo "Roses are $color <br>";
    echo "$PluralNoum are blue <br>";
    echo "I love $celebrity <br>";
  ?>

</body>
</html>
```

## Array
```php
<?php
  // declaration of an array
  $friends = array("Kevin", "Karen", "Oscar", "Jim");

  // changes the value of index 1
  $friends[1] = "Dwight";

  // prints Dwight
  echo $friends[1];

  // add a new value to the array
  $friends[4] = "Angela";

  // prints Angela
  echo $friends[4];

  // count the size of the array, prints 5
  echo count($friends);
  ?>
```

## Using checkboxes
 It's a good example for using array
 ```php
 <body>
  <form action="site.php" method="post">

    Apples: <input type="checkbox" name="fruits[]" value="apples"><br>
    Oranges: <input type="checkbox" name="fruits[]" value="oranges"><br>
    Pears: <input type="checkbox" name="fruits[]" value="pears"><br>

    <input type="submit" value="submit">
  </form>

  <?php
    $fruits = $_POST["fruits"];

    for ($i=0; $i < count($fruits); $i++) {

      // prints the fruits chosen
      echo $fruits[$i] . "<br>";
    }
  ?>
</body>
 ```
 >****Note****
 >
 >When we want the checkbox to be an array, in the input properties name, we name it using brackets: `name="fruits[]"`

# Associative Arrays
It is a special type of array, that not only we can store data values, but we can also store **key value pairs**

```php
<?php
  /* key is Jim, Pam, Oscar, and the value is A+ (reference to Jim), B- (reference to Pam) and so on */
  $grades = array("Jim" => "A+", "Pam" => "B-", "Oscar" => "C+");

  // prints Jim's grade, in this case = A+
  echo $grades["Jim"];

  // prints Oscar's grade B-
  echo $grades["Oscar"];

  // we can change the value using the key reference
  $grades["Oscar"] = "B+";

  // prints B+
  echo $grades["Oscar"];
  ?>
```
 >****Note****
 >
 >The keys must be unique. It's ok the values is similar, but cannot have similar key.

 Another example using associative array:
 ```php
  <form action="site.php" method="post">
  <input type="text" name="student">
  <input type="submit" value="submit">
</form>

<?php
  $grades = array("Jim" => "A+", "Pam" => "B-", "Oscar" => "C+");
 
  // prints the student's grade from submitted value in form
  echo $grades[$_POST["student"]];
?>
 ```

## Functions
 ```php
<?php
  function sayHi($name, $age){
    echo "Hello $name, you are $age <br>";
  }

  sayHi("Rodrigo", 36);
  sayHi("Mike", 13);
  sayHi("Oscar", 80);
?>
 ```

# Operators (from [w3schools](w3schools.com/php/php_operators.asp))

## PHP Arithmetic Operators
The PHP arithmetic operators are used with numeric values to perform common arithmetical operations, such as addition, subtraction, multiplication etc.

|Operator |	Name |	Example | Result |
|---|---|---|---|
| +	| Addition |	$x + $y |	Sum of $x and $y |	
| -	|Subtraction |	$x - $y |	Difference of $x and $y |	
| *	| Multiplication |	$x * $y |	Product of $x and $y |	
| /	| Division |	$x / $y |	Quotient of $x and $y |	
| %	| Modulus | 	$x % $y |	Remainder of $x divided by $y |	
| ** |Exponentiation |	$x ** $y | Result of raising $x to the $y'th power |

## PHP Assignment Operators
The PHP assignment operators are used with numeric values to write a value to a variable.

The basic assignment operator in PHP is "=". It means that the left operand gets set to the value of the assignment expression on the right.

| Assignment | Same as... |	Description	|
|---|---|---|
| x = y |	x = y |	The left operand gets set to the value of the expression on the right	|
| x += y |	x = x + y	| Addition	|
| x -= y |	x = x - y	| Subtraction	|
| x *= y |	x = x * y	| Multiplication	|
| x /= y |	x = x / y	| Division	|
| x %= y |	x = x % y	| Modulus|

## PHP Comparison Operators
The PHP comparison operators are used to compare two values (number or string):

| Operator |	Name |	Example |	Result	|
|---|---|---|---|
| == |	Equal |	$x == $y |	Returns true if $x is equal to $y	|
| === |	Identical |	$x === $y |	Returns true if $x is equal to $y, and they are of the same type |	
| != |	Not equal |	$x != $y |	Returns true if $x is not equal to $y	|
| <> |	Not equal |	$x <> $y |	Returns true if $x is not equal to $y |	
| !==	| Not identical |	$x !== $y |	Returns true if $x is not equal to $y, or they are not of the same type	|
| > |	Greater than |	$x > $y |	Returns true if $x is greater than $y	|
| < |	Less than |	$x < $y |	Returns true if $x is less than $y | 
| >= |	Greater than or equal to |	$x >= $y |	Returns true if $x is greater than or equal to $y |	
| <= |	Less than or equal to |	$x <= $y |	Returns true if $x is less than or equal to $y |	
| <=> |	Spaceship* |	$x <=> $y |	Returns `0` (zero) if $x and $y are equal. Returns `1` if $x is greater than $y (if left side is greater). And Returns `-1` if $y is greater than $x (if right side is greater) Introduced in PHP 7. |

*Example of [Spaceship](https://pt.stackoverflow.com/questions/87231/pra-que-serve-os-spaceship-operator-do-php7) Operator (`<=>`)
```php
echo 1 <=> 1; // 0
echo 3 <=> 4; // -1
echo 4 <=> 3; // 1
```

## PHP Increment / Decrement Operators
The PHP increment operators are used to increment a variable's value.

The PHP decrement operators are used to decrement a variable's value.

| Operator | Name | Description |
|---|---|---|
| ++$x |	Pre-increment	| Increments $x by one, then returns $x |	
| $x++ |	Post-increment	| Returns $x, then increments $x by one	|
| --$x |	Pre-decrement	| Decrements $x by one, then returns $x	|
| $x-- |	Post-decrement	| Returns $x, then decrements $x by one|

## PHP Logical Operators
The PHP logical operators are used to combine conditional statements.

| Operator |	Name |	Example |	Result |
|---|---|---|---|
| and |	And |	$x and $y |	True if both $x and $y are true	|
| or |	Or |	$x or $y |	True if either $x or $y is true	|
| xor |	Xor |	$x xor $y |	True if either $x or $y is true, but not both	|
| && |	And |	$x && $y |	True if both $x and $y are true	|
| \|\| |	Or |	$x \|\| $y |	True if either $x or $y is true	|
| ! |	Not |	!$x |	True if $x is not true|

## PHP String Operators
PHP has two operators that are specially designed for strings.

| Operator |	Name |	Example |	Result
|---|---|---|---|
| .	| Concatenation |	$txt1 . $txt2 |	Concatenation of $txt1 and $txt2 |	
| .= |	Concatenation assignment |	$txt1 .= $txt2 |	Appends $txt2 to $txt1 |

## PHP Array Operators
The PHP array operators are used to compare arrays.

| Operator |	Name |	Example |	Result |
|---|---|---|---|
| + |	Union |	$x + $y |	Union of $x and $y |	
| == |	Equality |	$x == $y |	Returns true if $x and $y have the same key/value pairs	|
| === |	Identity |	$x === $y |	Returns true if $x and $y have the same key/value pairs in the same order and of the same types	|
| != |	Inequality |	$x != $y |	Returns true if $x is not equal to $y	|
| <> |	Inequality |	$x <> $y |	Returns true if $x is not equal to $y	|
| !==	| Non-identity |	$x !== $y |	Returns true if $x is not identical to $y |

## PHP Conditional Assignment Operators
The PHP conditional assignment operators are used to set a value depending on conditions:

| Operator |	Name |	Example |	Result |
|---|---|---|---|
| ?: |	Ternary |	$x = expr1 ? expr2 : expr3 |	Returns the value of $x.<br> The value of $x is expr2 if expr1 = TRUE.<br> The value of $x is expr3 if expr1 = FALSE	|
| ?? |	Null coalescing |	$x = expr1 ?? expr2 |	Returns the value of $x.<br> The value of $x is expr1 if expr1 exists, and is not NULL.<br> If expr1 does not exist, or is NULL, the value of $x is expr2. Introduced in PHP 7	|

## if statement

```php
<?php
    $isMale = true;
    $isTall = true;

    if ($isMale && $isTall) {
        echo "You are a tall male";
    } elseif ($isMale && !$isTall) {
        echo "You are a short male";
    } elseif (!$isMale && $isTall) {
    echo "You are a not male, but is are tall";
    } else {
        echo "You are a not male and not tall";
    }
?>
```
>**Note**
>
>`else if` in PHP is written `elseif`

## Building a Better Calculator

```php
<body>
  <form action="site.php" method="post">

  First Num: <input type="number" step="0.1" name="num1"><br>
  OP: <input type="text" name="op"><br>
  Second Num: <input type="number" step="0.1" name="num2"><br>

  <input type="submit" value="submit"><br>
  </form>

  <?php
      $num1 = $_POST["num1"];
      $op = $_POST["op"];
      $num2 = $_POST["num2"];

      if ($op == "+") {
      echo $num1 + $num2;
      } elseif ($op == "-") {
      echo $num1 - $num2;
      } elseif ($op == "*") {
      echo $num1 * $num2;
      } elseif ($op == "/") {
      echo $num1 / $num2;
      } else {
      echo "Invalid Operator";
      }
  ?>
</body>
```

>****Note****
>
>In the previous example, we cannot use floating point because `number` filed does not accept it. To fix this, we use `step="0.1"` property.

## Switch Statement

```php
<form action="site.php" method="post">

  <input type="text" name="grade" id="">
  <input type="submit" value="submit"><br>
</form>

  <?php
      $grade = $_POST["grade"];
      switch($grade) {
      case "A":
          echo "You did amazing!";
          break;
      case "B":
          echo "You did pretty good";
          break;
      case "C":
          echo "You did poorly!";
          break;
      case "D":
          echo "You did very bad";
          break;
      case "F":
          echo "YOU FAIL!";
          break;
      default:
          echo "Invalid Grade";
      }
  ?>
```

## While Loops
```php
<?php
  $index = 1;

  while ($index <= 5) {
    // prints until $index = 5
      echo "$index <br>";
      $index++;
  }
?>
```

```php
$index = 6;

// the conditions do not let enter while
while ($index <= 5) {
  // prints nothing
    echo "$index <br>";
    $index++;
}

```

```php
$index = 6;

do {
  //prints once
    echo "$index <br>";
    $index++;
}while(($index <= 5));
```
## For Loops

```php
for ($i=1; $i < 5; $i++) { 
  echo "$i <br>";
}
```

Printing out an array
```php
$luchyNumbers = array(4, 8, 14, 16, 23, 42);

for ($i=0; $i < count($luchyNumbers); $i++) { 
    echo "$luchyNumbers[$i] <br>";
}
```

## foreach Loop

```php
$fruit = array("Orange", "Apple", "Strawberry", "Grape");

foreach ($fruits as $fruit) {
  echo "$fruit <br>";
}
```

Prints

```
Orange
Apple
Strawberry
Grape
```
Another example using associative arrays:

```php
$age = array("Peter"=>"35", "Ben"=>"37", "Joe"=>"43");

foreach($age as $x => $val) {
  echo "$x = $val<br>";
}
```

Prints

```
Peter = 35
Ben = 37
Joe = 43
```

## Comments

```php
 // single comment

 # single comment

/* multi-line
 comment */ 
```

## include

Just simple like this:

```php
<?php include "header.html" ?>
<p>Hello World</p>
<?php include "footer.html" ?>
```

## Include PHP

We can also include PHP files. This allows us to create some template of a page, let's see this example:

`article-header.php`
```php
<h2><?php echo $title; ?></h2>
<h4><?php echo $author; ?></h4>
word count: <?php echo $wordCount; ?>
```

If we run in this way, it will warning us not defined values in the variables, so we're going to set these variables in the `site.php` file, and also use the `include` statement:

`site.php`

```php
<body>
  <?php
      $title = "My first Post";
      $author = "Rodrigo";
      $wordCount = 400;
      include "article-header.php";
  ?>
</body>
```

And it'll render correctly.

> **Note**
>
>We can use others functionalities from the template file, like functions, variables and so on.

## Classes and Objects

```php
class Book {
  var $title;
  var $author;
  var $pages;
}

$book1 = new Book();
$book1->title = "Harry Potter";
$book1->author = "JK Rowling";
$book1->pages = 400;

// prints book's title
echo $book1->title;
```

> **Note**
>
> `->` is used to call a method, or access a property, on the object of a class<br>
> `=>` is used to assign values to the keys of an array. See [more](https://stackoverflow.com/questions/14037290/what-does-this-mean-in-php-or)

## Constructor

Considering the book example object created previously, a constructor (a function) is created in this way

```php
class Book {
            var $title;
            var $author;
            var $pages;

            function __construct(){
                echo "New Book Created <br>";
            }
        }

        $book1 = new Book();
        $book1->title = "Harry Potter";
        $book1->author = "JK Rowling";
        $book1->pages = 400;

        $book2 = new Book();
        $book2->title = "Lord of Rings";
        $book2->author = "Tolkien";
        $book2->pages = 700;

        echo $book1->title;
```

We use the reserved word `__construct` (underscores `__`), and considering the two objects created (book1 and book2), it will print out "New Book Created" twice.

We can also include the value need in the constructor, like this:

```php
class Book {
    var $title;
    var $author;
    var $pages;

    function __construct($aTitle, $aAuthor, $aPages){
        $this->title = $aTitle;
        $this->author = $aAuthor;
        $this->pages = $aPages;
    }
}

$book1 = new Book("Harry Potter", "JK Rowling", 400);

// we can update the value too
$book1->title = "Hunger Games";

$book2 = new Book("Lord of Rings", "Tolkien", 700);

// prints Hunger Games
echo $book1->title;
```

## Getters and Setters

This will prevent to access directly the variable of the Class (safety reasons), and if we try something like this:

```php
class Movie {
    private $title;
    private $rating;

    function __construct($title, $rating){
        $this->title = $title;
        $this->rating = $rating;
    }
}

$avengers = new Movie("Avengers", "PG-13");

echo $avengers->rating;
```
Will return this:
```
Fatal error: Uncaught Error: Cannot access private property Movie::$rating
```

So we allow just the getters and setters functions:

```php
class Movie {
    private $title;
    private $rating;

    function __construct($title, $rating){
        $this->title = $title;
        $this->rating = $rating;
    }

    function getRating() {
        return $this->rating;
    }
}

$avengers = new Movie("Avengers", "PG-13");

// returns "PG-13"
echo $avengers->getRating();
```

If we need to modify the rating, we use the setter function

```php
class Movie {
    private $title;
    private $rating;

    function __construct($title, $rating){
        $this->title = $title;
        $this->rating = $rating;
    }

    function getRating() {
        return $this->rating;
    }

    function setRating($rating) {
        $this->rating = $rating;
    }
}

$avengers = new Movie("Avengers", "PG-13");

// changes the rating
$avengers->setRating("Dog");

// prints "Dog"
echo $avengers->getRating();
```

In order to block other possibilities different from **G**, **PG**, **PG-13** and **R**, we can create an `if` statement inside `setRating` function, and use it in the constructor too.

```php
class Movie {
    private $title;
    private $rating;

    function __construct($title, $rating){
        $this->title = $title;
        $this->setRating($rating);
    }

    function getRating() {
        return $this->rating;
    }

    function setRating($rating) {
        if ($rating == "G" || $rating == "PG" || $rating == "PG-13" || $rating == "R") {
            $this->rating =  $rating;
        } else {
            $this->rating = "NR";
        }
    }
}

$avengers = new Movie("Avengers", "PG-13");

// it will set "NR"
$avengers->setRating("Dog");

// prints "NR"
echo $avengers->getRating();
```

## Regular Expression

In the previous example, we use an if statement to check if the user typed the correct "pattern" of the movie rating. Instead of using a very large if statement, we could use Regular Expression:

```php
$exp = "/learning/i";
```
Regarding the code above, `/` is the delimiter, `learning` is the pattern we are searching for, and `i` is a modifier that makes the search case-**i**nsensitive.

We are going to separate every pattern by brackets `[]`, in this way:

```php
"/[R][o][a-g]{2}/";
```

## Inheritance

We can extends other Class to a new Class with all information from the previous Class, using the `extends` statement. Here's an example:

```php
class Chef {
    function makeChicken() {
        echo "The chef makes chicken <br>";
    }
    function makeSalad() {
        echo "The chef makes salad <br>";
    }
    function makeSpecialDish() {
        echo "The chef makes bbq ribs <br>";
    }
}

class ItalianChef extends Chef {
    function makePasta(){
        echo "The chef makes pasta <br>";
    }
}

// Regular Chef
$chef = new Chef();
$chef->makeChicken();

// Regular Chef cannot make pasta, returns Uncaught Error undefined method
# $chef->makePasta();

// Unlike Regular Chef, the Italian Chef can make pasta
$ItalianChef = new ItalianChef();
$ItalianChef->makeChicken();
$ItalianChef->makePasta();
```

We can also overwrite a function that already exists in the Class just by using it with the same name:

```php
class Chef {
    function makeChicken() {
        echo "The chef makes chicken <br>";
    }
    function makeSalad() {
        echo "The chef makes salad <br>";
    }
    function makeSpecialDish() {
        echo "The chef makes bbq ribs <br>";
    }
}

class ItalianChef extends Chef {
    function makePasta(){
        echo "The chef makes pasta <br>";
    }

    function makeSpecialDish() {
        echo "The chef makes chicken parm <br>";
    }
}

$chef = new Chef();
$chef->makeSpecialDish(); // bbq ribs


$ItalianChef = new ItalianChef();
$ItalianChef->makeSpecialDish(); //chicken parm
```