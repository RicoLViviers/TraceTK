using OpenTK.Graphics.OpenGL4;
using TraceTK.OpenGL;
using static TraceTK.OpenGL.GlDebugLogger;

namespace TraceTK.Tests.OpenGL
{
    public class GlDebugMessageTests
    {
        [Fact]
        public void ConstructorStoresDebugMessageData()
        {
            DebugSource source = DebugSource.DebugSourceApi;
            DebugType type = DebugType.DebugTypeError;
            int id = 42;
            DebugSeverity severity = DebugSeverity.DebugSeverityHigh;
            string message = "Test OpenGL debug message";

            GlDebugMessage debugMessage = new GlDebugMessage(
                source,
                (MessageType)type,
                id,
                severity,
                message);

            Assert.Equal(source, debugMessage.Source);
            Assert.Equal((MessageType)type, debugMessage.Type);
            Assert.Equal(id, debugMessage.Id);
            Assert.Equal(severity, debugMessage.Severity);
            Assert.Equal(message, debugMessage.Message);
        }
    }
}
