using System.Diagnostics;

namespace TraceTK.Diagnostics
{
    /// <summary>
    /// Measures CPU execution time for individual frames.
    /// </summary>
    public class FrameTracer
    {
        private readonly Stopwatch _stopwatch = new();
        private long _frameNumber;

        /// <summary>
        /// Begins tracing a new frame.
        /// </summary>
        public void BeginFrame()
        {
            _stopwatch.Reset();
            _stopwatch.Start();
        }

        /// <summary>
        /// Ends tracing the current frame and returns the collected frame information.
        /// </summary>
        /// <returns>A <see cref="FrameTrace"/> containing the frame number and elapsed CPU time.</returns>
        public FrameTrace EndFrame()
        {
            _stopwatch.Stop();

            var tracer = new FrameTrace();
            tracer.FrameNumber = _frameNumber;
            tracer.Elapsed = _stopwatch.Elapsed;

            _frameNumber++;

            return tracer;
        }
    }
}
