using DeepFrees.VehicleRouting.Model;

namespace DeepFrees.VehicleRouting.Helper
{
    public static class DistanceMatrixConverter
    {
        public static long[,] ConvertToLongArray(List<DistanceMatrixModel> distanceMatrix)
        {
            int size = distanceMatrix.Max(dm => Math.Max(dm.LocationFrom, dm.LocationTo)) + 1;
            long[,] result = new long[size, size];

            foreach (var item in distanceMatrix)
            {
                result[item.LocationFrom, item.LocationTo] = item.Distance;
            }

            return result;
        }

        public static List<DistanceMatrixModel> ConvertToModelList(long[,] distanceMatrix)
        {
            List<DistanceMatrixModel> result = new List<DistanceMatrixModel>();

            for (int i = 0; i < distanceMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < distanceMatrix.GetLength(1); j++)
                {
                    if (distanceMatrix[i, j] != 0)  // Assuming 0 indicates no connection
                    {
                        result.Add(new DistanceMatrixModel
                        {
                            LocationFrom = i,
                            LocationTo = j,
                            Distance = distanceMatrix[i, j]
                        });
                    }
                }
            }

            return result;
        }
    }
}
