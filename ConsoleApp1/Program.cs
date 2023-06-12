using IoCContainer;
using System.ComponentModel.Design;

namespace ConsoleApp1
{
    public class Service
    {

    }
    public class Service2
    {
        public Service _service;
        public Service2(Service service)
        {
            _service = service;
        }
    }
    internal class Program
    {
        static Container container = new Container();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            container.RegisterService<Service2>((container) =>
            {
                var service = container.GetService<Service>();
                return new Service2(service);
            });
            container.RegisterService<Service>();

            var service1 = container.GetService<Service2>();

            Console.WriteLine(service1._service.GetHashCode());
            Console.WriteLine(container.GetService<Service>().GetHashCode());
        }
    }
}