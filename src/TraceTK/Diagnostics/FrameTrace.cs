using System;

namespace TraceTK.Diagnostics
{
    /// <summary>
    /// Represents diagnostic information collected for a single frame.
    /// </summary>
    public class FrameTrace
    {
        /// <summary>
        /// The sequential number of the frame.
        /// </summary>
        public long FrameNumber;

        /// <summary>
        /// The amount of CPU time elapsed during the frame.
        /// </summary>
        public TimeSpan Elapsed;
    }
}
