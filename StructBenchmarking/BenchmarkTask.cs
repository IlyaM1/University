using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;  // Add this line to include the StringBuilder class

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
    {
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            var stopwatch = new Stopwatch();
            double totalDuration = 0;

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
            return totalDuration - stopwatch.Elapsed.TotalMilliseconds;
        }
    }


    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        private class StringBuilderTask : ITask
        {
            public void Run()
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 10000; i++)
                {
                    sb.Append('a');
                }
                string s = sb.ToString();
            }
        }

        private class StringConstructorTask : ITask
        {
            public void Run()
            {
                string s = new string('a', 10000);
            }
        }

        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            // Choose the number of repetitions so that the total time for the test is around 1 second
            int repetitions = 1000;

            IBenchmark benchmark = new Benchmark();
            double stringBuilderTime = benchmark.MeasureDurationInMs(new StringBuilderTask(), repetitions);
            double stringConstructorTime = benchmark.MeasureDurationInMs(new StringConstructorTask(), repetitions);

            Assert.Less(stringConstructorTime, stringBuilderTime);
        }

        [Test]
        public void MeasureTimeExceptWarmingCall(int repetitionsCount, int delay)
        {
            IBenchmark benchmark = new Benchmark();
            double time = benchmark.MeasureDurationInMs(new DelayTask(delay), repetitionsCount);
            Assert.AreEqual(delay * repetitionsCount, time, delay / 2.0);
        }
    }

    public class DelayTask : ITask
    {
        private readonly int _delay;

        public DelayTask(int delay)
        {
            _delay = delay;
        }

        public void Run()
        {
            System.Threading.Thread.Sleep(_delay);
        }
    }
}
