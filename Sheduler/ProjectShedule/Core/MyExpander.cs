using System.Windows.Input;
using System;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.CommunityToolkit.UI.Views.Internals;
using Xamarin.Forms;
using ProjectShedule.Core.Expander;

namespace ProjectShedule.Core
{
    [ContentProperty("Content")]
    public class MyExpander : BaseTemplatedView<StackLayout>
    {
        #nullable enable annotations
        private const string expandAnimationName = "expandAnimationName";
        private const uint defaultAnimationLength = 250u;
        private readonly WeakEventManager tappedEventManager = new WeakEventManager();
        private ContentView? contentHolder;
        private GestureRecognizer? headerTapGestureRecognizer;
        private DataTemplate? previousTemplate;
        private double lastVisibleSize = -1.0;
        private Size previousSize = new Size(-1.0, -1.0);
        private bool shouldIgnoreContentSetting;
        private readonly object contentSetLocker = new object();

        #region BindableProperties
        public static readonly BindableProperty HeaderProperty = BindableProperty.Create("Header", typeof(View), typeof(MyExpander), null, BindingMode.OneWay, null, OnHeaderPropertyChanged);
        public static readonly BindableProperty ContentProperty = BindableProperty.Create("Content", typeof(View), typeof(MyExpander), null, BindingMode.OneWay, null, OnContentPropertyChanged);
        public static readonly BindableProperty ContentTemplateProperty = BindableProperty.Create("ContentTemplate", typeof(DataTemplate), typeof(MyExpander), null, BindingMode.OneWay, null, OnContentTemplatePropertyChanged);
        public static readonly BindableProperty IsExpandedProperty = BindableProperty.Create("IsExpanded", typeof(bool), typeof(MyExpander), false, BindingMode.TwoWay, null, OnIsExpandedPropertyChanged);
        public static readonly BindableProperty DirectionProperty = BindableProperty.Create("Direction", typeof(ExpandDirection), typeof(MyExpander), ExpandDirection.Down, BindingMode.OneWay, null, OnDirectionPropertyChanged);
        public static readonly BindableProperty TouchCaptureViewProperty = BindableProperty.Create("TouchCaptureView", typeof(View), typeof(MyExpander), null, BindingMode.OneWay, null, OnTouchCaptureViewPropertyChanged);
        public static readonly BindableProperty AnimationLengthProperty = BindableProperty.Create("AnimationLength", typeof(uint), typeof(MyExpander), 250u);
        public static readonly BindableProperty ExpandAnimationLengthProperty = BindableProperty.Create("ExpandAnimationLength", typeof(uint), typeof(MyExpander), uint.MaxValue);
        public static readonly BindableProperty CollapseAnimationLengthProperty = BindableProperty.Create("CollapseAnimationLength", typeof(uint), typeof(MyExpander), uint.MaxValue);
        public static readonly BindableProperty AnimationEasingProperty = BindableProperty.Create("AnimationEasing", typeof(Easing), typeof(MyExpander));
        public static readonly BindableProperty ExpandAnimationEasingProperty = BindableProperty.Create("ExpandAnimationEasing", typeof(Easing), typeof(MyExpander));
        public static readonly BindableProperty CollapseAnimationEasingProperty = BindableProperty.Create("CollapseAnimationEasing", typeof(Easing), typeof(MyExpander));
        public static readonly BindableProperty StateProperty = BindableProperty.Create("State", typeof(ExpandState), typeof(MyExpander), ExpandState.Collapsed, BindingMode.OneWayToSource);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(MyExpander));
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(MyExpander));
        public static readonly BindableProperty ForceUpdateSizeCommandProperty = BindableProperty.Create("ForceUpdateSizeCommand", typeof(ICommand), typeof(MyExpander), null, BindingMode.OneWayToSource, null, null, null, null, GetDefaultForceUpdateSizeCommand);
        #endregion BindableProperties 

        public static readonly BindableProperty ExpanderInfoProperty = BindableProperty.Create(nameof(ExpanderInfo), typeof(IExpanderInfo), typeof(MyExpander), null);
        
        public IExpanderInfo ExpanderInfo
        {
            get => (IExpanderInfo)GetValue(ExpanderInfoProperty);
            set => SetValue(ExpanderInfoProperty, value);
        }

        #region Properties

        public View? Header
        {
            get => (View)GetValue(HeaderProperty); set => SetValue(HeaderProperty, value);
        }
        public View Content
        {
            get => (View)GetValue(ContentProperty); set => SetValue(ContentProperty, value);
        }
        public DataTemplate? ContentTemplate
        {
            get => (DataTemplate)GetValue(ContentTemplateProperty); set => SetValue(ContentTemplateProperty, value);
        }
        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty); set => SetValue(IsExpandedProperty, value);
        }
        public ExpandDirection Direction
        {
            get => (ExpandDirection)GetValue(DirectionProperty); set => SetValue(DirectionProperty, value);
        }
        public View? TouchCaptureView
        {
            get => (View)GetValue(TouchCaptureViewProperty); set => SetValue(TouchCaptureViewProperty, value);
        }
        public uint AnimationLength
        {
            get => (uint)GetValue(AnimationLengthProperty); set => SetValue(AnimationLengthProperty, value);
        }
        public uint ExpandAnimationLength
        {
            get => (uint)GetValue(ExpandAnimationLengthProperty); set => SetValue(ExpandAnimationLengthProperty, value);
        }
        public uint CollapseAnimationLength
        {
            get => (uint)GetValue(CollapseAnimationLengthProperty); set => SetValue(CollapseAnimationLengthProperty, value);
        }
        public Easing AnimationEasing
        {
            get => (Easing)GetValue(AnimationEasingProperty); set => SetValue(AnimationEasingProperty, value);
        }
        public Easing ExpandAnimationEasing
        {
            get => (Easing)GetValue(ExpandAnimationEasingProperty);
            set => SetValue(ExpandAnimationEasingProperty, value);
        }
        public Easing CollapseAnimationEasing
        {
            get => (Easing)GetValue(CollapseAnimationEasingProperty);
            set => SetValue(CollapseAnimationEasingProperty, value);
        }
        public ExpandState State
        {
            get => (ExpandState)GetValue(StateProperty); set => SetValue(StateProperty, value);
        }
        public object? CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
        public ICommand? Command
        {
            get => (ICommand)GetValue(CommandProperty); set => SetValue(CommandProperty, value);
        }
        public ICommand ForceUpdateSizeCommand
        {
            get => (ICommand)GetValue(ForceUpdateSizeCommandProperty); set => SetValue(ForceUpdateSizeCommandProperty, value);
        }

        public event EventHandler Tapped
        {
            add => tappedEventManager.AddEventHandler(value, "Tapped");
            remove => tappedEventManager.RemoveEventHandler(value, "Tapped");
        }

        private double Size
        {
            get
            {
                if (!Direction.IsVertical())
                {
                    return base.Width;
                }

                return base.Height;
            }
        }
        private double ContentSize
        {
            get
            {
                if (!Direction.IsVertical())
                {
                    return (contentHolder ?? throw new NullReferenceException())!.Width;
                }

                return (contentHolder ?? throw new NullReferenceException())!.Height;
            }
        }
        private double ContentSizeRequest
        {
            get
            {
                double num = (Direction.IsVertical() ? Content.HeightRequest : Content.WidthRequest);
                if (!(num < 0.0))
                {
                    Layout layout = Content as Layout;
                    if (layout != null)
                    {
                        return num + (Direction.IsVertical() ? layout.Padding.VerticalThickness : layout.Padding.HorizontalThickness);
                    }
                }

                return num;
            }
            set
            {
                if (contentHolder == null)
                {
                    throw new NullReferenceException();
                }

                if (Direction.IsVertical())
                {
                    contentHolder!.HeightRequest = value;
                }
                else
                {
                    contentHolder!.WidthRequest = value;
                }
            }
        }
        private double MeasuredContentSize
        {
            get
            {
                if (!Direction.IsVertical())
                {
                    return (contentHolder ?? throw new NullReferenceException())!.Measure(double.PositiveInfinity, base.Height).Request.Width;
                }

                return (contentHolder ?? throw new NullReferenceException())!.Measure(base.Width, double.PositiveInfinity).Request.Height;
            }
        }
        #endregion Properties

        public void ForceUpdateSize()
        {
            lastVisibleSize = -1.0;
            OnIsExpandedChanged();
        }

        protected override void OnControlInitialized(StackLayout control)
        {
            ForceUpdateSizeCommand = new Command(ForceUpdateSize);
            headerTapGestureRecognizer = new TapGestureRecognizer
            {
                CommandParameter = this,
                Command = new Command(delegate (object parameter)
                {
                    if (ExpanderInfo.CanExpand() == false)
                        return;
                    Element parent = ((View)parameter).Parent;
                    while (parent != null && !(parent is Page))
                    {
                        MyExpander expander = parent as MyExpander;
                        if (expander != null)
                        {
                            expander.ContentSizeRequest = -1.0;
                        }

                        parent = parent.Parent;
                    }

                    IsExpanded = !IsExpanded;
                    Command?.Execute(CommandParameter);
                    OnTapped();
                })
            };
            control.Spacing = 0.0;
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            lastVisibleSize = -1.0;
            SetContent(isForceUpdate: true, shouldIgnoreAnimation: true);
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if ((Math.Abs(width - previousSize.Width) >= double.Epsilon && Direction.IsVertical()) || (Math.Abs(height - previousSize.Height) >= double.Epsilon && !Direction.IsVertical()))
            {
                ForceUpdateSize();
            }

            previousSize = new Size(width, height);
        }

        private static void OnHeaderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((MyExpander)bindable).OnHeaderPropertyChanged((View)oldValue);
        }
        private static void OnContentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((MyExpander)bindable).OnContentPropertyChanged();
        }
        private static void OnContentTemplatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((MyExpander)bindable).OnContentTemplatePropertyChanged();
        }
        private static void OnIsExpandedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((MyExpander)bindable).OnIsExpandedPropertyChanged();
        }
        private static void OnDirectionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((MyExpander)bindable).OnDirectionPropertyChanged((ExpandDirection)oldValue);
        }
        private static void OnTouchCaptureViewPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((MyExpander)bindable).OnTouchCaptureViewPropertyChanged((View)oldValue);
        }

        private static object GetDefaultForceUpdateSizeCommand(BindableObject bindable)
        {
            return new Command(((MyExpander)bindable).ForceUpdateSize);
        }

        private void OnHeaderPropertyChanged(View oldView)
        {
            SetHeader(oldView);
        }
        private void OnContentPropertyChanged()
        {
            SetContent();
        }
        private void OnContentTemplatePropertyChanged()
        {
            SetContent(isForceUpdate: true);
        }
        private void OnIsExpandedPropertyChanged()
        {
            SetContent(isForceUpdate: false);
        }
        private void OnDirectionPropertyChanged(ExpandDirection oldDirection)
        {
            SetDirection(oldDirection);
        }
        private void OnTouchCaptureViewPropertyChanged(View? oldView)
        {
            SetTouchCaptureView(oldView);
        }
        private void OnIsExpandedChanged(bool shouldIgnoreAnimation = false)
        {
            if (contentHolder == null || (!IsExpanded && !contentHolder!.IsVisible))
            {
                return;
            }

            bool flag = contentHolder.AnimationIsRunning("expandAnimationName");
            contentHolder.AbortAnimation("expandAnimationName");
            double startSize = (contentHolder!.IsVisible ? Math.Max(ContentSize, 0.0) : 0.0);
            if (IsExpanded)
            {
                contentHolder!.IsVisible = true;
            }

            double num = ((ContentSizeRequest >= 0.0) ? ContentSizeRequest : lastVisibleSize);
            if (IsExpanded)
            {
                if (num <= 0.0)
                {
                    ContentSizeRequest = -1.0;
                    num = MeasuredContentSize;
                    ContentSizeRequest = 0.0;
                }
            }
            else
            {
                startSize = (lastVisibleSize = ((ContentSizeRequest >= 0.0) ? ContentSizeRequest : ((!flag) ? ContentSize : lastVisibleSize)));
                num = 0.0;
            }

            InvokeAnimation(startSize, num, shouldIgnoreAnimation);
        }
        private void SetHeader(View? oldHeader)
        {
            if (oldHeader != null)
            {
                base.Control?.Children.Remove(oldHeader);
            }

            if (Header != null)
            {
                if (Direction.IsRegularOrder())
                {
                    base.Control?.Children.Insert(0, Header);
                }
                else
                {
                    base.Control?.Children.Add(Header);
                }
            }

            SetTouchCaptureView(oldHeader);
        }
        private void SetContent(bool isForceUpdate, bool shouldIgnoreAnimation = false, bool isForceContentReset = false)
        {
            if (IsExpanded && (Content == null || isForceUpdate || isForceContentReset))
            {
                lock (contentSetLocker)
                {
                    shouldIgnoreContentSetting = true;
                    View view = CreateContent();
                    if (view != null)
                    {
                        Content = view;
                    }
                    else if (isForceContentReset)
                    {
                        SetContent();
                    }

                    shouldIgnoreContentSetting = false;
                }
            }

            OnIsExpandedChanged(shouldIgnoreAnimation);
        }
        private void SetContent()
        {
            if (contentHolder != null)
            {
                contentHolder.AbortAnimation("expandAnimationName");
                base.Control?.Children.Remove(contentHolder);
                contentHolder = null;
            }

            if (Content != null)
            {
                contentHolder = new ContentView
                {
                    IsClippedToBounds = true,
                    IsVisible = false,
                    Content = Content
                };
                ContentSizeRequest = 0.0;
                if (Direction.IsRegularOrder())
                {
                    base.Control?.Children.Add(contentHolder);
                }
                else
                {
                    base.Control?.Children.Insert(0, contentHolder);
                }
            }

            if (!shouldIgnoreContentSetting)
            {
                SetContent(isForceUpdate: true);
            }
        }
        private View? CreateContent()
        {
            DataTemplate dataTemplate = ContentTemplate;
            while (true)
            {
                DataTemplateSelector dataTemplateSelector = dataTemplate as DataTemplateSelector;
                if (dataTemplateSelector == null)
                {
                    break;
                }

                dataTemplate = dataTemplateSelector.SelectTemplate(base.BindingContext, this);
            }

            if (dataTemplate == previousTemplate && Content != null)
            {
                return null;
            }

            previousTemplate = dataTemplate;
            return (View)(dataTemplate?.CreateContent());
        }
        private void SetDirection(ExpandDirection oldDirection)
        {
            if (oldDirection.IsVertical() == Direction.IsVertical())
            {
                SetHeader(Header);
                return;
            }

            if (base.Control != null)
            {
                base.Control!.Orientation = ((!Direction.IsVertical()) ? StackOrientation.Horizontal : StackOrientation.Vertical);
            }

            lastVisibleSize = -1.0;
            SetHeader(Header);
            SetContent(isForceUpdate: true, shouldIgnoreAnimation: true, isForceContentReset: true);
        }
        private void SetTouchCaptureView(View? oldView)
        {
            oldView?.GestureRecognizers.Remove(headerTapGestureRecognizer);
            TouchCaptureView?.GestureRecognizers?.Remove(headerTapGestureRecognizer);
            Header?.GestureRecognizers.Remove(headerTapGestureRecognizer);
            (TouchCaptureView ?? Header)?.GestureRecognizers.Add(headerTapGestureRecognizer);
        }
        private void InvokeAnimation(double startSize, double endSize, bool shouldIgnoreAnimation)
        {
            State = ((!IsExpanded) ? ExpandState.Collapsing : ExpandState.Expanding);
            if (shouldIgnoreAnimation || Size < 0.0)
            {
                State = (IsExpanded ? ExpandState.Expanded : ExpandState.Collapsed);
                ContentSizeRequest = endSize;
                if (contentHolder == null)
                {
                    throw new NullReferenceException();
                }

                contentHolder!.IsVisible = IsExpanded;
                return;
            }

            uint num = CollapseAnimationLength;
            Easing easing = CollapseAnimationEasing;
            if (IsExpanded)
            {
                num = ExpandAnimationLength;
                easing = ExpandAnimationEasing;
            }

            if (num == uint.MaxValue)
            {
                num = AnimationLength;
            }

            if (easing == null)
            {
                easing = AnimationEasing;
            }

            if (lastVisibleSize > 0.0)
            {
                num = (uint)((double)num * (Math.Abs(endSize - startSize) / lastVisibleSize));
            }

            num = Math.Max(num, 1u);
            new Xamarin.Forms.Animation(delegate (double v)
            {
                ContentSizeRequest = v;
            }, startSize, endSize).Commit(contentHolder, "expandAnimationName", 16u, num, easing, delegate (double value, bool isInterrupted)
            {
                if (!isInterrupted)
                {
                    if (!IsExpanded)
                    {
                        if (contentHolder == null)
                        {
                            throw new NullReferenceException();
                        }

                        contentHolder!.IsVisible = false;
                        State = ExpandState.Collapsed;
                    }
                    else
                    {
                        State = ExpandState.Expanded;
                    }
                }
            });
        }
        private void OnTapped()
        {
            tappedEventManager.RaiseEvent(this, EventArgs.Empty, "Tapped");
        }
    }

    internal static class ExpandDirectionExtensions
    {
        public static bool IsVertical(this ExpandDirection orientation)
        {
            if (orientation != 0)
            {
                return orientation == ExpandDirection.Up;
            }

            return true;
        }
        public static bool IsRegularOrder(this ExpandDirection orientation)
        {
            if (orientation != 0)
            {
                return orientation == ExpandDirection.Right;
            }

            return true;
        }
    }
}
