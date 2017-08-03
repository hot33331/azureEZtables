// copyright msg systems ag
// Tobias Hoppenthaler - tobias.hoppenthaler@msg.group
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace azureEZtable.Model
{
    public class AzureService
    {
        MobileServiceClient MobileService;
        IMobileServiceSyncTable<Data> dataTable;
        MobileServiceSQLiteStore store;


        public AzureService()
        {
            MobileService =
    new MobileServiceClient("https://your-backend.azurewebsites.net");
            store = new MobileServiceSQLiteStore("localstore.db");
            store.DefineTable<Data>();
            MobileService.SyncContext.InitializeAsync(store);
            dataTable = MobileService.GetSyncTable<Data>();
        }
        public async Task<ObservableCollection<Data>> GetDataAsync(bool syncItems = true)
        {
            if (syncItems)
            {
                await SyncAsync();
            }
            IEnumerable<Data> items = await dataTable.ToEnumerableAsync();

            return new ObservableCollection<Data>(items);
        }
        public async Task SaveDataAsync(Data item)
        {
            if (item.Id == null)
            {
                await dataTable.InsertAsync(item);
            }
            else
            {
                await dataTable.UpdateAsync(item);
            }
        }
        public async Task DeleteDataAsync(Data item)
        {

            await dataTable.DeleteAsync(item);

        }
        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await MobileService.SyncContext.PushAsync();

                await this.dataTable.PullAsync(
                    //The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                    //Use a different query name for each unique query in your program
                    "allDataItems",
                    this.dataTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }
    }
}
