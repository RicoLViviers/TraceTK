using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;

namespace TraceTK.OpenGL
{
    /// <summary>
    /// Captures OpenGL debug messages and writes them to the console and a log file.
    /// </summary>
    public class GlDebugLogger
    {
        private readonly string logPath;

        private readonly DebugProc debugCallback;

        /// <summary>
        /// Creates an OpenGL debug logger.
        /// </summary>
        /// <param name="logPath">The path of the file where debug messages are written.</param>
        public GlDebugLogger(string logPath = "logs/tracetk.log")
        {
            this.logPath = logPath;

            string? directory = Path.GetDirectoryName(logPath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            debugCallback = OnDebugMessage;
        }

        /// <summary>
        /// Enables OpenGL debug output and registers the debug message callback.
        /// </summary>
        /// <remarks>
        /// A valid OpenGL context must be current before this method is called.
        /// </remarks>
        public void Start()
        {
            GL.Enable(EnableCap.DebugOutput);
            GL.Enable(EnableCap.DebugOutputSynchronous);

            GL.DebugMessageCallback(debugCallback, IntPtr.Zero);
        }

        private void OnDebugMessage(
            DebugSource source,
            DebugType type,
            int id,
            DebugSeverity severity,
            int length,
            IntPtr message,
            IntPtr userParam)
        {
            string? messageText = Marshal.PtrToStringAnsi(message, length);

            if (messageText is null)
            {
                return;
            }

            GlDebugMessage debugMessage = new GlDebugMessage(
                source,
                type,
                id,
                severity,
                messageText);

            Log(debugMessage);
        }

        /// <summary>
        /// Writes an OpenGL debug message to the console and log file.
        /// </summary>
        /// <param name="message">The debug message to write.</param>
        public void Log(GlDebugMessage message)
        {
            string output = $"{message.Source} {message.Severity} {message.Type} {message.Id} {message.Message}";
            Console.WriteLine(output);

            File.AppendAllText(logPath, output + Environment.NewLine);
        }
    }
}
