using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.Enums;

namespace WpfPerfBench.Controls
{
    public class BusyIndicator : ContentControl
    {
        #region BusyType

        public static readonly DependencyProperty BusyTypeProperty = DependencyProperty.Register(
            nameof(BusyType), typeof(BusyType), typeof(BusyIndicator), new PropertyMetadata(BusyType.Standard));

        public BusyType BusyType
        {
            get => (BusyType)GetValue(BusyTypeProperty);
            set => SetValue(BusyTypeProperty, value);
        }

        #endregion BusyType

        #region IsBusy

        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register(
                nameof(IsBusy),
                typeof(bool),
                typeof(BusyIndicator),
                new PropertyMetadata(false, OnIsBusyChanged));

        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        #endregion IsBusy

        #region BusyText

        public static readonly DependencyProperty BusyTextProperty = 
            DependencyProperty.Register(
                nameof(BusyText), 
                typeof(string), 
                typeof(BusyIndicator), 
                new PropertyMetadata(default(string)));

        public string BusyText
        {
            get => (string)GetValue(BusyTextProperty);
            set => SetValue(BusyTextProperty, value);
        }

        #endregion BusyText

        #region BusySubText

        public static readonly DependencyProperty BusySubTextProperty = 
            DependencyProperty.Register(
                nameof(BusySubText), 
                typeof(string), 
                typeof(BusyIndicator), 
                new PropertyMetadata("Пожалуйста, подождите"));

        public string BusySubText
        {
            get => (string)GetValue(BusySubTextProperty);
            set => SetValue(BusySubTextProperty, value);
        }

        #endregion BusySubText

        #region Minimum

        public static readonly DependencyProperty MinimumProperty = 
            DependencyProperty.Register(
                nameof(Minimum), 
                typeof(double), 
                typeof(BusyIndicator), 
                new PropertyMetadata(0.0, MinimumChangedCallback));

        private static void MinimumChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not BusyIndicator indicator) return;
            indicator.Percent = indicator.CalculatePercent();
        }

        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        #endregion Minimum

        #region Maximum

        public static readonly DependencyProperty MaximumProperty = 
            DependencyProperty.Register(
                nameof(Maximum), 
                typeof(double), 
                typeof(BusyIndicator), 
                new PropertyMetadata(0.0, MaximumChangedCallback));

        private static void MaximumChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not BusyIndicator indicator) return;
            indicator.Percent = indicator.CalculatePercent();
        }

        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        #endregion Maximum

        #region Value

        public static readonly DependencyProperty ValueProperty = 
            DependencyProperty.Register(
                nameof(Value), 
                typeof(double), 
                typeof(BusyIndicator), 
                new PropertyMetadata(0.0, ValueChangedCallback));

        private static void ValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not BusyIndicator indicator) return;
            indicator.Percent = indicator.CalculatePercent();
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        #endregion Value

        #region Percent

        public static readonly DependencyProperty PercentProperty = DependencyProperty.Register(
            nameof(Percent), typeof(double), typeof(BusyIndicator), new PropertyMetadata(0.0));

        public double Percent
        {
            get => (double)GetValue(PercentProperty);
            set => SetValue(PercentProperty, value);
        }

        private double CalculatePercent()
        {
            return (Value - Minimum) / (Maximum - Minimum);
        }

        #endregion Percent

        #region CancelCommand

        public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register(
            nameof(CancelCommand), typeof(RelayCommand), typeof(BusyIndicator), new PropertyMetadata(default(RelayCommand)));

        public RelayCommand CancelCommand
        {
            get => (RelayCommand)GetValue(CancelCommandProperty);
            set => SetValue(CancelCommandProperty, value);
        }

        #endregion CancelCommand

        private Grid? _overlayGrid;
        private FrameworkElement? _contentPresenter;

        static BusyIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyIndicator), new FrameworkPropertyMetadata(typeof(BusyIndicator)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Находим именованные части из Generic.xaml
            _overlayGrid = GetTemplateChild("PART_Overlay") as Grid;
            _contentPresenter = GetTemplateChild("PART_ContentPresenter") as FrameworkElement;
        }

        private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not BusyIndicator indicator) return;
            if (!indicator.IsLoaded) return;
            if (indicator._overlayGrid == null) return;

            var isBusy = (bool)e.NewValue;

            if (isBusy)
            {
                // Поднимаем оверлей поверх контента
                Panel.SetZIndex(indicator._overlayGrid, 1000);

                // Делаем основной контент недоступным для мыши
                if (indicator._contentPresenter == null) return;
                indicator._contentPresenter.IsHitTestVisible = false;
                indicator._contentPresenter.Opacity = 0.3;
            }
            else
            {
                // Убираем оверлей вниз
                Panel.SetZIndex(indicator._overlayGrid, -1);

                // Возвращаем доступность контента
                if (indicator._contentPresenter == null) return;
                indicator._contentPresenter.IsHitTestVisible = true;
                indicator._contentPresenter.Opacity = 1.0;
            }
        }
    }
}
