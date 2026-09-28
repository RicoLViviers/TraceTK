using TraceTK.Diagnostics;

namespace TraceTK.Tests;

/// <summary>
/// Contains tests for <see cref="FrameTracer"/>.
/// </summary>
public class FrameTracerTests
{
    /// <summary>
    /// Verifies that ending a frame records a positive elapsed time.
    /// </summary>
    [Fact]
    public void EndFrame_ReturnsElapsedTime()
    {
        var tracer = new FrameTracer();

        tracer.BeginFrame();
        Thread.Sleep(10);
        var frame = tracer.EndFrame();

        Assert.True(frame.Elapsed > TimeSpan.Zero);
    }

    /// <summary>
    /// Verifies that consecutive frames receive incrementing frame numbers.
    /// </summary>
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
