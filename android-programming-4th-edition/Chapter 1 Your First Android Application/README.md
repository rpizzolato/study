## Chapter 1 - Your First Android Application

### Main Concepts
- **Activity**: is an instance of the `Class Activity` in the Android SDK. It manages the user interaction on a screen;
- **layout**: is a set of UI objects on screen (and its positions). It's made from a XML file;
- **Views**: are building blocks used to compese a UI, basiclly everything it's seen is a View;
- **Widgets**: are texts or graphics shown, buttons that are pressed, evething the user interact with. It is an instance of the `View Class` or its subclasses like `Button`, `TextView`.
- **ViewGroup**: is a kind of View, but instead of display something itself, it manages where the views contents is displayed. It's very common to refer them as layouts;

### Challenge
- show toast at the top of the screen using `Gravity.TOP`.

### Challenge warning
- the challenge of chapter one asks to change the position of the Toast to top, and the solution would be something like this:
    ```kotlin
    trueButton.setOnClickListener { view: View ->
            val toast = Toast.makeText(
                this,
                R.string.correct_toast,
                Toast.LENGTH_SHORT)
            
            //1st argument: goes to right. 2nd: goes down
            toast.setGravity(Gravity.TOP or Gravity.LEFT, 0, 0)
            toast.show()
        }
    ```
    but in the official Android documentation about [setGravity](https://developer.android.com/reference/kotlin/android/widget/Toast#setgravity) method shows a warning about the newer build versions no-op anymore:

    `Warning: Starting from Android Build.VERSION_CODES#R, for apps targeting API level Build.VERSION_CODES#R or higher, this method is a no-op when called on text toasts.`