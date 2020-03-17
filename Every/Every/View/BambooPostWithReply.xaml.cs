﻿using Every.Core.Bamboo.ViewModel;
using Every_AdminWin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace Every.View
{
    /// <summary>
    /// Interaction logic for BambooPostWithReply.xaml
    /// </summary>
    public partial class BambooPostWithReply : Window
    {
        int imsiIdx = 0;

        public BambooPostWithReply()
        {
            InitializeComponent();
            Loaded += (sender, e) =>
            {
                this.DataContext = App.bambooData.bambooViewModel;
            };
        }

        private void btn_CloseBambooPostWithReply_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // 특정 게시물에서 댓글 작성 클릭, TODO : 커맨드로 바꿔야함, 현재는 마땅히 떠오르지 않아서 임시로 나둠.
        private void btnMakeReply_Click(object sender, RoutedEventArgs e)
        {
            string replycontent = App.bambooData.bambooViewModel.BambooReplyContent;
            int? idx = Convert.ToInt32((sender as Button).Tag);

            if (idx != null && replycontent != null)
            {
                App.bambooData.bambooViewModel.BambooSpecificPostReply(replycontent, idx);
            }
        }

        private void btn_BambooReplyContextMenu_Click(object sender, RoutedEventArgs e)
        {
            imsiIdx = Convert.ToInt32((sender as Button).Tag); // ReplyIdx 저장
            (sender as Button).ContextMenu.IsOpen = true;
        }

        // ContextMenu(MenuItem) 댓글 수정하기
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        // ContextMenu(MenuItem) 댓글 삭제하기 
        private void mi_DeleteReply_Click(object sender, RoutedEventArgs e)
        {
            int? replyIdx = imsiIdx;
            int? postIdx = 1;

            if(replyIdx != null && postIdx != null)
            {
                App.bambooData.bambooViewModel.BambooReplyDelete(replyIdx, postIdx);
            }
        }
    }

    public class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get
            {
                return (object)GetValue(DataProperty);
            }
            set
            {
                SetValue(DataProperty, value);
            }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(BambooViewModel), typeof(BindingProxy), new UIPropertyMetadata(null));
    }
}