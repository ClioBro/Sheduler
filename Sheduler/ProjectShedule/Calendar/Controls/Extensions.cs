using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectShedule.Calendar.Controls
{
    internal static class Extensions
    {
        internal static string Capitalize(this string source)
        {
            if (source.Length == 0)
                return source;

            return char.ToUpper(source[0]) + source.Substring(1, source.Length - 1);
        }

        internal static object CreateContent(this DataTemplate dataTemplate, object itemModel)
        {
            if (dataTemplate is DataTemplateSelector templateSelector)
            {
                var template = templateSelector.SelectTemplate(itemModel, null);
                template.SetValue(BindableObject.BindingContextProperty, itemModel);

                return template.CreateContent();
            }

            dataTemplate.SetValue(BindableObject.BindingContextProperty, itemModel);
            return dataTemplate.CreateContent();
        }

        public static async Task<bool> HeightTo(this View view, double height, uint duration = 250, Easing easing = null)
        {
            var tcs = new TaskCompletionSource<bool>();

            var heightAnimation = new Animation(x => view.HeightRequest = x, view.Height, height);
            heightAnimation.Commit(view, "HeightAnimation", 10, duration, easing, (finalValue, finished) => { tcs.SetResult(finished); });

            return await tcs.Task;
        }

        public static async Task<bool> WidthTo(this View view, double width, uint duration = 250, Easing easing = null)
        {
            var tcs = new TaskCompletionSource<bool>();

            var heightAnimation = new Animation(x => view.WidthRequest = x, view.Height, width);
            heightAnimation.Commit(view, "WidthAnimation", 10, duration, easing, (finalValue, finished) => { tcs.SetResult(finished); });

            return await tcs.Task;
        }
    }
}
