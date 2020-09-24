# Android Basics in Kotlin

#### This is a repository for main annotations of the Android Basics in Kotlin course offered for free by Google at [here](https://developer.android.com/courses/android-basics-kotlin/unit-1).

## Android Basics: Introduction to Kotlin

### Welcome to Android Basics in Kotlin
- some statistics: at moment (about May of 2020) there are over **2.5B** Android devices in the world.
### Build your first Android app in Kotlin
- Some basics of Kotlin language and Android Studio [download](https://developer.android.com/studio) and requirements.
### Write your first program in Kotlin
- Hello World:
```kotlin
fun main() {
    println("Hello, world!")
}
```
  - `fun` is a word in Kotlin programming. It stands for _function_. Function is a section of the program that performs a specific task.
  - `main` is the function's name;
  - `()` the two parentheses is where the arguments stay, for short it is used `args`;
  - `{}` the pair of curly braces is where the code that accomplishes a task;
  - `println()` tells the system to print a line of text;
  - `print()`  prints the text without adding a new line break;
  - `"Hello, world!"` use quotes around text.
### Create a birthday message in Kotlin
- variables:
```kotlin
val age = 5
```
  - `val` is a special word used bu Kotlin, called a keyword, indicating the what follows is the name of the variable;<br>
**important:** variable declared using `val` keyword can only be set **once**. Changeable variable can be declared using `var` keyword
  - `age` is the name of variable;
  - `=` assign the value of variable's name;
  - sometimes is necessary to put a variable around a text and print it. Using by putting your variable inside curly braces preceded by a dollar sign, like this `${variable}`. Here's an example:
  ```kotlin
  println("You are already ${age}!")
  ```
  - if it is needed to print a border, it is a good choice to use the `repeat()` statement, like this:
  ```kotlin
  fun printBorder() {
    repeat(23) {
      print("=")
    }
    println()
  }
  ```
  it will repeat 23 times the `=` sign
  - and what if the pattern of the border would be different, print `%` instead of `=`. It can be solved by using _arguments_:
  ```kotlin
  fun main() {
    printBorder("%")
  }

  fun printBorder(border: String) {
    repeat(23) {
      print(border)
    }
    println()
  }
  ```
    - the name of argument is `border`;
    - the name is followed by a colon `:`;
    - and the word `String` is a description of what kind, or type, of the argument.
  - function with two arguments:
  ```kotlin
  fun main() {
    printBorder("%", 23)
  }


  fun printBorder(border: String, timesToRepeat: Int) {
    repeat(timesToRepeat) {
      print(border)
    }
    println()
  }
  ```
  it is important to to say that `Int` is a whole positive or negative number, such as 0, 23, or -1024, so instead of using text, use a number.

## Android Basics: Create your first Android app
### Introduction to Android Studio
- more info about Android Studio at [here](https://developer.android.com/studio/intro)

### Download and install Android Studio
- download it at [here](https://developer.android.com/studio)

### Create and run your first Android app
- Minimum SDK:
<img src="https://developer.android.com/codelabs/basic-android-kotlin-training-first-template-project/img/f650fadcfac653de.png">

### Run your app on a mobile device
- for real devices, it is necessary to turn on USB debugging. It is hidden by default. Go to Settings > About phone > tap Build number seven times > Advanced > Developer options > USB debugging;
- for Windows users is necessary to enable Google USB Driver, in Tools > SDK Manager > SDK Tools > check the Google USB Driver.

## Create a Birthday Card app

### Set up your Happy Birthday app
- create an empty Activity project:
  - minimum API Level of 19 (KitKat), name it as `Happy Birthday`
- `View` is almost everything that is seen in the app. `Views` can be interactive, like a clickable button or an editable input field, like a `TextView`;
- `ViewGroup` is a container that `View` objects can sit in. One kind of `ViewGroup` is a `ConstraintLayout`, which helps to arrange the `Views` in a flexible way.