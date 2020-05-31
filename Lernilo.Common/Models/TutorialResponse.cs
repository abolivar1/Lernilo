using System;
using System.Collections.Generic;
using System.Text;

namespace Lernilo.Common.Models
{
    public class TutorialResponse
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public string PicturePath { get; set; }

        public float TotalRate { get; set; }

        public UserResponse Customer { get; set; }

        public CategoryResponse Category { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();

        public ICollection<CommentResponse> Comments { get; set; }

        public ICollection<TutorialReportResponse> TutorialReports { get; set; }
    }
}
