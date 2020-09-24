## Chapter 4 - Persisting UI State

### ViewModel Dependency
- in order to fix the rotation bug from previous project app, it's necessary create a ViewModel class. It's needed to add a *lifecycle-extensions* in `build.gradle` file as a dependency;
    - just to be clear, there are two `build.gradle` files:
        1. one refers to the project as a whole;
        2. the other refers to the app module (`app/build.gradle`).
    - open the file that refers to the app module (`app/build.gradle`) and add the following dependency:
    ```gradle
    dependencies {
        ...
        implementation 'androidx.lifecycle:lifecycle-extensions:2.0.0'
        ...
    }
    ```
    any changes in this file, Android Studio will ask to Sync the project. Click in *Sync Now* (or through File > Sync Project with Grade Files).
- **very important**: the relationship between *ViewModel* and *Activity* is unidirectional, it means that just the *Activity* sould reference the *ViewModel*, and never the otherwise, because this could cause memory leak;
- **by lazy**: the following kotlin code:
```kotlin
private val quizViewModel: QuizViewModel by lazy {
    ViewModelProviders.of(this).get(QuizViewModel::class.java)
}
```
by lazy makes sure the quizViewModel property be a val instead of a var. This means the value can be assigned just one time. And more importantly the assignment is just made until the first time the access to quizViewModel is made.

- Android OS may claim for memory and kill the application that is on background, like if the user is using the application and later decide to watch a video for example. This background application may be killed by OS. To prevent this lose of data, it's possible to override `Activity.onSaveInstanceState(Bundle)` to sore temporaruly data outside of the activity. When user presses Home Button and lanches a different app, this activity is marked as *killable*. At the moment the Home Button is presses, `onSaveInstanceState()` is called (right before the activiey is marked as *killable*);
- tipically, `onSaveInstanceState(Bundle)` is used to stash small, transient-state data from current activity in *Bundle*. `onStop()` is used to save any permanent data;

### There were no challenge on this chapter