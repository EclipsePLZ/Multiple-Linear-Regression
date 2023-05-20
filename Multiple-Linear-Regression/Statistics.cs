using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepParameters {
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

        public static Dictionary<string, Func<List<double>, double>> Functions { get; } = 
            new Dictionary<string, Func<List<double>, double>>() {
                { "Начальный момент 1-го порядка",  FirstOrderStartMoment},
                { "Начальный момент 2-го порядка",  SecondOrderStartMoment},
                { "Начальный момент 3-го порядка",  ThirdOrderStartMoment},
                { "Начальный момент 4-го порядка",  FourthOrderStartMoment},
                { "Центральный момент 1-го порядка",  FirstOrderCentralMoment},
                { "Центральный момент 2-го порядка",  SecondOrderCentralMoment},
                { "Центральный момент 3-го порядка",  ThirdOrderCentralMoment},
                { "Центральный момент 4-го порядка",  FourthOrderCentralMoment},
                { "Минимум", Min },
                { "Максимум", Max },
                { "Коэффициент асимметрии", AsymmetryCoefficient },
                { "Коэффициент эксцесса", ExcessCoefficient },
                { "Медиана", Median },
                { "Коэффициент вариации", VariationCoefficient },
                { "Среднее на интервале (0, 1)", AverageOnInterval_0_1 },
                { "Стандартное отклонение на интервале (0, 1)", StandardDeviationOnInterval_0_1 },
                { "Коэффициент вариации на интервале (0, 1)", VariationCoefficientOnInterval_0_1 },
                { "Стандартная ошибка на интервале (0, 1)", StandardErrorOnInterval_0_1 },
                { "Waveform length (WL)", WavefromLength },
                { "Kurtosis (KURT)", Kurtosis }
            };

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
    }
}
