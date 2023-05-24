using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepParameters {
    internal static class StepsInfo {
        public static string Step1 { get; } = "Загрузка данных из файла (excel, csv) и выбор показателей для построения моделей.";

        public static string Step2 { get; } = "Данный блок является не обязательным\n" +
            "В данном блоке реализуется метод функциональной предобработки статистических данных.\n" +
            "Результатом является набор функций, преобразующих значения регрессоров для достижения\n" +
            "наибольшего по модулю значения коэффициента Пирсона.";
    }
}
