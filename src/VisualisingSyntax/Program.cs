namespace Examples;

internal class Program
{
	private static void Main()
	{
		var data = new Data { Id = 1 };
		Console.WriteLine(data.Id);
	}
}

public class Data
{
	public required int Id { get; init; }
}
