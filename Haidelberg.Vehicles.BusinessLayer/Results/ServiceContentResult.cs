using System.Collections.Generic;

namespace Haidelberg.Vehicles.BusinessLayer
{
    public class ServiceResult
    {
        public bool IsSuccessfull { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public void AddError(string error) => Errors.Add(error);
    }

    public class ServiceContentResult<T> : ServiceResult
    {
        public T Result { get; set; }
    }
}

