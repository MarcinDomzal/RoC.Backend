using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoC.Domain.Entities
{
    public class DriverVehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ControlDay { get; set; }
        public DateTime ExamDay { get; set; }
        public DateTime PeriodDay { get; set; }
        public DateTime NextDay { get; set; }
        public Boolean IsPassed { get; set; }
        public string Description { get; set; }

    }
}
