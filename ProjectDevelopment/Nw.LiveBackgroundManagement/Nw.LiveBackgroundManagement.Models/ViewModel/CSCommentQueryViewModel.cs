using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Models.ViewModel
{
    public class CSCommentQueryViewModel
    {
        public int CommentId { get; set; }

        public string ImgUrl { get; set; }

        public int ToUserId { get; set; }
        public string CommentName { get; set; }
         
        public string RepliesName { get; set; }
         
        public string CommentDate { get; set; }
         
        public string RepliesCount { get; set; }
        public string Content { get; set; }

        public List<CSCommentQueryViewModel> ChildData { get; set; } 
    }
}
