using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Common.Api;

namespace Nw.LiveBackgroundManagement.Models.Query
{
    public class CommentPageQuery : Page
    { 
        public int CommentType { get; set; }

        public int UserId { get; set; }
    }
}
