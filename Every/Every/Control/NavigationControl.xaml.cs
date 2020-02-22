﻿using Every.Common;
using System;
using System.Collections.Generic;
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

namespace Every.Control
{
    /// <summary>
    /// Interaction logic for NavigationControl.xaml
    /// </summary>
    public partial class NavigationControl : UserControl
    {
        List<NaviData> naviDatas = new List<NaviData>();

        public NavigationControl()
        {
            InitializeComponent();
            Loaded += NavigationControl_Loaded;
        }

        private void NavigationControl_Loaded(object sender, RoutedEventArgs e)
        {
            naviDatas.Add(new NaviData
            {
                Title = "Home",
                NaviImagePath = ComDef.Path + "HomeIcon.png",
                naviMenu = NaviMenu.Home
            });
            naviDatas.Add(new NaviData
            {
                Title = "대나무숲",
                NaviImagePath = ComDef.Path + "BambooIcon.png",
                naviMenu = NaviMenu.Bamboo
            });
            naviDatas.Add(new NaviData
            {
                Title = "일정 관리",
                NaviImagePath = ComDef.Path + "ScheduleIcon.png",
                naviMenu = NaviMenu.Schedule
            });
            naviDatas.Add(new NaviData
            {
                Title = "설정",
                NaviImagePath = ComDef.Path + "OptionIcon.png",
                naviMenu = NaviMenu.Option
            });

            lvNavi.ItemsSource = naviDatas;
        }

        private void lvNavi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IPage page = null;
            NaviData selectdata = lvNavi.SelectedItem as NaviData;

            switch(selectdata.naviMenu)
            {
                case NaviMenu.Home:
                    page = ctrlHome;
                    break;
                case NaviMenu.Bamboo:
                     page = ctrlBamboo;
                    ctrlBamboo.LoadData();
                     break;
                case NaviMenu.Schedule:
                    page = ctrlSchedule;
                    break;
                case NaviMenu.Option:
                    page = ctrlOption;
                    break;
            }

            ShowPage(page);
        }

        private void ShowPage(IPage page)
        {
            if (page != null && page is FrameworkElement element)
            {
                CollapsePages();
                element.Visibility = Visibility.Visible;
            }
        }

        private void CollapsePages()
        {
            foreach (FrameworkElement element in gdPage.Children)
            {
                element.Visibility = Visibility.Collapsed;
            }
        }
    }

    public class NaviData
    {
        public NaviMenu naviMenu { get; set; }
        public string Title { get; set; }
        public string NaviImagePath { get; set; }
    }

    public enum NaviMenu
    {
        Home, Bamboo, Schedule, Option
    }

    interface IPage
    {
        string GetTitle();
    }
}