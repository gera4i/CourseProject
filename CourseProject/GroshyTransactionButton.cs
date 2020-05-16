using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Controls;
using System.Windows.Media;

namespace CourseProject
{
    class GroshyTransactionButton : Button
    {
        public bool isChecked = false;

        public void ToggleButtonOn(bool state)
        {
            if (state)
            {
                isChecked = false;
                this.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#1167B1");
            }
            else
            {
                isChecked = true;
                this.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0F559A");
            }
        }
        override protected void OnClick()
        {
            if(isChecked)
            {
                isChecked = false;
                this.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#1167B1");
            }
            else
            {
                isChecked = true;
                this.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0F559A");
            }
        }
    }
}
