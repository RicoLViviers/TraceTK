using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;

namespace TraceTK.OpenGL
{
    /// <summary>
    /// Represents a debug message reported by OpenGL.
    /// </summary>
    public class GlDebugMessage
    {
        /// <summary>
        /// The source that generated the debug message.
        /// </summary>
        public DebugSource Source;

        /// <summary>
        /// The type of the debug message.
        /// </summary>
        public DebugType Type;

        /// <summary>
        /// The OpenGL-assigned identifier of the debug message.
        /// </summary>
        public int Id;

        /// <summary>
        /// The severity of the debug message.
        /// </summary>
        public DebugSeverity Severity;

        /// <summary>
        /// The human-readable debug message.
        /// </summary>
        public string Message;

        /// <summary>
        /// Creates an OpenGL debug message.
        /// </summary>
        /// <param name="source">The source that generated the message.</param>
        /// <param name="type">The type of the message.</param>
        /// <param name="id">The OpenGL-assigned message identifier.</param>
        /// <param name="severity">The severity of the message.</param>
        /// <param name="message">The human-readable debug message.</param>
        public GlDebugMessage(
            DebugSource source,
            DebugType type,
            int id,
            DebugSeverity severity,
            string message)
        {
            Source = source;
            Type = type;
            Id = id;
            Severity = severity;
            Message = message;
        }
    }
}
