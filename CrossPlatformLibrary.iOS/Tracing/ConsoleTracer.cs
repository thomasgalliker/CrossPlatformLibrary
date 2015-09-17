

using System;

using Guards;

namespace CrossPlatformLibrary.Tracing
{
    public class ConsoleTracer : TracerBase
    {
        private readonly string name;

        public ConsoleTracer(string name)
        {
            Guard.ArgumentNotNullOrEmpty(() => name);

            this.name = name;
        }

        protected override void WriteCore(TraceEntry entry)
        {
            if (entry.Exception == null)
            {
                Console.WriteLine("{0} - {1} - {2}", DateTime.Now, entry.Category, entry.Message);
            }
            else
            {
                Console.WriteLine("{0} - {1} - {2} - Exception: {3}", DateTime.UtcNow, entry.Category, entry.Message, entry.Exception);
            }
        }

        public override bool IsCategoryEnabled(Category category)
        {
            return true;
        }
    }
}