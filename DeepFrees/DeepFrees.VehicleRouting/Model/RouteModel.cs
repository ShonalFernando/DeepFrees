namespace DeepFrees.VehicleRouting.Model
{
    public class RouteModel
    {
        public List<DistanceMatrixModel> DistanceMatrices { get; set; } = null!;
        public int VehicleNumber { get; set; }
        public int Depot { get; set; }
    }

    public class DistanceMatrixModel
    {
        public string? StartLocation { get; set; }
        public string? Destination { get; set; }

        public int LocationFrom { get; set; }
        public int LocationTo { get; set; }
        public long Distance { get; set; }
    }
}
