namespace TraceTK.OpenGL
{
    /// <summary>
    /// Represents the type of an OpenGL debug message.
    /// </summary>
    public enum GlDebugMessageType
    {
        /// <summary>
        /// Indicates an informational debug message.
        /// </summary>
        Info,

        /// <summary>
        /// Indicates a warning about potentially problematic behavior.
        /// </summary>
        Warning,

        /// <summary>
        /// Indicates an OpenGL error.
        /// </summary>
        Error,

        /// <summary>
        /// Indicates a general diagnostic debug message.
        /// </summary>
        Debug
    }
}
