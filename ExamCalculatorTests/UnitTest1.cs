using System;
using ExamCalculatorWPF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExamCalculatorTests
{
    [TestClass]
    public class ExamCalculatorTests
    {
        // Потоковые тесты суммы баллов
        [DataTestMethod]
        [DataRow("БУ", 10, 15, 25, 0, 0, 50)]
        [DataRow("ПУ", 10, 15, 25, 25, 0, 75)]
        [DataRow("ПУ+", 10, 15, 25, 25, 25, 100)]
        [DataRow("БУ", 5, 10, 15, 0, 0, 30)]
        public void CalculateTotal_ValidData_ReturnsCorrectResult(
            string level,
            double m1,
            double m2,
            double m3,
            double m4,
            double m5,
            int expected)
        {
            int result = ExamCalculator.CalculateTotal(
                level,
                m1,
                m2,
                m3,
                m4,
                m5);

            Assert.AreEqual(expected, result);
        }

        // Проверка процентов
        [DataTestMethod]
        [DataRow(100, 100, 100)]
        [DataRow(50, 100, 50)]
        [DataRow(75, 100, 75)]
        [DataRow(40, 50, 80)]
        public void CalculatePercent_ValidData_ReturnsCorrectPercent(
            int total,
            int max,
            double expected)
        {
            double result = ExamCalculator.CalculatePercent(total, max);

            Assert.AreEqual(expected, result);
        }

        // Проверка оценок
        [DataTestMethod]
        [DataRow(100, 5)]
        [DataRow(80, 5)]
        [DataRow(79, 4)]
        [DataRow(60, 4)]
        [DataRow(59, 3)]
        [DataRow(40, 3)]
        [DataRow(39, 2)]
        [DataRow(0, 2)]
        public void GetGrade_ValidPercent_ReturnsCorrectGrade(
            double percent,
            int expected)
        {
            int result = ExamCalculator.GetGrade(percent);

            Assert.AreEqual(expected, result);
        }

        // Проверка исключений
        [TestMethod]
        public void CalculateTotal_InvalidModule_ThrowsException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                ExamCalculator.CalculateTotal(
                    "БУ",
                    100,
                    15,
                    25,
                    0,
                    0);
            });
        }

        [TestMethod]
        public void GetGrade_InvalidPercent_ThrowsException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                ExamCalculator.GetGrade(150);
            });
        }

        [TestMethod]
        public void CalculatePercent_InvalidMax_ThrowsException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                ExamCalculator.CalculatePercent(50, 0);
            });
        }

        // Граничные значения
        [DataTestMethod]
        [DataRow(79.99, 4)]
        [DataRow(80, 5)]
        [DataRow(59.99, 3)]
        [DataRow(60, 4)]
        [DataRow(39.99, 2)]
        [DataRow(40, 3)]
        public void GetGrade_BorderValues_ReturnsCorrectGrade(
            double percent,
            int expected)
        {
            int result = ExamCalculator.GetGrade(percent);

            Assert.AreEqual(expected, result);
        }
    }
}