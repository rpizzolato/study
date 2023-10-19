## Chapter 3 - The Activity Lifecycle

### Activity states

  | State       | In memory? | Visible to user? | In foreground? |
  |-------------|------------|------------------|--------------:
  | nonexistent | no         | no               | no             |
  | stopped     | yes        | no               | no             |
  | paused      | yes        | yes/partially*   | no             |
  | resumed     | yes        | yes              | yes            |
  *depending on circumstances, a paused activity may be fully or partially visible
  - more about lifecycle [here](https://developer.android.com/guide/components/-activities/activity-lifecycle)

- **override**: it asks the compiler to ensure that the class actually has the function that it's necessary to override. In this way, the compiler may alert for misspelled function name;
- multi-window is only avaliable on Android 7.0 Nougat and higher;
- **device configuration**: changes like rotating the device (screen orientation), screen density, screen size, keyboard type, dock mode, language, and etc. This changes will change current state of the application (like destrying and recreating a new instance of the Activity);
- multi-window broke the assumption that resumed activities were only fully visible activities. Post-Nougat world, if an user wants to watch a video and send a message at same time (with multi-window), some non-treated apps may cause misbehave. In order to fix this, it's necessary to move the playback video app to **onStart()** and **onStop()**, instead of in resuming and pausing cycle. Other way to fix this misbehave, introduced in November 2018 by Android team, is using multi-resume mode (in Android 9.0 Pie) by adding metadata to your Android manifest:
  ```
  <meta-data android:name="android.allow_multiple_resumed_activities" android:value="true" />
  ```
- Log Levels
  | Log Level   | Function     | Used for                             |
  |-------------|--------------|--------------------------------------:
  | ERROR       | Log.e(...)   | erros                                | 
  | WARNING     | Log.w(...)   | warnings                             |
  | INFO        | Log.i(...)   | informational messages               |
  | DEBUG       | Log.d(...)   | debug output ((may be filtered out)) |
  | VERBOSE     | Log.v(...)   | development only                     |

### Challenge
- prevent Repeat Answers by disabling the buttons;
- display a Toast with the percentage score for the quiz.