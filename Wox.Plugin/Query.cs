﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Wox.Plugin
{
    public class Query
    {
        /// <summary>
        /// Raw query, this includes action keyword if it has
        /// We didn't recommend use this property directly. You should always use Search property.
        /// </summary>
        public string RawQuery { get; internal set; }

        /// <summary>
        /// Search part of a query.
        /// This will not include action keyword if exclusive plugin gets it, otherwise it should be same as RawQuery.
        /// Since we allow user to switch a exclusive plugin to generic plugin, 
        /// so this property will always give you the "real" query part of the query
        /// </summary>
        public string Search { get; internal set; }

        /// <summary>
        /// The raw query splited into a string array.
        /// </summary>
        internal string[] Terms { private get; set; }

        public const string Seperater = " ";

        /// <summary>
        /// * is used for System Plugin
        /// </summary>
        public const string WildcardSign = "*";

        internal string ActionKeyword { get; set; }

        /// <summary>
        /// Return first search split by space if it has
        /// </summary>
        public string FirstSearch => SplitSearch(0);

        /// <summary>
        /// strings from second search (including) to last search
        /// </summary>
        public string SecondToEndSearch
        {
            get
            {
                var index = String.IsNullOrEmpty(ActionKeyword) ? 1 : 2;
                return String.Join(Seperater, Terms.Skip(index).ToArray());
            }
        }

        /// <summary>
        /// Return second search split by space if it has
        /// </summary>
        public string SecondSearch => SplitSearch(1);

        /// <summary>
        /// Return third search split by space if it has
        /// </summary>
        public string ThirdSearch => SplitSearch(2);

        private string SplitSearch(int index)
        {
            try
            {
                return String.IsNullOrEmpty(ActionKeyword) ? Terms[index] : Terms[index + 1];
            }
            catch (IndexOutOfRangeException)
            {
                return String.Empty;
            }
        }

        public override string ToString() => RawQuery;

        [Obsolete("Use Search instead, A plugin developer shouldn't care about action name, as it may changed by users. " +
                  "this property will be removed in v1.3.0")]
        public string ActionName { get; internal set; }

        [Obsolete("Use Search instead, this property will be removed in v1.3.0")]
        public List<string> ActionParameters { get; internal set; }

        [Obsolete("Use Search instead, this method will be removed in v1.3.0")]
        public string GetAllRemainingParameter() => Search;
    }
}
