using System.Reactive;

namespace ModelLib;

public enum PhoneCallStatus
{
    Failed,
    Missed,
    Declined,
    Accepted
}

public class PhoneCall
{
    private PhoneCallStatus _status;
    private DateTime _start;
    private DateTime _end;
    private DateTime _acceptedAt;
    private PhoneNumber _caller;
    private PhoneNumber _callee;

    private PhoneCall(PhoneCallStatus status, DateTime start, DateTime end, DateTime acceptedAt, PhoneNumber caller, PhoneNumber callee)
    {
        _status = status;
        _start = start;
        _end = end;
        _acceptedAt = acceptedAt;
        _caller = caller;
        _callee = callee;
    }

    public static PhoneCall CreateFailed(DateTime start, PhoneNumber caller, PhoneNumber callee)
    {
        if (caller.ToString() != callee.ToString())
        {
            return new PhoneCall(PhoneCallStatus.Failed, start, start, DateTime.MinValue, caller, callee);
        }
        else
        {
            throw new ArgumentException("Invalid argument");
        }
    }

    public static PhoneCall CreateMissed(DateTime start, DateTime end, PhoneNumber caller, PhoneNumber callee)
    {
        if (caller.ToString() != callee.ToString() && end > start)
        {
            return new PhoneCall(PhoneCallStatus.Missed, start, end, DateTime.MinValue, caller, callee);
        }
        else
        {
            throw new ArgumentException("Invalid argument");
        }
    }

    public static PhoneCall CreateDeclined(DateTime start, DateTime end, PhoneNumber caller, PhoneNumber callee)
    {
        if (caller.ToString() != callee.ToString() && end > start)
        {
            return new PhoneCall(PhoneCallStatus.Declined, start, end, DateTime.MinValue, caller, callee);
        }
        else
        {
            throw new ArgumentException("Invalid argument");
        }
    }

    public static PhoneCall CreateAccepted(DateTime start, DateTime acceptedAt, DateTime end, PhoneNumber caller, PhoneNumber callee)
    {
        if (caller.ToString() != callee.ToString() && end > acceptedAt && acceptedAt > start)
        {
            return new PhoneCall(PhoneCallStatus.Accepted, start, end, acceptedAt, caller, callee);
        }
        else
        {
            throw new ArgumentException("Invalid argument");
        }
    }

    public PhoneCallStatus Status()
    {
        return _status;
    }

    public DateTime Start()
    {
        return _start;
    }

    public DateTime End()
    {
        return _end;
    }

    public TimeInterval<DateTime> DialUpDuration()
    {
        switch (_status)
        {
            case PhoneCallStatus.Failed:
                return new TimeInterval<DateTime>(_start, TimeSpan.Zero);
            case PhoneCallStatus.Accepted:
                return new TimeInterval<DateTime>(_start, _acceptedAt - _start);
            default:
                return new TimeInterval<DateTime>(_start, _end - _start);
        }
    }

    public TimeInterval<DateTime> TalkDuration()
    {
        switch (_status)
        {
            case PhoneCallStatus.Accepted:
                return new TimeInterval<DateTime>(_start, _end - _acceptedAt);
            default:
                return new TimeInterval<DateTime>(_start, TimeSpan.Zero);
        }
    }

    public TimeInterval<DateTime> TotalDuration()
    {
        switch(_status)
        {
            case PhoneCallStatus.Failed:
                return new TimeInterval<DateTime>(_start, TimeSpan.Zero);
            default:
                return new TimeInterval<DateTime>(_start, _end - _start);
        }
    }

    public PhoneNumber Caller()
    {
        return _caller;
    }

    public PhoneNumber Callee()
    {
        return _callee;
    }
}