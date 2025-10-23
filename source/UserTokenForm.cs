﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalFinancialManager.source
{
    public partial class UserTokenForm : Form
    {
        private const string ASK_USER_API_TEXT = 
            "Для работы приложения вам необходимо:\r\n" +
            "1. зарегистрироваться на сайте proverkacheka.com\r\n" +
            "2. в личном кабинете получить API ключ.\r\n";

        private const string WRONG_API_REASK_USER_TEXT = 
            "Такого API ключа не существует. Необходимо:\r\n" +
            "1. зарегистрироваться на сайте proverkacheka.com\r\n" +
            "2. в личном кабинете получить API ключ.\r\n";
        
        public string? UserToken;
        public UserTokenForm(bool isWrongAPI)
        {
            InitializeComponent();

            if (isWrongAPI)
                infoTextBox.Text = WRONG_API_REASK_USER_TEXT;
            else infoTextBox.Text = ASK_USER_API_TEXT;
        }

        private void apiInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && apiInputTextBox.Text != "")
            {
                UserToken = apiInputTextBox.Text;
                Close();
            }
        }
    }
}
