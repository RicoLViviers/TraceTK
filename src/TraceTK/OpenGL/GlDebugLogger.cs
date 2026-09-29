using OpenTK.Graphics.OpenGL4;
using System;
using System.IO;
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

            GlDebugMessageType messageType = GetMessageType(type);

            GlDebugMessage debugMessage = new GlDebugMessage(
                source,
                messageType,
                id,
                severity,
                messageText);

            Log(debugMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Log(GlDebugMessage message)
        {
            string output =
                $"[{message.Source}][{message.Severity}][{message.Type}][ID: {message.Id}] {message.Message}";

            Console.ForegroundColor = GetConsoleColor(message.Type);
            Console.WriteLine(output);
            Console.ResetColor();

            File.AppendAllText(logPath, output + Environment.NewLine);
        }

        private static ConsoleColor GetConsoleColor(GlDebugMessageType type)
        {
            return type switch
            {
                GlDebugMessageType.Info => ConsoleColor.Cyan,
                GlDebugMessageType.Warning => ConsoleColor.Yellow,
                GlDebugMessageType.Error => ConsoleColor.Red,
                GlDebugMessageType.Debug => ConsoleColor.DarkGray,
                _ => ConsoleColor.White
            };
        }

        private static GlDebugMessageType GetMessageType(DebugType type)
        {
            return type switch
            {
                DebugType.DebugTypeError => GlDebugMessageType.Error,
                DebugType.DebugTypeDeprecatedBehavior => GlDebugMessageType.Warning,
                DebugType.DebugTypeUndefinedBehavior => GlDebugMessageType.Warning,
                DebugType.DebugTypePerformance => GlDebugMessageType.Warning,
                DebugType.DebugTypePortability => GlDebugMessageType.Info,
                DebugType.DebugTypeOther => GlDebugMessageType.Debug,
                _ => GlDebugMessageType.Info
            };
        }
    }
}
