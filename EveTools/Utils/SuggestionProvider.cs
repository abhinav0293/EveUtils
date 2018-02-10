using System.Collections;
using System.Collections.Generic;
using WpfControls;

namespace EveTools.Utils
{
    public class SuggestionProvider : ISuggestionProvider
    {
        public IEnumerable GetSuggestions(string filter)
        {
            List<string> li = new List<string>();
            int count = 0;
            foreach (string st in App.auto)
            {
                if (st.ToLower().StartsWith(filter.ToLower()))
                {
                    li.Add(st);
                    count++;
                }
                if (count > 5)
                    break;
            }
            return li;
        }
    }
}
