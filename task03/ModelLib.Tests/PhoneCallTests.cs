namespace ModelLib.Tests;
public class PhoneCallTests
{
    [Fact]
    public void Failed_call()
    {
        DateTime start = new DateTime(2025, 1, 15, 10, 0, 0);
        PhoneNumber caller = new PhoneNumber("+79161234567");
        PhoneNumber callee = new PhoneNumber("+79169876543");
        PhoneCall phoneCall = PhoneCall.CreateFailed(start, caller, callee);

        Assert.Equal(phoneCall.Status(), PhoneCallStatus.Failed);
        Assert.Equal(phoneCall.Start(), start);
        Assert.Equal(phoneCall.DialUpDuration().Interval, TimeSpan.Zero);
        Assert.Equal(phoneCall.TalkDuration().Interval, TimeSpan.Zero);
        Assert.Equal(phoneCall.TotalDuration().Interval, TimeSpan.Zero);
        Assert.Equal(phoneCall.End(), start);
        Assert.Equal(phoneCall.Caller(), caller);
        Assert.Equal(phoneCall.Callee(), callee);
    }

    [Fact]
    public void Missed_and_declined_calls()
    {
        DateTime start = new DateTime(2025, 1, 15, 10, 0, 0);
        DateTime end = new DateTime(2025, 1, 15, 10, 0, 30);
        PhoneNumber caller = new PhoneNumber("+79161234567");
        PhoneNumber callee = new PhoneNumber("+79169876543");
        PhoneCall phoneCall = PhoneCall.CreateMissed(start, end, caller, callee);
        TimeSpan totalDuration = new TimeSpan(0, 0, 30);

        Assert.Equal(phoneCall.Status(), PhoneCallStatus.Missed);
        Assert.Equal(phoneCall.Start(), start);
        Assert.Equal(phoneCall.DialUpDuration().Interval, totalDuration);
        Assert.Equal(phoneCall.TalkDuration().Interval, TimeSpan.Zero);
        Assert.Equal(phoneCall.TotalDuration().Interval, totalDuration);
        Assert.Equal(phoneCall.End(), end);
        Assert.Equal(phoneCall.Caller(), caller);
        Assert.Equal(phoneCall.Callee(), callee);
    }

    [Fact]
    public void Declined_call()
    {
        DateTime start = new DateTime(2025, 1, 15, 10, 0, 0);
        DateTime end = new DateTime(2025, 1, 15, 10, 0, 30);
        PhoneNumber caller = new PhoneNumber("+79161234567");
        PhoneNumber callee = new PhoneNumber("+79169876543");
        PhoneCall phoneCall = PhoneCall.CreateDeclined(start, end, caller, callee);
        TimeSpan totalDuration = new TimeSpan(0, 0, 30);

        Assert.Equal(phoneCall.Status(), PhoneCallStatus.Declined);
        Assert.Equal(phoneCall.Start(), start);
        Assert.Equal(phoneCall.DialUpDuration().Interval, totalDuration);
        Assert.Equal(phoneCall.TalkDuration().Interval, TimeSpan.Zero);
        Assert.Equal(phoneCall.TotalDuration().Interval, totalDuration);
        Assert.Equal(phoneCall.End(), end);
        Assert.Equal(phoneCall.Caller(), caller);
        Assert.Equal(phoneCall.Callee(), callee);
    }

    [Fact]
    public void Accepted_call()
    {
        DateTime start = new DateTime(2025, 1, 15, 10, 0, 0);
        DateTime acceptedAt = new DateTime(2025, 1, 15, 10, 0, 10);
        DateTime end = new DateTime(2025, 1, 15, 10, 2, 10);
        PhoneNumber caller = new PhoneNumber("+79161234567");
        PhoneNumber callee = new PhoneNumber("+79169876543");
        PhoneCall phoneCall = PhoneCall.CreateAccepted(start, acceptedAt, end, caller, callee);
        TimeSpan talkDuration = new TimeSpan(0, 2, 0);
        TimeSpan dialUpDuration = new TimeSpan(0, 0, 10);
        TimeSpan totalDuration = new TimeSpan(0, 2, 10);

        Assert.Equal(phoneCall.Status(), PhoneCallStatus.Accepted);
        Assert.Equal(phoneCall.Start(), start);
        Assert.Equal(phoneCall.DialUpDuration().Interval, dialUpDuration);
        Assert.Equal(phoneCall.TalkDuration().Interval, talkDuration);
        Assert.Equal(phoneCall.TotalDuration().Interval, totalDuration);
        Assert.Equal(phoneCall.DialUpDuration().Interval + phoneCall.TalkDuration().Interval, totalDuration);
        Assert.Equal(phoneCall.TotalDuration().Interval, end - start);
        Assert.Equal(phoneCall.End(), end);
        Assert.Equal(phoneCall.Caller(), caller);
        Assert.Equal(phoneCall.Callee(), callee);
    }

    [Fact]
    public void The_caller_cannot_call_himself()
    {
        PhoneNumber sameNumber = new PhoneNumber("+79161234567");
        DateTime start = new DateTime(2025, 1, 15, 10, 0, 0);

        Assert.Throws<ArgumentException>(() => PhoneCall.CreateFailed(start, sameNumber, sameNumber));
    }

    [Fact]
    public void Start_greater_than_end()
    {
        PhoneNumber caller = new PhoneNumber("+79161234567");
        PhoneNumber callee = new PhoneNumber("+79169876543");
        DateTime start = new DateTime(2025, 1, 15, 10, 0, 0);

        Assert.Throws<ArgumentException>(() => PhoneCall.CreateMissed(start, start.AddSeconds(-10), caller, callee));
    }

    [Fact]
    public void Start_equal_end()
    {
        PhoneNumber caller = new PhoneNumber("+79161234567");
        PhoneNumber callee = new PhoneNumber("+79169876543");
        DateTime start = new DateTime(2025, 1, 15, 10, 0, 0);

        Assert.Throws<ArgumentException>(() => PhoneCall.CreateMissed(start, start, caller, callee));
    }

    [Fact]
    public void Start_greater_than_acception_time()
    {
        PhoneNumber caller = new PhoneNumber("+79161234567");
        PhoneNumber callee = new PhoneNumber("+79169876543");
        DateTime start = new DateTime(2025, 1, 15, 10, 0, 0);
        DateTime acceptedAt = start.AddSeconds(-10);
        DateTime end = new DateTime(2025, 1, 15, 10, 2, 10);

        Assert.Throws<ArgumentException>(() => PhoneCall.CreateAccepted(start, acceptedAt, end, caller, callee));
    }

    [Fact]
    public void Minimal_dial_up_duration()
    {
        PhoneNumber caller = new PhoneNumber("+79161234567");
        PhoneNumber callee = new PhoneNumber("+79169876543");
        DateTime start = new DateTime(2025, 1, 15, 10, 0, 0);
        DateTime end = new DateTime(2025, 1, 15, 10, 0, 1);
        PhoneCall phoneCall = PhoneCall.CreateMissed(start, end, caller, callee);
        Assert.Equal(phoneCall.DialUpDuration().Interval, new TimeSpan(0, 0, 1));
    }

    [Fact]
    public void Minimal_talk_duration()
    {
        PhoneNumber caller = new PhoneNumber("+79161234567");
        PhoneNumber callee = new PhoneNumber("+79169876543");
        DateTime start = new DateTime(2025, 1, 15, 10, 0, 0);
        DateTime acceptedAt = new DateTime(2025, 1, 15, 10, 0, 1);
        DateTime end = new DateTime(2025, 1, 15, 10, 0, 2);
        PhoneCall phoneCall = PhoneCall.CreateAccepted(start, acceptedAt, end, caller, callee);
        Assert.Equal(phoneCall.TalkDuration().Interval, new TimeSpan(0, 0, 1));
    }

    [Fact]
    public void Minimal_total_duration()
    {
        PhoneNumber caller = new PhoneNumber("+79161234567");
        PhoneNumber callee = new PhoneNumber("+79169876543");
        DateTime start = new DateTime(2025, 1, 15, 10, 0, 0);
        DateTime end = new DateTime(2025, 1, 15, 10, 0, 1);
        PhoneCall phoneCall = PhoneCall.CreateMissed(start, end, caller, callee);
        Assert.Equal(phoneCall.TotalDuration().Interval, new TimeSpan(0, 0, 1));
    }
}