namespace Szmyd.Orchard.Modules.Menu.Enums
{
    /// <summary>
    /// Display modes for sub menu part
    /// </summary>
    public enum MenuWidgetMode
    {
        /// <summary>
        /// Display all menu items, in hierarchy
        /// </summary>
        AllItems,

        /// <summary>
        /// Display only sub-items, by current route position.
        /// </summary>
        ChildrenOnly,

        /// <summary>
        /// Displays only same-level items.
        /// </summary>
        SiblingsOnly,

        /// <summary>
        /// Displays same-level items fully expanded and other without children
        /// </summary>
        SiblingsExpanded
       
    }
}