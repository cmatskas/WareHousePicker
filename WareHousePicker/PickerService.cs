using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHousePicker
{
    public class PickerService
    {
        private Dictionary<string, string> rowA;
        private Dictionary<string, string> rowB;
        private Dictionary<string, string> rowC;
        private List<string> pickedItems;
        public IEnumerable<string> PickedItems { get { return pickedItems; } }
        public int Cost { get; private set; }

        public PickerService()
        {
            InitializeData();
        }

        public void CalculatePath(List<string> bays)
        {
            pickedItems = new List<string>();
            var rowAItems = bays.Where(b => b.StartsWith("a")).OrderByDescending(b => b).ToList();
            var rowBItems = bays.Where(b => b.StartsWith("b")).OrderBy(b => b).ToList();
            var rowCItems = bays.Where(b => b.StartsWith("c")).OrderBy(b => b).ToList();
            
            foreach(var bay in rowAItems)
            {
                pickedItems.Add(GetItemFromBay(bay));
            }

            foreach(var bay in rowCItems)
            {
                pickedItems.Add(GetItemFromBay(bay));
            }

            foreach(var bay in rowBItems)
            {
                pickedItems.Add(GetItemFromBay(bay));
            }

            Cost = bays.Count > 1 ? GetListCost(rowAItems, rowBItems, rowCItems) : 0;
        }

        public void CalculateBays(List<string> items)
        {
            pickedItems = new List<string>();
            var combinedList = rowA.Union(rowB).Union(rowC).ToDictionary(k => k.Key, v => v.Value);
            foreach(var item in items)
            {
                pickedItems.Add(combinedList.FirstOrDefault(b => b.Value.Equals(item, StringComparison.OrdinalIgnoreCase)).Key);
            }
        }

        private string GetItemFromBay(string bay)
        {
            if(bay.StartsWith("a"))
            {
                return rowA[bay];
            }

            if(bay.StartsWith("b"))
            {
                return rowB[bay];
            }

            return rowC[bay];
        }

        private int GetListCost(List<string> listA, List<string> listB, List<string> listC)
        {
            var cost = 0;
            if(listB.Count > 0)
            {
                cost = int.Parse(listB.Max(b => b).Substring(1));
            }
           
            if(listA.Count > 0)
            {
                cost += int.Parse(listA.Max(b => b).Substring(1));
            }

            if(listA.Any() && listB.Any())
            {
                cost += 10;
            }
            else if(listC.Count >= 1)
            {
                if (listA.Any())
                {
                    cost += int.Parse(listC.Max(b => b).Substring(1));
                }
                else if(listB.Any())
                {
                    cost += int.Parse(listC.Min(b => b).Substring(1));
                }
            }

            return cost - 1; 
        }

        private void InitializeData()
        {
            rowA = new Dictionary<string, string>
            {
                { "a3", "blouse" },
                { "a7", "bookmark"},
                {"a9", "glow stick"},
                { "a4", "hanger"},
                { "a8" ,"model car" },
                { "a1" , "needle"},
                { "a10", "rubber band"},
                { "a5", "rubber duck"},
                { "a6" ,"shovel"},
                { "a2", "stop sign"},
            };

            rowB = new Dictionary<string, string> {
                 { "b7" ,"bath fizzers"},
                { "b10" ,"cookie jar"},
                {"b9", "deodorant" },
                { "b5","nail filer"},
                {"b4" ,"photo album"},
                { "b3" ,"picture frame"},
                {"b2" ,"sharpie"},
                {"b1","tyre swing" },
                { "b8","tissue box" },
                { "b6" ,"tooth paste"}
            };

            rowC = new Dictionary<string, string> {
                {"c8", "candy wrapper"},
                { "c3","chalk"},
                { "c2","drill press"},
                { "c6", "face wash" },
                { "c10", "leg warmers"},
                { "c7", "paint brush"},
                { "c1", "rusty nail"},
                { "c9", "shoe lace"},
                { "c5" , "thermometer"},
                {"c4",  "word search"}
            };
        }
    }
}
