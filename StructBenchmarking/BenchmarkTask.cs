using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Text;  // Add this line to include the StringBuilder class

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
    {
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            var stopwatch = new Stopwatch();
            var totalDuration = 0.0;

            // Add an extra "warming" call to the task
            task.Run();

            GC.Collect();                   // These two lines are needed to reduce the probability
            GC.WaitForPendingFinalizers();  // that the Garbage Collector will be called during
                                            // the measurements and affect them in some way.

            for (int i = 0; i < repetitionCount; i++)
            {
                stopwatch.Restart();
                task.Run();
                stopwatch.Stop();
                totalDuration += stopwatch.Elapsed.TotalMilliseconds;
            }

            // Subtract the time taken by the "warming" call to get the actual time taken by the task
            return totalDuration / repetitionCount;
        }
    }


    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        private class StringBuilderTask : ITask
        {
            public void Run()
            {
                var sb = new StringBuilder();
                for (int i = 0; i < 10000; i++)
                {
                    sb.Append('a');
                }
                var s = sb.ToString();
            }
        }

        private class StringConstructorTask : ITask
        {
            public void Run()
            {
                var s = new string('a', 10000);
            }
        }

        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            // Choose the number of repetitions so that the total time for the test is around 1 second
            var repetitions = 1000;

            var benchmark = new Benchmark();
            var stringBuilderTime = benchmark.MeasureDurationInMs(new StringBuilderTask(), repetitions);
            var stringConstructorTime = benchmark.MeasureDurationInMs(new StringConstructorTask(), repetitions);

            Assert.Less(stringConstructorTime, stringBuilderTime);
        }
    }
}