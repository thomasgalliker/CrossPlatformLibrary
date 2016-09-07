using CrossPlatformLibrary.Bootstrapping;

namespace Sample.Console
{
    /// <summary>
    /// This sample console application demonstrates the use of CrossPlatformLibrary
    /// in environments where there is no synchronization context.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Startup();

            //var dispatcherService = SimpleIoc.Default.GetInstance<IDispatcherService>();

            System.Console.WriteLine("Sample.Console successfully bootstrapped");
            System.Console.ReadLine();
        }
    }
}