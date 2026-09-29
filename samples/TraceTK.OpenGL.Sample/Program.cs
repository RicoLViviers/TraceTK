using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace TraceTK.OpenGL.Sample
{
    /// <summary>
    /// Demonstrates OpenGL debug logging with TraceTK.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Runs the TraceTK OpenGL sample.
        /// </summary>
        public static void Main()
        {
            GameWindowSettings gameWindowSettings = GameWindowSettings.Default;

            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings
            {
                ClientSize = new Vector2i(800, 600),
                Title = "TraceTK OpenGL Sample"
            };

            using GameWindow window = new GameWindow(
                gameWindowSettings,
                nativeWindowSettings);

            window.Load += () =>
            {
                GlDebugLogger logger = new GlDebugLogger();
                logger.Start();

                GL.DebugMessageInsert(
                    DebugSourceExternal.DebugSourceApplication,
                    DebugType.DebugTypeMarker,
                    1,
                    DebugSeverity.DebugSeverityNotification,
                    -1,
                    "Hello from TraceTK");
            };

            window.Run();
        }
    }
}
