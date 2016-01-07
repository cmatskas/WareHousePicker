using System;
using System.Collections.Generic;
using System.Linq;
using WareHousePicker;
using Xunit;

namespace WareHousePickerTests
{
    public class PickerServiceTests
    {
        [Fact]
        public void ForOneItem_ShouldReturn_ZeroCost()
        {
            var itemsToTest = new List<string> { "a5" };
            var service = new PickerService();
            service.CalculatePath(itemsToTest);

            Assert.True(service.Cost == 0);
        }

        [Fact]
        public void ForTwoItemsInAandC_ShouldReturn_RightCost()
        {
            var itemsToTest = new List<string> { "a5", "c7" };
            var service = new PickerService();
            service.CalculatePath(itemsToTest);

            Assert.True(service.Cost == 11);
        }

        [Fact]
        public void ForItemsInCandB_ShouldReturn_RightCost()
        {
            var itemsToTest = new List<string> { "c5", "c7", "b3", "b4" };
            var service = new PickerService();
            service.CalculatePath(itemsToTest);

            Assert.True(service.Cost == 8);
        }

        [Fact]
        public void ForItemsInAandCandB_ShouldReturn_RightCost()
        {
            var itemsToTest = new List<string> { "a5", "c7", "b3", "b4" };
            var service = new PickerService();
            service.CalculatePath(itemsToTest);

            Assert.True(service.Cost == 18);
        }

        [Fact]
        public void ForTestCaseInReqs_ShouldReturn_RightCost()
        {
            var itemsToTest = new List<string> { "b3", "c7", "c9", "a3" };
            var service = new PickerService();
            service.CalculatePath(itemsToTest);

            Assert.True(service.Cost == 15);
        }

        [Fact]
        public void ForTestCaseInReqs_ShouldReturn_RightBay()
        {
            var expectedResult = "c1,b3,c7,c5,a6";
            var itemsToTest = new List<string> { "rusty nail", "picture frame", "paint brush", "thermometer", "shovel" };
            var service = new PickerService();
            service.CalculateBays(itemsToTest);

            var result = string.Join(",",service.PickedItems.ToArray());
            Assert.Equal(expectedResult, result);
        }
    }
}
