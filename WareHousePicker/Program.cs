using System;
using System.Collections.Generic;

namespace WareHousePicker
{
    class Program
    {
        static void Main(string[] args)
        {
            var bayList = new List<string> { "a3", "c2", "b3"};
            var itemList = new List<string> { "rusty nail", "picture frame", "paint brush", "thermometer", "shovel" };
            var pickerService = new PickerService();

            pickerService.CalculatePath(bayList);
            Console.WriteLine(string.Join("->",pickerService.PickedItems));
            Console.WriteLine(string.Format("Items were {0} apart", pickerService.Cost));

            pickerService.CalculateBays(itemList);
            Console.WriteLine(string.Join("->", pickerService.PickedItems));
            Console.ReadKey();
        }
    }
}
