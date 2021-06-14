using System.Collections.Generic;

namespace Haidelberg.Vehicles.BusinessLayer
{
    public class ServiceResult
    {
        public bool IsSuccessfull { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class ServiceContentResult<T> : ServiceResult
    {
        public T Result { get; set; }
    }
}

