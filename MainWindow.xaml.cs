using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConverterNew
{
    /// <summary>
    /// </summary>
    public partial class MainWindow : Window
    {
        // operation = true - SELL
        // operation = false - BUY
         
        private Converter converter = new Converter();
       
        
        public MainWindow()
        {
            InitializeComponent();

            UpdateCourse();
          
            Panel1.Visibility = Visibility.Collapsed;

            Value = "100";
            DataContext = this;
            radioBtnSale.Checked += radioBtnSale_Checked;
            comboBox1.SelectionChanged += comboBox1_SelectionChanged;
            comboBox2.SelectionChanged += comboBox2_SelectionChanged;


        }
        public string value_;

        public string Value
        {
            get { return value_; }
            set
            {
                value_ = value;
            }
        }
        private void Button_Reset(object sender, RoutedEventArgs e)
        {
            SumBox.Text = "0";
        }
       
        private void SumBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
        public void Update()
        {
            string valueText = SumBox.Text;
            float value = 0.0f;

            try
            {
                value = float.Parse(valueText);
            }
            catch (FormatException e)
            {
                // cannot convert string to float value
                // because text is empty or not a number
                return;
            }
            Bank comboBox2SelectedIndex = (Bank) comboBox2.SelectedIndex;
            Currency comboBox1SelectedIndex = (Currency) comboBox1.SelectedIndex;

            Operation operation = ((bool) radioBtnSale.IsChecked ? Operation.Sale : Operation.Buy);
         
            resUSD.Text = converter.CalculateValue(value, comboBox1SelectedIndex, Currency.USD, operation, comboBox2SelectedIndex ).ToString("#.##");
            resEUR.Text = converter.CalculateValue(value, comboBox1SelectedIndex, Currency.EUR, operation, comboBox2SelectedIndex).ToString("0.00");
            resRUB.Text = converter.CalculateValue(value, comboBox1SelectedIndex, Currency.RUB, operation, comboBox2SelectedIndex).ToString("0.00");
            resUAH.Text = converter.CalculateValue(value, comboBox1SelectedIndex, Currency.UAH, operation, comboBox2SelectedIndex).ToString("0.00");
      
        }
        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Currency comboBox1SelectedIndex = (Currency)comboBox1.SelectedIndex; // get int to enum 
            //Show all elements:
            Panel0.Visibility = Visibility.Visible;
            Panel1.Visibility = Visibility.Visible;
            Panel2.Visibility = Visibility.Visible;
            Panel3.Visibility = Visibility.Visible;
            toolTip1.Visibility = Visibility.Visible;
            toolTip2.Visibility = Visibility.Visible;
            toolTip3.Visibility = Visibility.Visible;
            // hide the one that is not required:

            if (comboBox1SelectedIndex == Currency.USD) // USD
            {
                Panel1.Visibility = Visibility.Collapsed;
            }
            if (comboBox1SelectedIndex == Currency.EUR) // EUR
            {
                Panel2.Visibility = Visibility.Collapsed;
            }
            if (comboBox1SelectedIndex == Currency.RUB) // RUB
            {
                Panel3.Visibility = Visibility.Collapsed;
            }
            if (comboBox1SelectedIndex == Currency.UAH) // UAH
            {
                Panel0.Visibility = Visibility.Collapsed;
                toolTip1.Visibility = Visibility.Collapsed;
                toolTip2.Visibility = Visibility.Collapsed;
                toolTip3.Visibility = Visibility.Collapsed;
            }
            
            Update();
            UpdateCourse();
        }
        private void UpdateCourse()
        {

            Currency comboBox1SelectedIndex = (Currency)comboBox1.SelectedIndex;

            Operation operation = radioBtnSale.IsChecked.Value ? Operation.Buy : Operation.Sale;

            actualUSD.Text = (converter.GetMatrix(comboBox2.SelectedIndex)[(int)operation, (int)Currency.USD]).ToString("0.000");
            actualEUR.Text = (converter.GetMatrix(comboBox2.SelectedIndex)[(int)operation, (int)Currency.EUR]).ToString("0.000");
            actualRUB.Text = (converter.GetMatrix(comboBox2.SelectedIndex)[(int)operation, (int)Currency.RUB]).ToString("0.0000");
            actualUAH.Text = (converter.GetMatrix(comboBox2.SelectedIndex)[1-(int)operation, (int)comboBox1SelectedIndex]).ToString("0.000");
         }
        private void radioBtnSale_Checked(object sender, RoutedEventArgs e)
        {
          
            UpdateCourse();
            Update();
        }
        private void radioBtnBuy_Checked(object sender, RoutedEventArgs e)
        {
            
            UpdateCourse();
            Update();
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCourse();
            Update();
        }

        
    }
}
