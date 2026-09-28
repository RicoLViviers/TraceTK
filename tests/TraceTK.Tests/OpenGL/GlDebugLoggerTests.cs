using OpenTK.Graphics.OpenGL4;
using TraceTK.OpenGL;

namespace TraceTK.Tests.OpenGL
{
    /// <summary>
    /// TODO
    /// </summary>
    public class GlDebugLoggerTests
    {
        /// <summary>
        /// TODO
        /// </summary>
        [Fact]
        public void LogWritesMessageToFile()
        {
            string directory = Path.Combine(
                Path.GetTempPath(),
                "TraceTK.Tests",
                Guid.NewGuid().ToString());

            string logPath = Path.Combine(directory, "tracetk.log");

            try
            {
                GlDebugLogger logger = new GlDebugLogger(logPath);

                GlDebugMessage message = new GlDebugMessage(
                    DebugSource.DebugSourceApi,
                    DebugType.DebugTypeError,
                    42,
                    DebugSeverity.DebugSeverityHigh,
                    "Test OpenGL debug message");

                logger.Log(message);

                Assert.True(File.Exists(logPath));

                string contents = File.ReadAllText(logPath);

                Assert.Contains("DebugSourceApi", contents);
                Assert.Contains("DebugSeverityHigh", contents);
                Assert.Contains("DebugTypeError", contents);
                Assert.Contains("42", contents);
                Assert.Contains("Test OpenGL debug message", contents);
            }
            finally
            {
                if (Directory.Exists(directory))
                {
                    Directory.Delete(directory, true);
                }
            }
        }

        /// <summary>
        /// TODO
        /// </summary>

        [Fact]
        public void LogAppendsMultipleMessages()
        {
            string directory = Path.Combine(
                Path.GetTempPath(),
                "TraceTK.Tests",
                Guid.NewGuid().ToString());

            string logPath = Path.Combine(directory, "tracetk.log");

            try
            {
                GlDebugLogger logger = new GlDebugLogger(logPath);

                GlDebugMessage first = new GlDebugMessage(
                    DebugSource.DebugSourceApi,
                    DebugType.DebugTypeError,
                    1,
                    DebugSeverity.DebugSeverityHigh,
                    "First message");

                GlDebugMessage second = new GlDebugMessage(
                    DebugSource.DebugSourceApi,
                    DebugType.DebugTypePerformance,
                    2,
                    DebugSeverity.DebugSeverityLow,
                    "Second message");

                logger.Log(first);
                logger.Log(second);

                string contents = File.ReadAllText(logPath);

                Assert.Contains("First message", contents);
                Assert.Contains("Second message", contents);
            }
            finally
            {
                if (Directory.Exists(directory))
                {
                    Directory.Delete(directory, true);
                }
            }
        }
    }
}
