using System;
using System.Collections.Generic;
using GTI.Modules.Shared.Data;

namespace GTI.Modules.ProductCenter.Data
{
    public static class CardLevelItems
    {
        public static List<CardLevelItem> Sorted(int cardLevelId)
        {
            var srtlist = GetCardLevelMessage.CardLevels(cardLevelId);
            srtlist.Sort((x, y) => x.CardLevelName.CompareTo(y.CardLevelName));
            return srtlist;
        }

        public static List<CardLevelItem> NameFilteredBy(int cardLevelId, string searchString)
        {
            var srtlist = Sorted(cardLevelId);
            return !string.IsNullOrEmpty(searchString)
                     ? srtlist.FindAll(item => item.CardLevelName.ToUpper().Contains(searchString.ToUpper()))
                     : srtlist;
        }
    }

    public class CardLevelItemsException : Exception
    {
        public CardLevelItemsException()
        {
        }
        public CardLevelItemsException(string message)
            : base(message)
        {
        }
        public CardLevelItemsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
