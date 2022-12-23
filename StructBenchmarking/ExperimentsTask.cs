using System;
using System.Collections.Generic;

namespace StructBenchmarking
{
    public static class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            foreach (var fieldCount in Constants.FieldCounts)
            {
                structuresTimes.Add(MeasureTaskDuration(
                    benchmark, repetitionsCount, fieldCount, typeof(StructArrayCreationTask)));

                classesTimes.Add(MeasureTaskDuration(
                    benchmark, repetitionsCount, fieldCount, typeof(ClassArrayCreationTask)));
            }

            return new ChartData
            {
                Title = "Create array",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            foreach (var fieldCount in Constants.FieldCounts)
            {
                structuresTimes.Add(MeasureTaskDuration(
                    benchmark, repetitionsCount, fieldCount, typeof(MethodCallWithStructArgumentTask)));

                classesTimes.Add(MeasureTaskDuration(
                    benchmark, repetitionsCount, fieldCount, typeof(MethodCallWithClassArgumentTask)));
            }

            return new ChartData
            {
                Title = "Call method with argument",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        private static ExperimentResult MeasureTaskDuration(
            IBenchmark benchmark, int repetitionsCount, int fieldCount, Type taskType)
        {
            var task = Activator.CreateInstance(taskType, fieldCount) as ITask;
            var duration = benchmark.MeasureDurationInMs(task, repetitionsCount);
            return new ExperimentResult(fieldCount, duration);
        }
    }
}