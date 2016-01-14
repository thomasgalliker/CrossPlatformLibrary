namespace CrossPlatformLibrary
{
    /// <summary>
    /// LinkerHook is used to instruct the Xamarin.iOS linker not to remove unused code from this library.
    /// Code which appears unused to the compiler is used via reflection.
    /// 
    /// LinkerHook must be referenced in the target Xamarin.iOS or MonoTouch project.
    /// </summary>
    public static class LinkerHook{}
}