// copyright msg systems ag
// Tobias Hoppenthaler - tobias.hoppenthaler@msg.group
using System;
using azureEZtable.Model;
using Xamarin.Forms;

namespace azureEZtable.ViewModel
{
    public class azureEZtableViewModel
    {
        AzureService service;
        public azureEZtableViewModel()
        {
			 service = new AzureService();
            DoitCommand = new Command(DoitCommandAction);

			
        }

        public Command DoitCommand { get; set; }

        private async void DoitCommandAction(object obj)
        {
			Data data = new Data();
			data.Accuracy = "11";
			data.Latitude = "122";
			data.Longitude = "3";
			data.Route = "0";
			data.Timestamp = DateTime.Now.Ticks.ToString();
			data.User = "msg";

			var content = await service.GetDataAsync();

			await service.SaveTaskAsync(data);
        }
    }
}
