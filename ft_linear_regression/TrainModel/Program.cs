using System;
using System.IO;
using System.Collections.Generic;

class Train
{
	static double EstimationPrice(double mileage, double theta0, double theta1)
	{
		return theta0 + theta1 * mileage;
	}

	static void Main()
	{
		Console.WriteLine("Starting training...");
		List<double> mileage = new List<double>();
		List<double> price = new List<double>();
		if(!File.Exists("data.csv"))
		{
			Console.WriteLine("Error: data.csv not found");
			return;
		}
		string[] lines = File.ReadAllLines("data.csv");
		for (int i = 1; i < lines.Length; i++)
		{
			string[] parts = lines[i].Split(',');
			double km = double.Parse(parts[0]);
			double pr = double.Parse(parts[1]);
			mileage.Add(km);
			price.Add(pr);
		}
		Console.WriteLine($"Loaded {mileage.Count} data points");
		double theta0 = 0;
		double theta1 = 0;
		double learningRate = 0.00000001;
		int m = mileage.Count;
		double maxMileage = 0;
		for(int i = 0; i < mileage.Count; i++)
		{
			if(mileage[i] > maxMileage)
				maxMileage = mileage[i];
		}
		for (int i = 0; i < mileage.Count; i++)
			mileage[i] = mileage[i] / maxMileage;
		for(int iter = 0; iter < 10000; iter++)
		{
			double sum0 = 0;
			double sum1 = 0;
			for(int i = 0; i < m; i++)
			{
				double estimate = EstimationPrice(mileage[i], theta0, theta1);
				double error = estimate - price[i];
				sum0 += error;
				sum1 += error * mileage[i];
			}
			double tmpTheta0 = learningRate * (1.0 / m) * sum0;
			double tmpTheta1 = learningRate * (1.0 / m) * sum1;
			theta0 -= tmpTheta0;
			theta1 -= tmpTheta1;
			if(double.IsNaN(theta0) || double.IsNaN(theta1))
			{
				Console.WriteLine("NaN detected, stopping training");
				break;
			}
		}
		Console.WriteLine($"theta0 = {theta0}");
		Console.WriteLine($"theta1 = {theta1}");
		File.WriteAllText("thetas.txt", theta0 + "\n" + theta1);
		Console.WriteLine("Training complete. Thetas saved to thetas.txt");
	}
}
