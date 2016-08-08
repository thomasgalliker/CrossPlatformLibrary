using System;

namespace CrossPlatformLibrary.Dispatching
{
    public class DispatcherService : IDispatcherService
    {
        public void CheckBeginInvokeOnUI(Action action)
        {
            throw new NotImplementedInReferenceAssemblyException();
        }
    }
}
