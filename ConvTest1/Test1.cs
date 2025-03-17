using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Conv;

using System;

namespace ConvTests
{
    [TestClass]
    public class CurrencyConverterTests
    {
        [TestMethod]
        public void Convert_UsdToEur_CorrectConversion()
        {
            // Arrange
            CurrencyConverter converter = new CurrencyConverter(); //Создание экземпляра конвертера
            decimal amount = 100m; //Исходная сумма USD
            string fromCurrency = "USD"; //Исходная валюта
            string toCurrency = "EUR"; //Валюта в которую переводим
            decimal expectedResult = 100m * converter.usdToEur; //Ожидаемый результат

            // Act
            decimal result = converter.Convert(amount, fromCurrency, toCurrency); //Вызов метода для конвертации Convert

            // Assert
            Assert.AreEqual(expectedResult, result); //Проверка результата 
        }

        [TestMethod]
        public void Convert_EurToUsd_CorrectConversion()
        {
            // Arrange
            CurrencyConverter converter = new CurrencyConverter();
            decimal amount = 50m; //Ихсодная сумма EUR
            string fromCurrency = "EUR"; //Исходная валюта
            string toCurrency = "USD"; //Валюта в которую переводим
            decimal expectedResult = 50m * converter.eurToUsd; //Ожидаемый результат

            // Act
            decimal result = converter.Convert(amount, fromCurrency, toCurrency); //Вызов метода для конвертации Convert

            // Assert
            Assert.AreEqual(expectedResult, result); //Проверка результата
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))] //Ожидается исключение 
        public void Convert_UnsupportedCurrencyPair_ThrowsException()
        {
            // Arrange
            CurrencyConverter converter = new CurrencyConverter();
            decimal amount = 100m; //Исходная сумма USD
            string fromCurrency = "USD"; //Исходная валюта
            string toCurrency = "GBP"; // Неподдерживаемая валюта

            // Act
            converter.Convert(amount, fromCurrency, toCurrency); //Вызов метода для конвертации Convert (должно вызваться исключение)

            // Assert Проверка происходить автоматически из-за атрибута ExpectedException
        }



        [TestMethod]
        public void Convert_ZeroAmount_ReturnsZero() 
        {
            // Arrange
            CurrencyConverter converter = new CurrencyConverter();
            decimal amount = 0m;
            string fromCurrency = "USD";
            string toCurrency = "EUR";
            decimal expectedResult = 0m;

            // Act
            decimal result = converter.Convert(amount, fromCurrency, toCurrency);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }


        [TestMethod]
        public void ConvertButton_Click_EmptyAmount_ShowsMessage()
        {
            // Arrange

            var form = new CurrencyConverterForm();
            // Act & Assert 
            Assert.ThrowsException<AssertFailedException>(() => //Проверка, что вызывается исключение
            {
                form.ConvertButton_Click(null, null);
                Assert.IsTrue(form.resultLabel.Text.Contains("Введите сумму")); //проверка текста сообщения об ошибке
            });

        }

        [TestMethod]
        public void ConvertButton_Click_InvalidAmount_ShowsMessage()
        {
            // Arrange
            var form = new CurrencyConverterForm();
            form.amountTextBox.Text = "abc"; //Неккоректное значение суммы

            // Act & Assert 
            Assert.ThrowsException<AssertFailedException>(() => //Проверка, что вызывается исключение
            {
                form.ConvertButton_Click(null, null);
                Assert.IsTrue(form.resultLabel.Text.Contains("Неверный формат суммы")); //проверка текста сообщения об ошибке
            });
        }


        [TestMethod]
        public void ConvertButton_Click_NoCurrencySelected_ShowsMessage()
        {
            // Arrange
            var form = new CurrencyConverterForm();
            form.amountTextBox.Text = "100";
            Assert.ThrowsException<AssertFailedException>(() => //Проверка, что вызывается исключение
            {
                // Act
                form.ConvertButton_Click(null, null);

                // Assert
                Assert.IsTrue(form.resultLabel.Text.Contains("Пожалуйста, выберите валюты")); //проверка текста сообщения об ошибке
            });
        }
    }
}