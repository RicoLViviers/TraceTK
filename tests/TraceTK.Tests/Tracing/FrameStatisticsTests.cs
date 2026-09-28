using TraceTK.Diagnostics;

namespace TraceTK.Tests.Tracing;

/// <summary>
/// Contains tests for <see cref="FrameStatistics"/>.
/// </summary>
public class FrameStatisticsTests
{
    /// <summary>
    /// Verifies that the average frame time is calculated from recorded frames.
    /// </summary>
    [Fact]
    public void AddFrame_CalculatesAverageFrameTime()
    {
        var statistics = new FrameStatistics();

        statistics.AddFrame(new FrameTrace
        {
            Elapsed = TimeSpan.FromMilliseconds(10)
        });

        statistics.AddFrame(new FrameTrace
        {
            Elapsed = TimeSpan.FromMilliseconds(20)
        });

        Assert.Equal(15, statistics.AverageFrameTime.TotalMilliseconds);
    }

    /// <summary>
    /// Verifies that frames per second is calculated from the average frame time.
    /// </summary>
    [Fact]
    public void AddFrame_CalculatesFramesPerSecond()
    {
        var statistics = new FrameStatistics();

        statistics.AddFrame(new FrameTrace
        {
            Elapsed = TimeSpan.FromMilliseconds(20)
        });

        Assert.Equal(50, statistics.FramesPerSecond, 5);
    }

    /// <summary>
    /// Verifies that frames older than the sampling window are excluded from statistics.
    /// </summary>
    [Fact]
    public void AddFrame_ExcludesFramesOutsideWindow()
    {
        var statistics = new FrameStatistics(2);

        statistics.AddFrame(new FrameTrace
        {
            Elapsed = TimeSpan.FromMilliseconds(100)
        });

        statistics.AddFrame(new FrameTrace
        {
            Elapsed = TimeSpan.FromMilliseconds(20)
        });

        statistics.AddFrame(new FrameTrace
        {
            Elapsed = TimeSpan.FromMilliseconds(10)
        });

        Assert.Equal(15, statistics.AverageFrameTime.TotalMilliseconds);
    }

    /// <summary>
    /// Verifies that a non-positive sampling window is rejected.
    /// </summary>
    [Fact]
    public void Constructor_RejectsNonPositiveWindowSize()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new FrameStatistics(0));
    }
}
