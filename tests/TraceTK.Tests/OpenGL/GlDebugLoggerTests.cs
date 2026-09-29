using OpenTK.Graphics.OpenGL4;
using TraceTK.OpenGL;

namespace TraceTK.Tests.OpenGL
{
    public class GlDebugLoggerTests
    {
        [Fact]
        public void LogWritesMessageToFile()
        {
            string directory = CreateTemporaryDirectory();
            string logPath = Path.Combine(directory, "tracetk.log");

            try
            {
                GlDebugLogger logger = new GlDebugLogger(logPath);

                GlDebugMessage message = new GlDebugMessage(
                    DebugSource.DebugSourceApi,
                    GlDebugMessageType.Error,
                    42,
                    DebugSeverity.DebugSeverityHigh,
                    "Test OpenGL debug message");

                logger.Log(message);

                Assert.True(File.Exists(logPath));

                string contents = File.ReadAllText(logPath);

                Assert.Contains("DebugSourceApi", contents);
                Assert.Contains("DebugSeverityHigh", contents);
                Assert.Contains("Error", contents);
                Assert.Contains("42", contents);
                Assert.Contains("Test OpenGL debug message", contents);
            }
            finally
            {
                DeleteDirectory(directory);
            }
        }

        [Fact]
        public void LogAppendsMultipleMessages()
        {
            string directory = CreateTemporaryDirectory();
            string logPath = Path.Combine(directory, "tracetk.log");

            try
            {
                GlDebugLogger logger = new GlDebugLogger(logPath);

                GlDebugMessage first = new GlDebugMessage(
                    DebugSource.DebugSourceApi,
                    GlDebugMessageType.Error,
                    1,
                    DebugSeverity.DebugSeverityHigh,
                    "First message");

                GlDebugMessage second = new GlDebugMessage(
                    DebugSource.DebugSourceApi,
                    GlDebugMessageType.Info,
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
                DeleteDirectory(directory);
            }
        }

        private static string CreateTemporaryDirectory()
        {
            return Path.Combine(
                Path.GetTempPath(),
                "TraceTK.Tests",
                Guid.NewGuid().ToString());
        }

        private static void DeleteDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }
        }
    }
}
