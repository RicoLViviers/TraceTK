using TraceTK.Diagnostics;

namespace TraceTK.Tests;
public class FrameTracerTests
{
    [Fact]
    public void EndFrame_ReturnsElapsedTime()
    {
        var tracer = new FrameTracer();

        tracer.BeginFrame();
        Thread.Sleep(10);
        var frame = tracer.EndFrame();

        Assert.True(frame.Elapsed > TimeSpan.Zero);
    }

    [Fact]
    public void EndFrame_IncrementsFrameNumber()
    {
        var tracer = new FrameTracer();

        tracer.BeginFrame();
        var firstFrame = tracer.EndFrame();

        tracer.BeginFrame();
        var secondFrame = tracer.EndFrame();

        Assert.Equal(0, firstFrame.FrameNumber);
        Assert.Equal(1, secondFrame.FrameNumber);
    }
}
