## Chapter 7 - Android SDK Versions and Compatibility

### Android SDK Versions
- Android codenames relative to API Level can be found [here](https://source.android.com/setup/start/build-numbers);
- The distribution percentage can be found [here](https://developer.android.com/about/dashboards/index.html)

### minSdkVersion
- in the `build.gradle` file there is an information regarding `minSdkVersion`. Depending on the version set here, the Android will refuse to install the application.

### targetSdkVersion
- still in the `build.gradle`, now in `targetSdkVersion`, this means the API Level that applications was designed, and most of the cases is the lastest version of Android.

### threat code from later APIs safely
- 