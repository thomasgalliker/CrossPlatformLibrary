

using System;

using Android.Util;

using Guards;

namespace CrossPlatformLibrary.Tracing
{
    public class AndroidLogTracer : TracerBase
    {
        private readonly string name;

        public AndroidLogTracer(string name)
        {
            Guard.ArgumentNotNullOrEmpty(() => name);

            this.name = name;
        }

        protected override void WriteCore(TraceEntry entry)
        {
            var logPriority = ConvertCategoryToLogPriority(entry.Category);
            Log.WriteLine(logPriority, this.name, entry.Message);
        }

        public override bool IsCategoryEnabled(Category category)
        {
            return true;
        }

        private static LogPriority ConvertCategoryToLogPriority(Category category)
        {
            LogPriority level;

            switch (category)
            {
                case Category.Fatal:
                    level = LogPriority.Error;
                    break;

                case Category.Error:
                    level = LogPriority.Error;
                    break;

                case Category.Information:
                    level = LogPriority.Info;
                    break;

                case Category.Warning:
                    level = LogPriority.Warn;
                    break;

                case Category.Debug:
                    level = LogPriority.Debug;
                    break;

                default:
                    throw new InvalidOperationException(string.Format("ConvertCategoryToLogPriority could not map Category {0} to LogPriority enum.", category));
            }

            return level;
        }
    }
}