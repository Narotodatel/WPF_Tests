using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCalculatorWPF.Logic
{
    public static class ExamCalculator
    {
        public static int CalculateTotal(
            string level,
            double m1,
            double m2,
            double m3,
            double m4,
            double m5)
        {
            ValidateModule(m1, 10, "Модуль 1");
            ValidateModule(m2, 15, "Модуль 2");
            ValidateModule(m3, 25, "Модуль 3");
            ValidateModule(m4, 25, "Модуль 4");
            ValidateModule(m5, 25, "Модуль 5");

            switch (level)
            {
                case "БУ":
                    return (int)(m1 + m2 + m3);

                case "ПУ":
                    return (int)(m1 + m2 + m3 + m4);

                case "ПУ+":
                    return (int)(m1 + m2 + m3 + m4 + m5);

                default:
                    throw new ArgumentException("Неизвестный уровень экзамена");
            }
        }

        public static int GetMaxScore(string level)
        {
            switch (level)
            {
                case "БУ":
                    return 50;

                case "ПУ":
                    return 75;

                case "ПУ+":
                    return 100;

                default:
                    throw new ArgumentException("Неизвестный уровень экзамена");
            }
        }

        public static double CalculatePercent(int total, int max)
        {
            if (max <= 0)
            {
                throw new ArgumentException("Максимальный балл должен быть больше 0");
            }

            return (double)total / max * 100;
        }

        public static int GetGrade(double percent)
        {
            if (percent < 0 || percent > 100)
            {
                throw new ArgumentException("Процент должен быть от 0 до 100");
            }

            if (percent >= 80)
                return 5;

            if (percent >= 60)
                return 4;

            if (percent >= 40)
                return 3;

            return 2;
        }

        private static void ValidateModule(double value, int max, string moduleName)
        {
            if (value < 0 || value > max)
            {
                throw new ArgumentException(
                    $"{moduleName}: значение должно быть от 0 до {max}");
            }
        }
    }
}
