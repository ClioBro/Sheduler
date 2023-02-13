using ProjectShedule.Shedule.Interfaces;
using System;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Builder
{
    public abstract class EditorPageBuilder<TEditTarget> : IBuilder<ContentPage>
    {
        protected ContentPage _page;

        public abstract ContentPage Build();
        public abstract EditorPageBuilder<TEditTarget> SetSavePressedCallBack(Action<TEditTarget> SavePressedCallBack);
        public abstract EditorPageBuilder<TEditTarget> SetEditTarget(TEditTarget target);
    }

}
