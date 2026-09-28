using System;
using System.Collections.Generic;

namespace TraceTK.Diagnostics
{
    /// <summary>
    /// Calculates performance statistics from recently recorded frames.
    /// </summary>
    public class FrameStatistics
    {
        private readonly Queue<FrameTrace> _frames = new();
        private readonly int _windowSize;

        /// <summary>
        /// The average CPU frame time across the current sampling window.
        /// </summary>
        public TimeSpan AverageFrameTime;

        /// <summary>
        /// The average number of frames processed per second.
        /// </summary>
        public double FramesPerSecond;

        /// <summary>
        /// Creates a new frame statistics collector.
        /// </summary>
        /// <param name="windowSize">The maximum number of recent frames used to calculate statistics.</param>
        public FrameStatistics(int windowSize = 60)
        {
            if (windowSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(windowSize));
            }

            _windowSize = windowSize;
        }

        /// <summary>
        /// Adds a completed frame and updates the current statistics.
        /// </summary>
        /// <param name="frame">The completed frame to include in the statistics.</param>
        public void AddFrame(FrameTrace frame)
        {
            _frames.Enqueue(frame);

            if (_frames.Count > _windowSize)
            {
                _frames.Dequeue();
            }

            double totalMilliseconds = 0;

            foreach (var currentFrame in _frames)
            {
                totalMilliseconds += currentFrame.Elapsed.TotalMilliseconds;
            }

            double averageMilliseconds = totalMilliseconds / _frames.Count;

            AverageFrameTime = TimeSpan.FromMilliseconds(averageMilliseconds);

            if (AverageFrameTime.TotalSeconds > 0)
            {
                FramesPerSecond = 1.0 / AverageFrameTime.TotalSeconds;
            }
            else
            {
                FramesPerSecond = 0;
            }
        }
    }
}
