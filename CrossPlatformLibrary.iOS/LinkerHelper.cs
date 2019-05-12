using Foundation;

// This assembly annotation is essential for CrossPlatformLibrary to work correctly in Xamarin.iOS and MonoTouch projects.
// It prevents the Xamarin linker from removing 'unused' code.
[assembly: Preserve(typeof(CrossPlatformLibrary.LinkerHook), AllMembers = true)]