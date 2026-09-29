using TraceTK.Diagnostics;

namespace TraceTK.Tests.Tracing;

public class FrameStatisticsTests
{
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

    [Fact]
    public void Constructor_RejectsNonPositiveWindowSize()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new FrameStatistics(0));
    }
}
