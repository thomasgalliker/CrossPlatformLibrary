using System;
using System.Collections.Generic;

using Android.Animation;
using Android.Views;

namespace Xamarin.Extensions
{
    public static class AnimationExtensions
    {
        private static readonly Dictionary<View, Animator> CurrentAnimations = new Dictionary<View, Animator>();

        private static void ClearOldAnimation(View view)
        {
            Animator oldAnimator;
            if (CurrentAnimations.TryGetValue(view, out oldAnimator))
            {
                oldAnimator.Cancel();
                CurrentAnimations.Remove(view);
            }
        }

        public static void AlphaAnimate(this View view, float alpha, int duration = 300, Action endAction = null, int startDelay = 0)
        {
            ClearOldAnimation(view);
            var animator = ObjectAnimator.OfFloat(view, "alpha", view.Alpha, alpha);
            CurrentAnimations[view] = animator;
            animator.SetDuration(duration);
            animator.StartDelay = startDelay;
            animator.AnimationEnd += (sender, e) =>
                {
                    CurrentAnimations.Remove(view);
                    if (endAction != null)
                    {
                        endAction();
                    }
                    ((Animator)sender).RemoveAllListeners();
                };
            animator.Start();
        }

        public static void TranslationYAnimate(this View view, int translation, int duration = 300, ITimeInterpolator interpolator = null, Action endAction = null)
        {
            ClearOldAnimation(view);
            var animator = ObjectAnimator.OfFloat(view, "translationY", view.TranslationY, translation);
            animator.SetDuration(duration);
            if (interpolator != null)
            {
                animator.SetInterpolator(interpolator);
            }
            animator.AnimationEnd += (sender, e) =>
                {
                    CurrentAnimations.Remove(view);
                    if (endAction != null)
                    {
                        endAction();
                    }
                    ((Animator)sender).RemoveAllListeners();
                };
            animator.Start();
        }
    }
}