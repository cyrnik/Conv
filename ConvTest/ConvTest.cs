using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            CurrencyConverter converter = new CurrencyConverter();
            decimal amount = 100m;
            string fromCurrency = "USD";
            string toCurrency = "EUR";
            decimal expectedResult = 100m * converter.usdToEur;

            // Act
            decimal result = converter.Convert(amount, fromCurrency, toCurrency);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Convert_EurToUsd_CorrectConversion()
        {
            // Arrange
            CurrencyConverter converter = new CurrencyConverter();
            decimal amount = 50m;
            string fromCurrency = "EUR";
            string toCurrency = "USD";
            decimal expectedResult = 50m * converter.eurToUsd;

            // Act
            decimal result = converter.Convert(amount, fromCurrency, toCurrency);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Convert_UnsupportedCurrencyPair_ThrowsException()
        {
            // Arrange
            CurrencyConverter converter = new CurrencyConverter();
            decimal amount = 100m;
            string fromCurrency = "USD";
            string toCurrency = "GBP"; // Неподдерживаемая валюта

            // Act
            converter.Convert(amount, fromCurrency, toCurrency);

            // Assert - Exception should be thrown
        }


        [TestMethod]
        [ExpectedException(typeof(Arg| umentException))]
        public void Convert_NegativeAmount_ThrowsException()
        {
            // Arrange
            CurrencyConverter converter = new CurrencyConverter();
            decimal amount = -100m;
            string fromCurrency = "USD";
            string toCurrency = "EUR";

            // Act
            converter.Convert(amount, fromCurrency, toCurrency);

            // Assert - Exception should be thrown
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
    }


    //Тесты для CurrencyConverterForm (требуется использование библиотеки Moq или подобной для имитации взаимодействия с GUI)
    [TestClass]
    public class CurrencyConverterFormTests
    {
        //[TestMethod] //Пример теста, требующий mocking
        //public void ConvertButton_Click_CorrectConversion()
        //{
        //    // Arrange
        //    var mockConverter = new Mock<CurrencyConverter>();
        //    mockConverter.Setup(c => c.Convert(100m, "USD", "EUR")).Returns(88m);
        //    var form = new CurrencyConverterForm { converter = mockConverter.Object };
        //    form.amountTextBox.Text = "100";
        //    form.fromCurrencyComboBox.SelectedItem = "USD";
        //    form.toCurrencyComboBox.SelectedItem = "EUR";

        //    // Act
        //    form.ConvertButton_Click(null, null);

        //    // Assert
        //    Assert.AreEqual("Результат: 100 USD = 88 EUR", form.resultLabel.Text);
        //}

        [TestMethod]
        public void ConvertButton_Click_EmptyAmount_ShowsMessage()
        {
            // Arrange
            var form = new CurrencyConverterForm();
            // Act & Assert (использование Assert.ThrowsException)
            Assert.ThrowsException<AssertFailedException>(() =>
            {
                form.ConvertButton_Click(null, null);
                Assert.IsTrue(form.resultLabel.Text.Contains("Введите сумму")); //проверка сообщения, если исключение не возникло
            });

        }

        [TestMethod]
        public void ConvertButton_Click_InvalidAmount_ShowsMessage()
        {
            // Arrange
            var form = new CurrencyConverterForm();
            form.amountTextBox.Text = "abc";

            // Act & Assert (использование Assert.ThrowsException)
            Assert.ThrowsException<AssertFailedException>(() =>
            {
                form.ConvertButton_Click(null, null);
                Assert.IsTrue(form.resultLabel.Text.Contains("Неверный формат суммы")); //проверка сообщения, если исключение не возникло
            });
        }


        [TestMethod]
        public void ConvertButton_Click_NoCurrencySelected_ShowsMessage()
        {
            // Arrange
            var form = new CurrencyConverterForm();
            form.amountTextBox.Text = "100";

            // Act
            form.ConvertButton_Click(null, null);

            // Assert
            Assert.IsTrue(form.resultLabel.Text.Contains("Пожалуйста, выберите валюты"));
        }
    }
}