using System;
using System.IO;

class Program
{
    static double EstimatePrice(double mileage, double theta0, double theta1)
    {
        return theta0 + theta1 * mileage;
    }
    static void Main()
    {
        if (!File.Exists("data.csv") || !File.Exists("thetas.txt"))
        {
            Console.WriteLine("Missing data.csv or thetas.txt");
            return;
        }

        string[] thetaLines = File.ReadAllLines("thetas.txt");

        double theta0 = double.Parse(thetaLines[0]);
        double theta1 = double.Parse(thetaLines[1]);

        string[] lines = File.ReadAllLines("data.csv");

        double mse = 0;
        int count = 0;

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');

            double mileage = double.Parse(parts[0]);
            double price = double.Parse(parts[1]);

            double predicted = EstimatePrice(mileage, theta0, theta1);

            double error = predicted - price;

            mse += error * error;
            count++;
        }

        mse /= count;

        Console.WriteLine($"Mean Squared Error (MSE): {mse}");
    }
}
