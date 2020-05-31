using System;
using System.Collections.Generic;
using System.Text;

namespace Lernilo.Common.Models
{
    public class CommentResponse
    {
        public int Id { get; set; }

        public string Remark { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartDateLocal => Date.ToLocalTime();

        public TutorialResponse Tutorial { get; set; }

        public UserResponse Customer { get; set; }
    }
}
