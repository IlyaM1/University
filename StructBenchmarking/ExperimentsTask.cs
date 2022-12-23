using System.Collections.Generic;

namespace StructBenchmarking
{
    public class Experiments
    {
        public ChartData BuildChartDataForArrayCreation()
        {
            // Create a list of ChartDataPoint objects to store the measurement results
            var dataPoints = new List<ChartDataPoint>();

            // Iterate through all of the field counts
            foreach (var fieldCount in Constants.FieldCounts)
            {
                // Measure the time taken to create an array of structs with the current field count
                var structArrayCreationTask = new StructArrayCreationTask();
                var structArrayCreationTime = Benchmark.Measure(() => structArrayCreationTask.Run(fieldCount));

                // Measure the time taken to create an array of classes with the current field count
                var classArrayCreationTask = new ClassArrayCreationTask();
                var classArrayCreationTime = Benchmark.Measure(() => classArrayCreationTask.Run(fieldCount));

                // Add the measurement results to the list of data points
                dataPoints.Add(new ChartDataPoint(fieldCount, structArrayCreationTime, classArrayCreationTime));
            }

            // Return the list of data points as a ChartData object
            return new ChartData("Array Creation", dataPoints);
        }


        public ChartData BuildChartDataForMethodCall()
        {
            // Create a list of ChartDataPoint objects to store the measurement results
            var dataPoints = new List<ChartDataPoint>();

            // Iterate through all of the argument counts
            foreach (var argumentCount in Constants.ArgumentCounts)
            {
                // Measure the time taken to pass the current number of arguments to a method using a struct
                var structMethodCallTask = new StructMethodCallTask();
                var structMethodCallTime = Benchmark.Measure(() => structMethodCallTask.Run(argumentCount));

                // Measure the time taken to pass the current number of arguments to a method using a class
                var classMethodCallTask = new ClassMethodCallTask();
                var classMethodCallTime = Benchmark.Measure(() => classMethodCallTask.Run(argumentCount));

                // Add the measurement results to the list of data points
                dataPoints.Add(new ChartDataPoint(argumentCount, structMethodCallTime, classMethodCallTime));
            }

            // Return the list of data

        }
    }
}