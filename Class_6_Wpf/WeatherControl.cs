using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Class_6_Wpf
{
    class WeatherControl : DependencyObject
    {
        // св зависимости и суфикс Property
        public static readonly DependencyProperty TemperatureProperty;
        // направление ветра
        private string direction_wind;
        // скорость ветра
        private int speed_wind;
        // наличие осадков 
        private Precipitation precipitation; 
        public int Temperature
        {
            get => (int)GetValue(TemperatureProperty); 
            set => SetValue(TemperatureProperty, value);
        }

        public string DirectionWind
        {
            get => direction_wind; 
            set => direction_wind = value;
        }
        public int SpeedWind
        {
            get => speed_wind;
            set => speed_wind = value;
        }
        public enum Precipitation 
        {
            солнечно = 0,
            облачно = 1,
            дождь = 2,
            снег = 3
        }
        public WeatherControl(int temperature, string direction_wind, int speed_wind, Precipitation precipitation)
        {
            this.Temperature = temperature;
            this.DirectionWind = direction_wind;
            this.SpeedWind = speed_wind;
            this.precipitation = precipitation;
        }
        // статич конструктор инициализировать TemperatureProperty через метод Register
        static WeatherControl() 
        {
            TemperatureProperty = DependencyProperty.Register(
                // аргумент название свва
                nameof(Temperature),
                // аргумент тип поля
                typeof(int),
                // аргумент тип владельца класс Person
                typeof(WeatherControl),
                // аргумент методата
                new FrameworkPropertyMetadata(
                    // значение по умолчанию 0
                    0,
                    // перечисление флаг AffectsMeasure (размер элементов при компановке)
                    FrameworkPropertyMetadataOptions.AffectsParentMeasure |
                    // флаг AffectsRender
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    // метод срабатывает при изменении свва (null - действия при изменении нет)
                    null,
                    // корректировка введенного значения
                    new CoerceValueCallback(CoerceTemperature)),
                // метод валидации     
                new ValidateValueCallback(ValidateTemperature));                    
        }
        // метод отвечает за проверку введеных значений
        private static bool ValidateTemperature(object value)   
        {
            int t = (int)value;
            if (t >= -50 && t <= 50)
                return true;
            else
                return false;
        }
        // метод отвечает за проверку введеных значений и позволяет корректировать значения
        private static object CoerceTemperature(DependencyObject d, object baseValue)  
        {
            int t = (int)baseValue;
            if (t >= -50 && t <= 50)
                return true;
            else
                return false;
        }
        
        public string Print()
        {
            return $" {Temperature} {DirectionWind} {SpeedWind} Precipitation";
        }
    }
}
