using System.Collections.Generic;

namespace GTI.Modules.ProductCenter.Data
{
    public static class MenuItems
    {
        public static List<POSMenuItem> NameSorted(int operatorId)
        {
            var srtlist = GetPOSMenuMessage.GetMenuList(operatorId);
            srtlist.Sort(new MenuItemSorter());
            return srtlist;
        }
    }
    public class MenuItemSorter : IComparer<POSMenuItem>
    {
        public int Compare(POSMenuItem x, POSMenuItem y)
        {
            return x.MenuName.CompareTo(y.MenuName);
        }
    }

    /// <summary>
    /// Returns a list of menus along with their pages and all the buttons within them
    /// </summary>
    public static class FullMenuItems
    {
        /// <summary>
        /// Returns a list of menus with all the info that encompases them. Note: may take a very long time.
        /// </summary>
        /// <param name="operatorId"></param>
        /// <returns></returns>
        public static List<FullMenuItem> GetList(int operatorId)
        {
            List<FullMenuItem> fullItems = new List<FullMenuItem>();
            var list = GetPOSMenuMessage.GetMenuList(operatorId);
            if (list != null)
            {
                foreach (var item in list)
                {
                    int totalPages = 0;
                    FullMenuItem menuItem = new FullMenuItem(item);
                    menuItem.MenuPages = GetMenuButtonMessage.GetButtons(menuItem.MenuId, 0, out totalPages);
                    fullItems.Add(menuItem);
                }
            }

            return fullItems;
        }
    }

    /// <summary>
    /// Represents a menu with all the pages within it
    /// </summary>
    public class FullMenuItem
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int MenuTypeId { get; set; }
        internal List<MenuButtonList> MenuPages { get; set; } // had to make this internal since that's how the MenuButtonList is defined. If it needs to be accessed, change that first

        public FullMenuItem(POSMenuItem src)
        {
            MenuId = src.MenuId;
            MenuName = src.MenuName;
            MenuTypeId = src.MenuTypeId; 
        }
    }
}
