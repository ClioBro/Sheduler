using System;
using System.Collections.Generic;
using System.Linq;
using ProjectShedule.Core.Swipe.Interfaces;
using Xamarin.Forms;

namespace ProjectShedule.Core.Swipe
{
    public class SwipeHorizontalContent : ISwipeHorizontalContent
    {
        #nullable enable annotations
        public SwipeItems RightItems { get; protected set; } = new SwipeItems();
        public SwipeItems LeftItems { get; protected set; } = new SwipeItems();
        public double Threshold { get; protected set; } = 100;

        public ISwipeViewController? SwipeViewController { get; set; }

        public void DisableSwipeItem(Type type)
        {
            SetVisible(GetByType(type), false);
        }
        public void EnableSwipeItem(Type type)
        {
            SetVisible(GetByType(type), true);
        }
        public void DisableSwipeItems()
        {
            SetVisible(RightItems, false);
            SetVisible(LeftItems, false);
        }
        public void EnableSwipeItems()
        {
            SetVisible(RightItems, true);
            SetVisible(LeftItems, true);
        }

        private void SetVisible(IEnumerable<ISwipeItem> swipeItems, bool isVisible)
        {
            foreach (ISwipeItem swipeItem in swipeItems)
                SetVisible(swipeItem, isVisible);
        }
        private void SetVisible(ISwipeItem swipeItem, bool isVisible)
        {
            swipeItem.IsVisible = isVisible;
        }
        private ISwipeItem GetByType(Type type)
        {
            List<ISwipeItem> tempList = new List<ISwipeItem>();
            tempList.AddRange(RightItems);
            tempList.AddRange(LeftItems);
            ISwipeItem result = tempList.FirstOrDefault(t => t.GetType() == type);
            if (result is null)
                throw new NullReferenceException($"{nameof(result)}");

            return result;
        }
    }
}
