using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using Microsoft.Xaml.Behaviors;
using WpfPerfBench.Data.Models;

namespace WpfPerfBench.WPF.Behaviors;

public class GMapListViewBehavior : Behavior<GMapControl>
{
    public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
        nameof(SelectedItem), 
        typeof(Item), 
        typeof(GMapListViewBehavior), 
        new PropertyMetadata(null, PropertyChangedCallback));

    public Item SelectedItem
    {
        get => (Item)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not GMapListViewBehavior owner) return;
        if (e.NewValue is not Item item) return;
        ShowRoute(owner.AssociatedObject, item);
    }

    private static void ShowRoute(GMapControl map, Item item)
    {
        var startPoint = CreatePoint(item.Latitude, item.Longitude);
        var endPoint = CreatePoint(item.DeliveryLatitude, item.DeliveryLongitude);
        var route = CreateRoute(map, startPoint, endPoint);

        map.Markers.Clear();

        if (route is null)
        {
            // Маршрут не найден — добавляем маркеры в начальную и конечную точки
            AddDirectionLine(map, startPoint, endPoint, Colors.Blue);
            AddMarker(map, startPoint, 15, Colors.Green, "Старт");
            AddMarker(map, endPoint, 20, Colors.Red, "Финиш");
        }
        else
        {
            // Создаем GMapRoute из точек полученного маршрута
            var gmRoute = new GMapRoute(route.Points)
            {
                // Настраиваем внешний вид линии маршрута
                Shape = new Path()
                {
                    Stroke = new SolidColorBrush(Colors.Blue),
                    StrokeThickness = 4
                }
            };

            // Добавляем маршрут на карту
            map.Markers.Add(gmRoute);
            AddMarker(map, startPoint, 15, Colors.Green, "Старт");
            AddMarker(map, endPoint, 20, Colors.Red, "Финиш");
        }

        // Опционально: центрируем карту по маршруту
        map.ShowCenter = true;
        map.ZoomAndCenterMarkers(null);
    }

    private static MapRoute? CreateRoute(
        GMapControl map, 
        PointLatLng startPoint, 
        PointLatLng endPoint)
    {
        // Получаем провайдера для построения маршрута
        var routingProvider = map.MapProvider as RoutingProvider ?? GMapProviders.OpenStreetMap;

        // Строим маршрут
        var route = routingProvider.GetRoute(startPoint, endPoint, false, false, (int)map.Zoom);
        return route;
    }

    private static PointLatLng CreatePoint(double lat, double lng)
    {
        var point = new PointLatLng(lat, lng);
        return point;
    }

    private static void AddMarker(
        GMapControl map,
        PointLatLng position,
        double diameter,
        Color color,
        string tooltip)
    {
        var marker = new GMapMarker(position);

        // Создаем визуальный элемент для маркера
        var ellipse = new Ellipse
        {
            Width = diameter,
            Height = diameter,
            Fill = new SolidColorBrush(color),
            Stroke = new SolidColorBrush(Colors.Black),
            StrokeThickness = 2,
            ToolTip = tooltip
        };

        marker.Shape = ellipse;
        map.Markers.Add(marker);
    }

    private static void AddDirectionLine(
        GMapControl map, 
        PointLatLng startPoint, 
        PointLatLng endPoint,
        Color color)
    {
        var points = new List<PointLatLng> { startPoint, endPoint };
        var directLine = new GMapRoute(points);

        var stroke = new SolidColorBrush(color)
        {
            Opacity = 0.5
        };

        directLine.Shape = new Path()
        {
            Stroke = stroke,
            StrokeThickness = 3,
            StrokeDashArray = [5, 5] // Пунктир
        };
        map.Markers.Add(directLine);
    }

    #region Overrides of Behavior

    protected override void OnAttached()
    {
        base.OnAttached();
        InitMapControl();
    }

    private void InitMapControl()
    {
        // 1. Задаём User-Agent, который идентифицирует ваше приложение
        // Это поможет администраторам OSM связаться с вами в случае проблем
        GMapProvider.UserAgent = "WpfPerfBench/1.0";

        // 2. Указываем Referer (может быть URL вашего сайта или просто "localhost")
        // Это нужно для соблюдения политики OSM
        OpenStreetMapProvider.Instance.RefererUrl = "http://localhost/";

        AssociatedObject.MapProvider = OpenStreetMapProvider.Instance; // или GMapProviders.GoogleMap[citation:2]

        // 2. Установите центр карты и масштаб
        AssociatedObject.Position = new PointLatLng(55.7558, 37.6173); // Москва
        AssociatedObject.Zoom = 10;
        AssociatedObject.MinZoom = 2;
        AssociatedObject.MaxZoom = 17;

        // 3. (Опционально) Настройте кнопку для перемещения карты
        AssociatedObject.DragButton = MouseButton.Left;

        // 4. Укажите, откуда брать тайлы карты
        AssociatedObject.Manager.Mode = AccessMode.ServerOnly;
    }

    #endregion
}