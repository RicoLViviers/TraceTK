using OpenTK.Graphics.OpenGL4;

namespace TraceTK.OpenGL
{
    /// <summary>
    /// Represents a debug message reported by OpenGL.
    /// </summary>
    public class GlDebugMessage
    {
        /// <summary>
        /// The source that generated the message.
        /// </summary>
        public DebugSource Source;

        /// <summary>
        /// The type of the message.
        /// </summary>
        public GlDebugMessageType Type;

        /// <summary>
        /// The OpenGL-assigned identifier of the message.
        /// </summary>
        public int Id;

        /// <summary>
        /// The severity of the message.
        /// </summary>
        public DebugSeverity Severity;

        /// <summary>
        /// The message reported by OpenGL.
        /// </summary>
        public string Message;

        /// <summary>
        /// Creates an OpenGL debug message.
        /// </summary>
        /// <param name="source">The source that generated the message.</param>
        /// <param name="type">The type of the message.</param>
        /// <param name="id">The OpenGL-assigned message identifier.</param>
        /// <param name="severity">The severity of the message.</param>
        /// <param name="message">The message reported by OpenGL.</param>
        public GlDebugMessage(
            DebugSource source,
            GlDebugMessageType type,
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
