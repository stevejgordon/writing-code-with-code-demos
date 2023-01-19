namespace Examples;

internal class Program
{
	private static void Main()
	{
		var data = new Data { Id = 1 };
		Console.WriteLine(data.Id); // Write to console.
	} 
}

public class Data
{
	/// <summary>
	/// The ID.
	/// </summary>
	public required int Id { get; init; }
}
