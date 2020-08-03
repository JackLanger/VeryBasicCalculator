using System;
using System.Windows;

namespace JacksCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal float number1, number2;
        private bool _add, _sub, _mult, _div, _NoCalcYet;
        private bool[] actions;
        private bool _PressedResult;

        public MainWindow()
        {

            actions = new bool[] { _add, _sub, _mult, _div};
            InitializeComponent();
            TbcalcInput.Focus();
            _NoCalcYet = true;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            if (TbcalcInput.Text == "")
            {
                _NoCalcYet = true;
                number1 = 0.0f;
                number2 = 0.0f;
                TbcalcInput.Clear();
                TbcalcInput.Focus();
                TbResult.Clear();
                LblResult.Content ="";

                return;
            }
            TbcalcInput.Clear();
            _NoCalcYet = true;
        }

        private void BtnPI_Click(object sender, RoutedEventArgs e)
        {
            TbcalcInput.Clear();
            TbcalcInput.Text += Math.PI;
        }

        private void Point_Click(object sender, RoutedEventArgs e)
        {
            TbcalcInput.Text += ".";
        }
        //------------------ActionButtons----------------------------------------
        private void Result_Click(object sender, RoutedEventArgs e)
        {
            number2 =  (TbcalcInput.Text != "")? (float)Convert.ToDouble(TbcalcInput.Text):0.0f;
            number1 = PerformCalculation(ref number1, number2);
           
            TbcalcInput.Clear();
            TbcalcInput.Focus();

            LblResult.Content = number1;
            TbResult.Text = number1.ToString();
            _PressedResult = true;
        }
        private void Add_Click(object sender, RoutedEventArgs e) => ActionButtonPressed(0);
        private void Subt_Click(object sender, RoutedEventArgs e) => ActionButtonPressed(1);
        private void Multi_Click(object sender, RoutedEventArgs e) => ActionButtonPressed(2);
        private void Div_Click(object sender, RoutedEventArgs e) => ActionButtonPressed(3);


        //--------------------NUmber Buttons --------------------
        private void Btn0_Click(object sender, RoutedEventArgs e) => TbcalcInput.Text += 0;

        private void Btn1_Click(object sender, RoutedEventArgs e) => TbcalcInput.Text += 1;

        private void Btn2_Click(object sender, RoutedEventArgs e) => TbcalcInput.Text += 2;

        private void Btn3_Click(object sender, RoutedEventArgs e) => TbcalcInput.Text += 3;

        private void Btn4_Click(object sender, RoutedEventArgs e) => TbcalcInput.Text += 4;

        private void Btn5_Click(object sender, RoutedEventArgs e) => TbcalcInput.Text += 5;

        private void Btn6_Click(object sender, RoutedEventArgs e) => TbcalcInput.Text += 6;

        private void Btn7_Click(object sender, RoutedEventArgs e) => TbcalcInput.Text += 7;

        private void Btn8_Click(object sender, RoutedEventArgs e) => TbcalcInput.Text += 8;

        private void Btn9_Click(object sender, RoutedEventArgs e) => TbcalcInput.Text += 9;


        // negates the current Result---------------------------------------------------
        private void PosNeg_Click(object sender, RoutedEventArgs e) => number1 *= -1;
        
        private float Division( float num1, float num2)
        {
            if (num2 == 0)
            {
                LblResult.Content = "inf";
                return 0;
            }
                var div = num1 / num2;
                return div;            
        }

        private float PerformCalculation(ref float num1, float num2)
        {

            if (num1 == 0 && _NoCalcYet)
            {
                _NoCalcYet = false;
                num1 = num2;
                return num1;
            }

           
            if (actions[3])
            {
                if (num2 != 0)
                {
                    return Division(num1, num2);
                }
                TbResult.Text = "inf";
            }
            
            var sum = ((num1 + num2) * Convert.ToInt32(actions[0])) + ((num1 - num2) * Convert.ToInt32(actions[1]))+ ((num1 * num2) * Convert.ToInt32(actions[2]));

            for (int i = 0; i < actions.Length; i++)
            {
                actions[i] = false;
            }

            _PressedResult = false;
            return sum;
        }

        void ActionButtonPressed(int actionCode)
        {
            if (TbcalcInput.Text == "" &&  !_PressedResult)
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    actions[i] = false;
                }

                actions[actionCode] = true;
                TbcalcInput.Focus();
                return;
            }

            if (_PressedResult)
            {

                for (int i = 0; i < actions.Length; i++)
                {
                    if ( actions[i] == true)
                    {
                        actions[i] = false;
                    }
                }

                actions[actionCode] = true;
                TbcalcInput.Clear();
                TbcalcInput.Focus();
                _PressedResult = true;
                return;
            }

            number2 = (float)Convert.ToDouble(TbcalcInput.Text);
            number1 = PerformCalculation(ref number1, number2);

            TbcalcInput.Clear();
            TbcalcInput.Focus();

            actions[actionCode] = true;

            LblResult.Content = number1;
            TbResult.Text = number1.ToString();
        }
    }
}
