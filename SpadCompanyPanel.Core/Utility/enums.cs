using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Core.Utility
{
    public enum StaticContentTypes
    {
        Slider = 1001,
        CompanyHistory = 2,
        BlogImage = 1004,
        TurkeyResidence = 1006,
        UaeResidence = 1007,
        EuropeResidence = 1008,
    }
    public enum StaticContents
    {
        Phone = 1002,
        Map = 1007,
        Address = 1001,
        Email = 1003,
        Youtube = 1008,
        Instagram = 1009,
        Twitter = 1011,
        Pinterest = 1012,
        Facebook = 1010,
        BlogImage = 1013,
        ContactInfo = 1014
    }

    public enum RealStateType
    {
        //Sell = 1,
        //PreOrder = 2,
        //Rent = 3
        Apartment = 1,
        Villa = 2,
        Office = 3,
        Commercial = 4,
        Land = 5
    }

    public enum Language
    {
        Farsi = 1,
        English = 2
    }
}
