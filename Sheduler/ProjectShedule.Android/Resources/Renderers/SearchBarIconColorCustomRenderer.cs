﻿using Android.Content;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SearchBar), typeof(ProjectShedule.Droid.Resources.Renderers.CustomSearchBarRenderer))]
namespace ProjectShedule.Droid.Resources.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        public CustomSearchBarRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            var icon = Control?.FindViewById(Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null));
            (icon as ImageView)?.SetColorFilter(Color.MediumPurple.ToAndroid());
        }
    }
}