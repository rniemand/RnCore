namespace RnCore.Abstractions;

public interface IDateTimeOffsetAbstraction
{
	DateTimeOffset Now { get; }
}

public class DateTimeOffsetAbstraction : IDateTimeOffsetAbstraction
{
	public DateTimeOffset Now => DateTimeOffset.Now;
}
