namespace RnCore.Abstractions;

public interface IDateTimeAbstraction
{
	DateTime UtcNow { get; }
	DateTime Now { get; }
}

public class DateTimeAbstraction : IDateTimeAbstraction
{
	public DateTime UtcNow => DateTime.UtcNow;
	public DateTime Now => DateTime.Now;
}
