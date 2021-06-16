namespace Haidelberg.Vehicles.BusinessLayer.Abstractions.Responses
{
    public class GetCategoryByIdSpecialResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UsedByVehiclesCount { get; set; }
    }
}
