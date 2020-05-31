using System;
using System.Collections.Generic;
using System.Text;

namespace Lernilo.Common.Models
{
    public class TutorialReportResponse
    {
        public int Id { get; set; }

        public string Report { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();

        public TutorialResponse Tutorial { get; set; }

        public UserResponse Customer { get; set; }

        


    }
}
