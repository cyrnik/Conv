using System;
using System.Windows.Forms;
using System.IO;

namespace Conv
{

    public class CurrencyConverter
    {
        public decimal usdToEur = 0.88m; // Примерный курс валют
        public decimal eurToUsd = 1.12m;
        public decimal usdToRub = 89.14m;
        public decimal rubToUsd = 0.011219m;
        public decimal eurToRub = 96.86m;
        public decimal rubToEur = 0.010324m;

        public decimal Convert(decimal amount, string fromCurrency, string toCurrency)  //Метод выполняющий конвертацию валют
        {
            if (amount < 0)
            {
                throw new ArgumentException("Сумма не может быть отрицательной.");
            }
            if (fromCurrency == "USD" && toCurrency == "EUR")
            {
                return amount * usdToEur;
            }
            else if (fromCurrency == "EUR" && toCurrency == "USD")
            {
                return amount * eurToUsd;
            }
            else if (fromCurrency == "USD" && toCurrency == "RUB")
            {
                return (amount * usdToRub);
            }
            else if (fromCurrency == "RUB" && toCurrency == "USD")
            {
                return (amount * rubToUsd);
            }
            else if (fromCurrency == "EUR" && toCurrency == "RUB")
            {
                return (amount * eurToRub);
            }
            else if (fromCurrency == "RUB" && toCurrency == "EUR")
            {
                return (amount * rubToEur);
            }
            else
            {
                throw new NotSupportedException("Не поддерживаемая пара валют.");
            }

        }
    }

}