using System;
using System.Windows.Forms;

namespace Conv
{
    public class CurrencyConverter
    {
        private decimal usdToEur = 0.88m; // ��������� ���� �����
        private decimal eurToUsd = 1.12m;

        public decimal Convert(decimal amount, string fromCurrency, string toCurrency)
        {
            if (amount < 0)
            {
                throw new ArgumentException("����� �� ����� ���� �������������.");
            }
            if (fromCurrency == "USD" && toCurrency == "EUR")
            {
                return amount * usdToEur;
            }
            else if (fromCurrency == "EUR" && toCurrency == "USD")
            {
                return amount * eurToUsd;
            }
            else
            {
                throw new NotSupportedException("�� �������������� ���� �����.");
            }
        }
    }

    public class CurrencyConverterForm : Form
    {
        private CurrencyConverter converter; // ������������� ����������
        private ComboBox fromCurrencyComboBox;
        private ComboBox toCurrencyComboBox;
        private TextBox amountTextBox;
        private Button convertButton;
        private Label resultLabel;

        public CurrencyConverterForm()
        {
            converter = new CurrencyConverter(); // ������������� ���������� ����������

            this.Text = "��������� �����";
            this.Width = 300;
            this.Height = 200;

            fromCurrencyComboBox = new ComboBox
            {
                Location = new System.Drawing.Point(10, 10),
                Width = 100,
                Items = { "USD", "EUR" }
            };
            toCurrencyComboBox = new ComboBox
            {
                Location = new System.Drawing.Point(120, 10),
                Width = 100,
                Items = { "USD", "EUR" }
            };
            amountTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 40),
                Width = 210,
                PlaceholderText = "�����"
            };
            convertButton = new Button
            {
                Location = new System.Drawing.Point(10, 70),
                Text = "��������������",
                Width = 210
            };
            convertButton.Click += ConvertButton_Click;

            resultLabel = new Label
            {
                Location = new System.Drawing.Point(10, 100),
                Width = 210,
                Text = "���������: "
            };

            this.Controls.Add(fromCurrencyComboBox);
            this.Controls.Add(toCurrencyComboBox);
            this.Controls.Add(amountTextBox);
            this.Controls.Add(convertButton);
            this.Controls.Add(resultLabel);
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(amountTextBox.Text))
            {
                MessageBox.Show("������� ����� ��� �����������!");
                return;
            }

            decimal amount;
            if (!decimal.TryParse(amountTextBox.Text, out amount))
            {
                MessageBox.Show("�������� ������ �����!");
                return;
            }

            // �������� �� ����� �����
            if (fromCurrencyComboBox.SelectedItem == null || toCurrencyComboBox.SelectedItem == null)
            {
                MessageBox.Show("����������, �������� ������ ��� �����������!");
                return;
            }

            string fromCurrency = fromCurrencyComboBox.SelectedItem.ToString();
            string toCurrency = toCurrencyComboBox.SelectedItem.ToString();

            try
            {
                decimal result = converter.Convert(amount, fromCurrency, toCurrency);
                resultLabel.Text = $"���������: {amount} {fromCurrency} = {result} {toCurrency}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}