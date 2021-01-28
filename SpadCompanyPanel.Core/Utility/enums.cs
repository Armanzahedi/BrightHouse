using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Core.Utility
{
    public enum StaticContentTypes
    {
        AboutUs = 1,
        AboutUsEnglish = 2009,
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
        AddressEnglish = 3029,
        Email = 1003,
        Youtube = 1008,
        Instagram = 1009,
        Twitter = 1011,
        Pinterest = 1012,
        Facebook = 1010,
        BlogImage = 1013,
        ContactInfo = 1014,

        TurkeyResidence = 2016,
        EuropeResidence = 2024,
        UaeResidence = 2021,

        TurkeyResidenceEnglish = 3033,
        EuropeResidenceEnglish = 3038,
        UaeResidenceEnglish = 3037,
    }

    public enum StaticContentDetails
    {
        AboutUs = 1012,
        AboutUsEnglish = 3028,
        AboutUsItem1 = 3020,
        AboutUsItem2 = 3021,
        AboutUsItem3 = 3022,
        AboutUsItem4 = 3023,
        AboutUsItemEnglish1 = 3024,
        AboutUsItemEnglish2 = 3025,
        AboutUsItemEnglish3 = 3026,
        AboutUsItemEnglish4 = 3027,

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
    public enum GeoDivisionType
    {
        Country = 0,
        State = 1,
        City = 2,
    }
    public enum PaymentStatus
    {
        Registered = 0,
        Payed = 1,
        Confirmed = 2,
    }
}
