using System;
using System.IO;
using System.Collections.Generic;
using ScottPlot;
using System.Drawing;

class PlotData
{
    static double EstimatePrice(double mileage, double theta0, double theta1)
    {
        return theta0 + theta1 * mileage;
    }

    static void Main()
    {
        if(!File.Exists("data.csv"))
        {
            Console.WriteLine("Error: data.csv not found");
            return;
            
        }
        if (!File.Exists("thetas.txt"))
        {
            Console.WriteLine("Error: thetas.txt not found");
            return;
        }
        List<double> mileage = new List<double>();
        List<double> price = new List<double>();

        string[] lines = File.ReadAllLines("data.csv");

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');

            double km = double.Parse(parts[0]);
            double pr = double.Parse(parts[1]);

            mileage.Add(km);
            price.Add(pr);
        }
        double[] x = mileage.ToArray();
        double[] y = price.ToArray();

        string[] thetaLines = File.ReadAllLines("thetas.txt");

        double theta0 = double.Parse(thetaLines[0]);
        double theta1 = double.Parse(thetaLines[1]);

        double minX = x[0];
        double maxX = x[0];
        for (int i = 0; i < x.Length; i++)
        {
            if (x[i] < minX)
                minX = x[i];

            if (x[i] > maxX)
                maxX = x[i];
        }

        double[] lineX = {minX, maxX};
        double[] lineY =
        {
            EstimatePrice(minX, theta0, theta1),
            EstimatePrice(maxX, theta0, theta1)
        };

        var plt = new ScottPlot.Plot();

        var points = plt.Add.Scatter(x, y);
        points.LineWidth = 0;
        points.MarkerSize = 6;
        points.LegendText = "Dataset";
        var line = plt.Add.Scatter(lineX, lineY);
        line.LegendText = "Regression Line";
        plt.Title("Car Price Prediction");
        plt.XLabel("Mileage");
        plt.YLabel("Price");
        plt.ShowLegend(Alignment.LowerLeft);
        plt.SavePng("regression.png", 800, 600);

        Console.WriteLine("Graph saved as regression.png");
    }
}
