using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class SatMorningBreakfastCerealDataSource : IDataSource<RssSchema>
    {
        private const string _url =@"http://feeds.feedburner.com/smbc-comics/PvLb";

        private IEnumerable<RssSchema> _data = null;

        public SatMorningBreakfastCerealDataSource()
        {
        }

        public async Task<IEnumerable<RssSchema>> LoadData()
        {
            if (_data == null)
            {
                try
                {
                    var rssDataProvider = new RssDataProvider(_url);
                    _data = await rssDataProvider.Load();
                }
                catch (Exception ex)
                {
                    AppLogs.WriteError("SatMorningBreakfastCerealDataSourceDataSource.LoadData", ex.ToString());
                }
            }
            return _data;
        }

        public async Task<IEnumerable<RssSchema>> Refresh()
        {
            _data = null;
            return await LoadData();
        }
    }
}
