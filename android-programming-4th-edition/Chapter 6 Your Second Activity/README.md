## Chapter 6 - Your Second Activity

### Second Activity
- there are two ways of creating a new activity, a manually one, that involves to touch at least three files: **Kotlin Class file**, **XML Layout file** and the **application manifest file**. The second way is through _Android Studio's New Activity_ wizard:
  - right-clicking on the `app/java` folder, choose _New > Activity > Empty Activity_.

### manifest XML
- it is a XML file that contains metadata that describes the application to the Android OS. It is located at `manifests/AndroidManifest.xml` or press CTRL + SHIFT + O (command-Shift-O) to find the file.

### starting an Activity
- using the method `startActivity(Intent)` is requested to the ActivityManager (managed by Android OS), that creates an Activity instance and call `onCreate(Bundle?)`. The _Intent_ param is the info of which Activity the OS should create an instance.


### Intent
- Intent is an object that a component can use to communicate with the Android OS (components seen until so far: _Activity_, but there are _services_, _broadcast receivers_, and _content providers_);
- Intent are a multipurpose communication tool, it means Intent provides a lot of constructos depending on the intent to do, in the case of project, it is going to be used this constructor: `Intent(packageContext: Context, class: Class<?>)`. Here's an example:
```kotlin
import android.content.Intent
...
val intent = Intent(this, Activity::class.java)
startActivity(intent)
...
```
### Explicit and implicit intents
- **explicit** intent is when start an activity inside the application, otherwise, **inplicit** is when start an activity in another application. Even though is an activity inside the application, it is managed by by the ActivityManager (all means, by the Android OS).

### intent extras
- it is a structure as a key-value pair. It calls from `Intent.putExtra(name: String, value: Boolean)`. The second argument can be used with a lot of types. The _name_ argument is recommended to use with the reverse DNS pattern, something like `com.company.appname.string_of_identification`, to differ from another application installed on user's device.

### campanion object
- A campanion object allows to access a function without having a class instance, it is similar to static functions in Java.

### retrieving the extra
- it is used `Intent.getBooleanExtra(String, Boolean)`. The first argument is the extra name used previously, and the second argument is a default value if the key-value is not found.

### getting a result back from child activity
- instead of starting an activity with `startActivity(Intent)` method, use the `startActivityForResult(Intent, Int)` to hear back a result from child activity. First parameter is the same intent as before, the second one is the _request code_. This is a user-defined integer that is sent to the child activity and then received back by the parent;
- The child activity may invoke to send data back to the parent in two functions:
  - `SetResult(resultCode: Int)` or
  - `SetResult(resultCode: Int, data: Intent)`
- Usually the _result code_ is predefined constants: `Activity.RESULT_OK` ou `Activity.RESULT_CANCELED`(when user presses Back Button). If it is necessary to use a custom result code, `RESULT_FIST_USER` will help;
- after user presses the Back Button to return to Parent Activity, the ActivityManager call the `onActivityResult(requestCode: Int, resultCode: Int, data: Intent)` function. The request code is the original passed from Parent Activity, the resul code and the intent are the same passed by `setResult(Int, Intent)`;
- the function `Activity.finish()` will pop the current activity off the stack of activities.

### some commands in kotlin I wasn't used to
- [apply](https://kotlinlang.org/docs/reference/scope-functions.html#apply)
- [when](https://kotlinlang.org/docs/reference/control-flow.html)

### Challenges
- Closing Loopholes for Cheaters;
- Tracking Cheat Status by Question.