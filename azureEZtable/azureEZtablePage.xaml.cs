// copyright msg systems ag
// Tobias Hoppenthaler - tobias.hoppenthaler@msg.group
using System;
using System.Windows.Input;
using azureEZtable.Model;
using azureEZtable.ViewModel;
using Xamarin.Forms;

namespace azureEZtable
{
    public partial class azureEZtablePage : ContentPage
    {
        public azureEZtablePage()
        {
            InitializeComponent();
			

			BindingContext = new azureEZtableViewModel();

        }
    }
	
	

}
