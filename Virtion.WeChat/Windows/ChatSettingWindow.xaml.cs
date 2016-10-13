﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Virtion.WeChat.Struct;

namespace Virtion.WeChat.Windows
{

    public partial class ChatSettingWindow : Window
    {
        public bool IsFilterMsg
        {
            get
            {
                return this.CB_IsFilterMsg.IsChecked.Value;
            }
            set
            {
                this.CB_IsFilterMsg.IsChecked = value;
            }
        }
        public string MaxMsgLength
        {
            get
            {
                return this.TB_MaxMsgLength.Text;
            }
            set
            {
                this.TB_MaxMsgLength.Text = value;
            }
        }
        public bool IsFilterAdd
        {
            get
            {
                return this.CB_IsFilterAdd.IsChecked.Value;
            }
            set
            {
                this.CB_IsFilterAdd.IsChecked = value;
            }
        }
        public bool IsFilterSelf
        {
            get
            {
                return this.CB_IsFilterSelf.IsChecked.Value;
            }
            set
            {
                this.CB_IsFilterSelf.IsChecked = value;
            }
        }
        public bool IsHightLight
        {
            get
            {
                return this.CB_IsHightLight.IsChecked.Value;
            }
            set
            {
                this.CB_IsHightLight.IsChecked = value;
            }
        }

        private ChatConfig config;

        public ChatSettingWindow()
        {
            InitializeComponent();
        }

        public void SetConfig(ChatConfig config)
        {
            this.config = config;

            IsFilterMsg = config.IsFilterMsgCount;
            MaxMsgLength = config.MaxMsgLength.ToString();
            IsFilterAdd = config.IsFilterAdd;
            IsFilterSelf = config.IsFilterSelfDef;
            IsHightLight = config.IsHightLight;

            this.CB_IsFilterUserMsg.IsChecked = this.config.IsFilterUserMsg;
            this.TB_UserMsg.Text = this.config.UserMsg;

            this.TB_Delay.Text = this.config.Delay.ToString();

            for (int i = 0; i < this.config.DefineList.Count; i++)
            {
                if (i != 0)
                {
                    this.TB_DefineList.Text += "/";
                }
                this.TB_DefineList.Text += this.config.DefineList[i];
            }
        }

        public ChatConfig GetConfig()
        {
            this.config.IsFilterMsgCount = this.CB_IsFilterMsg.IsChecked.Value;
            this.config.IsFilterSelfDef = this.CB_IsFilterSelf.IsChecked.Value;
            this.config.IsHightLight = this.CB_IsFilterAdd.IsChecked.Value;
            this.config.IsFilterAdd = this.CB_IsFilterAdd.IsChecked.Value;
            int i = 0;
            if (Int32.TryParse(this.TB_MaxMsgLength.Text, out i) == true)
            {
                this.config.MaxMsgLength = i;
            }
            else
            {
                MessageBox.Show("输入字符上限不是数字");
            }

            var isChecked = this.CB_IsFilterUserMsg.IsChecked;
            if (isChecked == true)
            {
                this.config.IsFilterUserMsg = true;
                string s = this.TB_UserMsg.Text;
                if (string.IsNullOrEmpty(s) == false)
                {
                    this.config.UserMsg = s;
                    string text = this.TB_DefineList.Text;

                    if (string.IsNullOrEmpty(this.TB_Delay.Text) == false)
                    {
                        if (Int32.TryParse(this.TB_Delay.Text, out i) == true)
                        {
                            this.config.Delay = i;
                        }
                        else
                        {
                            MessageBox.Show("输入延时不是数字");
                        }
                    }

                    var list = text.Split('/');
                    this.config.DefineList.Clear();
                    foreach (var item in list)
                    {
                        this.config.DefineList.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("自定义消息不能为空");
                }
            }

            return this.config;
        }

        private void B_OK_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
