using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Szmyd.Orchard.Modules.Menu.Enums;

namespace Szmyd.Orchard.Modules.Menu.Models
{
    [OrchardFeature("Szmyd.Menu.Counters")]
    public class RecentlySeenPart : ContentPart<RecentlySeenPartRecord>
    {
        /// <summary>
        /// Regular expression used to filter out the wanted elements.
        /// </summary>
        public string PositiveFilterRegex
        {
            get { return Record.PositiveFilterRegex; }
            set { Record.PositiveFilterRegex = value; }
        }

        /// <summary>
        /// Regular expression used to filter out the unwanted elements.
        /// </summary>
        public string NegativeFilterRegex
        {
            get { return Record.NegativeFilterRegex; }
            set { Record.NegativeFilterRegex = value; }
        }

        [Required]
        public int ItemCount
        {
            get { return Record.ItemCount; }
            set { Record.ItemCount = value; }
        }

        public bool ShowCounts
        {
            get { return Record.ShowCounts; }
            set { Record.ShowCounts = value; }
        }

        public VisitCounterType VisitCounter
        {
            get { return Record.VisitCounter; }
            set { Record.VisitCounter = value; }
        }
    }
}