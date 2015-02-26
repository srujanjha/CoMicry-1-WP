using System;

using Windows.ApplicationModel;

namespace AppStudio.ViewModels
{
    public class AboutThisAppViewModel
    {
        public string Publisher
        {
            get
            {
                return Package.Current.Id.Publisher.Substring(3);
            }
        }

        public string AppVersion
        {
            get
            {
                return string.Format("{0}.{1}", Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor);
            }
        }

        public string AboutText
        {
            get
            {
                return "This app brings to you the latest updates from great online comics. >The Oatmeal " +
    ">Garfield-minus-Garfield >xkcd >Least I Could Do >Nuklear Power";
            }
        }
    }
}

