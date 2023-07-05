using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiple_Linear_Regression.Mathematic {
    public static class FunctionPreprocess {
        public static Dictionary<string, Func<List<double>, double>> Functions { get; } =
            new Dictionary<string, Func<List<double>, double>>() {
                { "Начальный момент 1-го порядка", Statistics.FirstOrderStartMoment},
                { "Начальный момент 2-го порядка",  Statistics.SecondOrderStartMoment},
                { "Начальный момент 3-го порядка",  Statistics.ThirdOrderStartMoment},
                { "Начальный момент 4-го порядка",  Statistics.FourthOrderStartMoment},
                { "Центральный момент 1-го порядка",  Statistics.FirstOrderCentralMoment},
                { "Центральный момент 2-го порядка",  Statistics.SecondOrderCentralMoment},
                { "Центральный момент 3-го порядка",  Statistics.ThirdOrderCentralMoment},
                { "Центральный момент 4-го порядка",  Statistics.FourthOrderCentralMoment},
                { "Минимум", Statistics.Min },
                { "Максимум", Statistics.Max },
                { "Коэффициент асимметрии", Statistics.AsymmetryCoefficient },
                { "Коэффициент эксцесса", Statistics.ExcessCoefficient },
                { "Медиана", Statistics.Median },
                { "Коэффициент вариации", Statistics.VariationCoefficient },
                { "Среднее на интервале (0, 1)", Statistics.AverageOnInterval_0_1 },
                { "Стандартное отклонение на интервале (0, 1)", Statistics.StandardDeviationOnInterval_0_1 },
                { "Коэффициент вариации на интервале (0, 1)", Statistics.VariationCoefficientOnInterval_0_1 },
                { "Стандартная ошибка на интервале (0, 1)", Statistics.StandardErrorOnInterval_0_1 },
                { "Waveform length (WL)", Statistics.WavefromLength },
                { "Kurtosis (KURT)", Statistics.Kurtosis }
            };

        public static Dictionary<string, Func<List<double>, List<double>>> PreprocessingFunctionsGusev { get; } =
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

        public static Dictionary<string, Func<List<double>, List<double>>> PreprocessingFunctionsOkunev { get; } =
            new Dictionary<string, Func<List<double>, List<double>>>() {
                { "x^(1/7)", Pow_1_7},
                { "x^(1/5)", Pow_1_5},
                { "x^(1/3)", Pow_1_3},
                { "x^2", Pow_X2},
                { "x^3", Pow_X3},
                { "e^(1,7*x)", ExpX_1_7},
                { "e^(-1,7*x)", ExpX_M1_7},
                { "e^(1,6*x)", ExpX_1_6},
                { "e^(-1,6*x)", ExpX_M1_6},
                { "e^(1,5*x)", ExpX_1_5},
                { "e^(-1,5*x)", ExpX_M1_5},
                { "e^(1,4*x)", ExpX_1_4},
                { "e^(-1,4*x)", ExpX_M1_4},
                { "e^(1,3*x)", ExpX_1_3},
                { "e^(-1,3*x)", ExpX_M1_3},
                { "e^(1,2*x)", ExpX_1_2},
                { "e^(-1,2*x)", ExpX_M1_2},
                { "e^(1,1*x)", ExpX_1_1},
                { "e^(-1,1*x)", ExpX_M1_1},
                { "e^(0,95*x)", ExpX_0_95},
                { "e^(-0,95*x)", ExpX_M0_95},
                { "e^(0,8*x)", ExpX_0_8},
                { "e^(-0,8*x)", ExpX_M0_8},
                { "e^(0,65*x)", ExpX_0_65},
                { "e^(-0,65*x)", ExpX_M0_65},
                { "e^(0,5*x)", ExpX_0_5},
                { "e^(-0,5*x)", ExpX_M0_5},
                { "e^(0,35*x)", ExpX_0_35},
                { "e^(-0,35*x)", ExpX_M0_35},
                { "e^(0,2*x)", ExpX_0_2},
                { "e^(-0,2*x)", ExpX_M0_2},
                { "cos(3*x)", Cos_3},
                { "cos(2,8*x)", Cos_2_8},
                { "cos(2,65*x)", Cos_2_65},
                { "cos(2,5*x)", Cos_2_5},
                { "cos(2,35*x)", Cos_2_35},
                { "cos(2,2*x)", Cos_2_2},
                { "cos(2,05*x)", Cos_2_05},
                { "cos(1,9*x)", Cos_1_9},
                { "cos(1,75*x)", Cos_1_75},
                { "cos(1,6*x)", Cos_1_6},
                { "cos(1,45*x)", Cos_1_45},
                { "cos(1,3*x)", Cos_1_3},
                { "cos(1,15*x)", Cos_1_15},
                { "cos(0,95*x)", Cos_0_95},
                { "cos(0,7*x)", Cos_0_7},
                { "sin(3*x)", Sin_3},
                { "sin(-3*x)", Sin_M3},
                { "sin(2,8*x)", Sin_2_8},
                { "sin(-2,8*x)", Sin_M2_8},
                { "sin(2,6*x)", Sin_2_6},
                { "sin(-2,6*x)", Sin_M2_6},
                { "sin(2,3*x)", Sin_2_3},
                { "sin(-2,3*x)", Sin_M2_3},
                { "sin(2,05*x)", Sin_2_05},
                { "sin(-2,05*x)", Sin_M2_05},
                { "sin(1,75*x)", Sin_1_75},
                { "sin(-1,75*x)", Sin_M1_75},
                { "sin(1,45*x)", Sin_1_45},
                { "sin(-1,45*x)", Sin_M1_45},
                { "sin(1,25*x)", Sin_1_25},
                { "sin(-1,25*x)", Sin_M1_25},
                { "sin(1,1*x)", Sin_1_1},
                { "sin(-1,1*x)", Sin_M1_1},
                { "sin(0,95*x)", Sin_0_95},
                { "sin(-0,95*x)", Sin_M0_95},
                { "sin(0,8*x)", Sin_0_8},
                { "sin(-0,8*x)", Sin_M0_8},
                { "sin(0,65*x)", Sin_0_65},
                { "sin(-0,65*x)", Sin_M0_65},
                { "sin(0,5*x)", Sin_0_5},
                { "sin(-0,5*x)", Sin_M0_5},
                { "sin(0,38*x)", Sin_0_38},
                { "sin(-0,38*x)", Sin_M0_38},
                { "sin(0,27*x)", Sin_0_27},
                { "sin(-0,27*x)", Sin_M0_27},
                { "sin(0,15*x)", Sin_0_15},
                { "sin(-0,15*x)", Sin_M0_15},
                { "cosh(2*x)", Cosh_2 },
                { "cosh(1,9*x)", Cosh_1_9 },
                { "cosh(1,8*x)", Cosh_1_8 },
                { "cosh(1,7*x)", Cosh_1_7 },
                { "cosh(1,6*x)", Cosh_1_6 },
                { "cosh(1,5*x)", Cosh_1_5 },
                { "cosh(1,4*x)", Cosh_1_4 },
                { "cosh(1,3*x)", Cosh_1_3 },
                { "cosh(1,2*x)", Cosh_1_2 },
                { "cosh(1*x)", Cosh_1 },
                { "cosh(0,8*x)", Cosh_0_8 },
                { "cosh(0,6*x)", Cosh_0_6 },
                { "sinh(2*x)", Sinh_2 },
                { "sinh(-2*x)", Sinh_M2 },
                { "sinh(1,9*x)", Sinh_1_9 },
                { "sinh(-1,9*x)", Sinh_M1_9 },
                { "sinh(1,8*x)", Sinh_1_8 },
                { "sinh(-1,8*x)", Sinh_M1_8 },
                { "sinh(1,7*x)", Sinh_1_7 },
                { "sinh(-1,7*x)", Sinh_M1_7 },
                { "sinh(1,55*x)", Sinh_1_55 },
                { "sinh(-1,55*x)", Sinh_M1_55 },
                { "sinh(1,4*x)", Sinh_1_4 },
                { "sinh(-1,4*x)", Sinh_M1_4 },
                { "sinh(1,25*x)", Sinh_1_25 },
                { "sinh(-1,25*x)", Sinh_M1_25 },
                { "sinh(1,1*x)", Sinh_1_1 },
                { "sinh(-1,1*x)", Sinh_M1_1 },
                { "sinh(0,95*x)", Sinh_0_95 },
                { "sinh(-0,95*x)", Sinh_M0_95 },
                { "sinh(0,75*x)", Sinh_0_75 },
                { "sinh(-0,75*x)", Sinh_M0_75 },
                { "sinh(0,55*x)", Sinh_0_55 },
                { "sinh(-0,55*x)", Sinh_M0_55 },
                { "sinh(0,35*x)", Sinh_0_35 },
                { "sinh(-0,35*x)", Sinh_M0_35 },
                { "sinh(0,15*x)", Sinh_0_15 },
                { "sinh(-0,15*x)", Sinh_M0_15 },
                { "tanh(3*x)", Tanh_3 },
                { "tanh(-3*x)", Tanh_M3 },
                { "tanh(2,2*x)", Tanh_2_2 },
                { "tanh(-2,2*x)", Tanh_M2_2 },
                { "tanh(1,7*x)", Tanh_1_7 },
                { "tanh(-1,7*x)", Tanh_M1_7 },
                { "tanh(1,35*x)", Tanh_1_35 },
                { "tanh(-1,35*x)", Tanh_M1_35 },
                { "tanh(1,1*x)", Tanh_1_1 },
                { "tanh(-1,1*x)", Tanh_M1_1 },
                { "tanh(0,9*x)", Tanh_0_9 },
                { "tanh(-0,9*x)", Tanh_M0_9 },
                { "tanh(0,7*x)", Tanh_0_7 },
                { "tanh(-0,7*x)", Tanh_M0_7 },
                { "tanh(0,55*x)", Tanh_0_55 },
                { "tanh(-0,55*x)", Tanh_M0_55 },
                { "tanh(0,4*x)", Tanh_0_4 },
                { "tanh(-0,4*x)", Tanh_M0_4 },
                { "tanh(0,25*x)", Tanh_0_25 },
                { "tanh(-0,25*x)", Tanh_M0_25 },
                { "tanh(0,1*x)", Tanh_0_1 },
                { "tanh(-0,1*x)", Tanh_M0_1 },
                { "arctg(3*x)", Atan_3},
                { "arctg(-3*x)", Atan_M3},
                { "arctg(2,5*x)", Atan_2_5},
                { "arctg(-2,5*x)", Atan_M2_5},
                { "arctg(2,1*x)", Atan_2_1},
                { "arctg(-2,1*x)", Atan_M2_1},
                { "arctg(1,7*x)", Atan_1_7},
                { "arctg(-1,7*x)", Atan_M1_7},
                { "arctg(1,4*x)", Atan_1_4},
                { "arctg(-1,4*x)", Atan_M1_4},
                { "arctg(1,15*x)", Atan_1_15},
                { "arctg(-1,15*x)", Atan_M1_15},
                { "arctg(0,95*x)", Atan_0_95},
                { "arctg(-0,95*x)", Atan_M0_95},
                { "arctg(0,75*x)", Atan_0_75},
                { "arctg(-0,75*x)", Atan_M0_75},
                { "arctg(0,55*x)", Atan_0_55},
                { "arctg(-0,55*x)", Atan_M0_55},
                { "arctg(0,4*x)", Atan_0_4},
                { "arctg(-0,4*x)", Atan_M0_4},
                { "arctg(0,25*x)", Atan_0_25},
                { "arctg(-0,25*x)", Atan_M0_25},
                { "arctg(0,1*x)", Atan_0_1},
                { "arctg(-0,1*x)", Atan_M0_1},
                { "ln(x + 1,2)", Log_1_2 },
                { "ln(x + 1,25)", Log_1_25 },
                { "ln(x + 1,3)", Log_1_3 },
                { "ln(x + 1,35)", Log_1_35 },
                { "ln(x + 1,4)", Log_1_4 },
                { "ln(x + 1,45)", Log_1_45 },
                { "ln(x + 1,5)", Log_1_5 },
                { "ln(x + 1,6)", Log_1_6 },
                { "ln(x + 1,7)", Log_1_7 },
                { "ln(x + 1,8)", Log_1_8 },
                { "ln(x + 1,9)", Log_1_9 },
                { "ln(x + 2)", Log_2 },
                { "ln(x + 2,1)", Log_2_1 },
                { "ln(x + 2,2)", Log_2_2 },
                { "ln(x + 2,3)", Log_2_3 },
                { "ln(x + 2,4)", Log_2_4 },
                { "ln(x + 2,5)", Log_2_5 },
                { "ln(x + 2,6)", Log_2_6 },
                { "ln(x + 2,7)", Log_2_7 },
                { "ln(x + 2,8)", Log_2_8 },
                { "ln(x + 2,9)", Log_2_9 },
                { "ln(x + 3)", Log_3 },
                { "ln(x + 3,1)", Log_3_1 },
                { "ln(x + 3,2)", Log_3_2 },
                { "ln(x + 3,3)", Log_3_3 },
                { "ln(x + 3,4)", Log_3_4 },
                { "ln(x + 3,5)", Log_3_5 },
                { "1/(1+e^(4*x))", Sigm_4 },
                { "1/(1+e^(-4*x))", Sigm_M4 },
                { "1/(1+e^(2,5*x))", Sigm_2_5 },
                { "1/(1+e^(-2,5*x))", Sigm_M2_5 },
                { "1/(1+e^(1,7*x))", Sigm_1_7 },
                { "1/(1+e^(-1,7*x))", Sigm_M1_7 },
                { "1/(1+e^(1,1*x))", Sigm_1_1 },
                { "1/(1+e^(-1,1*x))", Sigm_M1_1 },
                { "1/(1+e^(0,5*x))", Sigm_0_5 },
                { "1/(1+e^(-0,5*x))", Sigm_M0_5 },
                { "1/(0,6*(x + 1,1))", Betha_Alpha_1_1_0_6 },
                { "1/(0,7*(x + 1,1))", Betha_Alpha_1_1_0_7 },
                { "1/(0,9*(x + 1,1))", Betha_Alpha_1_1_0_9 },
                { "1/(1,1*(x + 1,1))", Betha_Alpha_1_1_1_1 },
                { "1/(1,3*(x + 1,1))", Betha_Alpha_1_1_1_3 },
                { "1/(1,6*(x + 1,1))", Betha_Alpha_1_1_1_6 },
                { "1/(2*(x + 1,1))", Betha_Alpha_1_1_2 },
                { "1/(2,5*(x + 1,1))", Betha_Alpha_1_1_2_5 },
                { "1/(3*(x + 1,1))", Betha_Alpha_1_1_3 },
                { "1/(4*(x + 1,1))", Betha_Alpha_1_1_4 },
                { "1/(5*(x + 1,1))", Betha_Alpha_1_1_5 },
                { "1/(6*(x + 1,1))", Betha_Alpha_1_1_6 },
                { "1/(7*(x + 1,1))", Betha_Alpha_1_1_7 },
                { "1/(8*(x + 1,1))", Betha_Alpha_1_1_8 },
                { "1/(0,6*(x + 1,5))", Betha_Alpha_1_5_0_6 },
                { "1/(0,65*(x + 1,5))", Betha_Alpha_1_5_0_65 },
                { "1/(0,7*(x + 1,5))", Betha_Alpha_1_5_0_7 },
                { "1/(0,75*(x + 1,5))", Betha_Alpha_1_5_0_75 },
                { "1/(0,8*(x + 1,5))", Betha_Alpha_1_5_0_8 },
                { "1/(0,9*(x + 1,5))", Betha_Alpha_1_5_0_9 },
                { "1/(x + 1,5)", Betha_Alpha_1_5_1 },
                { "1/(1,1*(x + 1,5))", Betha_Alpha_1_5_1_1 },
                { "1/(1,2*(x + 1,5))", Betha_Alpha_1_5_1_2 },
                { "1/(1,3*(x + 1,5))", Betha_Alpha_1_5_1_3 },
                { "1/(1,4*(x + 1,5))", Betha_Alpha_1_5_1_4 },
                { "1/(1,6*(x + 1,5))", Betha_Alpha_1_5_1_6 },
                { "1/(1,8*(x + 1,5))", Betha_Alpha_1_5_1_8 },
                { "1/(2*(x + 1,5))", Betha_Alpha_1_5_2 },
                { "1/(2,5*(x + 1,5))", Betha_Alpha_1_5_2_5 },
                { "1/(0,2*(x + 2))", Betha_Alpha_2_0_2 },
                { "1/(0,25*(x + 2))", Betha_Alpha_2_0_25 },
                { "1/(0,3*(x + 2))", Betha_Alpha_2_0_3 },
                { "1/(0,35*(x + 2))", Betha_Alpha_2_0_35 },
                { "1/(0,4*(x + 2))", Betha_Alpha_2_0_4 },
                { "1/(0,45*(x + 2))", Betha_Alpha_2_0_45 },
                { "1/(0,5*(x + 2))", Betha_Alpha_2_0_5 },
                { "1/(0,55*(x + 2))", Betha_Alpha_2_0_55 },
                { "1/(0,6*(x + 2))", Betha_Alpha_2_0_6 },
                { "1/(0,7*(x + 2))", Betha_Alpha_2_0_7 },
                { "1/(0,8*(x + 2))", Betha_Alpha_2_0_8 },
                { "1/(0,9*(x + 2))", Betha_Alpha_2_0_9 },
                { "1/(1,1*(x + 2))", Betha_Alpha_2_1_1 },
                { "1/(1,2*(x + 2))", Betha_Alpha_2_1_2 },
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

        private static List<double> Pow_1_7(IEnumerable<double> values) {
            return Pow_XN(values, (1.0 / 7.0));
        }

        private static List<double> Pow_1_5(IEnumerable<double> values) {
            return Pow_XN(values, (1.0 / 5.0));
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
            foreach (var elem in values) {
                result.Add(Math.Pow(elem, n));
            }

            return result;
        }

        /// <summary>
        /// Take the natural logarithm of each number in the list
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns> List of natural logaritm from values</returns>
        public static List<double> Log(IEnumerable<double> values) {
            List<double> result = new List<double>();

            foreach (var elem in values) {
                result.Add(Math.Log(elem));
            }

            return result;
        }

        private static List<double> Log_1_2(IEnumerable<double> values) {
            return Log_N(values, 1.2);
        }

        private static List<double> Log_1_25(IEnumerable<double> values) {
            return Log_N(values, 1.25);
        }

        private static List<double> Log_1_3(IEnumerable<double> values) {
            return Log_N(values, 1.3);
        }

        private static List<double> Log_1_35(IEnumerable<double> values) {
            return Log_N(values, 1.35);
        }

        private static List<double> Log_1_4(IEnumerable<double> values) {
            return Log_N(values, 1.4);
        }

        private static List<double> Log_1_45(IEnumerable<double> values) {
            return Log_N(values, 1.45);
        }

        private static List<double> Log_1_5(IEnumerable<double> values) {
            return Log_N(values, 1.5);
        }

        private static List<double> Log_1_6(IEnumerable<double> values) {
            return Log_N(values, 1.6);
        }

        private static List<double> Log_1_7(IEnumerable<double> values) {
            return Log_N(values, 1.7);
        }

        private static List<double> Log_1_8(IEnumerable<double> values) {
            return Log_N(values, 1.8);
        }

        private static List<double> Log_1_9(IEnumerable<double> values) {
            return Log_N(values, 1.9);
        }

        private static List<double> Log_2(IEnumerable<double> values) {
            return Log_N(values, 2.0);
        }

        private static List<double> Log_2_1(IEnumerable<double> values) {
            return Log_N(values, 2.1);
        }

        private static List<double> Log_2_2(IEnumerable<double> values) {
            return Log_N(values, 2.2);
        }

        private static List<double> Log_2_3(IEnumerable<double> values) {
            return Log_N(values, 2.3);
        }

        private static List<double> Log_2_4(IEnumerable<double> values) {
            return Log_N(values, 2.4);
        }

        private static List<double> Log_2_5(IEnumerable<double> values) {
            return Log_N(values, 2.5);
        }

        private static List<double> Log_2_6(IEnumerable<double> values) {
            return Log_N(values, 2.6);
        }

        private static List<double> Log_2_7(IEnumerable<double> values) {
            return Log_N(values, 2.7);
        }

        private static List<double> Log_2_8(IEnumerable<double> values) {
            return Log_N(values, 2.8);
        }

        private static List<double> Log_2_9(IEnumerable<double> values) {
            return Log_N(values, 2.9);
        }

        private static List<double> Log_3(IEnumerable<double> values) {
            return Log_N(values, 3.0);
        }

        private static List<double> Log_3_1(IEnumerable<double> values) {
            return Log_N(values, 3.1);
        }

        private static List<double> Log_3_2(IEnumerable<double> values) {
            return Log_N(values, 3.2);
        }

        private static List<double> Log_3_3(IEnumerable<double> values) {
            return Log_N(values, 3.3);
        }

        private static List<double> Log_3_4(IEnumerable<double> values) {
            return Log_N(values, 3.4);
        }

        private static List<double> Log_3_5(IEnumerable<double> values) {
            return Log_N(values, 3.5);
        }

        /// <summary>
        /// Take the natural logarithm of sum of number and alpha in the list
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>List of natural logarithm from sum of values and alpha</returns>
        public static List<double> Log_N(IEnumerable<double> values, double alpha) {
            List<double> result = new List<double>();

            foreach(var elem in values) {
                result.Add(Math.Log(elem + alpha));
            }

            return result;
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

        private static List<double> Sin_0_15(IEnumerable<double> values) {
            return Sin_N(values, 0.15);
        }

        private static List<double> Sin_M0_15(IEnumerable<double> values) {
            return Sin_N(values, -0.15);
        }

        private static List<double> Sin_0_27(IEnumerable<double> values) {
            return Sin_N(values, 0.27);
        }

        private static List<double> Sin_M0_27(IEnumerable<double> values) {
            return Sin_N(values, -0.27);
        }

        private static List<double> Sin_0_38(IEnumerable<double> values) {
            return Sin_N(values, 0.38);
        }

        private static List<double> Sin_M0_38(IEnumerable<double> values) {
            return Sin_N(values, -0.38);
        }

        private static List<double> Sin_0_5(IEnumerable<double> values) {
            return Sin_N(values, 0.5);
        }

        private static List<double> Sin_M0_5(IEnumerable<double> values) {
            return Sin_N(values, -0.5);
        }

        private static List<double> Sin_0_65(IEnumerable<double> values) {
            return Sin_N(values, 0.65);
        }

        private static List<double> Sin_M0_65(IEnumerable<double> values) {
            return Sin_N(values, -0.65);
        }

        private static List<double> Sin_0_8(IEnumerable<double> values) {
            return Sin_N(values, 0.8);
        }

        private static List<double> Sin_M0_8(IEnumerable<double> values) {
            return Sin_N(values, -0.8);
        }

        private static List<double> Sin_0_95(IEnumerable<double> values) {
            return Sin_N(values, 0.95);
        }

        private static List<double> Sin_M0_95(IEnumerable<double> values) {
            return Sin_N(values, -0.95);
        }

        private static List<double> Sin_1(IEnumerable<double> values) {
            return Sin_N(values, 1.0);
        }

        private static List<double> Sin_M1(IEnumerable<double> values) {
            return Sin_N(values, -1.0);
        }

        private static List<double> Sin_1_1(IEnumerable<double> values) {
            return Sin_N(values, 1.1);
        }

        private static List<double> Sin_M1_1(IEnumerable<double> values) {
            return Sin_N(values, -1.1);
        }

        private static List<double> Sin_1_25(IEnumerable<double> values) {
            return Sin_N(values, 1.25);
        }

        private static List<double> Sin_M1_25(IEnumerable<double> values) {
            return Sin_N(values, -1.25);
        }

        private static List<double> Sin_1_45(IEnumerable<double> values) {
            return Sin_N(values, 1.45);
        }

        private static List<double> Sin_M1_45(IEnumerable<double> values) {
            return Sin_N(values, -1.45);
        }

        private static List<double> Sin_1_5(IEnumerable<double> values) {
            return Sin_N(values, 1.5);
        }

        private static List<double> Sin_M1_5(IEnumerable<double> values) {
            return Sin_N(values, -1.5);
        }

        private static List<double> Sin_1_75(IEnumerable<double> values) {
            return Sin_N(values, 1.75);
        }

        private static List<double> Sin_M1_75(IEnumerable<double> values) {
            return Sin_N(values, -1.75);
        }

        private static List<double> Sin_2(IEnumerable<double> values) {
            return Sin_N(values, 2.0);
        }

        private static List<double> Sin_M2(IEnumerable<double> values) {
            return Sin_N(values, -2.0);
        }

        private static List<double> Sin_2_05(IEnumerable<double> values) {
            return Sin_N(values, 2.05);
        }

        private static List<double> Sin_M2_05(IEnumerable<double> values) {
            return Sin_N(values, -2.05);
        }

        private static List<double> Sin_2_3(IEnumerable<double> values) {
            return Sin_N(values, 2.3);
        }

        private static List<double> Sin_M2_3(IEnumerable<double> values) {
            return Sin_N(values, -2.3);
        }

        private static List<double> Sin_2_5(IEnumerable<double> values) {
            return Sin_N(values, 2.5);
        }

        private static List<double> Sin_M2_5(IEnumerable<double> values) {
            return Sin_N(values, -2.5);
        }

        private static List<double> Sin_2_6(IEnumerable<double> values) {
            return Sin_N(values, 2.6);
        }

        private static List<double> Sin_M2_6(IEnumerable<double> values) {
            return Sin_N(values, -2.6);
        }

        private static List<double> Sin_2_8(IEnumerable<double> values) {
            return Sin_N(values, 2.8);
        }

        private static List<double> Sin_M2_8(IEnumerable<double> values) {
            return Sin_N(values, -2.8);
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

        /// <summary>
        /// Get the sin value from each number in the list multiple by alpha
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>List of sin of values multiple by alpha</returns>
        public static List<double> Sin_N(IEnumerable<double> values, double alpha) {
            List<double> result = new List<double>();

            foreach (var elem in values) {
                result.Add(Math.Sin(elem * alpha));
            }

            return result;
        }

        private static List<double> Cos_3(IEnumerable<double> values) {
            return Cos_N(values, 3.0);
        }

        private static List<double> Cos_2_8(IEnumerable<double> values) {
            return Cos_N(values, 2.8);
        }

        private static List<double> Cos_2_65(IEnumerable<double> values) {
            return Cos_N(values, 2.65);
        }

        private static List<double> Cos_2_5(IEnumerable<double> values) {
            return Cos_N(values, 2.5);
        }

        private static List<double> Cos_2_35(IEnumerable<double> values) {
            return Cos_N(values, 2.35);
        }

        private static List<double> Cos_2_2(IEnumerable<double> values) {
            return Cos_N(values, 2.2);
        }
        private static List<double> Cos_2_05(IEnumerable<double> values) {
            return Cos_N(values, 2.05);
        }

        private static List<double> Cos_1_9(IEnumerable<double> values) {
            return Cos_N(values, 1.9);
        }

        private static List<double> Cos_1_75(IEnumerable<double> values) {
            return Cos_N(values, 1.75);
        }

        private static List<double> Cos_1_6(IEnumerable<double> values) {
            return Cos_N(values, 1.6);
        }

        private static List<double> Cos_1_45(IEnumerable<double> values) {
            return Cos_N(values, 1.45);
        }

        private static List<double> Cos_1_3(IEnumerable<double> values) {
            return Cos_N(values, 1.3);
        }

        private static List<double> Cos_1_15(IEnumerable<double> values) {
            return Cos_N(values, 1.15);
        }

        private static List<double> Cos_0_95(IEnumerable<double> values) {
            return Cos_N(values, 0.95);
        }

        private static List<double> Cos_0_7(IEnumerable<double> values) {
            return Cos_N(values, 0.7);
        }

        /// <summary>
        /// Get the cos value from each number in the list multiple by alpha
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>List of cos of values multiple by alpha</returns>
        public static List<double> Cos_N(IEnumerable<double> values, double alpha) {
            List<double> result = new List<double>();

            foreach (var elem in values) {
                result.Add(Math.Cos(elem * alpha));
            }

            return result;
        }

        private static List<double> Cosh_2(IEnumerable<double> values) {
            return Cosh_N(values, 2.0);
        }

        private static List<double> Cosh_1_9(IEnumerable<double> values) {
            return Cosh_N(values, 1.9);
        }

        private static List<double> Cosh_1_8(IEnumerable<double> values) {
            return Cosh_N(values, 1.8);
        }

        private static List<double> Cosh_1_7(IEnumerable<double> values) {
            return Cosh_N(values, 1.7);
        }

        private static List<double> Cosh_1_6(IEnumerable<double> values) {
            return Cosh_N(values, 1.6);
        }

        private static List<double> Cosh_1_5(IEnumerable<double> values) {
            return Cosh_N(values, 1.5);
        }

        private static List<double> Cosh_1_4(IEnumerable<double> values) {
            return Cosh_N(values, 1.4);
        }

        private static List<double> Cosh_1_3(IEnumerable<double> values) {
            return Cosh_N(values, 1.3);
        }

        private static List<double> Cosh_1_2(IEnumerable<double> values) {
            return Cosh_N(values, 1.2);
        }

        private static List<double> Cosh_1(IEnumerable<double> values) {
            return Cosh_N(values, 1.0);
        }

        private static List<double> Cosh_0_8(IEnumerable<double> values) {
            return Cosh_N(values, 0.8);
        }

        private static List<double> Cosh_0_6(IEnumerable<double> values) {
            return Cosh_N(values, 0.6);
        }

        /// <summary>
        /// Get the cosh value from each number in the list multiple by alpha
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>List of cosh of values multiple by alpha</returns>
        public static List<double> Cosh_N(IEnumerable<double> values, double alpha) {
            List<double> result = new List<double>();

            foreach (var elem in values) {
                result.Add(Math.Cosh(elem * alpha));
            }

            return result;
        }

        private static List<double> Sinh_2(IEnumerable<double> values) {
            return Sinh_N(values, 2.0);
        }

        private static List<double> Sinh_M2(IEnumerable<double> values) {
            return Sinh_N(values, -2.0);
        }

        private static List<double> Sinh_1_9(IEnumerable<double> values) {
            return Sinh_N(values, 1.9);
        }

        private static List<double> Sinh_M1_9(IEnumerable<double> values) {
            return Sinh_N(values, -1.9);
        }

        private static List<double> Sinh_1_8(IEnumerable<double> values) {
            return Sinh_N(values, 1.8);
        }

        private static List<double> Sinh_M1_8(IEnumerable<double> values) {
            return Sinh_N(values, -1.8);
        }

        private static List<double> Sinh_1_7(IEnumerable<double> values) {
            return Sinh_N(values, 1.7);
        }

        private static List<double> Sinh_M1_7(IEnumerable<double> values) {
            return Sinh_N(values, -1.7);
        }

        private static List<double> Sinh_1_55(IEnumerable<double> values) {
            return Sinh_N(values, 1.55);
        }

        private static List<double> Sinh_M1_55(IEnumerable<double> values) {
            return Sinh_N(values, -1.55);
        }

        private static List<double> Sinh_1_4(IEnumerable<double> values) {
            return Sinh_N(values, 1.4);
        }

        private static List<double> Sinh_M1_4(IEnumerable<double> values) {
            return Sinh_N(values, -1.4);
        }

        private static List<double> Sinh_1_25(IEnumerable<double> values) {
            return Sinh_N(values, 1.25);
        }

        private static List<double> Sinh_M1_25(IEnumerable<double> values) {
            return Sinh_N(values, -1.25);
        }

        private static List<double> Sinh_1_1(IEnumerable<double> values) {
            return Sinh_N(values, 1.1);
        }

        private static List<double> Sinh_M1_1(IEnumerable<double> values) {
            return Sinh_N(values, -1.1);
        }

        private static List<double> Sinh_0_95(IEnumerable<double> values) {
            return Sinh_N(values, 0.95);
        }

        private static List<double> Sinh_M0_95(IEnumerable<double> values) {
            return Sinh_N(values, -0.95);
        }

        private static List<double> Sinh_0_75(IEnumerable<double> values) {
            return Sinh_N(values, 0.75);
        }

        private static List<double> Sinh_M0_75(IEnumerable<double> values) {
            return Sinh_N(values, -0.75);
        }

        private static List<double> Sinh_0_55(IEnumerable<double> values) {
            return Sinh_N(values, 0.55);
        }

        private static List<double> Sinh_M0_55(IEnumerable<double> values) {
            return Sinh_N(values, -0.55);
        }

        private static List<double> Sinh_0_35(IEnumerable<double> values) {
            return Sinh_N(values, 0.35);
        }

        private static List<double> Sinh_M0_35(IEnumerable<double> values) {
            return Sinh_N(values, -0.35);
        }

        private static List<double> Sinh_0_15(IEnumerable<double> values) {
            return Sinh_N(values, 0.15);
        }

        private static List<double> Sinh_M0_15(IEnumerable<double> values) {
            return Sinh_N(values, -0.15);
        }

        /// <summary>
        /// Get the sinh value from each number in the list multiple by alpha
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>List of sinh of values multiple by alpha</returns>
        public static List<double> Sinh_N(IEnumerable<double> values, double alpha) {
            List<double> result = new List<double>();

            foreach (var elem in values) {
                result.Add(Math.Sinh(elem * alpha));
            }

            return result;
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

        private static List<double> Tanh_3(IEnumerable<double> values) {
            return Tanh_N(values, 3.0);
        }

        private static List<double> Tanh_M3(IEnumerable<double> values) {
            return Tanh_N(values, -3.0);
        }

        private static List<double> Tanh_2_2(IEnumerable<double> values) {
            return Tanh_N(values, 2.2);
        }

        private static List<double> Tanh_M2_2(IEnumerable<double> values) {
            return Tanh_N(values, -2.2);
        }

        private static List<double> Tanh_1_7(IEnumerable<double> values) {
            return Tanh_N(values, 1.7);
        }

        private static List<double> Tanh_M1_7(IEnumerable<double> values) {
            return Tanh_N(values, -1.7);
        }

        private static List<double> Tanh_1_35(IEnumerable<double> values) {
            return Tanh_N(values, 1.35);
        }

        private static List<double> Tanh_M1_35(IEnumerable<double> values) {
            return Tanh_N(values, -1.35);
        }

        private static List<double> Tanh_1_1(IEnumerable<double> values) {
            return Tanh_N(values, 1.1);
        }

        private static List<double> Tanh_M1_1(IEnumerable<double> values) {
            return Tanh_N(values, -1.1);
        }

        private static List<double> Tanh_0_9(IEnumerable<double> values) {
            return Tanh_N(values, 0.9);
        }

        private static List<double> Tanh_M0_9(IEnumerable<double> values) {
            return Tanh_N(values, -0.9);
        }

        private static List<double> Tanh_0_7(IEnumerable<double> values) {
            return Tanh_N(values, 0.7);
        }

        private static List<double> Tanh_M0_7(IEnumerable<double> values) {
            return Tanh_N(values, -0.7);
        }

        private static List<double> Tanh_0_55(IEnumerable<double> values) {
            return Tanh_N(values, 0.55);
        }

        private static List<double> Tanh_M0_55(IEnumerable<double> values) {
            return Tanh_N(values, -0.55);
        }

        private static List<double> Tanh_0_4(IEnumerable<double> values) {
            return Tanh_N(values, 0.4);
        }

        private static List<double> Tanh_M0_4(IEnumerable<double> values) {
            return Tanh_N(values, -0.4);
        }

        private static List<double> Tanh_0_25(IEnumerable<double> values) {
            return Tanh_N(values, 0.25);
        }

        private static List<double> Tanh_M0_25(IEnumerable<double> values) {
            return Tanh_N(values, -0.25);
        }

        private static List<double> Tanh_0_1(IEnumerable<double> values) {
            return Tanh_N(values, 0.1);
        }

        private static List<double> Tanh_M0_1(IEnumerable<double> values) {
            return Tanh_N(values, -0.1);
        }

        /// <summary>
        /// Get the th value from each number in the list multiple by alpha
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>List of th of values multiple by alpha</returns>
        public static List<double> Tanh_N(IEnumerable<double> values, double alpha) {
            List<double> result = new List<double>();

            foreach(var elem in values) {
                result.Add(Math.Tanh(elem * alpha));
            }

            return result;
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

        private static List<double> Atan_0_25(IEnumerable<double> values) {
            return Atan_N(values, 0.25);
        }

        private static List<double> Atan_M0_25(IEnumerable<double> values) {
            return Atan_N(values, -0.25);
        }

        private static List<double> Atan_0_4(IEnumerable<double> values) {
            return Atan_N(values, 0.4);
        }

        private static List<double> Atan_M0_4(IEnumerable<double> values) {
            return Atan_N(values, -0.4);
        }

        private static List<double> Atan_0_5(IEnumerable<double> values) {
            return Atan_N(values, 0.5);
        }

        private static List<double> Atan_M0_5(IEnumerable<double> values) {
            return Atan_N(values, -0.5);
        }
        private static List<double> Atan_0_55(IEnumerable<double> values) {
            return Atan_N(values, 0.55);
        }

        private static List<double> Atan_M0_55(IEnumerable<double> values) {
            return Atan_N(values, -0.55);
        }

        private static List<double> Atan_0_75(IEnumerable<double> values) {
            return Atan_N(values, 0.75);
        }

        private static List<double> Atan_M0_75(IEnumerable<double> values) {
            return Atan_N(values, -0.75);
        }

        private static List<double> Atan_0_95(IEnumerable<double> values) {
            return Atan_N(values, 0.95);
        }

        private static List<double> Atan_M0_95(IEnumerable<double> values) {
            return Atan_N(values, -0.95);
        }

        private static List<double> Atan_1(IEnumerable<double> values) {
            return Atan_N(values, 1.0);
        }

        private static List<double> Atan_M1(IEnumerable<double> values) {
            return Atan_N(values, -1.0);
        }

        private static List<double> Atan_1_15(IEnumerable<double> values) {
            return Atan_N(values, 1.15);
        }

        private static List<double> Atan_M1_15(IEnumerable<double> values) {
            return Atan_N(values, -1.15);
        }

        private static List<double> Atan_1_4(IEnumerable<double> values) {
            return Atan_N(values, 1.4);
        }

        private static List<double> Atan_M1_4(IEnumerable<double> values) {
            return Atan_N(values, -1.4);
        }

        private static List<double> Atan_1_5(IEnumerable<double> values) {
            return Atan_N(values, 1.5);
        }

        private static List<double> Atan_M1_5(IEnumerable<double> values) {
            return Atan_N(values, -1.5);
        }

        private static List<double> Atan_1_7(IEnumerable<double> values) {
            return Atan_N(values, 1.7);
        }

        private static List<double> Atan_M1_7(IEnumerable<double> values) {
            return Atan_N(values, -1.7);
        }

        private static List<double> Atan_2(IEnumerable<double> values) {
            return Atan_N(values, 2.0);
        }

        private static List<double> Atan_M2(IEnumerable<double> values) {
            return Atan_N(values, -2.0);
        }

        private static List<double> Atan_2_1(IEnumerable<double> values) {
            return Atan_N(values, 2.1);
        }

        private static List<double> Atan_M2_1(IEnumerable<double> values) {
            return Atan_N(values, -2.1);
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

        private static List<double> ExpX_0_2(IEnumerable<double> values) {
            return ExpX_N(values, 0.2);
        }

        private static List<double> ExpX_M0_2(IEnumerable<double> values) {
            return ExpX_N(values, -0.2);
        }

        private static List<double> ExpX_0_35(IEnumerable<double> values) {
            return ExpX_N(values, 0.35);
        }

        private static List<double> ExpX_M0_35(IEnumerable<double> values) {
            return ExpX_N(values, -0.35);
        }

        private static List<double> ExpX_0_5(IEnumerable<double> values) {
            return ExpX_N(values, 0.5);
        }

        private static List<double> ExpX_M0_5(IEnumerable<double> values) {
            return ExpX_N(values, -0.5);
        }

        private static List<double> ExpX_0_65(IEnumerable<double> values) {
            return ExpX_N(values, 0.65);
        }

        private static List<double> ExpX_M0_65(IEnumerable<double> values) {
            return ExpX_N(values, -0.65);
        }

        private static List<double> ExpX_M0_8(IEnumerable<double> values) {
            return ExpX_N(values, -0.8);
        }

        private static List<double> ExpX_0_8(IEnumerable<double> values) {
            return ExpX_N(values, 0.8);
        }

        private static List<double> ExpX_M0_95(IEnumerable<double> values) {
            return ExpX_N(values, -0.95);
        }

        private static List<double> ExpX_0_95(IEnumerable<double> values) {
            return ExpX_N(values, 0.95);
        }

        private static List<double> ExpX_1(IEnumerable<double> values) {
            return ExpX_N(values, 1.0);
        }

        private static List<double> ExpX_M1(IEnumerable<double> values) {
            return ExpX_N(values, -1.0);
        }

        private static List<double> ExpX_M1_1(IEnumerable<double> values) {
            return ExpX_N(values, -1.1);
        }

        private static List<double> ExpX_1_1(IEnumerable<double> values) {
            return ExpX_N(values, 1.1);
        }

        private static List<double> ExpX_1_2(IEnumerable<double> values) {
            return ExpX_N(values, 1.2);
        }

        private static List<double> ExpX_M1_2(IEnumerable<double> values) {
            return ExpX_N(values, -1.2);
        }

        private static List<double> ExpX_1_3(IEnumerable<double> values) {
            return ExpX_N(values, 1.3);
        }

        private static List<double> ExpX_M1_3(IEnumerable<double> values) {
            return ExpX_N(values, -1.3);
        }

        private static List<double> ExpX_1_4(IEnumerable<double> values) {
            return ExpX_N(values, 1.4);
        }

        private static List<double> ExpX_M1_4(IEnumerable<double> values) {
            return ExpX_N(values, -1.4);
        }

        private static List<double> ExpX_1_5(IEnumerable<double> values) {
            return ExpX_N(values, 1.5);
        }

        private static List<double> ExpX_M1_5(IEnumerable<double> values) {
            return ExpX_N(values, -1.5);
        }

        private static List<double> ExpX_1_6(IEnumerable<double> values) {
            return ExpX_N(values, 1.6);
        }

        private static List<double> ExpX_M1_6(IEnumerable<double> values) {
            return ExpX_N(values, -1.6);
        }

        private static List<double> ExpX_1_7(IEnumerable<double> values) {
            return ExpX_N(values, 1.7);
        }

        private static List<double> ExpX_M1_7(IEnumerable<double> values) {
            return ExpX_N(values, -1.7);
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

        private static List<double> Sigm_1_1(IEnumerable<double> values) {
            return Sigm_X(values, 1.1);
        }

        private static List<double> Sigm_M1_1(IEnumerable<double> values) {
            return Sigm_X(values, -1.1);
        }

        private static List<double> Sigm_1_5(IEnumerable<double> values) {
            return Sigm_X(values, 1.5);
        }

        private static List<double> Sigm_M1_5(IEnumerable<double> values) {
            return Sigm_X(values, -1.5);
        }

        private static List<double> Sigm_1_7(IEnumerable<double> values) {
            return Sigm_X(values, 1.7);
        }

        private static List<double> Sigm_M1_7(IEnumerable<double> values) {
            return Sigm_X(values, -1.7);
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

        private static List<double> Betha_Alpha_1_1_0_6(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.1, 0.6);
        }

        private static List<double> Betha_Alpha_1_1_0_7(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.1, 0.7);
        }

        private static List<double> Betha_Alpha_1_1_0_9(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.1, 0.9);
        }

        private static List<double> Betha_Alpha_1_1_1_1(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.1, 1.1);
        }

        private static List<double> Betha_Alpha_1_1_1_3(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.1, 1.3);
        }

        private static List<double> Betha_Alpha_1_1_1_6(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.1, 1.6);
        }

        private static List<double> Betha_Alpha_1_1_2(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.1, 2.0);
        }

        private static List<double> Betha_Alpha_1_1_2_5(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.1, 2.5);
        }

        private static List<double> Betha_Alpha_1_1_3(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.1, 3.0);
        }

        private static List<double> Betha_Alpha_1_1_4(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.1, 4.0);
        }

        private static List<double> Betha_Alpha_1_1_5(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.1, 5.0);
        }

        private static List<double> Betha_Alpha_1_1_6(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.1, 6.0);
        }

        private static List<double> Betha_Alpha_1_1_7(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.1, 7.0);
        }

        private static List<double> Betha_Alpha_1_1_8(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.1, 8.0);
        }

        private static List<double> Betha_Alpha_1_5_0_6(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.5, 0.6);
        }

        private static List<double> Betha_Alpha_1_5_0_65(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.5, 0.65);
        }

        private static List<double> Betha_Alpha_1_5_0_7(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.5, 0.7);
        }

        private static List<double> Betha_Alpha_1_5_0_75(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.5, 0.75);
        }

        private static List<double> Betha_Alpha_1_5_0_8(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.5, 0.8);
        }

        private static List<double> Betha_Alpha_1_5_0_9(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.5, 0.9);
        }

        private static List<double> Betha_Alpha_1_5_1(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.5, 1.0);
        }

        private static List<double> Betha_Alpha_1_5_1_1(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.5, 1.1);
        }

        private static List<double> Betha_Alpha_1_5_1_2(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.5, 1.2);
        }

        private static List<double> Betha_Alpha_1_5_1_3(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.5, 1.3);
        }

        private static List<double> Betha_Alpha_1_5_1_4(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.5, 1.4);
        }

        private static List<double> Betha_Alpha_1_5_1_6(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.5, 1.6);
        }

        private static List<double> Betha_Alpha_1_5_1_8(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.5, 1.8);
        }

        private static List<double> Betha_Alpha_1_5_2(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.5, 2.0);
        }

        private static List<double> Betha_Alpha_1_5_2_5(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 1.5, 2.5);
        }

        private static List<double> Betha_Alpha_2_0_2(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 2.0, 0.2);
        }

        private static List<double> Betha_Alpha_2_0_25(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 2.0, 0.25);
        }

        private static List<double> Betha_Alpha_2_0_3(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 2.0, 0.3);
        }

        private static List<double> Betha_Alpha_2_0_35(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 2.0, 0.35);
        }

        private static List<double> Betha_Alpha_2_0_4(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 2.0, 0.4);
        }

        private static List<double> Betha_Alpha_2_0_45(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 2.0, 0.45);
        }

        private static List<double> Betha_Alpha_2_0_5(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 2.0, 0.5);
        }

        private static List<double> Betha_Alpha_2_0_55(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 2.0, 0.55);
        }

        private static List<double> Betha_Alpha_2_0_6(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 2.0, 0.6);
        }

        private static List<double> Betha_Alpha_2_0_7(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 2.0, 0.7);
        }

        private static List<double> Betha_Alpha_2_0_8(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 2.0, 0.8);
        }

        private static List<double> Betha_Alpha_2_0_9(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 2.0, 0.9);
        }

        private static List<double> Betha_Alpha_2_1_1(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 2.0, 1.1);
        }

        private static List<double> Betha_Alpha_2_1_2(IEnumerable<double> values) {
            return Betha_Alpha_N_N(values, 2.0, 1.2);
        }

        /// <summary>
        /// Get the 1/(alpha*(x + betha)) from list of values with alpha and betha parameters
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="betha">Value of betha</param>
        /// <param name="alpha">Value of alpha</param>
        /// <returns>List of 1/(alpha*(x + betha)) of values with alpha and betha parameters</returns>
        public static List<double> Betha_Alpha_N_N(IEnumerable<double> values, double betha, double alpha) {
            List<double> result = new List<double>();

            foreach (var elem in values) {
                result.Add(1 / (alpha * (elem + betha)));
            }

            return result;
        }
    }
}
