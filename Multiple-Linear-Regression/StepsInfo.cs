﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiple_Linear_Regression {
    internal static class StepsInfo {
        public static string Step1 { get; } = "Загрузка данных из файла (excel, csv) и выбор показателей для построения моделей.";

        public static string Step2 { get; } = "Данный блок является необязательным.\n\n" +
            "В данном блоке реализуется метод функциональной предобработки статистических данных.\n\n" +
            "Результатом является набор функций, преобразующих значения регрессоров для достижения\n" +
            "наибольшего по модулю значения коэффициента Пирсона.";

        public static string Step3 { get; } = "Данный блок является необязательным.\n\n" +
            "В данном блоке блоке выполняется фильтрация управляющих факторов, с помощью одного из двух подходов.\n\n" +
            "Эмпирический подход заключается в сравнении модуля коэффициента корреляции между управляющим и\n" +
            "управляемым фактором с некоторым пороговым значением.\n\n" +
            "Классический подход заключается в проверке на значимость при уровне значимости a=0,05 на основании\n" +
            "стандартной гипотезы.";

        public static string Step4 { get; } = "На данном этапе выполняется построение многмерных регрессионных уравнений\n" +
            "для выражения управляемых факторов через управляющие.";

        public static string Step5 { get; } = "На данном этапе происходит выбор моделей и настройка параметров области опередения\n" +
            "для имитации управления.";

        public static string UserWarningFuncPreprocessing { get; } = "Функциональная предобработка статистических\n" +
            "данных может занять много времени.\nДанный блок является необязательным.";

        public static string UserWarningFormClosingRegressorsFromFile { get; } = "Вы не сохранили результаты управления\n" +
            "в файл и можете потерять данные.";

        public static string SymbiosisInfo { get; } = "Симбиоз областей представляет собой объединение\n" +
            "эмпирической и теоретической областей.\n" +
            "В качестве минимума - наименьшее значение из полученной области.\n" +
            "В качестве максимума - наибольшее значение из полученной области.";

        public static string AutoProportionInfo { get; } = "Увеличивает область определение обратно\n" +
            "пропорционально расстоянию от максимума и минимума до среднего.";

        public static string PercentAreaExpansion { get; } = "Задает процент расширения области определения.\n" +
            "Настройка доступна при выборе эмпирической области или симбиоза.";

        public static string ImitationOfControlForm { get; } = "Здесь вы можете задать параметры для управляющих факторов\n" +
            "модели для имитации управления.\n\n" +
            "Вы можете задавать параметры вручную, а также загрузить их из excel-файла.\n" +
            "При загрузке файлов из excel-файла, результат управления также будет записан в excel-файл.\n\n" +
            "Вы можете сохранить результат управления в виде excel-файла.";

        public static string ImitationRegressorsFromFile { get; } = "На данной форме представлены результаты имитации управления\n" +
            "для набора параметров, считанных из файла.\n\n" +
            "Вы можете сохранить результаты имитации управления в файл.";
    }
}
