using System;
using System.IO;
using System.Collections.Generic;

class Predict
{
	static double EstimatePrice(double mileage, double theta0, double theta1)
	{
		return theta0 + theta1 * mileage;
	}
	static void Main()
	{
		if(!File.Exists("thetas.txt"))
		{
			Console.WriteLine("No trained model found. Please run training first");
			return;
		}
		string[] lines = File.ReadAllLines("thetas.txt");
		double theta0 = double.Parse(lines[0]);
		double theta1 = double.Parse(lines[1]);
		Console.WriteLine("Enter a car mileage: ");
		string input = Console.ReadLine();
		if (!double.TryParse(input, out double mileage))
        {
            Console.WriteLine("Invalid mileage input");
            return;
        }
		double price = EstimatePrice(mileage, theta0, theta1);
		Console.WriteLine($"Estimated price: {price}");
	}
}
