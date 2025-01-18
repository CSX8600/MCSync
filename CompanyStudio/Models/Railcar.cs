using CompanyStudio.Extensions;
using MesaSuite.Common.Data;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace CompanyStudio.Models
{
    public class Railcar
    {
        public long? RailcarID { get; set; }
        public string ReportingMark { get; set; }
        public string ReportingNumber { get; set; }
        public string ReportingID => $"{ReportingMark}{ReportingNumber}";
        public RailLocation RailLocation { get; set; }
        public long? TrackIDDestination { get; set; }
        public Track TrackDestination { get; set; }
        public long? CompanyIDOwner { get; set; }
        public Company CompanyPossessor { get; set; }
        public Government GovernmentPossessor { get; set; }
        public RailcarModel RailcarModel { get; set; }
        public List<BillOfLading> BillsOfLading { get; set; } = new List<BillOfLading>();
        public List<RailcarLoad> RailcarLoads { get; set; } = new List<RailcarLoad>();
        public List<RailcarRoute> RailcarRoutes { get; set; } = new List<RailcarRoute>();

        public async Task<Image> GetImage(long? companyID, long? locationID)
        {
            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Railcar/GetImage/" + RailcarID);
            get.AddLocationHeader(companyID, locationID);
            byte[] imageData = await get.GetObject<byte[]>();

            if (imageData != null)
            {
                using (MemoryStream stream = new MemoryStream(imageData))
                {
                    return Image.FromStream(stream);
                }
            }

            return null;
        }
    }
}
