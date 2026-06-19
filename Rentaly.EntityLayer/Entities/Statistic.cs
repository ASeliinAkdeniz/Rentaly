using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.EntityLayer.Entities
{
    // Statistic.cs
    public class Statistic
    {
        public int StatisticId { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }
        public string IconUrl { get; set; }
    }
}
