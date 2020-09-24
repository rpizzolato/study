## Chapter 5 - Debugging Android Apps

### buggy message
- versions before _Android Pie (API 28)_, an error message informing that the app crashed. On _Android Pie_ and latest above this version, it's shown a modal informing the app stopped, with options of _App info_ and _Close app_.
- if it's necessary to log a stack trace, sometimes when the bug doesn't report anything in the _Logcat_, use the following statment:
```kotlin
Log.d(SOME_TAG, "Some text to help find this on Stack", Exception())
```
### breakpoints
- a breakpoint allows to pause the execution for examining line by line what's going on with the code;
- in order to use it, mark the line code in the gray gutter in the lefthand margin, a red circle will apear. It's also possible toggle a breakpoint by pressing CTRL + F8 (or command-F8 on MacOS users);
- after set a breakpoint, press the Debug 'app' button or attach Debugger to Android process to debug;

### Android Lint
- Android Lint is a static analyzer for Android Code, that check the code before the compiler. It brings very good advices, so it's very important to consider it;
- in order to analyze the code manually, select Analyze > Inspect Code.

### Layout inspector
- it's a way to debug layout file issue. Make sure the app is running in the Android emulator, and go to Tools > Layout Inspector, and explore the properties of the layout by clicking them.

### Profiler
- the profiler creates a detailed reports of how the application is using resources of the Android's device, such as CPU and memory. To enable it, go to View > Tool Windows > Profiler.

#### In this chapter, the challenges just explained the Layout inspector and the Profiler