using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Models
{
    public class AboutViewModel
    {
        public List<Statistic> Statistics { get; set; } = new();
        public List<OurFuture> OurFutures { get; set; } = new();
    }
}
