using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;

namespace TraceTK.OpenGL
{
    /// <summary>
    /// Captures OpenGL debug messages and writes them to the console and a log file.
    /// </summary>
    public class GlDebugLogger
    {
        /// <summary>
        /// 
        /// </summary>
        public enum MessageType
        {
            /// <summary>
            /// 
            /// </summary>
            Info,
            /// <summary>
            /// 
            /// </summary>
            Success,
            /// <summary>
            /// 
            /// </summary>
            Warning,
            /// <summary>
            /// 
            /// </summary>
            Error,
            /// <summary>
            /// 
            /// </summary>
            Debug
        }

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

            // FIX: Safely map OpenGL's DebugType to your custom MessageType enum
            MessageType mappedType = MapGlToCustomType(type);

            GlDebugMessage debugMessage = new GlDebugMessage(
                source,
                mappedType, // Pass the cleanly mapped type
                id,
                severity,
                messageText);

            Log(debugMessage, debugMessage.Type);
        }

        /// <summary>
        /// Writes an OpenGL debug message to the console and log file.
        /// </summary>
        /// <param name="message">The debug message to write.</param>
        /// <param name="type">Type of message</param>
        public void Log(GlDebugMessage message, MessageType type)
        {
            string output = $"[{message.Source}][{message.Severity}][{message.Type}][ID: {message.Id}] {message.Message}";

            // Color the console output
            Console.ForegroundColor = GetConsoleColor(type);
            Console.WriteLine(output);
            Console.ResetColor(); // Safely reset immediately after printing

            // Write to the file without console color styling artifacts
            File.AppendAllText(logPath, output + Environment.NewLine);
        }

        private ConsoleColor GetConsoleColor(MessageType type)
        {
            return type switch
            {
                MessageType.Info => ConsoleColor.Cyan,
                MessageType.Success => ConsoleColor.Green,
                MessageType.Warning => ConsoleColor.Yellow,
                MessageType.Error => ConsoleColor.Red,
                MessageType.Debug => ConsoleColor.DarkGray,
                _ => ConsoleColor.White
            };
        }

        // Helper method to safely translate OpenTK enums to your logger enums
        private static MessageType MapGlToCustomType(DebugType glType)
        {
            return glType switch
            {
                DebugType.DebugTypeError => MessageType.Error,
                DebugType.DebugTypeDeprecatedBehavior => MessageType.Warning,
                DebugType.DebugTypeUndefinedBehavior => MessageType.Warning,
                DebugType.DebugTypePerformance => MessageType.Warning,
                DebugType.DebugTypePortability => MessageType.Info,
                DebugType.DebugTypeOther => MessageType.Debug,
                _ => MessageType.Info
            };
        }
    }
}
