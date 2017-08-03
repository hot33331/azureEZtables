// copyright msg systems ag
// Tobias Hoppenthaler - tobias.hoppenthaler@msg.group
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace azureEZtable.Model
{
    public class AzureService
    {
        public static MobileServiceClient MobileService;
        IMobileServiceTable<Data> dataTable;

        public AzureService()
        {
            MobileService =
    new MobileServiceClient("https://your-backend.azurewebsites.net");
            dataTable = MobileService.GetTable<Data>();
        }
        public async Task<ObservableCollection<Data>> GetDataAsync(bool syncItems = false)
        {

            IEnumerable<Data> items = await dataTable.ToEnumerableAsync();

            return new ObservableCollection<Data>(items);
        }
        public async Task SaveTaskAsync(Data item)
        {
            if (String.IsNullOrEmpty(item.Id))
            {
                await dataTable.InsertAsync(item);
            }
            else
            {
                await dataTable.UpdateAsync(item);
            }
        }
        public async Task DeleteTaskAsync(Data item)
        {

            await dataTable.DeleteAsync(item);

        }
    }
}
