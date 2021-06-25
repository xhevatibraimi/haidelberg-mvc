using Haidelberg.Vehicles.BusinessLayer.Abstractions;

namespace Haidelberg.Vehicles.BusinessLayer
{
    public class Service: IService
    {
        private static int counter = 0;

        public Service()
        {
            counter++;
        }

        public int Counter => counter;
    }
}
