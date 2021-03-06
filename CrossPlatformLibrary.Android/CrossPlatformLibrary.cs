﻿using System;
using Android.App;

namespace CrossPlatformLibrary
{
    public static class CrossPlatformLibrary
    {
        private static Func<Activity> activityResolver;

        public static Activity CurrentActivity => GetCurrentActivity();

        public static void SetCurrentActivityResolver(Func<Activity> activityResolver)
        {
            CrossPlatformLibrary.activityResolver = activityResolver;
        }

        private static Activity GetCurrentActivity()
        {
            if (activityResolver is null)
            {
                throw new InvalidOperationException("Resolver for the current activity is not set. Call CrossPlatformLibrary.SetCurrentActivityResolver somewhere in your startup code.");
            }

            var activity = activityResolver();
            if (activity is null)
            {
                throw new InvalidOperationException("The configured CurrentActivityResolver returned null. " +
                                                    "You need to setup the Android implementation via CrossPlatformLibrary.SetCurrentActivityResolver(). " +
                                                    "If you are using CrossCurrentActivity don't forget to initialize it, too!");
            }

            return activity;
        }
    }
}
