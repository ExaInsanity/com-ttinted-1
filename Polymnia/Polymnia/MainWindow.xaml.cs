using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;

namespace Polymnia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetResult_Click(Object sender, RoutedEventArgs e)
        {
            Int32 required = Convert.ToInt32(this.MinRequired.Text);
            String[] members1 = Array.Empty<String>();
            String[] members2 = Array.Empty<String>();

            CultureInfo ci = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            ci.NumberFormat.NumberGroupSeparator = ",";
            Thread.CurrentThread.CurrentCulture = ci;

            foreach(var v in List1.Text.Split('\n'))
            {
                if(!v.StartsWith('#'))
                    continue;

                if(ParseThousandsSeparator(v.Split(' ')[2]) > required - 1)
                    continue;

                members1 = members1.Append(v.Split(' ')[1].Trim(':')).ToArray();
            }

            foreach(var v in List2.Text.Split('\n'))
            {
                if(!v.StartsWith('#'))
                    continue;

                if(ParseThousandsSeparator(v.Split(' ')[2]) > required - 1)
                    continue;

                members2 = members2.Append(v.Split(' ')[1].Trim(':')).ToArray();
            }

            String result = "The following members did not reach the quota:\n";

            foreach(var v in members1)
            {
                if(!members2.Contains(v))
                    continue;

                result += $"{v}\n";
            }

            Output.Text = result;
        }

        private Int32 ParseThousandsSeparator(String value)
        {
            String[] thousands = value.Trim('\r').Split(',');
            String final = "";

            foreach(var v in thousands)
                final += v;

            return Convert.ToInt32(final);
        }
    }
}
