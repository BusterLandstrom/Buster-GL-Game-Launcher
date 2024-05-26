using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Items
{
    public class Theme
    {
        public required string BackgroundColor {  get; set; }
        public required string ForegroundColor { get; set; }
        public required string SelectablePrimaryColor { get; set; }
        public required string SelectableSecondaryColor { get; set; }
        public required string SelectableLinkColor { get; set; }

    }
}
