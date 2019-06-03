using CrossPlatformLibrary.Bootstrapping;

namespace SampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Startup();

            //var dispatcherService = SimpleIoc.Default.GetInstance<IDispatcherService>();

            System.Console.WriteLine("SampleConsoleApp successfully bootstrapped");
            System.Console.ReadLine();
        }
    }
}
