using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SCWorldEdit
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : RibbonWindow
    {
        public MainView()
        {
            InitializeComponent();
        }

		private void RibbonWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			MainViewModel localVM = this.DataContext as MainViewModel;

			if (localVM != null && localVM.CloseAction == null)
			{
				localVM.CloseAction = Close;
			}
		}
    }
}
