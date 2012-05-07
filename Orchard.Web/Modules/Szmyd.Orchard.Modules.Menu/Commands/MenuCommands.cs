using System;
using System.Linq;
using Orchard;
using Orchard.Commands;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.Core.Common.Models;
using Orchard.Security;
using Orchard.Settings;
using Orchard.Widgets.Models;
using Orchard.Widgets.Services;
using Szmyd.Orchard.Modules.Menu.Models;
using Szmyd.Orchard.Modules.Menu.Enums;

namespace Szmyd.Orchard.Modules.Menu.Commands
{
    public class MenuCommands : DefaultOrchardCommandHandler
    {
        private readonly IWidgetsService _widgetsService;
        private readonly ISiteService _siteService;
        private readonly IMembershipService _membershipService;

        public MenuCommands(IWidgetsService widgetsService, ISiteService siteService, IMembershipService membershipService)
        {
            _widgetsService = widgetsService;
            _siteService = siteService;
            _membershipService = membershipService;
        }

        [OrchardSwitch]
        public string MenuStyle { get; set; }

        [OrchardSwitch]
        public string MenuName { get; set; }

        [OrchardSwitch]
        public string Title { get; set; }

        [OrchardSwitch]
        public string Zone { get; set; }

        [OrchardSwitch]
        public string Position { get; set; }

        [OrchardSwitch]
        public string Layer { get; set; }

        [OrchardSwitch]
        public string Identity { get; set; }

        [OrchardSwitch]
        public string Owner { get; set; }

        [OrchardSwitch]
        public bool Publish { get; set; }

        [CommandName("menuwidget create")]
        [CommandHelp("menuwidget create <type> [/MenuStyle:<style>] [/MenuName:<name>] /Title:<title> /Zone:<zone> /Position:<position> /Layer:<layer> [/Identity:<identity>] [/Owner:<owner>] [/Text:<text>] \r\n\t" + "Creates a new menu widget")]
        [OrchardSwitches("MenuStyle,MenuName,Title,Zone,Position,Layer,Identity,Owner")]
        public void Create(string type)
        {
            var widgetTypeNames = _widgetsService.GetWidgetTypeNames();
            if (!widgetTypeNames.Contains(type))
            {
                throw new OrchardException(T("Creating widget failed : type {0} was not found. Supported widget types are: {1}.",
                    type,
                    widgetTypeNames.Aggregate(String.Empty, (current, widgetType) => current + " " + widgetType)));
            }

            var layer = GetLayer(Layer);
            if (layer == null)
            {
                throw new OrchardException(T("Creating widget failed : layer {0} was not found.", Layer));
            }

            var widget = _widgetsService.CreateWidget(layer.ContentItem.Id, type, T(Title).Text, Position, Zone);
            if (String.IsNullOrEmpty(Owner))
            {
                Owner = _siteService.GetSiteSettings().SuperUser;
            }
            var owner = _membershipService.GetUser(Owner);
            widget.As<ICommonPart>().Owner = owner;

            widget.As<MenuWidgetPart>().Levels = 0;
            widget.As<MenuWidgetPart>().CutOrFlattenLower = false;
            widget.As<MenuWidgetPart>().MenuName = string.IsNullOrWhiteSpace(MenuName) ? MenuName : "main";
            widget.As<MenuWidgetPart>().WrapChildrenInDiv = false;
            widget.As<MenuWidgetPart>().Mode = MenuWidgetMode.AllItems;

            if (widget.Has<MenuStylingPart>() )
            {
                MenuStyles style; // default
                widget.As<MenuStylingPart>().Style = Enum.TryParse(MenuStyle, out style) ? style : MenuStyles.SuperfishHorizontal;
                widget.As<MenuStylingPart>().HoverBackColor = "#F7F7F7";
                widget.As<MenuStylingPart>().HoverForeColor = "#444444";
                widget.As<MenuStylingPart>().SelectedForeColor = "#333333";
                widget.As<MenuStylingPart>().SelectedBackColor = "#F1F1F1";
            }

            if (widget.Has<IdentityPart>() && !String.IsNullOrEmpty(Identity))
            {
                widget.As<IdentityPart>().Identifier = Identity;
            }

            Context.Output.WriteLine(T("Menu widget created successfully.").Text);
        }

        private LayerPart GetLayer(string layer)
        {
            var layers = _widgetsService.GetLayers();
            return layers.FirstOrDefault(layerPart => String.Equals(layerPart.Name, layer, StringComparison.OrdinalIgnoreCase));
        }
    }
}