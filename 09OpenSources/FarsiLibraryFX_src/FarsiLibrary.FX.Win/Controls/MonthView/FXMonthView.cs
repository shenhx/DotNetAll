using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using FarsiLibrary.FX.Utils;
//using FarsiLibrary.FX.Win.ThemeInfo;

namespace FarsiLibrary.FX.Win.Controls
{
    [TemplatePart(Name = FXMonthView.PART_MonthDays, Type = typeof(FXMonthViewContainer))]
    [TemplatePart(Name = FXMonthView.PART_ViewBorder, Type = typeof(Border))]
    [TemplatePart(Name = FXMonthView.PART_MonthName, Type = typeof(FXMonthViewHeader))]
    [TemplatePart(Name = FXMonthView.PART_YearName, Type = typeof(FXMonthViewHeader))]
    [TemplatePart(Name = FXMonthView.PART_NextMonthButton, Type = typeof(RepeatButton))]
    [TemplatePart(Name = FXMonthView.PART_NextYearButton, Type = typeof(RepeatButton))]
    [TemplatePart(Name = FXMonthView.PART_PreviousYearButton, Type = typeof(RepeatButton))]
    [TemplatePart(Name = FXMonthView.PART_PreviousMonthButton, Type = typeof(RepeatButton))]
    [TemplatePart(Name = FXMonthView.PART_WeekDayNames, Type = typeof(Grid))]
    [TemplatePart(Name = FXMonthView.PART_ButtonPanel, Type = typeof(Grid))]
    public class FXMonthView : Control
    {
        #region Part Names

        public const string PART_MonthDays = "PART_MonthDays";
        public const string PART_ViewBorder = "PART_ViewBorder";
        public const string PART_MonthName = "PART_MonthName";
        public const string PART_YearName = "PART_YearName";
        public const string PART_WeekDayNames = "PART_WeekDayNames";
        public const string PART_NextMonthButton = "PART_NextMonthButton";
        public const string PART_PreviousYearButton = "PART_PreviousYearButton";
        public const string PART_PreviousMonthButton = "PART_PreviousMonthButton";
        public const string PART_NextYearButton = "PART_NextYearButton";
        public const string PART_ButtonPanel = "PART_ButtonPanel";

        #endregion

        #region Fields

        private bool IsUpdatingSelection = false;
        private CalendarDayCollection calendarDays;
        private FXMonthViewContainer container;
        private static DataTemplate dayTemplate;
        private static readonly Dictionary<string, ResourceDictionary> themeList;

        #endregion

        #region Dependency Properties

        //public static readonly DependencyProperty ThemeProperty = DependencyProperty.Register("Theme", typeof(Theme), typeof(FXMonthView), new PropertyMetadata(new GenericTheme(), OnThemeChangedCallback, CoerceThemeValue));
        public static readonly DependencyProperty ButtonStyleProperty = DependencyProperty.Register("ButtonStyle", typeof(Style), typeof(FXMonthView), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty WeekDayHeaderStyleProperty = DependencyProperty.Register("WeekDayHeaderStyle",   typeof(Style),     typeof(FXMonthView), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty HeaderMonthStyleProperty = DependencyProperty.Register("HeaderMonthStyle", typeof(Style), typeof(FXMonthView), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty HeaderYearStyleProperty = DependencyProperty.Register("HeaderYearStyle", typeof(Style), typeof(FXMonthView), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty DayContainerStyleProperty = DependencyProperty.Register("DayContainerStyle", typeof(Style), typeof(FXMonthView), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnDayContainerStyleSelectorChanged)));
        public static readonly DependencyProperty DayContainerStyleSelectorProperty = DependencyProperty.Register("DayContainerStyleSelector", typeof(StyleSelector), typeof(FXMonthView), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty DayTemplateSelectorProperty = DependencyProperty.Register("DayTemplateSelector", typeof(DataTemplateSelector), typeof(FXMonthView), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnDayTemplateSelectorChanged)));
        public static readonly DependencyProperty DayTemplateProperty = DependencyProperty.Register("DayTemplate", typeof(DataTemplate), typeof(FXMonthView), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnDayTemplateChanged)));
        public static readonly DependencyProperty ShowWeekDayNamesProperty = DependencyProperty.Register("ShowWeekDayNames", typeof(bool), typeof(FXMonthView), new FrameworkPropertyMetadata(true));
        public static readonly DependencyProperty ShowTodayButtonProperty = DependencyProperty.Register("ShowTodayButton", typeof(bool), typeof(FXMonthView), new FrameworkPropertyMetadata(true));
        public static readonly DependencyProperty ShowEmptyButtonProperty = DependencyProperty.Register("ShowEmptyButton", typeof(bool), typeof(FXMonthView), new FrameworkPropertyMetadata(true));
        public static readonly DependencyProperty SelectedDateTimeProperty = DependencyProperty.Register("SelectedDateTime", typeof(DateTime?), typeof(FXMonthView), new FrameworkPropertyMetadata(DateTime.Now.Date, new PropertyChangedCallback(OnSelectedDateTimeChanged), CoerceSelectedDateTime));
        public static readonly DependencyProperty ViewDateTimeProperty = DependencyProperty.Register("ViewDateTime", typeof(DateTime), typeof(FXMonthView), new FrameworkPropertyMetadata(DateTime.Now.Date, new PropertyChangedCallback(OnViewDateTimeChanged), CoerceViewDateTime));
        public static readonly DependencyProperty ViewPreChangeAnimationProperty = DependencyProperty.Register("ViewPreChangeAnimation", typeof(Storyboard), typeof(FXMonthView), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty ViewPostChangeAnimationProperty = DependencyProperty.Register("ViewPostChangeAnimation", typeof(Storyboard), typeof(FXMonthView), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty MaxDateProperty = DependencyProperty.Register("MaxDate", typeof(DateTime), typeof(FXMonthView), new FrameworkPropertyMetadata(CultureHelper.MaxCultureDateTime, new PropertyChangedCallback(OnMaxDateChanged), CoerceMaxDate), IsValidDate);
        public static readonly DependencyProperty MinDateProperty = DependencyProperty.Register("MinDate", typeof(DateTime), typeof(FXMonthView), new FrameworkPropertyMetadata(CultureHelper.MinCultureDateTime, new PropertyChangedCallback(OnMinDateChanged)), IsValidDate);

        #endregion

        #region Routed Events

        public static readonly RoutedEvent SelectedDateTimeChangedEvent = EventManager.RegisterRoutedEvent("SelectedDateTimeChanged", RoutingStrategy.Bubble, typeof(DateSelectionChangedEventHandler), typeof(FXMonthView));
        public static readonly RoutedEvent PreviewSelectedDateTimeChangedEvent = EventManager.RegisterRoutedEvent("PreviewSelectedDateTimeChanged", RoutingStrategy.Tunnel, typeof(DateSelectionChangedEventHandler), typeof(FXMonthView));
        public static readonly RoutedEvent ViewDateTimeChangedEvent = EventManager.RegisterRoutedEvent("ViewDateTimeChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<DateTime>), typeof(FXMonthView));
        public static readonly RoutedEvent PreviewViewDateTimeChangedEvent = EventManager.RegisterRoutedEvent("PreviewViewDateTimeChanged", RoutingStrategy.Tunnel, typeof(RoutedPropertyChangedEventHandler<DateTime>), typeof(FXMonthView));
        public static readonly RoutedEvent RecreatingViewEvent = EventManager.RegisterRoutedEvent("RecreatingView", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FXMonthView));

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        static FXMonthView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FXMonthView), new FrameworkPropertyMetadata(typeof(FXMonthView)));
            IsTabStopProperty.OverrideMetadata(typeof(FXMonthView), new FrameworkPropertyMetadata(false));
            themeList = new Dictionary<string, ResourceDictionary>(4);
            PreloadThemes();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public FXMonthView()
        {
            
            InitializeCommandBindings();
        }

        #endregion

        #region Events

        /// <summary>
        /// Fired when previewing SelectedDateTimeChanged
        /// </summary>
        public event RoutedPropertyChangedEventHandler<DateTime> PreviewSelectedDateTimeChanged
        {
            add { AddHandler(PreviewSelectedDateTimeChangedEvent, value); }
            remove { RemoveHandler(PreviewSelectedDateTimeChangedEvent, value); }
        }

        /// <summary>
        /// Fired when the SelectedDateTime property is changed
        /// </summary>
        public event DateSelectionChangedEventHandler SelectedDateTimeChanged
        {
            add { AddHandler(SelectedDateTimeChangedEvent, value); }
            remove { RemoveHandler(SelectedDateTimeChangedEvent, value); }
        }

        /// <summary>
        /// Fired when previewing ViewDateTime changed event.
        /// </summary>
        public event RoutedPropertyChangedEventHandler<DateTime> PreviewViewDateTimeChanged
        {
            add { AddHandler(PreviewViewDateTimeChangedEvent, value); }
            remove { RemoveHandler(PreviewViewDateTimeChangedEvent, value); }
        }

        /// <summary>
        /// Fired when ViewDateTime is changed
        /// </summary>
        public event RoutedPropertyChangedEventHandler<DateTime> ViewDateTimeChanged
        {
            add { AddHandler(ViewDateTimeChangedEvent, value); }
            remove { RemoveHandler(ViewDateTimeChangedEvent, value); }
        }

        /// <summary>
        /// Recreating View event.
        /// </summary>
        public event RoutedEventHandler RecreatingView
        {
            add { AddHandler(RecreatingViewEvent, value); }
            remove { RemoveHandler(RecreatingViewEvent, value); }
        }

        #endregion

        #region Properties

        private CalendarDayCollection CalendarDays
        {
            get
            {
                if (calendarDays == null)
                    calendarDays = CreateVisibleDaysCollection();
                
                return calendarDays;
            }
        }

        /// <summary>
        /// View Year
        /// </summary>
        public int ViewYear
        {
            get { return CultureHelper.CurrentCalendar.GetYear(ViewDateTime); }
        }

        /// <summary>
        /// View Month
        /// </summary>
        public int ViewMonth
        {
            get { return CultureHelper.CurrentCalendar.GetMonth(ViewDateTime); }
        }

        /// <summary>
        /// View Day
        /// </summary>
        public int ViewDay
        {
            get { return CultureHelper.CurrentCalendar.GetDayOfMonth(ViewDateTime); }
        }

        /// <summary>
        /// Preview DateTime
        /// </summary>
        public DateTime ViewDateTime
        {
            get { return (DateTime)GetValue(ViewDateTimeProperty); }
            set { SetValue(ViewDateTimeProperty, value); }
        }

        /// <summary>
        /// SelectedDateTime
        /// </summary>
        public DateTime? SelectedDateTime
        {
            get { return (DateTime?)GetValue(SelectedDateTimeProperty); }
            set { SetValue(SelectedDateTimeProperty, value); }
        }

        /// <summary>
        /// Show Today Button
        /// </summary>
        public bool ShowTodayButton
        {
            get { return (bool)GetValue(ShowTodayButtonProperty); }
            set { SetValue(ShowTodayButtonProperty, value); }
        }

        /// <summary>
        /// Show Empty Button
        /// </summary>
        public bool ShowEmptyButton
        {
            get { return (bool)GetValue(ShowEmptyButtonProperty); }
            set { SetValue(ShowEmptyButtonProperty, value); }
        }

        /// <summary>
        /// ShowWeekDayNames
        /// </summary>
        public bool ShowWeekDayNames
        {
            get { return (bool)GetValue(ShowWeekDayNamesProperty); }
            set { SetValue(ShowWeekDayNamesProperty, value); }
        }

        /// <summary>
        /// Button Styles
        /// </summary>
        public Style ButtonStyle
        {
            get { return (Style)GetValue(ButtonStyleProperty); }
            set { SetValue(ButtonStyleProperty, value); }
        }

        /// <summary>
        /// DayHeaderStyle property
        /// </summary>
        public Style WeekDayHeaderStyle
        {
            get { return (Style)GetValue(WeekDayHeaderStyleProperty); }
            set { SetValue(WeekDayHeaderStyleProperty, value); }
        }

        /// <summary>
        /// Header Month Style property
        /// </summary>
        public Style HeaderMonthStyle
        {
            get { return (Style)GetValue(HeaderMonthStyleProperty); }
            set { SetValue(HeaderMonthStyleProperty, value); }
        }

        /// <summary>
        /// Day Container Style
        /// </summary>
        public Style DayContainerStyle
        {
            get { return (Style)GetValue(DayContainerStyleProperty); }
            set { SetValue(DayContainerStyleProperty, value); }
        }

        /// <summary>
        /// Header Year Style property
        /// </summary>
        public Style HeaderYearStyle
        {
            get { return (Style)GetValue(HeaderYearStyleProperty); }
            set { SetValue(HeaderYearStyleProperty, value); }
        }

        /// <summary>
        /// Day Container selector Style
        /// </summary>
        public StyleSelector DayContainerStyleSelector
        {
            get { return (StyleSelector)GetValue(DayContainerStyleSelectorProperty); }
            set { SetValue(DayContainerStyleSelectorProperty, value); }
        }

        /// <summary>
        /// DayTemplateSelector allows the app writer to provide custom template selection logic
        /// for a template to apply to each item.
        /// </summary>
        public DataTemplateSelector DayTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(DayTemplateSelectorProperty); }
            set { SetValue(DayTemplateSelectorProperty, value); }
        }

        /// <summary>
        /// Day Data Template
        /// </summary>
        public DataTemplate DayTemplate
        {
            get { return (DataTemplate)GetValue(DayTemplateProperty); }
            set { SetValue(DayTemplateProperty, value); }
        }

        /// <summary>
        /// View change animation.
        /// </summary>
        public Storyboard ViewPreChangeAnimation
        {
            get { return (Storyboard)GetValue(ViewPreChangeAnimationProperty); }
            set { SetValue(ViewPreChangeAnimationProperty, value); }
        }

        /// <summary>
        /// View change animation.
        /// </summary>
        public Storyboard ViewPostChangeAnimation
        {
            get { return (Storyboard)GetValue(ViewPostChangeAnimationProperty); }
            set { SetValue(ViewPostChangeAnimationProperty, value); }
        }

        /// <summary>
        /// Control Theme
        /// </summary>
        //public Theme Theme
        //{
        //    get { return (Theme)GetValue(ThemeProperty); }
        //    set { SetValue(ThemeProperty, value); }
        //}

        /// <summary>
        /// List of available themes
        /// </summary>
        public Dictionary<string, ResourceDictionary> AvailableThemes
        {
            get { return themeList; }
        }

        /// <summary>
        /// The max date of MonthView
        /// </summary>
        public DateTime MaxDate
        {
            get { return (DateTime)GetValue(MaxDateProperty); }
            set { SetValue(MaxDateProperty, value); }
        }

        /// <summary>
        /// The min date of MonthView
        /// </summary>
        public DateTime MinDate
        {
            get { return (DateTime)GetValue(MinDateProperty); }
            set { SetValue(MinDateProperty, value); }
        }

        #endregion

        #region Methods

        ///<summary>
        ///When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"></see>.
        ///</summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateVisualTree();
        }

        /// <summary>
        /// Raise PreviewSelectedDateTimeChanged event.
        /// </summary>
        protected virtual void RaisePreviewSelectedDateTimeChanged(DateSelectionChangedEventArgs e)
        {
            RaiseEvent(e);
        }

        /// <summary>
        /// Raise SelectedDateTimeChanged event.
        /// </summary>
        protected virtual void RaiseSelectedDateTimeChanged(DateSelectionChangedEventArgs e)
        {
            RaiseEvent(e);
        }

        /// <summary>
        /// Raise PreviewViewDateTimeChanged event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void RaisePreviewViewDateTimeChanged(RoutedPropertyChangedEventArgs<DateTime> e)
        {
            RaiseEvent(e);
        }

        /// <summary>
        /// Raise ViewDateTimeChanged event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void RaiseViewDateTimeChanged(RoutedPropertyChangedEventArgs<DateTime> e)
        {
            RaiseEvent(e);
        }

        private static bool IsValidDate(object value)
        {
            DateTime date = (DateTime)value;
            return (date >= CultureHelper.MinCultureDateTime) && (date <= CultureHelper.MaxCultureDateTime);
        }

        private static object CoerceMaxDate(DependencyObject d, object value)
        {
            FXMonthView mv = (FXMonthView)d;
            DateTime newValue = (DateTime)value;

            DateTime min = mv.MinDate;
            if (newValue < min)
                return min;

            return value;
        }

        private static object CoerceThemeValue(DependencyObject d, object newValue)
        {
            return newValue;
        }

        private static object CoerceViewDateTime(DependencyObject d, object value)
        {
            if (value != null)
            {
                DateTime newValue = (DateTime)value;
                return ValidateDateRange(newValue);
            }

            return value;
        }

        private static object CoerceSelectedDateTime(DependencyObject d, object value)
        {
            if (value != null)
            {
                DateTime newValue = (DateTime)value;
                return ValidateDateRange(newValue);
            }
         
            return value;
        }

        private static void OnMinDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FXMonthView mv = (FXMonthView)d;

            DateTime oldMaxDate = mv.MaxDate;
            DateTime oldViewDate = mv.ViewDateTime;
            mv.CoerceValue(MaxDateProperty);
            mv.CoerceValue(ViewDateTimeProperty);

            if (CompareYearMonthDay(oldMaxDate, mv.MaxDate) == 0 && CompareYearMonth(oldViewDate, mv.ViewDateTime) == 0)
            {
                mv.OnMaxMinDateChanged((DateTime)e.NewValue, mv.MaxDate);
            }
        }

        private static void OnMaxDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FXMonthView mv = (FXMonthView)d;

            DateTime oldViewDate = mv.ViewDateTime;
            mv.CoerceValue(ViewDateTimeProperty);

            if (oldViewDate != mv.ViewDateTime)
                mv.OnMaxMinDateChanged(mv.MinDate, (DateTime)e.NewValue);
        }

        private static void OnThemeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //FXMonthView mv = (FXMonthView)d;
            //Theme newTheme = e.NewValue as Theme;

            //if(newTheme != null && mv.AvailableThemes.ContainsKey(newTheme.ThemeName))
            //{
            //    Application.Current.Resources = mv.AvailableThemes[mv.Theme.ThemeName];
            //}
        }

        private static void OnSelectedDateTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FXMonthView mv = (FXMonthView)d;
            DateTime? newValue = (DateTime?)e.NewValue;
            DateTime? oldValue = (DateTime?)e.OldValue;

            DateSelectionChangedEventArgs args = new DateSelectionChangedEventArgs(PreviewSelectedDateTimeChangedEvent);
            args.RemovedDates.Add(oldValue);
            args.AddedDates.Add(newValue);
            mv.RaisePreviewSelectedDateTimeChanged(args);

            if(!args.Handled)
            {
                args = new DateSelectionChangedEventArgs(SelectedDateTimeChangedEvent);
                args.RemovedDates.Add(oldValue);
                args.AddedDates.Add(newValue);
                mv.RaiseSelectedDateTimeChanged(args);
            }
        }

        private static void OnViewDateTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FXMonthView mv = (FXMonthView)d;
            DateTime newValue = (DateTime)e.NewValue;
            DateTime oldValue = (DateTime)e.OldValue;

            RoutedPropertyChangedEventArgs<DateTime> args = new RoutedPropertyChangedEventArgs<DateTime>(oldValue, newValue);
            args.RoutedEvent = PreviewViewDateTimeChangedEvent;
            mv.RaisePreviewViewDateTimeChanged(args);
            
            if(!args.Handled)
            {
                args = new RoutedPropertyChangedEventArgs<DateTime>(oldValue, newValue);
                args.RoutedEvent = ViewDateTimeChangedEvent;
                mv.RaiseViewDateTimeChanged(args);

                if(!args.Handled)
                    mv.RecreateDays();
            }
        }

        private void OnMaxMinDateChanged(DateTime minDate, DateTime maxDate)
        {
            int count = CalendarDays.Count;
            for (int i = 0; i < count; ++i)
            {
                CalendarDays[i].IsSelectable = IsWithinRange(CalendarDays[i].Date, minDate, maxDate);
            }

            ////Update the selected dates if the new Max/MinDate value is within visible days
            //if (minDate > FirstVisibleDate || maxDate < LastVisibleDate)
            //{
            //    SelectionChange.Begin();
            //    bool succeeded = false;
            //    try
            //    {
            //        foreach (DateTime dt in SelectedDates)
            //        {
            //            if (!Helper.IsWithinRange(dt, minDate, maxDate))
            //            {
            //                SelectionChange.Unselect(dt);
            //            }
            //        }

            //        SelectionChange.End(true, true);
            //        succeeded = true;
            //    }
            //    finally
            //    {
            //        if (!succeeded)
            //        {
            //            SelectionChange.Cancel();
            //        }
            //    }
            //}
        }

        internal static int CompareYearMonthDay(DateTime dt1, DateTime dt2)
        {
            DateTime first = new DateTime(dt1.Year, dt1.Month, dt1.Day);
            DateTime second = new DateTime(dt2.Year, dt2.Month, dt2.Day);
            TimeSpan ts = first - second;
            return ts.Days;
        }

        internal static bool IsWithinRange(DateTime date, DateTime start, DateTime end)
        {
            return CompareYearMonthDay(date, start) >= 0 && CompareYearMonthDay(date, end) <= 0;
        }

        internal static int CompareYearMonth(DateTime dt1, DateTime dt2)
        {
            return SubtractByMonth(dt1, dt2);
        }

        internal static int SubtractByMonth(DateTime dt1, DateTime dt2)
        {
            return (dt1.Year - dt2.Year) * 12 + (dt1.Month - dt2.Month);
        }

        /// <summary>
        /// Update the SelectedDates to UI if those dates are added before UI ready
        /// </summary>
        private void OnContainerLayoutUpdated(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Sync selected value of container control with the SelectedDateTime property.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnContainerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(container.TemplatedParent == null || e.OriginalSource != container)
                return;

            //Single selection
            if(container.SelectedItems.Count == 1)
            {
                CalendarDay selected = container.SelectedItem as CalendarDay;
                System.Diagnostics.Debug.Assert(selected != null);

                if(!selected.IsOtherMonth && selected.IsSelectable)
                {
                    SelectedDateTime = selected.Date;
                }
            }
            else if(!IsUpdatingSelection)
            {
                if(container.SelectedItems == null || container.SelectedItems.Count <= 0)
                    SelectedDateTime = null;
            }
        }

        private static void OnDayTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FXMonthView)d).RefreshDaysTemplate();
        }

        private static void OnDayTemplateSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FXMonthView)d).RefreshDaysTemplate();
        }

        private static void OnDayContainerStyleSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FXMonthView)d).RefreshDaysTemplate();
        }

        private void OnChangeToNextMonth(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                IsUpdatingSelection = true;
                ViewDateTime = CultureHelper.CurrentCalendar.AddMonths(ViewDateTime, 1);
                RecreateDays();
            }
            catch (Exception)
            {
                ViewDateTime = CultureHelper.MaxCultureDateTime;
            }
            finally
            {
                IsUpdatingSelection = false;
            }
        }

        public FXMonthViewItem GetContainerFromDate(DateTime date)
        {
            CalendarDay day = GetCalendarDateByDate(date);
            if (day != null && container != null)
                return container.ItemContainerGenerator.ContainerFromItem(day) as FXMonthViewItem;
            
            return null;
        }

        private CalendarDay GetCalendarDateByDate(DateTime date)
        {
            foreach (CalendarDay day in calendarDays)
            {
                if (day.Date == date)
                    return day;
            }

            return null;
        }

        private void OnChangeToPrevYear(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                IsUpdatingSelection = true;
                ViewDateTime = CultureHelper.CurrentCalendar.AddYears(ViewDateTime, -1);
                RecreateDays();
            }
            catch(Exception)
            {
                ViewDateTime = CultureHelper.MinCultureDateTime;
            }
            finally
            {
                IsUpdatingSelection = false;
            }
        }

        private void OnChangeToPrevMonth(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                IsUpdatingSelection = true;
                ViewDateTime = CultureHelper.CurrentCalendar.AddMonths(ViewDateTime, -1);
                RecreateDays();
            }
            catch (Exception)
            {
                ViewDateTime = CultureHelper.MinCultureDateTime;
            }
            finally
            {
                IsUpdatingSelection = false;
            }
        }

        private void OnChangeToNextYear(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                IsUpdatingSelection = true;
                ViewDateTime = CultureHelper.CurrentCalendar.AddYears(ViewDateTime, 1);
                RecreateDays();
            }
            catch (Exception)
            {
                ViewDateTime = CultureHelper.MaxCultureDateTime;
            }
            finally
            {
                IsUpdatingSelection = false;
            }
        }

        private void OnSelectTodayDate(object sender, ExecutedRoutedEventArgs e)
        {
            ViewDateTime = DateTime.Now.Date;
            SelectedDateTime = ViewDateTime;
            UpdateContainerSelection();
        }

        private void OnSelectEmptyDate(object sender, ExecutedRoutedEventArgs e)
        {
            container.SelectedValue = null;
        }

        private void InitializeCommandBindings()
        {
            CommandBindings.Add(new CommandBinding(FXMonthViewCommands.ChangeToNextMonth, OnChangeToNextMonth));
            CommandBindings.Add(new CommandBinding(FXMonthViewCommands.ChangeToNextYear, OnChangeToNextYear));
            CommandBindings.Add(new CommandBinding(FXMonthViewCommands.ChangeToPrevMonth, OnChangeToPrevMonth));
            CommandBindings.Add(new CommandBinding(FXMonthViewCommands.ChangeToPrevYear, OnChangeToPrevYear));
            CommandBindings.Add(new CommandBinding(FXMonthViewCommands.SelectEmptyDate, OnSelectEmptyDate));
            CommandBindings.Add(new CommandBinding(FXMonthViewCommands.SelectTodayDate, OnSelectTodayDate));
        }

        private void UpdateVisualTree()
        {
            container = GetTemplateChild(PART_MonthDays) as FXMonthViewContainer;
            if(container != null)
            {
                container.ItemsSource = CalendarDays;
                container.LayoutUpdated += OnContainerLayoutUpdated;
                container.SelectionChanged += OnContainerSelectionChanged;

                RefreshDaysTemplate();
            }
        }

        private CalendarDayCollection CreateVisibleDaysCollection()
        {
            CalendarDayCollection collection = new CalendarDayCollection();

            int NumberOfDays = CultureHelper.CurrentCalendar.GetDaysInMonth(ViewYear, ViewMonth);
            DateTime dtStartOfMonth = new DateTime(ViewYear, ViewMonth, 1, 0, 0, 0, CultureHelper.CurrentCalendar);

            int TotalDays = 0;
            int FirstDay = CultureHelper.GetDayOfWeek(dtStartOfMonth, CultureHelper.CurrentCalendar);

            int LastMonth = ViewMonth;
            int LastYear = ViewYear;

            if (ViewMonth - 1 < 1 && LastYear > 1)
            {
                LastMonth = 12;
                LastYear--;
            }
            else if(ViewMonth - 1 > 0)
            {
                LastMonth--;
            }

            int PrevMonthDays = CultureHelper.CurrentCalendar.GetDaysInMonth(LastYear, LastMonth);
            int LastingDays = PrevMonthDays - FirstDay;

            //Create Pre-Day paddings
            for(int i=LastingDays; i<PrevMonthDays; i++)
            {
                CalendarDay newDay = new CalendarDay(new DateTime(LastYear, LastMonth, i + 1, 0, 0, 0, CultureHelper.CurrentCalendar));
                newDay.IsOtherMonth = true;
                newDay.IsSelectable = false;
                collection.Add(newDay);

                TotalDays++;
            }

            //Create CurrentMonth Days
            for(int i=1; i<= NumberOfDays; i++)
            {
                CalendarDay newDay = new CalendarDay(new DateTime(ViewYear, ViewMonth, i, 0, 0, 0, CultureHelper.CurrentCalendar));
                newDay.IsOtherMonth = false;
                newDay.IsSelectable = true;

                collection.Add(newDay);
                TotalDays++;
            }

            int EndDay;
            if(FirstDay != 0)
            {
                EndDay = NumberOfDays + 1;
            }
            else
            {
                EndDay = NumberOfDays;
            }

            //Create Post-Padding Days
            for(int i=EndDay; i<42; i++)
            {
                if(TotalDays == 42)
                    break;

                CalendarDay newDay = new CalendarDay(new DateTime(ViewYear, ViewMonth, i - EndDay + 1, 0, 0, 0, CultureHelper.CurrentCalendar));
                collection.Add(newDay);
            }

            return collection;
        }

        private static DateTime ValidateDateRange(DateTime value)
        {
            if (value > CultureHelper.MaxCultureDateTime)
            {
                return CultureHelper.MaxCultureDateTime;
            }
            else if (value < CultureHelper.MinCultureDateTime)
            {
                return CultureHelper.MinCultureDateTime;
            }
            else
            {
                return value;
            }
        }

        private void RecreateDays()
        {
            if(container != null)
            {
                PreChangeAnimate();
                calendarDays = CreateVisibleDaysCollection();
                container.SelectedItems.Clear();
                container.ItemsSource = calendarDays;
                PostChangeAnimate();

                UpdateContainerSelection();
            }
        }

        private void PreChangeAnimate()
        {
            if(ViewPreChangeAnimation != null)
                ViewPreChangeAnimation.Begin(container);
        }

        private void PostChangeAnimate()
        {
            if (ViewPostChangeAnimation != null)
                ViewPostChangeAnimation.Begin(container);
        }

        private void UpdateContainerSelection()
        {
            if (SelectedDateTime.HasValue)
            {
                CalendarDay day = GetCalendarDateByDate(SelectedDateTime.Value);
                if (day != null && container != null)
                {
                    container.SelectedValue = day;
                }
            }
        }

        private void RefreshDaysTemplate()
        {
            if (container != null)
            {
                if(DayTemplate == null || DayTemplateSelector == null)
                {
                    if(dayTemplate == null)
                    {
                        dayTemplate = new DataTemplate();
                        FrameworkElementFactory text = new FrameworkElementFactory(typeof(TextBlock));
                        text.SetBinding(TextBlock.TextProperty, new Binding());
                        dayTemplate.VisualTree = text;
                    }

                    container.ItemTemplateSelector = null;
                    container.ItemTemplate = DayTemplate;
                }
                else
                {
                    container.ItemTemplateSelector = DayTemplateSelector;
                    container.ItemTemplate = DayTemplate;
                }
            }
        }

        private static void PreloadThemes()
        {
            //GlassTheme glass = new GlassTheme();
            //ClassicTheme classic = new ClassicTheme();
            //GenericTheme generic = new GenericTheme();

            //themeList.Add(glass.ThemeName, (ResourceDictionary)Application.LoadComponent(glass.DictionaryUri));
            //themeList.Add(classic.ThemeName, (ResourceDictionary)Application.LoadComponent(classic.DictionaryUri));
            //themeList.Add(generic.ThemeName, (ResourceDictionary)Application.LoadComponent(generic.DictionaryUri));
        }

        #endregion
    }
}