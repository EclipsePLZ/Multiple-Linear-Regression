﻿using System;
using System.Collections.Generic;
using System.Linq;


namespace Multiple_Linear_Regression {
    public static class Statistics {
        /// <summary>
        /// Get reliabity interval for vibration signal
        /// </summary>
        /// <param name="values">List of values of vibration signal</param>
        /// <param name="numberOfValuesForNormalLevel">Number of values that will be used to calc the maximum level of normal work</param>
        /// <param name="numberOfStds">Number of standard deviations that will be used to calc the maximum level of normal work</param>
        /// <returns>Reliability Interval that contains 100 values (each value is 1 percent of relilability)</returns>
        public static List<double> GetReliabilityInterval(List<double> values, int numberOfValuesForNormalLevel, double numberOfStds) {
            // List that contains reliability sections
            List<double> reliabilityInterval = new List<double>();

            // List of values for calculating max values for normal work
            List<double> valuesForNormalLevel = values.GetRange(0, numberOfValuesForNormalLevel);

            double maxValue = values.Max();

            // Calc value of max level for normal work
            double maxNormalLevel = GetMaxNormalVibrSignal(valuesForNormalLevel, numberOfStds);

            // If max value of signal for normal work is more than max signal return
            // list of values that equals to max value
            if (maxNormalLevel >= maxValue) {
                return Enumerable.Repeat(maxValue, 100).ToList();
            }

            double oneDivision = (maxValue - maxNormalLevel) / 99;

            // Add values to reliability interval
            reliabilityInterval.Add(maxNormalLevel);
            for (int i = 1; i < 99; i++) {
                reliabilityInterval.Add(maxNormalLevel + i * oneDivision);
            }
            reliabilityInterval.Add(maxValue);

            return reliabilityInterval;
        }

        /// <summary>
        /// Calc value of max level for normal work
        /// </summary>
        /// <param name="values">Values of vibration signal</param>
        /// <param name="stdCount">Number of stds for calc max level for normal work</param>
        /// <returns>Value of max normal level for normal work</returns>
        private static double GetMaxNormalVibrSignal(List<double> values, double stdCount) {
            return values.Average() + stdCount * StandardDeviation(values);
        }

        /// <summary>
        /// Get standard deviation
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Value of standard deviation</returns>
        public static double StandardDeviation(IEnumerable<double> values) {
            double avg = values.Average();
            return Math.Sqrt(values.Average(v => Math.Pow(v - avg, 2)));
        }

        /// <summary>
        /// Get Pearson correlation coefficient
        /// </summary>
        /// <param name="values1">List of values</param>
        /// <param name="values2">List of values</param>
        /// <returns>Value of correlation coefficient</returns>
        public static double PearsonCorrelationCoefficient(IEnumerable<double> values1, IEnumerable<double> values2) {
            if (values1.Count() != values2.Count()) {
                throw new ArgumentException("Values must be the same length");
            }

            // Find average values of two lists
            double avg1 = values1.Average();
            double avg2 = values2.Average();

            // Calc sum in the numerator
            double sum1 = values1.Zip(values2, (x1, y1) => (x1 - avg1) * (y1 - avg2)).Sum();

            // Calc sums in the denominator
            double sumSqr1 = values1.Sum(x => Math.Pow((x - avg1), 2.0));
            double sumSqr2 = values2.Sum(y => Math.Pow((y - avg2), 2.0));

            return sum1 / Math.Sqrt(sumSqr1 * sumSqr2);
        }

        /// <summary>
        /// Convert list of values to interval [a, b]
        /// </summary>
        /// <param name="a">Min value of the interval</param>
        /// <param name="b">Max value of the interval</param>
        /// <param name="values">List of values</param>
        /// <returns>List of values in interval [a, b]</returns>
        public static List<double> ConvertValuesToInterval(double a, double b, IEnumerable<double> values) {
            List<double> result = new List<double>();
            double minValue = values.Min();
            double maxValue = values.Max();

            foreach(var value in values) {
                result.Add(((value - minValue) / (maxValue - minValue)) * (b - a) + a);
            }

            return result;
        }

        /// <summary>
        /// Find t-statistics with k degrees of freedom
        /// </summary>
        /// <param name="k">Degrees of freedom</param>
        /// <param name="corr">Pearson correlation coefficient</param>
        /// <returns>Value of t-statistics</returns>
        public static double T_Statistics(int k, double corr) {
            return Math.Abs(corr) * Math.Sqrt(k / (1.0 - Math.Pow(Math.Abs(corr), 2)));
        }

        /// <summary>
        /// Get adjusted coefficietn of determination
        /// </summary>
        /// <param name="realValues">Real values</param>
        /// <param name="predictedValues">Predicted values</param>
        /// <param name="k">Number of model parameters</param>
        /// <returns>Adjusted coefficient of determination</returns>
        public static double AdjustedDetermCoefficient(IEnumerable<double> realValues, IEnumerable<double> predictedValues, int k) {
            if (realValues.Count() != predictedValues.Count()) {
                throw new Exception("Values must be the same length");
            }

            int n = realValues.Count();
            double avgReal = realValues.Average();
            double rss = 0.0;
            double tss = realValues.Sum(v => Math.Pow(v - avgReal, 2));

            for (int i = 0; i < realValues.Count(); i++) {
                rss += Math.Pow(realValues.ElementAt(i) - predictedValues.ElementAt(i), 2);
            }

            return 1 - ((rss / (n - k)) / (tss / (n - 1)));
        }

        /// <summary>
        /// Get coefficietn of determination
        /// </summary>
        /// <param name="realValues">Real values</param>
        /// <param name="predictedValues">Predicted values</param>
        /// <returns>Coefficient of determination</returns>
        public static double DetermCoefficient(IEnumerable<double> realValues, IEnumerable<double> predictedValues) {
            if (realValues.Count() != predictedValues.Count()) {
                throw new Exception("Values must be the same length");
            }
            
            double avgReal = realValues.Average();
            double sst = realValues.Sum(v => Math.Pow(v - avgReal, 2));
            double sse = 0.0;

            for (int i = 0; i < realValues.Count(); i++) {
                sse += Math.Pow(realValues.ElementAt(i) - predictedValues.ElementAt(i), 2);
            }

            return 1 - (sse / sst);
        }

        /// <summary>
        /// Get symbiosis of auto empirical and theoretical definition areas
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="percentExpansion">Percent of area expansion</param>
        /// <returns>Definition area (min, max)</returns>
        public static (double, double) AutoSymbiosisDefinitionArea(IEnumerable<double> values, double percentExpansion = 10) {
            (double, double) theoreticalDefArea = TheoreticalDefinitionArea(values);
            (double, double) empDefArea = AutoEmpiricalDefinitionArea(values, percentExpansion);

            return FindExtremeAreas(theoreticalDefArea, empDefArea);
        }

        /// <summary>
        /// Get symbiosis of equal empirical and theoretical definition areas
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="percentExpansion">Percent of area expansion</param>
        /// <returns>Definition area (min, max)</returns>
        public static (double, double) EqualSymbiosisDefinitionArea(IEnumerable<double> values, double percentExpansion = 10) {
            (double, double) theoreticalDefArea = TheoreticalDefinitionArea(values);
            (double, double) empDefArea = EqualEmpiricalDefinitionArea(values, percentExpansion);

            return FindExtremeAreas(theoreticalDefArea, empDefArea);
        }

        /// <summary>
        /// Find definition area as extreme area from two different areas
        /// </summary>
        /// <param name="defArea1">First definition area</param>
        /// <param name="defArea2">Second definition area</param>
        /// <returns>Extreme definition area (min, max)</returns>
        private static (double, double) FindExtremeAreas((double, double) defArea1, (double, double) defArea2) {
            // Find minimum of minimums
            double newMin = defArea1.Item1 < defArea2.Item1 ? defArea1.Item1 : defArea2.Item1;

            // Find maximum of maximums
            double newMax = defArea1.Item2 > defArea2.Item2 ? defArea1.Item2 : defArea2.Item2;

            return (newMin, newMax);
        }

        /// <summary>
        /// Get theoretical definition area
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Definition area (min, max)</returns>
        public static (double, double) TheoreticalDefinitionArea(IEnumerable<double> values) {
            double avgValue = values.Average();
            double stdValue = StandardDeviation(values);

            double newMin = avgValue - 3 * stdValue;
            double newMax = avgValue + 3 * stdValue;

            if (values.Min() >= 0 && newMin < 0) {
                newMin = 0;
            }

            return (newMin, newMax);
        }

        /// <summary>
        /// Get auto definition area by empirical way
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="percentExpansion">Percent of area expansion</param>
        /// <returns>Definition area (min, max)</returns>
        public static (double, double) AutoEmpiricalDefinitionArea(IEnumerable<double> values, double percentExpansion = 10) {
            // Get proportions for each side
            (double, double) proportions = GetProprotions(values);
            double leftProp = (percentExpansion / 100.0) * (proportions.Item1);
            double rightProp = (percentExpansion / 100.0) * (proportions.Item2);

            return GetBoundaries(values, leftProp, rightProp);
        }

        /// <summary>
        /// Get equals definition area by empirical way
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="percentExpansion">Percent of area expansion</param>
        /// <returns>Definition area (min, max)</returns>
        public static (double, double) EqualEmpiricalDefinitionArea(IEnumerable<double> values, double percentExpansion = 10) {
            // Get proportions for each side
            double leftProp = (percentExpansion / 100.0) * 0.5;
            double rightProp = (percentExpansion / 100.0) * 0.5;

            return GetBoundaries(values, leftProp, rightProp);
        }

        /// <summary>
        /// Get new boundaries to expand definition area
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="leftProp">Proportion for left side (min)</param>
        /// <param name="rightProp">Proportion for right side (max)</param>
        /// <returns>Definition area (min, max)</returns>
        private static (double, double) GetBoundaries(IEnumerable<double> values, double leftProp, double rightProp) {
            double maxValue = values.Max();
            double minValue = values.Min();

            double newMin = minValue - (maxValue - minValue) * leftProp;
            double newMax = maxValue + (maxValue - minValue) * rightProp;

            if (minValue >= 0 && newMin < 0) {
                newMin = 0;
            }

            return (newMin, newMax);
        }

        /// <summary>
        /// Find the proportions from the average to the minimum and maximum
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>(left propotion, right proportion)</returns>
        private static (double, double) GetProprotions(IEnumerable<double> values) {
            double avgValue = values.Average();
            double maxValue = values.Max();
            double minValue = values.Min();

            // Find bounaries
            double leftBoundary = 1 - (avgValue - minValue) / (maxValue - minValue);
            double rightBoundary = 1 - leftBoundary;

            return (leftBoundary, rightBoundary);
        }

        /// <summary>
        /// Get first-order start moment
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Value of first-order start moment</returns>
        public static double FirstOrderStartMoment(IEnumerable<double> values) {
            return values.Average();
        }

        /// <summary>
        /// Get second-order start moment
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Value of second-order start moment</returns>
        public static double SecondOrderStartMoment(IEnumerable<double> values) {
            return values.Average(v => Math.Pow(v, 2));
        }

        /// <summary>
        /// Get third-order start moment
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Value of third-order start moment</returns>
        public static double ThirdOrderStartMoment(IEnumerable<double> values) {
            return values.Average(v => Math.Pow(v, 3));
        }

        /// <summary>
        /// Get fourth-order start moment
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Value of fourth-order start moment</returns>
        public static double FourthOrderStartMoment(IEnumerable<double> values) {
            return values.Average(v => Math.Pow(v, 4));
        }

        /// <summary>
        /// Get first-order central moment
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Value of first-order central moment</returns>
        public static double FirstOrderCentralMoment(IEnumerable<double> values) {
            double avg = values.Average();
            return values.Average(v => (v - avg));
        }

        /// <summary>
        /// Get second-order central moment
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Value of second-order central moment</returns>
        public static double SecondOrderCentralMoment(IEnumerable<double> values) {
            double avg = values.Average();
            return values.Average(v => Math.Pow((v - avg), 2));
        }

        /// <summary>
        /// Get third-order central moment
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Value of third-order central moment</returns>
        public static double ThirdOrderCentralMoment(IEnumerable<double> values) {
            double avg = values.Average();
            return values.Average(v => Math.Pow((v - avg), 3));
        }

        /// <summary>
        /// Get fourth-order central moment
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Value of fourth-order central moment</returns>
        public static double FourthOrderCentralMoment(IEnumerable<double> values) {
            double avg = values.Average();
            return values.Average(v => Math.Pow((v - avg), 4));
        }

        /// <summary>
        /// Get min value
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Min value</returns>
        public static double Min(IEnumerable<double> values) {
            return values.Min();
        }

        /// <summary>
        /// Get max value
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Max value</returns>
        public static double Max(IEnumerable<double> values) {
            return values.Max();
        }

        /// <summary>
        /// Get asymmetry coefficient of values
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Asymmetry coefficient</returns>
        public static double AsymmetryCoefficient(IEnumerable<double> values) { 
            return (ThirdOrderCentralMoment(values) / Math.Pow(StandardDeviation(values), 3));
        }

        /// <summary>
        /// Get excess coefficient of values
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Excess coefficient</returns>
        public static double ExcessCoefficient(IEnumerable<double> values) {
            return ((FourthOrderCentralMoment(values) / Math.Pow(StandardDeviation(values), 4)) - 3);
        }

        /// <summary>
        /// Get median value of list
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Median</returns>
        public static double Median(IEnumerable<double> values) {
            if (values.Count() == 1) {
                return values.First();
            }

            List<double>sortedValues = values.OrderBy(s => s).ToList();
            return sortedValues.Count % 2 != 0 ? sortedValues[(sortedValues.Count - 1) / 2] : 
                (sortedValues[(sortedValues.Count / 2)] + sortedValues[(sortedValues.Count / 2) - 1]) / 2;
        }

        /// <summary>
        /// Get variation coefficient of values
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Variation coefficient</returns>
        public static double VariationCoefficient(IEnumerable<double> values) {
            return StandardDeviation(values) / values.Average();
        }

        /// <summary>
        /// Get average value on interval (0, 1)
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Average value on (0, 1)</returns>
        public static double AverageOnInterval_0_1(IEnumerable<double> values) {
            double min = values.Min();
            double maxMinDiff = values.Max() - min;
            return values.Average(v => (v - min) / maxMinDiff);
        }

        /// <summary>
        /// Get standard deviation on interval (0, 1)
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Standard deviation on (0, 1)</returns>
        public static double StandardDeviationOnInterval_0_1(IEnumerable<double> values) {
            double min = values.Min();
            double maxMinDiff = values.Max() - min;
            double avg = AverageOnInterval_0_1(values);
            return Math.Sqrt(values.Average(v => Math.Pow((((v - min) / maxMinDiff) - avg), 2)));
        }

        /// <summary>
        /// Get variation coefficient on interval (0, 1)
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Variation coefficient on (0, 1)</returns>
        public static double VariationCoefficientOnInterval_0_1(IEnumerable<double> values) {
            return StandardDeviationOnInterval_0_1(values) / AverageOnInterval_0_1(values);
        }

        /// <summary>
        /// Get standard error on interval (0, 1)
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Standard error on (0, 1)</returns>
        public static double StandardErrorOnInterval_0_1(IEnumerable<double> values) {
            return StandardDeviationOnInterval_0_1(values) / Math.Sqrt(values.Count());
        }

        /// <summary>
        /// Get waveform length (WL)
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Waveform length (WL)</returns>
        public static double WavefromLength(IEnumerable<double> values) {
            double result = 0;
            
            for (int i = 1; i < values.Count(); i++) {
                result += Math.Abs(values.ElementAt(i) - values.ElementAt(i - 1));
            }

            return result;
        }

        /// <summary>
        /// Get kurtosis (KURT)
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Kurtosis (KURT)</returns>
        public static double Kurtosis(IEnumerable<double> values) { 
            return (FourthOrderCentralMoment(values) / Math.Pow(SecondOrderCentralMoment(values), 2));
        }

        /// <summary>
        /// Get predict error (MAPE)
        /// </summary>
        /// <param name="realValue">Real value</param>
        /// <param name="predictValue">Predicted value</param>
        /// <returns>Prediction error</returns>
        public static double PredictError(double realValue, double predictValue) {
            return (Math.Abs(realValue - predictValue) / realValue) * 100;
        }

        /// <summary>
        /// Get min max error (range error)
        /// </summary>
        /// <param name="realValue">Real value</param>
        /// <param name="predictValue">Predicted value</param>
        /// <param name="minValue">Min value of real values</param>
        /// <param name="maxValue">Max value of real values</param>
        /// <returns>Range error</returns>
        public static double RangeError(double realValue, double predictValue, double minValue, double maxValue) {
            return (Math.Abs(realValue - predictValue) / (maxValue - minValue)) * 100;
        }

        /// <summary>
        /// Get absolute error
        /// </summary>
        /// <param name="realValue">Real value</param>
        /// <param name="predictValue">Predicted value</param>
        /// <returns>Absolute error</returns>
        public static double AbsoluteError(double realValue, double predictValue) {
            return Math.Abs(realValue - predictValue);
        }

        /// <summary>
        /// Get max percentage error
        /// </summary>
        /// <param name="realValue">Real value</param>
        /// <param name="predictValue">Predicted value</param>
        /// <param name="maxValue">Max value of real values</param>
        /// <returns>Max percentage error</returns>
        public static double MaxPercentError(double realValue, double predictValue, double maxValue) {
            return (Math.Abs(realValue - predictValue) / maxValue) * 100;
        }

        /// <summary>
        /// Get min percentage error
        /// </summary>
        /// <param name="realValue">Real value</param>
        /// <param name="predictValue">Predicted value</param>
        /// <param name="minValue">Min value of real values</param>
        /// <returns>Min percentage error</returns>
        public static double MinPercentError(double realValue, double predictValue, double minValue) {
            return (Math.Abs(realValue - predictValue) / minValue) * 100;
        }
    }
}
