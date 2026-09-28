using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using TraceTK.OpenGL;

namespace TraceTK.OpenGL.Sample
{
    /// <summary>
    /// TODO
    /// </summary>
    public class Program
    {
        /// <summary>
        /// TODO
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

            GlDebugLogger? logger = null;

            window.Load += () =>
            {
                logger = new GlDebugLogger();
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
