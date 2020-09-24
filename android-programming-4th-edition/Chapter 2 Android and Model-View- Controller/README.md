## Chapter 2 - Android and Model-View-Controller

### Class Question
- in `Question.kt` file, is added the following code:
    ```kotlin
    data class Question(@StringRes val textResId: Int, val answer: Boolean)
    ```
    the annotation` @StringRes` is not required, tha authors recommend the use of it for two reasons:
    1. the Lint code inspector verify at compile time that a valid string resource ID is being used, preventing runtime crashes;
    2. it turns the code more readable for others developers.
- the use of data in front the name of the Class indicates that is a Class to store data, and it does some extra work like providing the `equals()`, `hashCode()` and `toString()` functions;
- when it's necessary to put a text dynamically in a TextView, it makes more sense to use `tools:text="@string/dynamical_text"` instead of `android:text=""` property, because with `tools` property it's just rendered on Android Studio preview. In runtime it shows nothing, so it's possible change it programmatically;
- pixel density of a device:

    | sufix      |  pixel density  |
    | ---------- | ------------------- |
    |  mdpi      |  medium-density screens (~160dpi) |
    |  hdpi      |  high-density screens (~240dpi) |
    |  xhdpi     |  extra-high-density screens (~320dpi) |
    |  xxhdpi    |  extra-extra-high-density screens (~480dpi) |
    |  xxxhdpi   |  extra-extra-extra-high-density screens (~640dpi) |

- **dp**: *density-independent pixel*: used for margins, padding or anything else that usually would specify with a pixel value;
- **sp**: *scale-independent pixel*: user for font-size;

### Challenge
- add a listener to the TextView;
- add a previous button;
- turn previous and next button into imagebutton. 