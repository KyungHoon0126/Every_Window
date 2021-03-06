﻿using Every.Common;
using Every_AdminWin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Every.Control.SignUp
{
    /// <summary>
    /// Interaction logic for SignUpControl.xaml
    /// </summary>
    public partial class StudentSignUpControl : UserControl
    {
        public delegate void BackWardLoginPage_Handler(object sender, RoutedEventArgs e);
        public event BackWardLoginPage_Handler StudentSignUpBackWardLoginPage;

        public delegate void LoadSearchSchoolWindow_Handler(object sender, RoutedEventArgs e);
        public event LoadSearchSchoolWindow_Handler LoadSearchSchoolWindow;

        public StudentSignUpControl()
        {
            InitializeComponent();
            this.DataContext = App.signUpData.signUpViewModel;
        }

        private void btnBackWardLoginPage_Click(object sender, RoutedEventArgs e)
        {
            StudentSignUpBackWardLoginPage?.Invoke(this, e);
        }

        private void btnSearchSchool_Click(object sender, RoutedEventArgs e)
        {
            LoadSearchSchoolWindow?.Invoke(this, e);
        }

        // 숫자만 입력 가능, 키패드 사용 가능
        private void tbInputBirth_Year_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
