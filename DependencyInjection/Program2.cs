//using System;
//using System.Collections.Generic;

//namespace DependencyInjection
//{
//    class Program2
//    {
//        static void Main(string[] args)
//        {
//            var package = new Package { Destination = "skopje/kisela voda" };
//            var deliveryEngine = new DeliveryEngine(new Human());
//            deliveryEngine.DeliverPackage(package);
//        }
//    }

//    public class Package
//    {
//        public string Destination { get; set; }
//    }

//    public class DeliveryEngine
//    {
//        private readonly IDeliveryExecutioner _deliveryExecutioner;

//        public DeliveryEngine(IDeliveryExecutioner deliveryExecutioner)
//        {
//            _deliveryExecutioner = deliveryExecutioner;
//        }

//        public void DeliverPackage(Package package)
//        {
//            Console.WriteLine("validating package");
//            Console.WriteLine("calculating customs");
//            Console.WriteLine("calculating tax");
//            _deliveryExecutioner.DeliverPackage(package);
//            Console.WriteLine("gathering feedback");
//        }
//    }

//    public interface IDeliveryExecutioner
//    {
//        void DeliverPackage(Package p);
//    }

//    public class Human : IDeliveryExecutioner
//    {
//        public void DeliverPackage(Package p)
//        {
//            Console.WriteLine("delivering package to " + p.Destination + " by walk");
//        }
//    }

//    public class Drone : IDeliveryExecutioner
//    {
//        public void DeliverPackage(Package p)
//        {
//            Console.WriteLine("delivering package to " + p.Destination + " with Drone");
//        }
//    }

//    //public class HumanWithBike
//    //{
//    //    internal void DeliverPackage(Package p)
//    //    {
//    //        Console.WriteLine("delivering package to " + p.Destination + " with Bike");
//    //    }
//    //}

//    //public class Helicopter
//    //{
//    //    internal void DeliverPackage(Package p)
//    //    {
//    //        Console.WriteLine("delivering package to " + p.Destination + " with Helicopter");
//    //    }
//    //}
//}
