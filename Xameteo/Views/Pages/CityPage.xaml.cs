﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xameteo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CityPage : ContentPage
    {
        CityViewModel VM;
        public CityPage()
        {
            InitializeComponent();
            BindingContext = VM = new CityViewModel();
        }
    }
}