using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Get auto definition area by empirical way
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>Definition area (min, max)</returns>
        public static (double, double) AutoEmpiricalDefinitionArea(IEnumerable<double> values) {
            // Get proportions for each side
            (double, double) proportions = GetProprotions(values);

            double maxValue = values.Max();
            double minValue = values.Min();

            // Find new boundaries
            double newMin = minValue - (maxValue - minValue) * proportions.Item1;
            double newMax = maxValue + (maxValue - minValue) * proportions.Item2;

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

        public static Dictionary<string, Func<List<double>, List<double>>> PreprocessingFunctions { get; } =
            new Dictionary<string, Func<List<double>, List<double>>>() {
                { "x^2", Pow_X2},
                { "x^3", Pow_X3},
                { "x^(-1)", Pow_M1},
                { "x^(-2)", Pow_M2},
                { "x^(-3)", Pow_M3},
                { "x^(1/3)", Pow_1_3},
                { "ln(x)", Log},
                { "sqrt(x)", Sqrt},
                { "sin(0,001*x)", Sin_0_001},
                { "sin(-0,001*x)", Sin_M0_001},
                { "sin(0,01*x)", Sin_0_01},
                { "sin(-0,01*x)", Sin_M0_01},
                { "sin(0,1*x)", Sin_0_1},
                { "sin(-0,1*x)", Sin_M0_1},
                { "sin(0,5*x)", Sin_0_5},
                { "sin(-0,5*x)", Sin_M0_5},
                { "sin(1*x)", Sin_1},
                { "sin(-1*x)", Sin_M1},
                { "sin(1,5*x)", Sin_1_5},
                { "sin(-1,5*x)", Sin_M1_5},
                { "sin(2*x)", Sin_2},
                { "sin(-2*x)", Sin_M2},
                { "sin(2,5*x)", Sin_2_5},
                { "sin(-2,5*x)", Sin_M2_5},
                { "sin(3*x)", Sin_3},
                { "sin(-3*x)", Sin_M3},
                { "sin(3,5*x)", Sin_3_5},
                { "sin(-3,5*x)", Sin_M3_5},
                { "sin(4*x)", Sin_4},
                { "sin(-4*x)", Sin_M4},
                { "sin(4,5*x)", Sin_4_5},
                { "sin(-4,5*x)", Sin_M4_5},
                { "sin(5*x)", Sin_5},
                { "sin(-5*x)", Sin_M5},
                { "sin(6*x)", Sin_6},
                { "sin(-6*x)", Sin_M6},
                { "sin(7*x)", Sin_7},
                { "sin(-7*x)", Sin_M7},
                { "sin(8*x)", Sin_8},
                { "sin(-8*x)", Sin_M8},
                { "sin(9*x)", Sin_9},
                { "sin(-9*x)", Sin_M9},
                { "sin(10*x)", Sin_10},
                { "sin(-10*x)", Sin_M10},
                { "tg(0,001*x)", Tan_0_001},
                { "tg(-0,001*x)", Tan_M0_001},
                { "tg(0,01*x)", Tan_0_01},
                { "tg(-0,01*x)", Tan_M0_01},
                { "tg(0,1*x)", Tan_0_1},
                { "tg(-0,1*x)", Tan_M0_1},
                { "tg(0,5*x)", Tan_0_5},
                { "tg(-0,5*x)", Tan_M0_5},
                { "tg(1*x)", Tan_1},
                { "tg(-1*x)", Tan_M1},
                { "tg(1,5*x)", Tan_1_5},
                { "tg(-1,5*x)", Tan_M1_5},
                { "tg(2*x)", Tan_2},
                { "tg(-2*x)", Tan_M2},
                { "tg(2,5*x)", Tan_2_5},
                { "tg(-2,5*x)", Tan_M2_5},
                { "tg(3*x)", Tan_3},
                { "tg(-3*x)", Tan_M3},
                { "tg(3,5*x)", Tan_3_5},
                { "tg(-3,5*x)", Tan_M3_5},
                { "tg(4*x)", Tan_4},
                { "tg(-4*x)", Tan_M4},
                { "tg(4,5*x)", Tan_4_5},
                { "tg(-4,5*x)", Tan_M4_5},
                { "tg(5*x)", Tan_5},
                { "tg(-5*x)", Tan_M5},
                { "tg(6*x)", Tan_6},
                { "tg(-6*x)", Tan_M6},
                { "tg(7*x)", Tan_7},
                { "tg(-7*x)", Tan_M7},
                { "tg(8*x)", Tan_8},
                { "tg(-8*x)", Tan_M8},
                { "tg(9*x)", Tan_9},
                { "tg(-9*x)", Tan_M9},
                { "tg(10*x)", Tan_10},
                { "tg(-10*x)", Tan_M10},
                { "arctg(0,001*x)", Atan_0_001},
                { "arctg(-0,001*x)", Atan_M0_001},
                { "arctg(0,01*x)", Atan_0_01},
                { "arctg(-0,01*x)", Atan_M0_01},
                { "arctg(0,1*x)", Atan_0_1},
                { "arctg(-0,1*x)", Atan_M0_1},
                { "arctg(0,5*x)", Atan_0_5},
                { "arctg(-0,5*x)", Atan_M0_5},
                { "arctg(1*x)", Atan_1},
                { "arctg(-1*x)", Atan_M1},
                { "arctg(1,5*x)", Atan_1_5},
                { "arctg(-1,5*x)", Atan_M1_5},
                { "arctg(2*x)", Atan_2},
                { "arctg(-2*x)", Atan_M2},
                { "arctg(2,5*x)", Atan_2_5},
                { "arctg(-2,5*x)", Atan_M2_5},
                { "arctg(3*x)", Atan_3},
                { "arctg(-3*x)", Atan_M3},
                { "arctg(3,5*x)", Atan_3_5},
                { "arctg(-3,5*x)", Atan_M3_5},
                { "arctg(4*x)", Atan_4},
                { "arctg(-4*x)", Atan_M4},
                { "arctg(4,5*x)", Atan_4_5},
                { "arctg(-4,5*x)", Atan_M4_5},
                { "arctg(5*x)", Atan_5},
                { "arctg(-5*x)", Atan_M5},
                { "arctg(6*x)", Atan_6},
                { "arctg(-6*x)", Atan_M6},
                { "arctg(7*x)", Atan_7},
                { "arctg(-7*x)", Atan_M7},
                { "arctg(8*x)", Atan_8},
                { "arctg(-8*x)", Atan_M8},
                { "arctg(9*x)", Atan_9},
                { "arctg(-9*x)", Atan_M9},
                { "arctg(10*x)", Atan_10},
                { "arctg(-10*x)", Atan_M10},
                { "e^(0,001*x)", ExpX_0_001},
                { "e^(-0,001*x)", ExpX_M0_001},
                { "e^(0,01*x)", ExpX_0_01},
                { "e^(-0,01*x)", ExpX_M0_01},
                { "e^(0,1*x)", ExpX_0_1},
                { "e^(-0,1*x)", ExpX_M0_1},
                { "e^(0,5*x)", ExpX_0_5},
                { "e^(-0,5*x)", ExpX_M0_5},
                { "e^(1*x)", ExpX_1},
                { "e^(-1*x)", ExpX_M1},
                { "e^(1,5*x)", ExpX_1_5},
                { "e^(-1,5*x)", ExpX_M1_5},
                { "e^(2*x)", ExpX_2},
                { "e^(-2*x)", ExpX_M2},
                { "e^(2,5*x)", ExpX_2_5},
                { "e^(-2,5*x)", ExpX_M2_5},
                { "e^(3*x)", ExpX_3},
                { "e^(-3*x)", ExpX_M3},
                { "e^(3,5*x)", ExpX_3_5},
                { "e^(-3,5*x)", ExpX_M3_5},
                { "e^(4*x)", ExpX_4},
                { "e^(-4*x)", ExpX_M4},
                { "e^(4,5*x)", ExpX_4_5},
                { "e^(-4,5*x)", ExpX_M4_5},
                { "e^(5*x)", ExpX_5},
                { "e^(-5*x)", ExpX_M5},
                { "e^(6*x)", ExpX_6},
                { "e^(-6*x)", ExpX_M6},
                { "e^(7*x)", ExpX_7},
                { "e^(-7*x)", ExpX_M7},
                { "e^(8*x)", ExpX_8},
                { "e^(-8*x)", ExpX_M8},
                { "e^(9*x)", ExpX_9},
                { "e^(-9*x)", ExpX_M9},
                { "e^(10*x)", ExpX_10},
                { "e^(-10*x)", ExpX_M10},
                { "e^(0,001*x^2)", Exp_X2_0_001},
                { "e^(-0,001*x^2)", Exp_X2_M0_001},
                { "e^(0,01*x^2)", Exp_X2_0_01},
                { "e^(-0,01*x^2)", Exp_X2_M0_01},
                { "e^(0,1*x^2)", Exp_X2_0_1},
                { "e^(-0,1*x^2)", Exp_X2_M0_1},
                { "e^(0,5*x^2)", Exp_X2_0_5},
                { "e^(-0,5*x^2)", Exp_X2_M0_5},
                { "e^(1*x^2)", Exp_X2_1},
                { "e^(-1*x^2)", Exp_X2_M1},
                { "e^(1,5*x^2)", Exp_X2_1_5},
                { "e^(-1,5*x^2)", Exp_X2_M1_5},
                { "e^(2*x^2)", Exp_X2_2},
                { "e^(-2*x^2)", Exp_X2_M2},
                { "e^(2,5*x^2)", Exp_X2_2_5},
                { "e^(-2,5*x^2)", Exp_X2_M2_5},
                { "e^(3*x^2)", Exp_X2_3},
                { "e^(-3*x^2)", Exp_X2_M3},
                { "e^(3,5*x^2)", Exp_X2_3_5},
                { "e^(-3,5*x^2)", Exp_X2_M3_5},
                { "e^(4*x^2)", Exp_X2_4},
                { "e^(-4*x^2)", Exp_X2_M4},
                { "e^(4,5*x^2)", Exp_X2_4_5},
                { "e^(-4,5*x^2)", Exp_X2_M4_5},
                { "e^(5*x^2)", Exp_X2_5},
                { "e^(-5*x^2)", Exp_X2_M5},
                { "e^(6*x^2)", Exp_X2_6},
                { "e^(-6*x^2)", Exp_X2_M6},
                { "e^(7*x^2)", Exp_X2_7},
                { "e^(-7*x^2)", Exp_X2_M7},
                { "e^(8*x^2)", Exp_X2_8},
                { "e^(-8*x^2)", Exp_X2_M8},
                { "e^(9*x^2)", Exp_X2_9},
                { "e^(-9*x^2)", Exp_X2_M9},
                { "e^(10*x^2)", Exp_X2_10},
                { "e^(-10*x^2)", Exp_X2_M10},
                { "1/(1+e^(0,001*x))", Sigm_0_001},
                { "1/(1+e^(-0,001*x))", Sigm_M0_001},
                { "1/(1+e^(0,01*x))", Sigm_0_01},
                { "1/(1+e^(-0,01*x))", Sigm_M0_01},
                { "1/(1+e^(0,1*x))", Sigm_0_1},
                { "1/(1+e^(-0,1*x))", Sigm_M0_1},
                { "1/(1+e^(0,5*x))", Sigm_0_5},
                { "1/(1+e^(-0,5*x))", Sigm_M0_5},
                { "1/(1+e^(1*x))", Sigm_1},
                { "1/(1+e^(-1*x))", Sigm_M1},
                { "1/(1+e^(1,5*x))", Sigm_1_5},
                { "1/(1+e^(-1,5*x))", Sigm_M1_5},
                { "1/(1+e^(2*x))", Sigm_2},
                { "1/(1+e^(-2*x))", Sigm_M2},
                { "1/(1+e^(2,5*x))", Sigm_2_5},
                { "1/(1+e^(-2,5*x))", Sigm_M2_5},
                { "1/(1+e^(3*x))", Sigm_3},
                { "1/(1+e^(-3*x))", Sigm_M3},
                { "1/(1+e^(3,5*x))", Sigm_3_5},
                { "1/(1+e^(-3,5*x))", Sigm_M3_5},
                { "1/(1+e^(4*x))", Sigm_4},
                { "1/(1+e^(-4*x))", Sigm_M4},
                { "1/(1+e^(4,5*x))", Sigm_4_5},
                { "1/(1+e^(-4,5*x))", Sigm_M4_5},
                { "1/(1+e^(5*x))", Sigm_5},
                { "1/(1+e^(-5*x))", Sigm_M5},
                { "1/(1+e^(6*x))", Sigm_6},
                { "1/(1+e^(-6*x))", Sigm_M6},
                { "1/(1+e^(7*x))", Sigm_7},
                { "1/(1+e^(-7*x))", Sigm_M7},
                { "1/(1+e^(8*x))", Sigm_8},
                { "1/(1+e^(-8*x))", Sigm_M8},
                { "1/(1+e^(9*x))", Sigm_9},
                { "1/(1+e^(-9*x))", Sigm_M9},
                { "1/(1+e^(10*x))", Sigm_10},
                { "1/(1+e^(-10*x))", Sigm_M10},
                { "(e^(0,001*x)-1)/(1+e^(0,001*x))", Exp_Exp_0_001},
                { "(e^(-0,001*x)-1)/(1+e^(-0,001*x))", Exp_Exp_M0_001},
                { "(e^(0,01*x)-1)/(1+e^(0,01*x))", Exp_Exp_0_01},
                { "(e^(-0,01*x)-1)/(1+e^(-0,01*x))", Exp_Exp_M0_01},
                { "(e^(0,1*x)-1)/(1+e^(0,1*x))", Exp_Exp_0_1},
                { "(e^(-0,1*x)-1)/(1+e^(-0,1*x))", Exp_Exp_M0_1},
                { "(e^(0,5*x)-1)/(1+e^(0,5*x))", Exp_Exp_0_5},
                { "(e^(-0,5*x)-1)/(1+e^(-0,5*x))", Exp_Exp_M0_5},
                { "(e^(1*x)-1)/(1+e^(1*x))", Exp_Exp_1},
                { "(e^(-1*x)-1)/(1+e^(-1*x))", Exp_Exp_M1},
                { "(e^(1,5*x)-1)/(1+e^(1,5*x))", Exp_Exp_1_5},
                { "(e^(-1,5*x)-1)/(1+e^(-1,5*x))", Exp_Exp_M1_5},
                { "(e^(2*x)-1)/(1+e^(2*x))", Exp_Exp_2},
                { "(e^(-2*x)-1)/(1+e^(-2*x))", Exp_Exp_M2},
                { "(e^(2,5*x)-1)/(1+e^(2,5*x))", Exp_Exp_2_5},
                { "(e^(-2,5*x)-1)/(1+e^(-2,5*x))", Exp_Exp_M2_5},
                { "(e^(3*x)-1)/(1+e^(3*x))", Exp_Exp_3},
                { "(e^(-3*x)-1)/(1+e^(-3*x))", Exp_Exp_M3},
                { "(e^(3,5*x)-1)/(1+e^(3,5*x))", Exp_Exp_3_5},
                { "(e^(-3,5*x)-1)/(1+e^(-3,5*x))", Exp_Exp_M3_5},
                { "(e^(4*x)-1)/(1+e^(4*x))", Exp_Exp_4},
                { "(e^(-4*x)-1)/(1+e^(-4*x))", Exp_Exp_M4},
                { "(e^(4,5*x)-1)/(1+e^(4,5*x))", Exp_Exp_4_5},
                { "(e^(-4,5*x)-1)/(1+e^(-4,5*x))", Exp_Exp_M4_5},
                { "(e^(5*x)-1)/(1+e^(5*x))", Exp_Exp_5},
                { "(e^(-5*x)-1)/(1+e^(-5*x))", Exp_Exp_M5},
                { "(e^(6*x)-1)/(1+e^(6*x))", Exp_Exp_6},
                { "(e^(-6*x)-1)/(1+e^(-6*x))", Exp_Exp_M6},
                { "(e^(7*x)-1)/(1+e^(7*x))", Exp_Exp_7},
                { "(e^(-7*x)-1)/(1+e^(-7*x))", Exp_Exp_M7},
                { "(e^(8*x)-1)/(1+e^(8*x))", Exp_Exp_8},
                { "(e^(-8*x)-1)/(1+e^(-8*x))", Exp_Exp_M8},
                { "(e^(9*x)-1)/(1+e^(9*x))", Exp_Exp_9},
                { "(e^(-9*x)-1)/(1+e^(-9*x))", Exp_Exp_M9},
                { "(e^(10*x)-1)/(1+e^(10*x))", Exp_Exp_10},
                { "(e^(-10*x)-1)/(1+e^(-10*x))", Exp_Exp_M10},
                { "(e^(0,001*x)-e^(-0,001*x))/2", Exp_Exp_2_0_001},
                { "(e^(-0,001*x)-e^(0,001*x))/2", Exp_Exp_2_M0_001},
                { "(e^(0,01*x)-e^(-0,01*x))/2", Exp_Exp_2_0_01},
                { "(e^(-0,01*x)-e^(0,01*x))/2", Exp_Exp_2_M0_01},
                { "(e^(0,1*x)-e^(-0,1*x))/2", Exp_Exp_2_0_1},
                { "(e^(-0,1*x)-e^(0,1*x))/2", Exp_Exp_2_M0_1},
                { "(e^(0,5*x)-e^(-0,5*x))/2", Exp_Exp_2_0_5},
                { "(e^(-0,5*x)-e^(0,5*x))/2", Exp_Exp_2_M0_5},
                { "(e^(x)-e^(-x))/2", Exp_Exp_2_1},
                { "(e^(-x)-e^(x))/2", Exp_Exp_2_M1},
                { "(e^(1,5*x)-e^(-1,5*x))/2", Exp_Exp_2_1_5},
                { "(e^(-1,5*x)-e^(1,5*x))/2", Exp_Exp_2_M1_5},
                { "(e^(2*x)-e^(-2*x))/2", Exp_Exp_2_2},
                { "(e^(-2*x)-e^(2*x))/2", Exp_Exp_2_M2},
                { "(e^(2,5*x)-e^(-2,5*x))/2", Exp_Exp_2_2_5},
                { "(e^(-2,5*x)-e^(2,5*x))/2", Exp_Exp_2_M2_5},
                { "(e^(3*x)-e^(-3*x))/2", Exp_Exp_2_3},
                { "(e^(-3*x)-e^(3*x))/2", Exp_Exp_2_M3},
                { "(e^(3,5*x)-e^(-3,5*x))/2", Exp_Exp_2_3_5},
                { "(e^(-3,5*x)-e^(3,5*x))/2", Exp_Exp_2_M3_5},
                { "(e^(4*x)-e^(-4*x))/2", Exp_Exp_2_4},
                { "(e^(-4*x)-e^(4*x))/2", Exp_Exp_2_M4},
                { "(e^(4,5*x)-e^(-4,5*x))/2", Exp_Exp_2_4_5},
                { "(e^(-4,5*x)-e^(4,5*x))/2", Exp_Exp_2_M4_5},
                { "(e^(5*x)-e^(-5*x))/2", Exp_Exp_2__5},
                { "(e^(-5*x)-e^(5*x))/2", Exp_Exp_2_M5},
                { "(e^(6*x)-e^(-6*x))/2", Exp_Exp_2_6},
                { "(e^(-6*x)-e^(6*x))/2", Exp_Exp_2_M6},
                { "(e^(7*x)-e^(-7*x))/2", Exp_Exp_2_7},
                { "(e^(-7*x)-e^(7*x))/2", Exp_Exp_2_M7},
                { "(e^(8*x)-e^(-8*x))/2", Exp_Exp_2_8},
                { "(e^(-8*x)-e^(8*x))/2", Exp_Exp_2_M8},
                { "(e^(9*x)-e^(-9*x))/2", Exp_Exp_2_9},
                { "(e^(-9*x)-e^(9*x))/2", Exp_Exp_2_M9},
                { "(e^(10*x)-e^(-10*x))/2", Exp_Exp_2_10},
                { "(e^(-10*x)-e^(10*x))/2", Exp_Exp_2_M10},
            };

        public static Dictionary<string, Func<double, double>> ConversionFuntions { get; } =
            new Dictionary<string, Func<double, double>>() {
                { "x^2", Pow_X2},
                { "x^3", Pow_X3},
                { "x^(-1)", Pow_M1},
                { "x^(-2)", Pow_M2},
                { "x^(-3)", Pow_M3},
                { "x^(1/3)", Pow_1_3},
                { "ln(x)", Log},
                { "sqrt(x)", Sqrt},
                { "sin(0,001*x)", Sin_0_001},
                { "sin(-0,001*x)", Sin_M0_001},
                { "sin(0,01*x)", Sin_0_01},
                { "sin(-0,01*x)", Sin_M0_01},
                { "sin(0,1*x)", Sin_0_1},
                { "sin(-0,1*x)", Sin_M0_1},
                { "sin(0,5*x)", Sin_0_5},
                { "sin(-0,5*x)", Sin_M0_5},
                { "sin(1*x)", Sin_1},
                { "sin(-1*x)", Sin_M1},
                { "sin(1,5*x)", Sin_1_5},
                { "sin(-1,5*x)", Sin_M1_5},
                { "sin(2*x)", Sin_2},
                { "sin(-2*x)", Sin_M2},
                { "sin(2,5*x)", Sin_2_5},
                { "sin(-2,5*x)", Sin_M2_5},
                { "sin(3*x)", Sin_3},
                { "sin(-3*x)", Sin_M3},
                { "sin(3,5*x)", Sin_3_5},
                { "sin(-3,5*x)", Sin_M3_5},
                { "sin(4*x)", Sin_4},
                { "sin(-4*x)", Sin_M4},
                { "sin(4,5*x)", Sin_4_5},
                { "sin(-4,5*x)", Sin_M4_5},
                { "sin(5*x)", Sin_5},
                { "sin(-5*x)", Sin_M5},
                { "sin(6*x)", Sin_6},
                { "sin(-6*x)", Sin_M6},
                { "sin(7*x)", Sin_7},
                { "sin(-7*x)", Sin_M7},
                { "sin(8*x)", Sin_8},
                { "sin(-8*x)", Sin_M8},
                { "sin(9*x)", Sin_9},
                { "sin(-9*x)", Sin_M9},
                { "sin(10*x)", Sin_10},
                { "sin(-10*x)", Sin_M10},
                { "tg(0,001*x)", Tan_0_001},
                { "tg(-0,001*x)", Tan_M0_001},
                { "tg(0,01*x)", Tan_0_01},
                { "tg(-0,01*x)", Tan_M0_01},
                { "tg(0,1*x)", Tan_0_1},
                { "tg(-0,1*x)", Tan_M0_1},
                { "tg(0,5*x)", Tan_0_5},
                { "tg(-0,5*x)", Tan_M0_5},
                { "tg(1*x)", Tan_1},
                { "tg(-1*x)", Tan_M1},
                { "tg(1,5*x)", Tan_1_5},
                { "tg(-1,5*x)", Tan_M1_5},
                { "tg(2*x)", Tan_2},
                { "tg(-2*x)", Tan_M2},
                { "tg(2,5*x)", Tan_2_5},
                { "tg(-2,5*x)", Tan_M2_5},
                { "tg(3*x)", Tan_3},
                { "tg(-3*x)", Tan_M3},
                { "tg(3,5*x)", Tan_3_5},
                { "tg(-3,5*x)", Tan_M3_5},
                { "tg(4*x)", Tan_4},
                { "tg(-4*x)", Tan_M4},
                { "tg(4,5*x)", Tan_4_5},
                { "tg(-4,5*x)", Tan_M4_5},
                { "tg(5*x)", Tan_5},
                { "tg(-5*x)", Tan_M5},
                { "tg(6*x)", Tan_6},
                { "tg(-6*x)", Tan_M6},
                { "tg(7*x)", Tan_7},
                { "tg(-7*x)", Tan_M7},
                { "tg(8*x)", Tan_8},
                { "tg(-8*x)", Tan_M8},
                { "tg(9*x)", Tan_9},
                { "tg(-9*x)", Tan_M9},
                { "tg(10*x)", Tan_10},
                { "tg(-10*x)", Tan_M10},
                { "arctg(0,001*x)", Atan_0_001},
                { "arctg(-0,001*x)", Atan_M0_001},
                { "arctg(0,01*x)", Atan_0_01},
                { "arctg(-0,01*x)", Atan_M0_01},
                { "arctg(0,1*x)", Atan_0_1},
                { "arctg(-0,1*x)", Atan_M0_1},
                { "arctg(0,5*x)", Atan_0_5},
                { "arctg(-0,5*x)", Atan_M0_5},
                { "arctg(1*x)", Atan_1},
                { "arctg(-1*x)", Atan_M1},
                { "arctg(1,5*x)", Atan_1_5},
                { "arctg(-1,5*x)", Atan_M1_5},
                { "arctg(2*x)", Atan_2},
                { "arctg(-2*x)", Atan_M2},
                { "arctg(2,5*x)", Atan_2_5},
                { "arctg(-2,5*x)", Atan_M2_5},
                { "arctg(3*x)", Atan_3},
                { "arctg(-3*x)", Atan_M3},
                { "arctg(3,5*x)", Atan_3_5},
                { "arctg(-3,5*x)", Atan_M3_5},
                { "arctg(4*x)", Atan_4},
                { "arctg(-4*x)", Atan_M4},
                { "arctg(4,5*x)", Atan_4_5},
                { "arctg(-4,5*x)", Atan_M4_5},
                { "arctg(5*x)", Atan_5},
                { "arctg(-5*x)", Atan_M5},
                { "arctg(6*x)", Atan_6},
                { "arctg(-6*x)", Atan_M6},
                { "arctg(7*x)", Atan_7},
                { "arctg(-7*x)", Atan_M7},
                { "arctg(8*x)", Atan_8},
                { "arctg(-8*x)", Atan_M8},
                { "arctg(9*x)", Atan_9},
                { "arctg(-9*x)", Atan_M9},
                { "arctg(10*x)", Atan_10},
                { "arctg(-10*x)", Atan_M10},
                { "e^(0,001*x)", ExpX_0_001},
                { "e^(-0,001*x)", ExpX_M0_001},
                { "e^(0,01*x)", ExpX_0_01},
                { "e^(-0,01*x)", ExpX_M0_01},
                { "e^(0,1*x)", ExpX_0_1},
                { "e^(-0,1*x)", ExpX_M0_1},
                { "e^(0,5*x)", ExpX_0_5},
                { "e^(-0,5*x)", ExpX_M0_5},
                { "e^(1*x)", ExpX_1},
                { "e^(-1*x)", ExpX_M1},
                { "e^(1,5*x)", ExpX_1_5},
                { "e^(-1,5*x)", ExpX_M1_5},
                { "e^(2*x)", ExpX_2},
                { "e^(-2*x)", ExpX_M2},
                { "e^(2,5*x)", ExpX_2_5},
                { "e^(-2,5*x)", ExpX_M2_5},
                { "e^(3*x)", ExpX_3},
                { "e^(-3*x)", ExpX_M3},
                { "e^(3,5*x)", ExpX_3_5},
                { "e^(-3,5*x)", ExpX_M3_5},
                { "e^(4*x)", ExpX_4},
                { "e^(-4*x)", ExpX_M4},
                { "e^(4,5*x)", ExpX_4_5},
                { "e^(-4,5*x)", ExpX_M4_5},
                { "e^(5*x)", ExpX_5},
                { "e^(-5*x)", ExpX_M5},
                { "e^(6*x)", ExpX_6},
                { "e^(-6*x)", ExpX_M6},
                { "e^(7*x)", ExpX_7},
                { "e^(-7*x)", ExpX_M7},
                { "e^(8*x)", ExpX_8},
                { "e^(-8*x)", ExpX_M8},
                { "e^(9*x)", ExpX_9},
                { "e^(-9*x)", ExpX_M9},
                { "e^(10*x)", ExpX_10},
                { "e^(-10*x)", ExpX_M10},
                { "e^(0,001*x^2)", Exp_X2_0_001},
                { "e^(-0,001*x^2)", Exp_X2_M0_001},
                { "e^(0,01*x^2)", Exp_X2_0_01},
                { "e^(-0,01*x^2)", Exp_X2_M0_01},
                { "e^(0,1*x^2)", Exp_X2_0_1},
                { "e^(-0,1*x^2)", Exp_X2_M0_1},
                { "e^(0,5*x^2)", Exp_X2_0_5},
                { "e^(-0,5*x^2)", Exp_X2_M0_5},
                { "e^(1*x^2)", Exp_X2_1},
                { "e^(-1*x^2)", Exp_X2_M1},
                { "e^(1,5*x^2)", Exp_X2_1_5},
                { "e^(-1,5*x^2)", Exp_X2_M1_5},
                { "e^(2*x^2)", Exp_X2_2},
                { "e^(-2*x^2)", Exp_X2_M2},
                { "e^(2,5*x^2)", Exp_X2_2_5},
                { "e^(-2,5*x^2)", Exp_X2_M2_5},
                { "e^(3*x^2)", Exp_X2_3},
                { "e^(-3*x^2)", Exp_X2_M3},
                { "e^(3,5*x^2)", Exp_X2_3_5},
                { "e^(-3,5*x^2)", Exp_X2_M3_5},
                { "e^(4*x^2)", Exp_X2_4},
                { "e^(-4*x^2)", Exp_X2_M4},
                { "e^(4,5*x^2)", Exp_X2_4_5},
                { "e^(-4,5*x^2)", Exp_X2_M4_5},
                { "e^(5*x^2)", Exp_X2_5},
                { "e^(-5*x^2)", Exp_X2_M5},
                { "e^(6*x^2)", Exp_X2_6},
                { "e^(-6*x^2)", Exp_X2_M6},
                { "e^(7*x^2)", Exp_X2_7},
                { "e^(-7*x^2)", Exp_X2_M7},
                { "e^(8*x^2)", Exp_X2_8},
                { "e^(-8*x^2)", Exp_X2_M8},
                { "e^(9*x^2)", Exp_X2_9},
                { "e^(-9*x^2)", Exp_X2_M9},
                { "e^(10*x^2)", Exp_X2_10},
                { "e^(-10*x^2)", Exp_X2_M10},
                { "1/(1+e^(0,001*x))", Sigm_0_001},
                { "1/(1+e^(-0,001*x))", Sigm_M0_001},
                { "1/(1+e^(0,01*x))", Sigm_0_01},
                { "1/(1+e^(-0,01*x))", Sigm_M0_01},
                { "1/(1+e^(0,1*x))", Sigm_0_1},
                { "1/(1+e^(-0,1*x))", Sigm_M0_1},
                { "1/(1+e^(0,5*x))", Sigm_0_5},
                { "1/(1+e^(-0,5*x))", Sigm_M0_5},
                { "1/(1+e^(1*x))", Sigm_1},
                { "1/(1+e^(-1*x))", Sigm_M1},
                { "1/(1+e^(1,5*x))", Sigm_1_5},
                { "1/(1+e^(-1,5*x))", Sigm_M1_5},
                { "1/(1+e^(2*x))", Sigm_2},
                { "1/(1+e^(-2*x))", Sigm_M2},
                { "1/(1+e^(2,5*x))", Sigm_2_5},
                { "1/(1+e^(-2,5*x))", Sigm_M2_5},
                { "1/(1+e^(3*x))", Sigm_3},
                { "1/(1+e^(-3*x))", Sigm_M3},
                { "1/(1+e^(3,5*x))", Sigm_3_5},
                { "1/(1+e^(-3,5*x))", Sigm_M3_5},
                { "1/(1+e^(4*x))", Sigm_4},
                { "1/(1+e^(-4*x))", Sigm_M4},
                { "1/(1+e^(4,5*x))", Sigm_4_5},
                { "1/(1+e^(-4,5*x))", Sigm_M4_5},
                { "1/(1+e^(5*x))", Sigm_5},
                { "1/(1+e^(-5*x))", Sigm_M5},
                { "1/(1+e^(6*x))", Sigm_6},
                { "1/(1+e^(-6*x))", Sigm_M6},
                { "1/(1+e^(7*x))", Sigm_7},
                { "1/(1+e^(-7*x))", Sigm_M7},
                { "1/(1+e^(8*x))", Sigm_8},
                { "1/(1+e^(-8*x))", Sigm_M8},
                { "1/(1+e^(9*x))", Sigm_9},
                { "1/(1+e^(-9*x))", Sigm_M9},
                { "1/(1+e^(10*x))", Sigm_10},
                { "1/(1+e^(-10*x))", Sigm_M10},
                { "(e^(0,001*x)-1)/(1+e^(0,001*x))", Exp_Exp_0_001},
                { "(e^(-0,001*x)-1)/(1+e^(-0,001*x))", Exp_Exp_M0_001},
                { "(e^(0,01*x)-1)/(1+e^(0,01*x))", Exp_Exp_0_01},
                { "(e^(-0,01*x)-1)/(1+e^(-0,01*x))", Exp_Exp_M0_01},
                { "(e^(0,1*x)-1)/(1+e^(0,1*x))", Exp_Exp_0_1},
                { "(e^(-0,1*x)-1)/(1+e^(-0,1*x))", Exp_Exp_M0_1},
                { "(e^(0,5*x)-1)/(1+e^(0,5*x))", Exp_Exp_0_5},
                { "(e^(-0,5*x)-1)/(1+e^(-0,5*x))", Exp_Exp_M0_5},
                { "(e^(1*x)-1)/(1+e^(1*x))", Exp_Exp_1},
                { "(e^(-1*x)-1)/(1+e^(-1*x))", Exp_Exp_M1},
                { "(e^(1,5*x)-1)/(1+e^(1,5*x))", Exp_Exp_1_5},
                { "(e^(-1,5*x)-1)/(1+e^(-1,5*x))", Exp_Exp_M1_5},
                { "(e^(2*x)-1)/(1+e^(2*x))", Exp_Exp_2},
                { "(e^(-2*x)-1)/(1+e^(-2*x))", Exp_Exp_M2},
                { "(e^(2,5*x)-1)/(1+e^(2,5*x))", Exp_Exp_2_5},
                { "(e^(-2,5*x)-1)/(1+e^(-2,5*x))", Exp_Exp_M2_5},
                { "(e^(3*x)-1)/(1+e^(3*x))", Exp_Exp_3},
                { "(e^(-3*x)-1)/(1+e^(-3*x))", Exp_Exp_M3},
                { "(e^(3,5*x)-1)/(1+e^(3,5*x))", Exp_Exp_3_5},
                { "(e^(-3,5*x)-1)/(1+e^(-3,5*x))", Exp_Exp_M3_5},
                { "(e^(4*x)-1)/(1+e^(4*x))", Exp_Exp_4},
                { "(e^(-4*x)-1)/(1+e^(-4*x))", Exp_Exp_M4},
                { "(e^(4,5*x)-1)/(1+e^(4,5*x))", Exp_Exp_4_5},
                { "(e^(-4,5*x)-1)/(1+e^(-4,5*x))", Exp_Exp_M4_5},
                { "(e^(5*x)-1)/(1+e^(5*x))", Exp_Exp_5},
                { "(e^(-5*x)-1)/(1+e^(-5*x))", Exp_Exp_M5},
                { "(e^(6*x)-1)/(1+e^(6*x))", Exp_Exp_6},
                { "(e^(-6*x)-1)/(1+e^(-6*x))", Exp_Exp_M6},
                { "(e^(7*x)-1)/(1+e^(7*x))", Exp_Exp_7},
                { "(e^(-7*x)-1)/(1+e^(-7*x))", Exp_Exp_M7},
                { "(e^(8*x)-1)/(1+e^(8*x))", Exp_Exp_8},
                { "(e^(-8*x)-1)/(1+e^(-8*x))", Exp_Exp_M8},
                { "(e^(9*x)-1)/(1+e^(9*x))", Exp_Exp_9},
                { "(e^(-9*x)-1)/(1+e^(-9*x))", Exp_Exp_M9},
                { "(e^(10*x)-1)/(1+e^(10*x))", Exp_Exp_10},
                { "(e^(-10*x)-1)/(1+e^(-10*x))", Exp_Exp_M10},
                { "(e^(0,001*x)-e^(-0,001*x))/2", Exp_Exp_2_0_001},
                { "(e^(-0,001*x)-e^(0,001*x))/2", Exp_Exp_2_M0_001},
                { "(e^(0,01*x)-e^(-0,01*x))/2", Exp_Exp_2_0_01},
                { "(e^(-0,01*x)-e^(0,01*x))/2", Exp_Exp_2_M0_01},
                { "(e^(0,1*x)-e^(-0,1*x))/2", Exp_Exp_2_0_1},
                { "(e^(-0,1*x)-e^(0,1*x))/2", Exp_Exp_2_M0_1},
                { "(e^(0,5*x)-e^(-0,5*x))/2", Exp_Exp_2_0_5},
                { "(e^(-0,5*x)-e^(0,5*x))/2", Exp_Exp_2_M0_5},
                { "(e^(x)-e^(-x))/2", Exp_Exp_2_1},
                { "(e^(-x)-e^(x))/2", Exp_Exp_2_M1},
                { "(e^(1,5*x)-e^(-1,5*x))/2", Exp_Exp_2_1_5},
                { "(e^(-1,5*x)-e^(1,5*x))/2", Exp_Exp_2_M1_5},
                { "(e^(2*x)-e^(-2*x))/2", Exp_Exp_2_2},
                { "(e^(-2*x)-e^(2*x))/2", Exp_Exp_2_M2},
                { "(e^(2,5*x)-e^(-2,5*x))/2", Exp_Exp_2_2_5},
                { "(e^(-2,5*x)-e^(2,5*x))/2", Exp_Exp_2_M2_5},
                { "(e^(3*x)-e^(-3*x))/2", Exp_Exp_2_3},
                { "(e^(-3*x)-e^(3*x))/2", Exp_Exp_2_M3},
                { "(e^(3,5*x)-e^(-3,5*x))/2", Exp_Exp_2_3_5},
                { "(e^(-3,5*x)-e^(3,5*x))/2", Exp_Exp_2_M3_5},
                { "(e^(4*x)-e^(-4*x))/2", Exp_Exp_2_4},
                { "(e^(-4*x)-e^(4*x))/2", Exp_Exp_2_M4},
                { "(e^(4,5*x)-e^(-4,5*x))/2", Exp_Exp_2_4_5},
                { "(e^(-4,5*x)-e^(4,5*x))/2", Exp_Exp_2_M4_5},
                { "(e^(5*x)-e^(-5*x))/2", Exp_Exp_2__5},
                { "(e^(-5*x)-e^(5*x))/2", Exp_Exp_2_M5},
                { "(e^(6*x)-e^(-6*x))/2", Exp_Exp_2_6},
                { "(e^(-6*x)-e^(6*x))/2", Exp_Exp_2_M6},
                { "(e^(7*x)-e^(-7*x))/2", Exp_Exp_2_7},
                { "(e^(-7*x)-e^(7*x))/2", Exp_Exp_2_M7},
                { "(e^(8*x)-e^(-8*x))/2", Exp_Exp_2_8},
                { "(e^(-8*x)-e^(8*x))/2", Exp_Exp_2_M8},
                { "(e^(9*x)-e^(-9*x))/2", Exp_Exp_2_9},
                { "(e^(-9*x)-e^(9*x))/2", Exp_Exp_2_M9},
                { "(e^(10*x)-e^(-10*x))/2", Exp_Exp_2_10},
                { "(e^(-10*x)-e^(10*x))/2", Exp_Exp_2_M10},
            };

        private static List<double> Pow_X2(IEnumerable<double> values) {
            return Pow_XN(values, 2.0);
        }

        private static List<double> Pow_X3(IEnumerable<double> values) {
            return Pow_XN(values, 3.0);
        }

        private static List<double> Pow_M1(IEnumerable<double> values) {
            return Pow_XN(values, -1.0);
        }

        private static List<double> Pow_M2(IEnumerable<double> values) {
            return Pow_XN(values, -2.0);
        }

        private static List<double> Pow_M3(IEnumerable<double> values) {
            return Pow_XN(values, -3.0);
        }

        private static List<double> Pow_1_3(IEnumerable<double> values) {
            return Pow_XN(values, (1.0 / 3.0));
        }

        private static double Pow_X2(double value) {
            return Pow_XN(value, 2.0);
        }

        private static double Pow_X3(double value) {
            return Pow_XN(value, 3.0);
        }

        private static double Pow_M1(double value) {
            return Pow_XN(value, -1.0);
        }

        private static double Pow_M2(double value) {
            return Pow_XN(value, -2.0);
        }

        private static double Pow_M3(double value) {
            return Pow_XN(value, -3.0);
        }

        private static double Pow_1_3(double value) {
            return Pow_XN(value, (1.0 / 3.0));
        }

        /// <summary>
        /// Increment each element of the list to the power of N
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="n">Value of power</param>
        /// <returns>List of values in the N degree</returns>
        public static List<double> Pow_XN(IEnumerable<double> values, double n) {
            List<double> result = new List<double>();

            // Expand each element of the list to a power
            foreach(var elem in values) {
                result.Add(Math.Pow(elem, n));
            }

            return result;
        }

        /// <summary>
        /// Increment value to the power of N
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="n">Value of power</param>
        /// <returns>Value in the N degree</returns>
        public static double Pow_XN(double value, double n) {
            return Math.Pow(value, n);
        }

        /// <summary>
        /// Take the natural logarithm of each number in the list
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns> List of natural logaritm from values</returns>
        public static List<double> Log(IEnumerable<double> values) {
            List<double> result = new List<double>();

            foreach(var elem in values) {
                result.Add(Math.Log(elem));
            }

            return result;
        }

        /// <summary>
        /// Take the natural logarithm from number
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Value of natural logarithm</returns>
        public static double Log(double value) {
            return Math.Log(value);
        }

        /// <summary>
        /// Take the root of each number in the list
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns> List of root of values</returns>
        public static List<double> Sqrt(IEnumerable<double> values) {
            List<double> result = new List<double>();

            foreach (var elem in values) {
                result.Add(Math.Sqrt(elem));
            }

            return result;
        }

        /// <summary>
        /// Take the root of the value
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Root of the value</returns>
        public static double Sqrt(double value) {
            return Math.Sqrt(value);
        }

        private static List<double> Sin_0_001(IEnumerable<double> values) {
            return Sin_N(values, 0.001);
        }

        private static List<double> Sin_M0_001(IEnumerable<double> values) {
            return Sin_N(values, -0.001);
        }

        private static List<double> Sin_0_01(IEnumerable<double> values) {
            return Sin_N(values, 0.01);
        }

        private static List<double> Sin_M0_01(IEnumerable<double> values) {
            return Sin_N(values, -0.01);
        }

        private static List<double> Sin_0_1(IEnumerable<double> values) {
            return Sin_N(values, 0.1);
        }

        private static List<double> Sin_M0_1(IEnumerable<double> values) {
            return Sin_N(values, -0.1);
        }

        private static List<double> Sin_0_5(IEnumerable<double> values) {
            return Sin_N(values, 0.5);
        }

        private static List<double> Sin_M0_5(IEnumerable<double> values) {
            return Sin_N(values, -0.5);
        }

        private static List<double> Sin_1(IEnumerable<double> values) {
            return Sin_N(values, 1.0);
        }

        private static List<double> Sin_M1(IEnumerable<double> values) {
            return Sin_N(values, -1.0);
        }

        private static List<double> Sin_1_5(IEnumerable<double> values) {
            return Sin_N(values, 1.5);
        }

        private static List<double> Sin_M1_5(IEnumerable<double> values) {
            return Sin_N(values, -1.5);
        }

        private static List<double> Sin_2(IEnumerable<double> values) {
            return Sin_N(values, 2.0);
        }

        private static List<double> Sin_M2(IEnumerable<double> values) {
            return Sin_N(values, -2.0);
        }

        private static List<double> Sin_2_5(IEnumerable<double> values) {
            return Sin_N(values, 2.5);
        }

        private static List<double> Sin_M2_5(IEnumerable<double> values) {
            return Sin_N(values, -2.5);
        }

        private static List<double> Sin_3(IEnumerable<double> values) {
            return Sin_N(values, 3.0);
        }

        private static List<double> Sin_M3(IEnumerable<double> values) {
            return Sin_N(values, -3.0);
        }

        private static List<double> Sin_3_5(IEnumerable<double> values) {
            return Sin_N(values, 3.5);
        }

        private static List<double> Sin_M3_5(IEnumerable<double> values) {
            return Sin_N(values, -3.5);
        }

        private static List<double> Sin_4(IEnumerable<double> values) {
            return Sin_N(values, 4.0);
        }

        private static List<double> Sin_M4(IEnumerable<double> values) {
            return Sin_N(values, -4.0);
        }

        private static List<double> Sin_4_5(IEnumerable<double> values) {
            return Sin_N(values, 4.5);
        }

        private static List<double> Sin_M4_5(IEnumerable<double> values) {
            return Sin_N(values, -4.5);
        }

        private static List<double> Sin_5(IEnumerable<double> values) {
            return Sin_N(values, 5.0);
        }

        private static List<double> Sin_M5(IEnumerable<double> values) {
            return Sin_N(values, -5.0);
        }

        private static List<double> Sin_6(IEnumerable<double> values) {
            return Sin_N(values, 6.0);
        }

        private static List<double> Sin_M6(IEnumerable<double> values) {
            return Sin_N(values, -6.0);
        }

        private static List<double> Sin_7(IEnumerable<double> values) {
            return Sin_N(values, 7.0);
        }

        private static List<double> Sin_M7(IEnumerable<double> values) {
            return Sin_N(values, -7.0);
        }

        private static List<double> Sin_8(IEnumerable<double> values) {
            return Sin_N(values, 8.0);
        }

        private static List<double> Sin_M8(IEnumerable<double> values) {
            return Sin_N(values, -8.0);
        }

        private static List<double> Sin_9(IEnumerable<double> values) {
            return Sin_N(values, 9.0);
        }

        private static List<double> Sin_M9(IEnumerable<double> values) {
            return Sin_N(values, -9.0);
        }

        private static List<double> Sin_10(IEnumerable<double> values) {
            return Sin_N(values, 10.0);
        }

        private static List<double> Sin_M10(IEnumerable<double> values) {
            return Sin_N(values, -10.0);
        }

        private static double Sin_0_001(double value) {
            return Sin_N(value, 0.001);
        }

        private static double Sin_M0_001(double value) {
            return Sin_N(value, -0.001);
        }

        private static double Sin_0_01(double value) {
            return Sin_N(value, 0.01);
        }

        private static double Sin_M0_01(double value) {
            return Sin_N(value, -0.01);
        }

        private static double Sin_0_1(double value) {
            return Sin_N(value, 0.1);
        }

        private static double Sin_M0_1(double value) {
            return Sin_N(value, -0.1);
        }

        private static double Sin_0_5(double value) {
            return Sin_N(value, 0.5);
        }

        private static double Sin_M0_5(double value) {
            return Sin_N(value, -0.5);
        }

        private static double Sin_1(double value) {
            return Sin_N(value, 1.0);
        }

        private static double Sin_M1(double value) {
            return Sin_N(value, -1.0);
        }

        private static double Sin_1_5(double value) {
            return Sin_N(value, 1.5);
        }

        private static double Sin_M1_5(double value) {
            return Sin_N(value, -1.5);
        }

        private static double Sin_2(double value) {
            return Sin_N(value, 2.0);
        }

        private static double Sin_M2(double value) {
            return Sin_N(value, -2.0);
        }

        private static double Sin_2_5(double value) {
            return Sin_N(value, 2.5);
        }

        private static double Sin_M2_5(double value) {
            return Sin_N(value, -2.5);
        }

        private static double Sin_3(double value) {
            return Sin_N(value, 3.0);
        }

        private static double Sin_M3(double value) {
            return Sin_N(value, -3.0);
        }

        private static double Sin_3_5(double value) {
            return Sin_N(value, 3.5);
        }

        private static double Sin_M3_5(double value) {
            return Sin_N(value, -3.5);
        }

        private static double Sin_4(double value) {
            return Sin_N(value, 4.0);
        }

        private static double Sin_M4(double value) {
            return Sin_N(value, -4.0);
        }

        private static double Sin_4_5(double value) {
            return Sin_N(value, 4.5);
        }

        private static double Sin_M4_5(double value) {
            return Sin_N(value, -4.5);
        }

        private static double Sin_5(double value) {
            return Sin_N(value, 5.0);
        }

        private static double Sin_M5(double value) {
            return Sin_N(value, -5.0);
        }

        private static double Sin_6(double value) {
            return Sin_N(value, 6.0);
        }

        private static double Sin_M6(double value) {
            return Sin_N(value, -6.0);
        }

        private static double Sin_7(double value) {
            return Sin_N(value, 7.0);
        }

        private static double Sin_M7(double value) {
            return Sin_N(value, -7.0);
        }

        private static double Sin_8(double value) {
            return Sin_N(value, 8.0);
        }

        private static double Sin_M8(double value) {
            return Sin_N(value, -8.0);
        }

        private static double Sin_9(double value) {
            return Sin_N(value, 9.0);
        }

        private static double Sin_M9(double value) {
            return Sin_N(value, -9.0);
        }

        private static double Sin_10(double value) {
            return Sin_N(value, 10.0);
        }

        private static double Sin_M10(double value) {
            return Sin_N(value, -10.0);
        }

        /// <summary>
        /// Get the sin value from each number in the list multiple by alpha
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>List of sin of values multiple by alpha</returns>
        public static List<double> Sin_N(IEnumerable<double> values, double alpha) {
            List<double> result = new List<double>();

            foreach(var elem in values) {
                result.Add(Math.Sin(elem * alpha));
            }

            return result;
        }

        /// <summary>
        /// Get the sin value for number multiple by alpha
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>Sin of value multiple by alpha</returns>
        public static double Sin_N(double value, double alpha) {
            return Math.Sin(value * alpha);
        }

        private static List<double> Tan_0_001(IEnumerable<double> values) {
            return Tan_N(values, 0.001);
        }

        private static List<double> Tan_M0_001(IEnumerable<double> values) {
            return Tan_N(values, -0.001);
        }

        private static List<double> Tan_0_01(IEnumerable<double> values) {
            return Tan_N(values, 0.01);
        }

        private static List<double> Tan_M0_01(IEnumerable<double> values) {
            return Tan_N(values, -0.01);
        }

        private static List<double> Tan_0_1(IEnumerable<double> values) {
            return Tan_N(values, 0.1);
        }

        private static List<double> Tan_M0_1(IEnumerable<double> values) {
            return Tan_N(values, -0.1);
        }

        private static List<double> Tan_0_5(IEnumerable<double> values) {
            return Tan_N(values, 0.5);
        }

        private static List<double> Tan_M0_5(IEnumerable<double> values) {
            return Tan_N(values, -0.5);
        }

        private static List<double> Tan_1(IEnumerable<double> values) {
            return Tan_N(values, 1.0);
        }

        private static List<double> Tan_M1(IEnumerable<double> values) {
            return Tan_N(values, -1.0);
        }

        private static List<double> Tan_1_5(IEnumerable<double> values) {
            return Tan_N(values, 1.5);
        }

        private static List<double> Tan_M1_5(IEnumerable<double> values) {
            return Tan_N(values, -1.5);
        }

        private static List<double> Tan_2(IEnumerable<double> values) {
            return Tan_N(values, 2.0);
        }

        private static List<double> Tan_M2(IEnumerable<double> values) {
            return Tan_N(values, -2.0);
        }

        private static List<double> Tan_2_5(IEnumerable<double> values) {
            return Tan_N(values, 2.5);
        }

        private static List<double> Tan_M2_5(IEnumerable<double> values) {
            return Tan_N(values, -2.5);
        }

        private static List<double> Tan_3(IEnumerable<double> values) {
            return Tan_N(values, 3.0);
        }

        private static List<double> Tan_M3(IEnumerable<double> values) {
            return Tan_N(values, -3.0);
        }

        private static List<double> Tan_3_5(IEnumerable<double> values) {
            return Tan_N(values, 3.5);
        }

        private static List<double> Tan_M3_5(IEnumerable<double> values) {
            return Tan_N(values, -3.5);
        }

        private static List<double> Tan_4(IEnumerable<double> values) {
            return Tan_N(values, 4.0);
        }

        private static List<double> Tan_M4(IEnumerable<double> values) {
            return Tan_N(values, -4.0);
        }

        private static List<double> Tan_4_5(IEnumerable<double> values) {
            return Tan_N(values, 4.5);
        }

        private static List<double> Tan_M4_5(IEnumerable<double> values) {
            return Tan_N(values, -4.5);
        }

        private static List<double> Tan_5(IEnumerable<double> values) {
            return Tan_N(values, 5.0);
        }

        private static List<double> Tan_M5(IEnumerable<double> values) {
            return Tan_N(values, -5.0);
        }

        private static List<double> Tan_6(IEnumerable<double> values) {
            return Tan_N(values, 6.0);
        }

        private static List<double> Tan_M6(IEnumerable<double> values) {
            return Tan_N(values, -6.0);
        }

        private static List<double> Tan_7(IEnumerable<double> values) {
            return Tan_N(values, 7.0);
        }

        private static List<double> Tan_M7(IEnumerable<double> values) {
            return Tan_N(values, -7.0);
        }

        private static List<double> Tan_8(IEnumerable<double> values) {
            return Tan_N(values, 8.0);
        }

        private static List<double> Tan_M8(IEnumerable<double> values) {
            return Tan_N(values, -8.0);
        }

        private static List<double> Tan_9(IEnumerable<double> values) {
            return Tan_N(values, 9.0);
        }

        private static List<double> Tan_M9(IEnumerable<double> values) {
            return Tan_N(values, -9.0);
        }

        private static List<double> Tan_10(IEnumerable<double> values) {
            return Tan_N(values, 10.0);
        }

        private static List<double> Tan_M10(IEnumerable<double> values) {
            return Tan_N(values, -10.0);
        }

        private static double Tan_0_001(double value) {
            return Tan_N(value, 0.001);
        }

        private static double Tan_M0_001(double value) {
            return Tan_N(value, -0.001);
        }

        private static double Tan_0_01(double value) {
            return Tan_N(value, 0.01);
        }

        private static double Tan_M0_01(double value) {
            return Tan_N(value, -0.01);
        }

        private static double Tan_0_1(double value) {
            return Tan_N(value, 0.1);
        }

        private static double Tan_M0_1(double value) {
            return Tan_N(value, -0.1);
        }

        private static double Tan_0_5(double value) {
            return Tan_N(value, 0.5);
        }

        private static double Tan_M0_5(double value) {
            return Tan_N(value, -0.5);
        }

        private static double Tan_1(double value) {
            return Tan_N(value, 1.0);
        }

        private static double Tan_M1(double value) {
            return Tan_N(value, -1.0);
        }

        private static double Tan_1_5(double value) {
            return Tan_N(value, 1.5);
        }

        private static double Tan_M1_5(double value) {
            return Tan_N(value, -1.5);
        }

        private static double Tan_2(double value) {
            return Tan_N(value, 2.0);
        }

        private static double Tan_M2(double value) {
            return Tan_N(value, -2.0);
        }

        private static double Tan_2_5(double value) {
            return Tan_N(value, 2.5);
        }

        private static double Tan_M2_5(double value) {
            return Tan_N(value, -2.5);
        }

        private static double Tan_3(double value) {
            return Tan_N(value, 3.0);
        }

        private static double Tan_M3(double value) {
            return Tan_N(value, -3.0);
        }

        private static double Tan_3_5(double value) {
            return Tan_N(value, 3.5);
        }

        private static double Tan_M3_5(double value) {
            return Tan_N(value, -3.5);
        }

        private static double Tan_4(double value) {
            return Tan_N(value, 4.0);
        }

        private static double Tan_M4(double value) {
            return Tan_N(value, -4.0);
        }

        private static double Tan_4_5(double value) {
            return Tan_N(value, 4.5);
        }

        private static double Tan_M4_5(double value) {
            return Tan_N(value, -4.5);
        }

        private static double Tan_5(double value) {
            return Tan_N(value, 5.0);
        }

        private static double Tan_M5(double value) {
            return Tan_N(value, -5.0);
        }

        private static double Tan_6(double value) {
            return Tan_N(value, 6.0);
        }

        private static double Tan_M6(double value) {
            return Tan_N(value, -6.0);
        }

        private static double Tan_7(double value) {
            return Tan_N(value, 7.0);
        }

        private static double Tan_M7(double value) {
            return Tan_N(value, -7.0);
        }

        private static double Tan_8(double value) {
            return Tan_N(value, 8.0);
        }

        private static double Tan_M8(double value) {
            return Tan_N(value, -8.0);
        }

        private static double Tan_9(double value) {
            return Tan_N(value, 9.0);
        }

        private static double Tan_M9(double value) {
            return Tan_N(value, -9.0);
        }

        private static double Tan_10(double value) {
            return Tan_N(value, 10.0);
        }

        private static double Tan_M10(double value) {
            return Tan_N(value, -10.0);
        }

        /// <summary>
        /// Get the tg value from each number in the list multiple by alpha
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>List of tg of values multiple by alpha</returns>
        public static List<double> Tan_N(IEnumerable<double> values, double alpha) {
            List<double> result = new List<double>();

            foreach (var elem in values) {
                result.Add(Math.Tan(elem * alpha));
            }

            return result;
        }

        /// <summary>
        /// Get the tg from value multiple by alpha 
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>Tg value</returns>
        public static double Tan_N(double value, double alpha) {
            return Math.Tan(value * alpha);
        }

        private static List<double> Atan_0_001(IEnumerable<double> values) {
            return Atan_N(values, 0.001);
        }

        private static List<double> Atan_M0_001(IEnumerable<double> values) {
            return Atan_N(values, -0.001);
        }

        private static List<double> Atan_0_01(IEnumerable<double> values) {
            return Atan_N(values, 0.01);
        }

        private static List<double> Atan_M0_01(IEnumerable<double> values) {
            return Atan_N(values, -0.01);
        }

        private static List<double> Atan_0_1(IEnumerable<double> values) {
            return Atan_N(values, 0.1);
        }

        private static List<double> Atan_M0_1(IEnumerable<double> values) {
            return Atan_N(values, -0.1);
        }

        private static List<double> Atan_0_5(IEnumerable<double> values) {
            return Atan_N(values, 0.5);
        }

        private static List<double> Atan_M0_5(IEnumerable<double> values) {
            return Atan_N(values, -0.5);
        }

        private static List<double> Atan_1(IEnumerable<double> values) {
            return Atan_N(values, 1.0);
        }

        private static List<double> Atan_M1(IEnumerable<double> values) {
            return Atan_N(values, -1.0);
        }

        private static List<double> Atan_1_5(IEnumerable<double> values) {
            return Atan_N(values, 1.5);
        }

        private static List<double> Atan_M1_5(IEnumerable<double> values) {
            return Atan_N(values, -1.5);
        }

        private static List<double> Atan_2(IEnumerable<double> values) {
            return Atan_N(values, 2.0);
        }

        private static List<double> Atan_M2(IEnumerable<double> values) {
            return Atan_N(values, -2.0);
        }

        private static List<double> Atan_2_5(IEnumerable<double> values) {
            return Atan_N(values, 2.5);
        }

        private static List<double> Atan_M2_5(IEnumerable<double> values) {
            return Atan_N(values, -2.5);
        }

        private static List<double> Atan_3(IEnumerable<double> values) {
            return Atan_N(values, 3.0);
        }

        private static List<double> Atan_M3(IEnumerable<double> values) {
            return Atan_N(values, -3.0);
        }

        private static List<double> Atan_3_5(IEnumerable<double> values) {
            return Atan_N(values, 3.5);
        }

        private static List<double> Atan_M3_5(IEnumerable<double> values) {
            return Atan_N(values, -3.5);
        }

        private static List<double> Atan_4(IEnumerable<double> values) {
            return Atan_N(values, 4.0);
        }

        private static List<double> Atan_M4(IEnumerable<double> values) {
            return Atan_N(values, -4.0);
        }

        private static List<double> Atan_4_5(IEnumerable<double> values) {
            return Atan_N(values, 4.5);
        }

        private static List<double> Atan_M4_5(IEnumerable<double> values) {
            return Atan_N(values, -4.5);
        }

        private static List<double> Atan_5(IEnumerable<double> values) {
            return Atan_N(values, 5.0);
        }

        private static List<double> Atan_M5(IEnumerable<double> values) {
            return Atan_N(values, -5.0);
        }

        private static List<double> Atan_6(IEnumerable<double> values) {
            return Atan_N(values, 6.0);
        }

        private static List<double> Atan_M6(IEnumerable<double> values) {
            return Atan_N(values, -6.0);
        }

        private static List<double> Atan_7(IEnumerable<double> values) {
            return Atan_N(values, 7.0);
        }

        private static List<double> Atan_M7(IEnumerable<double> values) {
            return Atan_N(values, -7.0);
        }

        private static List<double> Atan_8(IEnumerable<double> values) {
            return Atan_N(values, 8.0);
        }

        private static List<double> Atan_M8(IEnumerable<double> values) {
            return Atan_N(values, -8.0);
        }

        private static List<double> Atan_9(IEnumerable<double> values) {
            return Atan_N(values, 9.0);
        }

        private static List<double> Atan_M9(IEnumerable<double> values) {
            return Atan_N(values, -9.0);
        }

        private static List<double> Atan_10(IEnumerable<double> values) {
            return Atan_N(values, 10.0);
        }

        private static List<double> Atan_M10(IEnumerable<double> values) {
            return Atan_N(values, -10.0);
        }

        private static double Atan_0_001(double value) {
            return Atan_N(value, 0.001);
        }

        private static double Atan_M0_001(double value) {
            return Atan_N(value, -0.001);
        }

        private static double Atan_0_01(double value) {
            return Atan_N(value, 0.01);
        }

        private static double Atan_M0_01(double value) {
            return Atan_N(value, -0.01);
        }

        private static double Atan_0_1(double value) {
            return Atan_N(value, 0.1);
        }

        private static double Atan_M0_1(double value) {
            return Atan_N(value, -0.1);
        }

        private static double Atan_0_5(double value) {
            return Atan_N(value, 0.5);
        }

        private static double Atan_M0_5(double value) {
            return Atan_N(value, -0.5);
        }

        private static double Atan_1(double value) {
            return Atan_N(value, 1.0);
        }

        private static double Atan_M1(double value) {
            return Atan_N(value, -1.0);
        }

        private static double Atan_1_5(double value) {
            return Atan_N(value, 1.5);
        }

        private static double Atan_M1_5(double value) {
            return Atan_N(value, -1.5);
        }

        private static double Atan_2(double value) {
            return Atan_N(value, 2.0);
        }

        private static double Atan_M2(double value) {
            return Atan_N(value, -2.0);
        }

        private static double Atan_2_5(double value) {
            return Atan_N(value, 2.5);
        }

        private static double Atan_M2_5(double value) {
            return Atan_N(value, -2.5);
        }

        private static double Atan_3(double value) {
            return Atan_N(value, 3.0);
        }

        private static double Atan_M3(double value) {
            return Atan_N(value, -3.0);
        }

        private static double Atan_3_5(double value) {
            return Atan_N(value, 3.5);
        }

        private static double Atan_M3_5(double value) {
            return Atan_N(value, -3.5);
        }

        private static double Atan_4(double value) {
            return Atan_N(value, 4.0);
        }

        private static double Atan_M4(double value) {
            return Atan_N(value, -4.0);
        }

        private static double Atan_4_5(double value) {
            return Atan_N(value, 4.5);
        }

        private static double Atan_M4_5(double value) {
            return Atan_N(value, -4.5);
        }

        private static double Atan_5(double value) {
            return Atan_N(value, 5.0);
        }

        private static double Atan_M5(double value) {
            return Atan_N(value, -5.0);
        }

        private static double Atan_6(double value) {
            return Atan_N(value, 6.0);
        }

        private static double Atan_M6(double value) {
            return Atan_N(value, -6.0);
        }

        private static double Atan_7(double value) {
            return Atan_N(value, 7.0);
        }

        private static double Atan_M7(double value) {
            return Atan_N(value, -7.0);
        }

        private static double Atan_8(double value) {
            return Atan_N(value, 8.0);
        }

        private static double Atan_M8(double value) {
            return Atan_N(value, -8.0);
        }

        private static double Atan_9(double value) {
            return Atan_N(value, 9.0);
        }

        private static double Atan_M9(double value) {
            return Atan_N(value, -9.0);
        }

        private static double Atan_10(double value) {
            return Atan_N(value, 10.0);
        }

        private static double Atan_M10(double value) {
            return Atan_N(value, -10.0);
        }

        /// <summary>
        /// Get the arctg value from each number in the list multiple by alpha
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>List of arctg of values multiple by alpha</returns>
        public static List<double> Atan_N(IEnumerable<double> values, double alpha) {
            List<double> result = new List<double>();

            foreach (var elem in values) {
                result.Add(Math.Atan(elem * alpha));
            }

            return result;
        }

        /// <summary>
        /// Get the arctg from value multiple by alpha
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>Atan value</returns>
        public static double Atan_N(double value, double alpha) {
            return Math.Atan(value * alpha);
        }

        private static List<double> ExpX_0_001(IEnumerable<double> values) {
            return ExpX_N(values, 0.001);
        }

        private static List<double> ExpX_M0_001(IEnumerable<double> values) {
            return ExpX_N(values, -0.001);
        }

        private static List<double> ExpX_0_01(IEnumerable<double> values) {
            return ExpX_N(values, 0.01);
        }

        private static List<double> ExpX_M0_01(IEnumerable<double> values) {
            return ExpX_N(values, -0.01);
        }

        private static List<double> ExpX_0_1(IEnumerable<double> values) {
            return ExpX_N(values, 0.1);
        }

        private static List<double> ExpX_M0_1(IEnumerable<double> values) {
            return ExpX_N(values, -0.1);
        }

        private static List<double> ExpX_0_5(IEnumerable<double> values) {
            return ExpX_N(values, 0.5);
        }

        private static List<double> ExpX_M0_5(IEnumerable<double> values) {
            return ExpX_N(values, -0.5);
        }

        private static List<double> ExpX_1(IEnumerable<double> values) {
            return ExpX_N(values, 1.0);
        }

        private static List<double> ExpX_M1(IEnumerable<double> values) {
            return ExpX_N(values, -1.0);
        }

        private static List<double> ExpX_1_5(IEnumerable<double> values) {
            return ExpX_N(values, 1.5);
        }

        private static List<double> ExpX_M1_5(IEnumerable<double> values) {
            return ExpX_N(values, -1.5);
        }

        private static List<double> ExpX_2(IEnumerable<double> values) {
            return ExpX_N(values, 2.0);
        }

        private static List<double> ExpX_M2(IEnumerable<double> values) {
            return ExpX_N(values, -2.0);
        }

        private static List<double> ExpX_2_5(IEnumerable<double> values) {
            return ExpX_N(values, 2.5);
        }

        private static List<double> ExpX_M2_5(IEnumerable<double> values) {
            return ExpX_N(values, -2.5);
        }

        private static List<double> ExpX_3(IEnumerable<double> values) {
            return ExpX_N(values, 3.0);
        }

        private static List<double> ExpX_M3(IEnumerable<double> values) {
            return ExpX_N(values, -3.0);
        }

        private static List<double> ExpX_3_5(IEnumerable<double> values) {
            return ExpX_N(values, 3.5);
        }

        private static List<double> ExpX_M3_5(IEnumerable<double> values) {
            return ExpX_N(values, -3.5);
        }

        private static List<double> ExpX_4(IEnumerable<double> values) {
            return ExpX_N(values, 4.0);
        }

        private static List<double> ExpX_M4(IEnumerable<double> values) {
            return ExpX_N(values, -4.0);
        }

        private static List<double> ExpX_4_5(IEnumerable<double> values) {
            return ExpX_N(values, 4.5);
        }

        private static List<double> ExpX_M4_5(IEnumerable<double> values) {
            return ExpX_N(values, -4.5);
        }

        private static List<double> ExpX_5(IEnumerable<double> values) {
            return ExpX_N(values, 5.0);
        }

        private static List<double> ExpX_M5(IEnumerable<double> values) {
            return ExpX_N(values, -5.0);
        }

        private static List<double> ExpX_6(IEnumerable<double> values) {
            return ExpX_N(values, 6.0);
        }

        private static List<double> ExpX_M6(IEnumerable<double> values) {
            return ExpX_N(values, -6.0);
        }

        private static List<double> ExpX_7(IEnumerable<double> values) {
            return ExpX_N(values, 7.0);
        }

        private static List<double> ExpX_M7(IEnumerable<double> values) {
            return ExpX_N(values, -7.0);
        }

        private static List<double> ExpX_8(IEnumerable<double> values) {
            return ExpX_N(values, 8.0);
        }

        private static List<double> ExpX_M8(IEnumerable<double> values) {
            return ExpX_N(values, -8.0);
        }

        private static List<double> ExpX_9(IEnumerable<double> values) {
            return ExpX_N(values, 9.0);
        }

        private static List<double> ExpX_M9(IEnumerable<double> values) {
            return ExpX_N(values, -9.0);
        }

        private static List<double> ExpX_10(IEnumerable<double> values) {
            return ExpX_N(values, 10.0);
        }

        private static List<double> ExpX_M10(IEnumerable<double> values) {
            return ExpX_N(values, -10.0);
        }

        private static double ExpX_0_001(double value) {
            return ExpX_N(value, 0.001);
        }

        private static double ExpX_M0_001(double value) {
            return ExpX_N(value, -0.001);
        }

        private static double ExpX_0_01(double value) {
            return ExpX_N(value, 0.01);
        }

        private static double ExpX_M0_01(double value) {
            return ExpX_N(value, -0.01);
        }

        private static double ExpX_0_1(double value) {
            return ExpX_N(value, 0.1);
        }

        private static double ExpX_M0_1(double value) {
            return ExpX_N(value, -0.1);
        }

        private static double ExpX_0_5(double value) {
            return ExpX_N(value, 0.5);
        }

        private static double ExpX_M0_5(double value) {
            return ExpX_N(value, -0.5);
        }

        private static double ExpX_1(double value) {
            return ExpX_N(value, 1.0);
        }

        private static double ExpX_M1(double value) {
            return ExpX_N(value, -1.0);
        }

        private static double ExpX_1_5(double value) {
            return ExpX_N(value, 1.5);
        }

        private static double ExpX_M1_5(double value) {
            return ExpX_N(value, -1.5);
        }

        private static double ExpX_2(double value) {
            return ExpX_N(value, 2.0);
        }

        private static double ExpX_M2(double value) {
            return ExpX_N(value, -2.0);
        }

        private static double ExpX_2_5(double value) {
            return ExpX_N(value, 2.5);
        }

        private static double ExpX_M2_5(double value) {
            return ExpX_N(value, -2.5);
        }

        private static double ExpX_3(double value) {
            return ExpX_N(value, 3.0);
        }

        private static double ExpX_M3(double value) {
            return ExpX_N(value, -3.0);
        }

        private static double ExpX_3_5(double value) {
            return ExpX_N(value, 3.5);
        }

        private static double ExpX_M3_5(double value) {
            return ExpX_N(value, -3.5);
        }

        private static double ExpX_4(double value) {
            return ExpX_N(value, 4.0);
        }

        private static double ExpX_M4(double value) {
            return ExpX_N(value, -4.0);
        }

        private static double ExpX_4_5(double value) {
            return ExpX_N(value, 4.5);
        }

        private static double ExpX_M4_5(double value) {
            return ExpX_N(value, -4.5);
        }

        private static double ExpX_5(double value) {
            return ExpX_N(value, 5.0);
        }

        private static double ExpX_M5(double value) {
            return ExpX_N(value, -5.0);
        }

        private static double ExpX_6(double value) {
            return ExpX_N(value, 6.0);
        }

        private static double ExpX_M6(double value) {
            return ExpX_N(value, -6.0);
        }

        private static double ExpX_7(double value) {
            return ExpX_N(value, 7.0);
        }

        private static double ExpX_M7(double value) {
            return ExpX_N(value, -7.0);
        }

        private static double ExpX_8(double value) {
            return ExpX_N(value, 8.0);
        }

        private static double ExpX_M8(double value) {
            return ExpX_N(value, -8.0);
        }

        private static double ExpX_9(double value) {
            return ExpX_N(value, 9.0);
        }

        private static double ExpX_M9(double value) {
            return ExpX_N(value, -9.0);
        }

        private static double ExpX_10(double value) {
            return ExpX_N(value, 10.0);
        }

        private static double ExpX_M10(double value) {
            return ExpX_N(value, -10.0);
        }

        /// <summary>
        /// Get the exp value from each number in the list multiple by alpha
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>List of exp of values multiple by alpha</returns>
        public static List<double> ExpX_N(IEnumerable<double> values, double alpha) {
            List<double> result = new List<double>();

            foreach (var elem in values) {
                result.Add(Math.Exp(elem * alpha));
            }

            return result;
        }

        /// <summary>
        /// Get the exp from value multiple by alpha
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>Exp value</returns>
        public static double ExpX_N(double value, double alpha) {
            return Math.Exp(value * alpha);
        }

        private static List<double> Exp_X2_0_001(IEnumerable<double> values) {
            return Exp_X2_N(values, 0.001);
        }

        private static List<double> Exp_X2_M0_001(IEnumerable<double> values) {
            return Exp_X2_N(values, -0.001);
        }

        private static List<double> Exp_X2_0_01(IEnumerable<double> values) {
            return Exp_X2_N(values, 0.01);
        }

        private static List<double> Exp_X2_M0_01(IEnumerable<double> values) {
            return Exp_X2_N(values, -0.01);
        }

        private static List<double> Exp_X2_0_1(IEnumerable<double> values) {
            return Exp_X2_N(values, 0.1);
        }

        private static List<double> Exp_X2_M0_1(IEnumerable<double> values) {
            return Exp_X2_N(values, -0.1);
        }

        private static List<double> Exp_X2_0_5(IEnumerable<double> values) {
            return Exp_X2_N(values, 0.5);
        }

        private static List<double> Exp_X2_M0_5(IEnumerable<double> values) {
            return Exp_X2_N(values, -0.5);
        }

        private static List<double> Exp_X2_1(IEnumerable<double> values) {
            return Exp_X2_N(values, 1.0);
        }

        private static List<double> Exp_X2_M1(IEnumerable<double> values) {
            return Exp_X2_N(values, -1.0);
        }

        private static List<double> Exp_X2_1_5(IEnumerable<double> values) {
            return Exp_X2_N(values, 1.5);
        }

        private static List<double> Exp_X2_M1_5(IEnumerable<double> values) {
            return Exp_X2_N(values, -1.5);
        }

        private static List<double> Exp_X2_2(IEnumerable<double> values) {
            return Exp_X2_N(values, 2.0);
        }

        private static List<double> Exp_X2_M2(IEnumerable<double> values) {
            return Exp_X2_N(values, -2.0);
        }

        private static List<double> Exp_X2_2_5(IEnumerable<double> values) {
            return Exp_X2_N(values, 2.5);
        }

        private static List<double> Exp_X2_M2_5(IEnumerable<double> values) {
            return Exp_X2_N(values, -2.5);
        }

        private static List<double> Exp_X2_3(IEnumerable<double> values) {
            return Exp_X2_N(values, 3.0);
        }

        private static List<double> Exp_X2_M3(IEnumerable<double> values) {
            return Exp_X2_N(values, -3.0);
        }

        private static List<double> Exp_X2_3_5(IEnumerable<double> values) {
            return Exp_X2_N(values, 3.5);
        }

        private static List<double> Exp_X2_M3_5(IEnumerable<double> values) {
            return Exp_X2_N(values, -3.5);
        }

        private static List<double> Exp_X2_4(IEnumerable<double> values) {
            return Exp_X2_N(values, 4.0);
        }

        private static List<double> Exp_X2_M4(IEnumerable<double> values) {
            return Exp_X2_N(values, -4.0);
        }

        private static List<double> Exp_X2_4_5(IEnumerable<double> values) {
            return Exp_X2_N(values, 4.5);
        }

        private static List<double> Exp_X2_M4_5(IEnumerable<double> values) {
            return Exp_X2_N(values, -4.5);
        }

        private static List<double> Exp_X2_5(IEnumerable<double> values) {
            return Exp_X2_N(values, 5.0);
        }

        private static List<double> Exp_X2_M5(IEnumerable<double> values) {
            return Exp_X2_N(values, -5.0);
        }

        private static List<double> Exp_X2_6(IEnumerable<double> values) {
            return Exp_X2_N(values, 6.0);
        }

        private static List<double> Exp_X2_M6(IEnumerable<double> values) {
            return Exp_X2_N(values, -6.0);
        }

        private static List<double> Exp_X2_7(IEnumerable<double> values) {
            return Exp_X2_N(values, 7.0);
        }

        private static List<double> Exp_X2_M7(IEnumerable<double> values) {
            return Exp_X2_N(values, -7.0);
        }

        private static List<double> Exp_X2_8(IEnumerable<double> values) {
            return Exp_X2_N(values, 8.0);
        }

        private static List<double> Exp_X2_M8(IEnumerable<double> values) {
            return Exp_X2_N(values, -8.0);
        }

        private static List<double> Exp_X2_9(IEnumerable<double> values) {
            return Exp_X2_N(values, 9.0);
        }

        private static List<double> Exp_X2_M9(IEnumerable<double> values) {
            return Exp_X2_N(values, -9.0);
        }

        private static List<double> Exp_X2_10(IEnumerable<double> values) {
            return Exp_X2_N(values, 10.0);
        }

        private static List<double> Exp_X2_M10(IEnumerable<double> values) {
            return Exp_X2_N(values, -10.0);
        }

        private static double Exp_X2_0_001(double value) {
            return Exp_X2_N(value, 0.001);
        }

        private static double Exp_X2_M0_001(double value) {
            return Exp_X2_N(value, -0.001);
        }

        private static double Exp_X2_0_01(double value) {
            return Exp_X2_N(value, 0.01);
        }

        private static double Exp_X2_M0_01(double value) {
            return Exp_X2_N(value, -0.01);
        }

        private static double Exp_X2_0_1(double value) {
            return Exp_X2_N(value, 0.1);
        }

        private static double Exp_X2_M0_1(double value) {
            return Exp_X2_N(value, -0.1);
        }

        private static double Exp_X2_0_5(double value) {
            return Exp_X2_N(value, 0.5);
        }

        private static double Exp_X2_M0_5(double value) {
            return Exp_X2_N(value, -0.5);
        }

        private static double Exp_X2_1(double value) {
            return Exp_X2_N(value, 1.0);
        }

        private static double Exp_X2_M1(double value) {
            return Exp_X2_N(value, -1.0);
        }

        private static double Exp_X2_1_5(double value) {
            return Exp_X2_N(value, 1.5);
        }

        private static double Exp_X2_M1_5(double value) {
            return Exp_X2_N(value, -1.5);
        }

        private static double Exp_X2_2(double value) {
            return Exp_X2_N(value, 2.0);
        }

        private static double Exp_X2_M2(double value) {
            return Exp_X2_N(value, -2.0);
        }

        private static double Exp_X2_2_5(double value) {
            return Exp_X2_N(value, 2.5);
        }

        private static double Exp_X2_M2_5(double value) {
            return Exp_X2_N(value, -2.5);
        }

        private static double Exp_X2_3(double value) {
            return Exp_X2_N(value, 3.0);
        }

        private static double Exp_X2_M3(double value) {
            return Exp_X2_N(value, -3.0);
        }

        private static double Exp_X2_3_5(double value) {
            return Exp_X2_N(value, 3.5);
        }

        private static double Exp_X2_M3_5(double value) {
            return Exp_X2_N(value, -3.5);
        }

        private static double Exp_X2_4(double value) {
            return Exp_X2_N(value, 4.0);
        }

        private static double Exp_X2_M4(double value) {
            return Exp_X2_N(value, -4.0);
        }

        private static double Exp_X2_4_5(double value) {
            return Exp_X2_N(value, 4.5);
        }

        private static double Exp_X2_M4_5(double value) {
            return Exp_X2_N(value, -4.5);
        }

        private static double Exp_X2_5(double value) {
            return Exp_X2_N(value, 5.0);
        }

        private static double Exp_X2_M5(double value) {
            return Exp_X2_N(value, -5.0);
        }

        private static double Exp_X2_6(double value) {
            return Exp_X2_N(value, 6.0);
        }

        private static double Exp_X2_M6(double value) {
            return Exp_X2_N(value, -6.0);
        }

        private static double Exp_X2_7(double value) {
            return Exp_X2_N(value, 7.0);
        }

        private static double Exp_X2_M7(double value) {
            return Exp_X2_N(value, -7.0);
        }

        private static double Exp_X2_8(double value) {
            return Exp_X2_N(value, 8.0);
        }

        private static double Exp_X2_M8(double value) {
            return Exp_X2_N(value, -8.0);
        }

        private static double Exp_X2_9(double value) {
            return Exp_X2_N(value, 9.0);
        }

        private static double Exp_X2_M9(double value) {
            return Exp_X2_N(value, -9.0);
        }

        private static double Exp_X2_10(double value) {
            return Exp_X2_N(value, 10.0);
        }

        private static double Exp_X2_M10(double value) {
            return Exp_X2_N(value, -10.0);
        }

        /// <summary>
        /// Get the exp value from each number in the list multiple by alpha
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>List of exp of values multiple by alpha</returns>
        public static List<double> Exp_X2_N(IEnumerable<double> values, double alpha) {
            List<double> result = new List<double>();

            foreach (var elem in values) {
                result.Add(Math.Exp(Math.Pow(elem, 2) * alpha));
            }

            return result;
        }

        /// <summary>
        /// Get the exp from multiple by alpha
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>Exp value</returns>
        public static double Exp_X2_N(double value, double alpha) {
            return Math.Exp(Math.Pow(value, 2) * alpha);
        }

        private static List<double> Sigm_0_001(IEnumerable<double> values) {
            return Sigm_X(values, 0.001);
        }

        private static List<double> Sigm_M0_001(IEnumerable<double> values) {
            return Sigm_X(values, -0.001);
        }

        private static List<double> Sigm_0_01(IEnumerable<double> values) {
            return Sigm_X(values, 0.01);
        }

        private static List<double> Sigm_M0_01(IEnumerable<double> values) {
            return Sigm_X(values, -0.01);
        }

        private static List<double> Sigm_0_1(IEnumerable<double> values) {
            return Sigm_X(values, 0.1);
        }

        private static List<double> Sigm_M0_1(IEnumerable<double> values) {
            return Sigm_X(values, -0.1);
        }

        private static List<double> Sigm_0_5(IEnumerable<double> values) {
            return Sigm_X(values, 0.5);
        }

        private static List<double> Sigm_M0_5(IEnumerable<double> values) {
            return Sigm_X(values, -0.5);
        }

        private static List<double> Sigm_1(IEnumerable<double> values) {
            return Sigm_X(values, 1.0);
        }

        private static List<double> Sigm_M1(IEnumerable<double> values) {
            return Sigm_X(values, -1.0);
        }

        private static List<double> Sigm_1_5(IEnumerable<double> values) {
            return Sigm_X(values, 1.5);
        }

        private static List<double> Sigm_M1_5(IEnumerable<double> values) {
            return Sigm_X(values, -1.5);
        }

        private static List<double> Sigm_2(IEnumerable<double> values) {
            return Sigm_X(values, 2.0);
        }

        private static List<double> Sigm_M2(IEnumerable<double> values) {
            return Sigm_X(values, -2.0);
        }

        private static List<double> Sigm_2_5(IEnumerable<double> values) {
            return Sigm_X(values, 2.5);
        }

        private static List<double> Sigm_M2_5(IEnumerable<double> values) {
            return Sigm_X(values, -2.5);
        }

        private static List<double> Sigm_3(IEnumerable<double> values) {
            return Sigm_X(values, 3.0);
        }

        private static List<double> Sigm_M3(IEnumerable<double> values) {
            return Sigm_X(values, -3.0);
        }

        private static List<double> Sigm_3_5(IEnumerable<double> values) {
            return Sigm_X(values, 3.5);
        }

        private static List<double> Sigm_M3_5(IEnumerable<double> values) {
            return Sigm_X(values, -3.5);
        }

        private static List<double> Sigm_4(IEnumerable<double> values) {
            return Sigm_X(values, 4.0);
        }

        private static List<double> Sigm_M4(IEnumerable<double> values) {
            return Sigm_X(values, -4.0);
        }

        private static List<double> Sigm_4_5(IEnumerable<double> values) {
            return Sigm_X(values, 4.5);
        }

        private static List<double> Sigm_M4_5(IEnumerable<double> values) {
            return Sigm_X(values, -4.5);
        }

        private static List<double> Sigm_5(IEnumerable<double> values) {
            return Sigm_X(values, 5.0);
        }

        private static List<double> Sigm_M5(IEnumerable<double> values) {
            return Sigm_X(values, -5.0);
        }

        private static List<double> Sigm_6(IEnumerable<double> values) {
            return Sigm_X(values, 6.0);
        }

        private static List<double> Sigm_M6(IEnumerable<double> values) {
            return Sigm_X(values, -6.0);
        }

        private static List<double> Sigm_7(IEnumerable<double> values) {
            return Sigm_X(values, 7.0);
        }

        private static List<double> Sigm_M7(IEnumerable<double> values) {
            return Sigm_X(values, -7.0);
        }

        private static List<double> Sigm_8(IEnumerable<double> values) {
            return Sigm_X(values, 8.0);
        }

        private static List<double> Sigm_M8(IEnumerable<double> values) {
            return Sigm_X(values, -8.0);
        }

        private static List<double> Sigm_9(IEnumerable<double> values) {
            return Sigm_X(values, 9.0);
        }

        private static List<double> Sigm_M9(IEnumerable<double> values) {
            return Sigm_X(values, -9.0);
        }

        private static List<double> Sigm_10(IEnumerable<double> values) {
            return Sigm_X(values, 10.0);
        }

        private static List<double> Sigm_M10(IEnumerable<double> values) {
            return Sigm_X(values, -10.0);
        }

        private static double Sigm_0_001(double value) {
            return Sigm_X(value, 0.001);
        }

        private static double Sigm_M0_001(double value) {
            return Sigm_X(value, -0.001);
        }

        private static double Sigm_0_01(double value) {
            return Sigm_X(value, 0.01);
        }

        private static double Sigm_M0_01(double value) {
            return Sigm_X(value, -0.01);
        }

        private static double Sigm_0_1(double value) {
            return Sigm_X(value, 0.1);
        }

        private static double Sigm_M0_1(double value) {
            return Sigm_X(value, -0.1);
        }

        private static double Sigm_0_5(double value) {
            return Sigm_X(value, 0.5);
        }

        private static double Sigm_M0_5(double value) {
            return Sigm_X(value, -0.5);
        }

        private static double Sigm_1(double value) {
            return Sigm_X(value, 1.0);
        }

        private static double Sigm_M1(double value) {
            return Sigm_X(value, -1.0);
        }

        private static double Sigm_1_5(double value) {
            return Sigm_X(value, 1.5);
        }

        private static double Sigm_M1_5(double value) {
            return Sigm_X(value, -1.5);
        }

        private static double Sigm_2(double value) {
            return Sigm_X(value, 2.0);
        }

        private static double Sigm_M2(double value) {
            return Sigm_X(value, -2.0);
        }

        private static double Sigm_2_5(double value) {
            return Sigm_X(value, 2.5);
        }

        private static double Sigm_M2_5(double value) {
            return Sigm_X(value, -2.5);
        }

        private static double Sigm_3(double value) {
            return Sigm_X(value, 3.0);
        }

        private static double Sigm_M3(double value) {
            return Sigm_X(value, -3.0);
        }

        private static double Sigm_3_5(double value) {
            return Sigm_X(value, 3.5);
        }

        private static double Sigm_M3_5(double value) {
            return Sigm_X(value, -3.5);
        }

        private static double Sigm_4(double value) {
            return Sigm_X(value, 4.0);
        }

        private static double Sigm_M4(double value) {
            return Sigm_X(value, -4.0);
        }

        private static double Sigm_4_5(double value) {
            return Sigm_X(value, 4.5);
        }

        private static double Sigm_M4_5(double value) {
            return Sigm_X(value, -4.5);
        }

        private static double Sigm_5(double value) {
            return Sigm_X(value, 5.0);
        }

        private static double Sigm_M5(double value) {
            return Sigm_X(value, -5.0);
        }

        private static double Sigm_6(double value) {
            return Sigm_X(value, 6.0);
        }

        private static double Sigm_M6(double value) {
            return Sigm_X(value, -6.0);
        }

        private static double Sigm_7(double value) {
            return Sigm_X(value, 7.0);
        }

        private static double Sigm_M7(double value) {
            return Sigm_X(value, -7.0);
        }

        private static double Sigm_8(double value) {
            return Sigm_X(value, 8.0);
        }

        private static double Sigm_M8(double value) {
            return Sigm_X(value, -8.0);
        }

        private static double Sigm_9(double value) {
            return Sigm_X(value, 9.0);
        }

        private static double Sigm_M9(double value) {
            return Sigm_X(value, -9.0);
        }

        private static double Sigm_10(double value) {
            return Sigm_X(value, 10.0);
        }

        private static double Sigm_M10(double value) {
            return Sigm_X(value, -10.0);
        }

        /// <summary>
        /// Get the sigm x value from each number in the list multiple by alpha
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>List of sigm x of values multiple by alpha</returns>
        public static List<double> Sigm_X(IEnumerable<double> values, double alpha) {
            List<double> result = new List<double>();

            foreach (var elem in values) {
                result.Add(1.0 / (1.0 + Math.Exp(elem * alpha)));
            }

            return result;
        }

        /// <summary>
        /// Get the sigm from x value multiple by alpha
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>Sigm value</returns>
        public static double Sigm_X(double value, double alpha) {
            return 1.0 / (1.0 + Math.Exp(value * alpha));
        }

        private static List<double> Exp_Exp_0_001(IEnumerable<double> values) {
            return Exp_Exp(values, 0.001);
        }

        private static List<double> Exp_Exp_M0_001(IEnumerable<double> values) {
            return Exp_Exp(values, -0.001);
        }

        private static List<double> Exp_Exp_0_01(IEnumerable<double> values) {
            return Exp_Exp(values, 0.01);
        }

        private static List<double> Exp_Exp_M0_01(IEnumerable<double> values) {
            return Exp_Exp(values, -0.01);
        }

        private static List<double> Exp_Exp_0_1(IEnumerable<double> values) {
            return Exp_Exp(values, 0.1);
        }

        private static List<double> Exp_Exp_M0_1(IEnumerable<double> values) {
            return Exp_Exp(values, -0.1);
        }

        private static List<double> Exp_Exp_0_5(IEnumerable<double> values) {
            return Exp_Exp(values, 0.5);
        }

        private static List<double> Exp_Exp_M0_5(IEnumerable<double> values) {
            return Exp_Exp(values, -0.5);
        }

        private static List<double> Exp_Exp_1(IEnumerable<double> values) {
            return Exp_Exp(values, 1.0);
        }

        private static List<double> Exp_Exp_M1(IEnumerable<double> values) {
            return Exp_Exp(values, -1.0);
        }

        private static List<double> Exp_Exp_1_5(IEnumerable<double> values) {
            return Exp_Exp(values, 1.5);
        }

        private static List<double> Exp_Exp_M1_5(IEnumerable<double> values) {
            return Exp_Exp(values, -1.5);
        }

        private static List<double> Exp_Exp_2(IEnumerable<double> values) {
            return Exp_Exp(values, 2.0);
        }

        private static List<double> Exp_Exp_M2(IEnumerable<double> values) {
            return Exp_Exp(values, -2.0);
        }

        private static List<double> Exp_Exp_2_5(IEnumerable<double> values) {
            return Exp_Exp(values, 2.5);
        }

        private static List<double> Exp_Exp_M2_5(IEnumerable<double> values) {
            return Exp_Exp(values, -2.5);
        }

        private static List<double> Exp_Exp_3(IEnumerable<double> values) {
            return Exp_Exp(values, 3.0);
        }

        private static List<double> Exp_Exp_M3(IEnumerable<double> values) {
            return Exp_Exp(values, -3.0);
        }

        private static List<double> Exp_Exp_3_5(IEnumerable<double> values) {
            return Exp_Exp(values, 3.5);
        }

        private static List<double> Exp_Exp_M3_5(IEnumerable<double> values) {
            return Exp_Exp(values, -3.5);
        }

        private static List<double> Exp_Exp_4(IEnumerable<double> values) {
            return Exp_Exp(values, 4.0);
        }

        private static List<double> Exp_Exp_M4(IEnumerable<double> values) {
            return Exp_Exp(values, -4.0);
        }

        private static List<double> Exp_Exp_4_5(IEnumerable<double> values) {
            return Exp_Exp(values, 4.5);
        }

        private static List<double> Exp_Exp_M4_5(IEnumerable<double> values) {
            return Exp_Exp(values, -4.5);
        }

        private static List<double> Exp_Exp_5(IEnumerable<double> values) {
            return Exp_Exp(values, 5.0);
        }

        private static List<double> Exp_Exp_M5(IEnumerable<double> values) {
            return Exp_Exp(values, -5.0);
        }

        private static List<double> Exp_Exp_6(IEnumerable<double> values) {
            return Exp_Exp(values, 6.0);
        }

        private static List<double> Exp_Exp_M6(IEnumerable<double> values) {
            return Exp_Exp(values, -6.0);
        }

        private static List<double> Exp_Exp_7(IEnumerable<double> values) {
            return Exp_Exp(values, 7.0);
        }

        private static List<double> Exp_Exp_M7(IEnumerable<double> values) {
            return Exp_Exp(values, -7.0);
        }

        private static List<double> Exp_Exp_8(IEnumerable<double> values) {
            return Exp_Exp(values, 8.0);
        }

        private static List<double> Exp_Exp_M8(IEnumerable<double> values) {
            return Exp_Exp(values, -8.0);
        }

        private static List<double> Exp_Exp_9(IEnumerable<double> values) {
            return Exp_Exp(values, 9.0);
        }

        private static List<double> Exp_Exp_M9(IEnumerable<double> values) {
            return Exp_Exp(values, -9.0);
        }

        private static List<double> Exp_Exp_10(IEnumerable<double> values) {
            return Exp_Exp(values, 10.0);
        }

        private static List<double> Exp_Exp_M10(IEnumerable<double> values) {
            return Exp_Exp(values, -10.0);
        }

        private static double Exp_Exp_0_001(double value) {
            return Exp_Exp(value, 0.001);
        }

        private static double Exp_Exp_M0_001(double value) {
            return Exp_Exp(value, -0.001);
        }

        private static double Exp_Exp_0_01(double value) {
            return Exp_Exp(value, 0.01);
        }

        private static double Exp_Exp_M0_01(double value) {
            return Exp_Exp(value, -0.01);
        }

        private static double Exp_Exp_0_1(double value) {
            return Exp_Exp(value, 0.1);
        }

        private static double Exp_Exp_M0_1(double value) {
            return Exp_Exp(value, -0.1);
        }

        private static double Exp_Exp_0_5(double value) {
            return Exp_Exp(value, 0.5);
        }

        private static double Exp_Exp_M0_5(double value) {
            return Exp_Exp(value, -0.5);
        }

        private static double Exp_Exp_1(double value) {
            return Exp_Exp(value, 1.0);
        }

        private static double Exp_Exp_M1(double value) {
            return Exp_Exp(value, -1.0);
        }

        private static double Exp_Exp_1_5(double value) {
            return Exp_Exp(value, 1.5);
        }

        private static double Exp_Exp_M1_5(double value) {
            return Exp_Exp(value, -1.5);
        }

        private static double Exp_Exp_2(double value) {
            return Exp_Exp(value, 2.0);
        }

        private static double Exp_Exp_M2(double value) {
            return Exp_Exp(value, -2.0);
        }

        private static double Exp_Exp_2_5(double value) {
            return Exp_Exp(value, 2.5);
        }

        private static double Exp_Exp_M2_5(double value) {
            return Exp_Exp(value, -2.5);
        }

        private static double Exp_Exp_3(double value) {
            return Exp_Exp(value, 3.0);
        }

        private static double Exp_Exp_M3(double value) {
            return Exp_Exp(value, -3.0);
        }

        private static double Exp_Exp_3_5(double value) {
            return Exp_Exp(value, 3.5);
        }

        private static double Exp_Exp_M3_5(double value) {
            return Exp_Exp(value, -3.5);
        }

        private static double Exp_Exp_4(double value) {
            return Exp_Exp(value, 4.0);
        }

        private static double Exp_Exp_M4(double value) {
            return Exp_Exp(value, -4.0);
        }

        private static double Exp_Exp_4_5(double value) {
            return Exp_Exp(value, 4.5);
        }

        private static double Exp_Exp_M4_5(double value) {
            return Exp_Exp(value, -4.5);
        }

        private static double Exp_Exp_5(double value) {
            return Exp_Exp(value, 5.0);
        }

        private static double Exp_Exp_M5(double value) {
            return Exp_Exp(value, -5.0);
        }

        private static double Exp_Exp_6(double value) {
            return Exp_Exp(value, 6.0);
        }

        private static double Exp_Exp_M6(double value) {
            return Exp_Exp(value, -6.0);
        }

        private static double Exp_Exp_7(double value) {
            return Exp_Exp(value, 7.0);
        }

        private static double Exp_Exp_M7(double value) {
            return Exp_Exp(value, -7.0);
        }

        private static double Exp_Exp_8(double value) {
            return Exp_Exp(value, 8.0);
        }

        private static double Exp_Exp_M8(double value) {
            return Exp_Exp(value, -8.0);
        }

        private static double Exp_Exp_9(double value) {
            return Exp_Exp(value, 9.0);
        }

        private static double Exp_Exp_M9(double value) {
            return Exp_Exp(value, -9.0);
        }

        private static double Exp_Exp_10(double value) {
            return Exp_Exp(value, 10.0);
        }

        private static double Exp_Exp_M10(double value) {
            return Exp_Exp(value, -10.0);
        }

        /// <summary>
        /// Get the (exp-1)/(exp+1) value from each number in the list multiple by alpha
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>List of (exp-1)/(exp+1) of values multiple by alpha</returns>
        public static List<double> Exp_Exp(IEnumerable<double> values, double alpha) {
            List<double> result = new List<double>();

            foreach (var elem in values) {
                result.Add((Math.Exp(elem * alpha) - 1) / (1.0 + Math.Exp(elem * alpha)));
            }

            return result;
        }

        /// <summary>
        /// Get the (exp-1)/(exp+1) from value multiple by alpha
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>(exp-1)/(exp+1) value</returns>
        public static double Exp_Exp(double value, double alpha) {
            return (Math.Exp(value * alpha) - 1) / (1.0 + Math.Exp(value * alpha));
        }

        private static List<double> Exp_Exp_2_0_001(IEnumerable<double> values) {
            return Exp_Exp_2(values, 0.001);
        }

        private static List<double> Exp_Exp_2_M0_001(IEnumerable<double> values) {
            return Exp_Exp_2(values, -0.001);
        }

        private static List<double> Exp_Exp_2_0_01(IEnumerable<double> values) {
            return Exp_Exp_2(values, 0.01);
        }

        private static List<double> Exp_Exp_2_M0_01(IEnumerable<double> values) {
            return Exp_Exp_2(values, -0.01);
        }

        private static List<double> Exp_Exp_2_0_1(IEnumerable<double> values) {
            return Exp_Exp_2(values, 0.1);
        }

        private static List<double> Exp_Exp_2_M0_1(IEnumerable<double> values) {
            return Exp_Exp_2(values, -0.1);
        }

        private static List<double> Exp_Exp_2_0_5(IEnumerable<double> values) {
            return Exp_Exp_2(values, 0.5);
        }

        private static List<double> Exp_Exp_2_M0_5(IEnumerable<double> values) {
            return Exp_Exp_2(values, -0.5);
        }

        private static List<double> Exp_Exp_2_1(IEnumerable<double> values) {
            return Exp_Exp_2(values, 1.0);
        }

        private static List<double> Exp_Exp_2_M1(IEnumerable<double> values) {
            return Exp_Exp_2(values, -1.0);
        }

        private static List<double> Exp_Exp_2_1_5(IEnumerable<double> values) {
            return Exp_Exp_2(values, 1.5);
        }

        private static List<double> Exp_Exp_2_M1_5(IEnumerable<double> values) {
            return Exp_Exp_2(values, -1.5);
        }

        private static List<double> Exp_Exp_2_2(IEnumerable<double> values) {
            return Exp_Exp_2(values, 2.0);
        }

        private static List<double> Exp_Exp_2_M2(IEnumerable<double> values) {
            return Exp_Exp_2(values, -2.0);
        }

        private static List<double> Exp_Exp_2_2_5(IEnumerable<double> values) {
            return Exp_Exp_2(values, 2.5);
        }

        private static List<double> Exp_Exp_2_M2_5(IEnumerable<double> values) {
            return Exp_Exp_2(values, -2.5);
        }

        private static List<double> Exp_Exp_2_3(IEnumerable<double> values) {
            return Exp_Exp_2(values, 3.0);
        }

        private static List<double> Exp_Exp_2_M3(IEnumerable<double> values) {
            return Exp_Exp_2(values, -3.0);
        }

        private static List<double> Exp_Exp_2_3_5(IEnumerable<double> values) {
            return Exp_Exp_2(values, 3.5);
        }

        private static List<double> Exp_Exp_2_M3_5(IEnumerable<double> values) {
            return Exp_Exp_2(values, -3.5);
        }

        private static List<double> Exp_Exp_2_4(IEnumerable<double> values) {
            return Exp_Exp_2(values, 4.0);
        }

        private static List<double> Exp_Exp_2_M4(IEnumerable<double> values) {
            return Exp_Exp_2(values, -4.0);
        }

        private static List<double> Exp_Exp_2_4_5(IEnumerable<double> values) {
            return Exp_Exp_2(values, 4.5);
        }

        private static List<double> Exp_Exp_2_M4_5(IEnumerable<double> values) {
            return Exp_Exp_2(values, -4.5);
        }

        private static List<double> Exp_Exp_2__5(IEnumerable<double> values) {
            return Exp_Exp_2(values, 5.0);
        }

        private static List<double> Exp_Exp_2_M5(IEnumerable<double> values) {
            return Exp_Exp_2(values, -5.0);
        }

        private static List<double> Exp_Exp_2_6(IEnumerable<double> values) {
            return Exp_Exp_2(values, 6.0);
        }

        private static List<double> Exp_Exp_2_M6(IEnumerable<double> values) {
            return Exp_Exp_2(values, -6.0);
        }

        private static List<double> Exp_Exp_2_7(IEnumerable<double> values) {
            return Exp_Exp_2(values, 7.0);
        }

        private static List<double> Exp_Exp_2_M7(IEnumerable<double> values) {
            return Exp_Exp_2(values, -7.0);
        }

        private static List<double> Exp_Exp_2_8(IEnumerable<double> values) {
            return Exp_Exp_2(values, 8.0);
        }

        private static List<double> Exp_Exp_2_M8(IEnumerable<double> values) {
            return Exp_Exp_2(values, -8.0);
        }

        private static List<double> Exp_Exp_2_9(IEnumerable<double> values) {
            return Exp_Exp_2(values, 9.0);
        }

        private static List<double> Exp_Exp_2_M9(IEnumerable<double> values) {
            return Exp_Exp_2(values, -9.0);
        }

        private static List<double> Exp_Exp_2_10(IEnumerable<double> values) {
            return Exp_Exp_2(values, 10.0);
        }

        private static List<double> Exp_Exp_2_M10(IEnumerable<double> values) {
            return Exp_Exp_2(values, -10.0);
        }

        private static double Exp_Exp_2_0_001(double value) {
            return Exp_Exp_2(value, 0.001);
        }

        private static double Exp_Exp_2_M0_001(double value) {
            return Exp_Exp_2(value, -0.001);
        }

        private static double Exp_Exp_2_0_01(double value) {
            return Exp_Exp_2(value, 0.01);
        }

        private static double Exp_Exp_2_M0_01(double value) {
            return Exp_Exp_2(value, -0.01);
        }

        private static double Exp_Exp_2_0_1(double value) {
            return Exp_Exp_2(value, 0.1);
        }

        private static double Exp_Exp_2_M0_1(double value) {
            return Exp_Exp_2(value, -0.1);
        }

        private static double Exp_Exp_2_0_5(double value) {
            return Exp_Exp_2(value, 0.5);
        }

        private static double Exp_Exp_2_M0_5(double value) {
            return Exp_Exp_2(value, -0.5);
        }

        private static double Exp_Exp_2_1(double value) {
            return Exp_Exp_2(value, 1.0);
        }

        private static double Exp_Exp_2_M1(double value) {
            return Exp_Exp_2(value, -1.0);
        }

        private static double Exp_Exp_2_1_5(double value) {
            return Exp_Exp_2(value, 1.5);
        }

        private static double Exp_Exp_2_M1_5(double value) {
            return Exp_Exp_2(value, -1.5);
        }

        private static double Exp_Exp_2_2(double value) {
            return Exp_Exp_2(value, 2.0);
        }

        private static double Exp_Exp_2_M2(double value) {
            return Exp_Exp_2(value, -2.0);
        }

        private static double Exp_Exp_2_2_5(double value) {
            return Exp_Exp_2(value, 2.5);
        }

        private static double Exp_Exp_2_M2_5(double value) {
            return Exp_Exp_2(value, -2.5);
        }

        private static double Exp_Exp_2_3(double value) {
            return Exp_Exp_2(value, 3.0);
        }

        private static double Exp_Exp_2_M3(double value) {
            return Exp_Exp_2(value, -3.0);
        }

        private static double Exp_Exp_2_3_5(double value) {
            return Exp_Exp_2(value, 3.5);
        }

        private static double Exp_Exp_2_M3_5(double value) {
            return Exp_Exp_2(value, -3.5);
        }

        private static double Exp_Exp_2_4(double value) {
            return Exp_Exp_2(value, 4.0);
        }

        private static double Exp_Exp_2_M4(double value) {
            return Exp_Exp_2(value, -4.0);
        }

        private static double Exp_Exp_2_4_5(double value) {
            return Exp_Exp_2(value, 4.5);
        }

        private static double Exp_Exp_2_M4_5(double value) {
            return Exp_Exp_2(value, -4.5);
        }

        private static double Exp_Exp_2__5(double value) {
            return Exp_Exp_2(value, 5.0);
        }

        private static double Exp_Exp_2_M5(double value) {
            return Exp_Exp_2(value, -5.0);
        }

        private static double Exp_Exp_2_6(double value) {
            return Exp_Exp_2(value, 6.0);
        }

        private static double Exp_Exp_2_M6(double value) {
            return Exp_Exp_2(value, -6.0);
        }

        private static double Exp_Exp_2_7(double value) {
            return Exp_Exp_2(value, 7.0);
        }

        private static double Exp_Exp_2_M7(double value) {
            return Exp_Exp_2(value, -7.0);
        }

        private static double Exp_Exp_2_8(double value) {
            return Exp_Exp_2(value, 8.0);
        }

        private static double Exp_Exp_2_M8(double value) {
            return Exp_Exp_2(value, -8.0);
        }

        private static double Exp_Exp_2_9(double value) {
            return Exp_Exp_2(value, 9.0);
        }

        private static double Exp_Exp_2_M9(double value) {
            return Exp_Exp_2(value, -9.0);
        }

        private static double Exp_Exp_2_10(double value) {
            return Exp_Exp_2(value, 10.0);
        }

        private static double Exp_Exp_2_M10(double value) {
            return Exp_Exp_2(value, -10.0);
        }

        /// <summary>
        /// Get the (exp-exp^-1)/2 value from each number in the list multiple by alpha
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>List of (exp-exp^-1)/2 of values multiple by alpha</returns>
        public static List<double> Exp_Exp_2(IEnumerable<double> values, double alpha) {
            List<double> result = new List<double>();

            foreach (var elem in values) {
                result.Add((Math.Exp(elem * alpha) - Math.Exp((-1.0) * alpha * elem)) / 2.0);
            }

            return result;
        }

        /// <summary>
        /// Get the (exp-exp^-1)/2 from value multiple by alpha
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>(exp-exp^-1)/2 value</returns>
        public static double Exp_Exp_2(double value, double alpha) {
            return (Math.Exp(value * alpha) - Math.Exp((-1.0) * alpha * value)) / 2.0;
        }
    }
}
