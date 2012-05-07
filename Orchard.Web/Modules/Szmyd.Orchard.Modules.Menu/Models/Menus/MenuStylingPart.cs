using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Szmyd.Orchard.Modules.Menu.Enums;

namespace Szmyd.Orchard.Modules.Menu.Models
{
    /// <summary>
    /// Part for styling the plain menu
    /// </summary>
    public class MenuStylingPart : ContentPart<MenuStylingPartRecord>
    {
        [DefaultValue("#FFF")]
        public string BackColor
        {
            get { return Record.BackColor; }
            set { Record.BackColor = value; }
        }

        [DefaultValue("#000")]
        public string ForeColor
        {
            get { return Record.ForeColor; }
            set { Record.ForeColor = value; }
        }

        [DefaultValue("#000")]
        public string SelectedForeColor
        {
            get { return Record.SelectedForeColor; }
            set { Record.SelectedForeColor = value; }
        }

        [DefaultValue("#DDD")]
        public string SelectedBackColor
        {
            get { return Record.SelectedBackColor; }
            set { Record.SelectedBackColor = value; }
        }

        [DefaultValue("#000")]
        public string HoverForeColor
        {
            get { return Record.HoverForeColor; }
            set { Record.HoverForeColor = value; }
        }

        [DefaultValue("#EEE")]
        public string HoverBackColor
        {
            get { return Record.HoverBackColor; }
            set { Record.HoverBackColor = value; }
        }

        [DefaultValue(MenuStyles.SuperfishHorizontal)]
        public MenuStyles Style
        {
            get { return Record.Style; }
            set { Record.Style = value; }
        }
    }
}