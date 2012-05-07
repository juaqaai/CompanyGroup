using System;
using Orchard.ContentManagement;
using Orchard.DisplayManagement.Descriptors;
using Orchard.Utility.Extensions;
using Szmyd.Orchard.Modules.Menu.Enums;
using Szmyd.Orchard.Modules.Menu.Models;

// ReSharper disable InconsistentNaming

namespace Szmyd.Orchard.Modules.Menu
{
    public class Shapes : IShapeTableProvider
    {

        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("Menu")
                .OnDisplaying(displaying =>
                {
                    var menu = displaying.Shape;
                    string menuId = menu.ItemId;
                    if (menuId == null) return;

                    menu.Id = "menu-" + menuId.ToLowerInvariant();
                    menu.Metadata.Alternates.Add("Menu__" + menuId);
                });

            builder.Describe("Breadcrumbs")
                .OnDisplaying(displaying =>
                {
                    dynamic bc = displaying.Shape;
                    bc.Classes.Add("breadcrumbs");
                    var item = (IContent) bc.ContentItem;
                    if (item == null) return;

                    bc.Classes.Add("breadcrumbs-" + item.ContentItem.ContentType.HtmlClassify());
                    bc.Id = "breadcrumbs-" + item.ContentItem.Id;
                    bc.Metadata.Alternates.Add("Breadcrumbs__" + item.ContentItem.ContentType);
                    bc.Metadata.Alternates.Add("Breadcrumbs__" + item.ContentItem.Id);

                    if(item.Is<BreadcrumbsPart>()) {
                        var part = item.As<BreadcrumbsPart>();
                        bc.Metadata.Alternates.Add("Breadcrumbs__" + part.MenuName);
                    }
                });

            builder.Describe("Counter")
                .OnDisplaying(displaying =>
                {
                    dynamic counter = displaying.Shape;
                    counter.Classes.Add("counter");
                    counter.Classes.Add("counter-" + Enum.GetName(typeof(VisitCounterType),counter.Type).HtmlClassify());
                    counter.Metadata.Alternates.Add("Counter__" + counter.Type);

                    var item = (IContent) counter.ContentItem;
                    if (item != null) {
                        counter.Classes.Add("counter-" + item.ContentItem.ContentType.HtmlClassify());
                        counter.Id = "counter-" + item.ContentItem.Id;
                        counter.Metadata.Alternates.Add("Counter__" + item.ContentItem.ContentType);
                        counter.Metadata.Alternates.Add("Counter__" + item.ContentItem.Id);
                    }  
                });
        }
    }
}
