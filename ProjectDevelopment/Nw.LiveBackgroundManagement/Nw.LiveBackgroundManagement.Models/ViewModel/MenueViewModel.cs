using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Models.ViewModel
{
    public class MenueViewModel
    {

        public MenueViewModel()
        {
            this.SubMenueViewModel = new List<MenueViewModel>();
        }

        public int Id { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public byte MenuLevel { get; set; }

        public byte MenuType { get; set; }

        public string MenuIcon { get; set; }

        public string Description { get; set; }

        public string SourcePath { get; set; }

        public int Sort { get; set; }

        public byte Status { get; set; }

        public List<MenueViewModel> SubMenueViewModel { get; set; }
    }
}
