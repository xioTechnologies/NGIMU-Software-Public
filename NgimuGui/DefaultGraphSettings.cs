using System.Collections.Generic;
using Rug.LiteGL.Controls;

namespace NgimuGui
{
    internal static class DefaultGraphSettings
    {
        private static Dictionary<string, GraphSettings> graphSettings = new Dictionary<string, GraphSettings>();

        public static IEnumerable<GraphSettings> Settings { get { return graphSettings.Values; } }

        public static IEnumerable<string> Keys { get { return graphSettings.Keys; } }

        static DefaultGraphSettings()
        {
            const float lowSpeedTimespan = 60;
            const float highSpeedTimespan = 10;

            uint graphSampleBufferSize = GetGraphSampleBufferSize();

            #region Define Default Configurations

            {
                GraphSettings settings = new GraphSettings()
                {
                    Title = "Messages Per Second",
                    YAxisLabel = "Messages per second",
                    AxesRange = new AxesRange(0, -1, lowSpeedTimespan, 1),
                    GraphType = GraphType.Timestamp,
                    VerticalAutoscaleIndex = int.MaxValue,
                     
                };
                settings.Traces.Add(new Trace() { Color = Colors.Yellow, Name = "Messages Per Second", MaxDataPoints = graphSampleBufferSize });

                settings.ShowLegend = settings.Traces.Count > 1;
                graphSettings.Add(settings.Title, settings);
            }

            {
                GraphSettings settings = new GraphSettings()
                {
                    Title = "Gyroscope",
                    YAxisLabel = "Angular velocity (°/s)",
                    AxesRange = new AxesRange(0, -1, highSpeedTimespan, 1),
                    GraphType = GraphType.Timestamp,
                    VerticalAutoscaleIndex = int.MaxValue,
                    
                };
                settings.Traces.Add(new Trace() { Color = Colors.Red, Name = "X", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Green, Name = "Y", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Blue, Name = "Z", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.White, Name = "Magnitude", MaxDataPoints = graphSampleBufferSize });

                settings.ShowLegend = settings.Traces.Count > 1;
                graphSettings.Add(settings.Title, settings);
            }

            {
                GraphSettings settings = new GraphSettings()
                {
                    Title = "Accelerometer",
                    YAxisLabel = "Acceleration (g)",
                    AxesRange = new AxesRange(0, -1, highSpeedTimespan, 1),
                    GraphType = GraphType.Timestamp,
                    VerticalAutoscaleIndex = int.MaxValue,
                    
                };
                settings.Traces.Add(new Trace() { Color = Colors.Red, Name = "X", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Green, Name = "Y", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Blue, Name = "Z", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.White, Name = "Magnitude", MaxDataPoints = graphSampleBufferSize });

                settings.ShowLegend = settings.Traces.Count > 1;
                graphSettings.Add(settings.Title, settings);
            }

            {
                GraphSettings settings = new GraphSettings()
                {
                    Title = "Magnetometer",
                    YAxisLabel = "Intensity (uT)",
                    AxesRange = new AxesRange(0, -1, highSpeedTimespan, 1),
                    GraphType = GraphType.Timestamp,
                    ShowLegend = true,
                    VerticalAutoscaleIndex = int.MaxValue,
                    
                };
                settings.Traces.Add(new Trace() { Color = Colors.Red, Name = "X", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Green, Name = "Y", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Blue, Name = "Z", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.White, Name = "Magnitude", MaxDataPoints = graphSampleBufferSize });

                settings.ShowLegend = settings.Traces.Count > 1;
                graphSettings.Add(settings.Title, settings);
            }

            {
                GraphSettings settings = new GraphSettings()
                {
                    Title = "Barometer",
                    YAxisLabel = "Pressure (hPa)",
                    AxesRange = new AxesRange(0, -1, highSpeedTimespan, 1),
                    GraphType = GraphType.Timestamp,
                    VerticalAutoscaleIndex = int.MaxValue,
                    
                };
                settings.Traces.Add(new Trace() { Color = Colors.Yellow, Name = "Barometer", MaxDataPoints = graphSampleBufferSize });

                settings.ShowLegend = settings.Traces.Count > 1;
                graphSettings.Add(settings.Title, settings);
            }

            {
                GraphSettings settings = new GraphSettings()
                {
                    Title = "Euler Angles",
                    YAxisLabel = "Angle (°)",
                    AxesRange = new AxesRange(0, -1, highSpeedTimespan, 1),
                    GraphType = GraphType.Timestamp,
                    VerticalAutoscaleIndex = int.MaxValue,
                    
                };
                settings.Traces.Add(new Trace() { Color = Colors.Red, Name = "Roll", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Green, Name = "Pitch", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Blue, Name = "Yaw", MaxDataPoints = graphSampleBufferSize });

                settings.ShowLegend = settings.Traces.Count > 1;
                graphSettings.Add(settings.Title, settings);
            }

            {
                GraphSettings settings = new GraphSettings()
                {
                    Title = "Linear Acceleration",
                    YAxisLabel = "Acceleration (g)",
                    AxesRange = new AxesRange(0, -1, highSpeedTimespan, 1),
                    GraphType = GraphType.Timestamp,
                    VerticalAutoscaleIndex = int.MaxValue,
                    
                };
                settings.Traces.Add(new Trace() { Color = Colors.Red, Name = "X", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Green, Name = "Y", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Blue, Name = "Z", MaxDataPoints = graphSampleBufferSize });

                settings.ShowLegend = settings.Traces.Count > 1;
                graphSettings.Add(settings.Title, settings);
            }

            {
                GraphSettings settings = new GraphSettings()
                {
                    Title = "Earth Acceleration",
                    YAxisLabel = "Acceleration (g)",
                    AxesRange = new AxesRange(0, -1, highSpeedTimespan, 1),
                    GraphType = GraphType.Timestamp,
                    VerticalAutoscaleIndex = int.MaxValue,
                    
                };
                settings.Traces.Add(new Trace() { Color = Colors.Red, Name = "X", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Green, Name = "Y", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Blue, Name = "Z", MaxDataPoints = graphSampleBufferSize });

                settings.ShowLegend = settings.Traces.Count > 1;
                graphSettings.Add(settings.Title, settings);
            }

            {
                GraphSettings settings = new GraphSettings()
                {
                    Title = "Altimeter",
                    YAxisLabel = "Altitude (m)",
                    AxesRange = new AxesRange(0, -1, highSpeedTimespan, 1),
                    GraphType = GraphType.Timestamp,
                    VerticalAutoscaleIndex = int.MaxValue,
                    
                };
                settings.Traces.Add(new Trace() { Color = Colors.Yellow, Name = "Altitude", MaxDataPoints = graphSampleBufferSize });

                settings.ShowLegend = settings.Traces.Count > 1;
                graphSettings.Add(settings.Title, settings);
            }

            {
                GraphSettings settings = new GraphSettings()
                {
                    Title = "Temperature",
                    YAxisLabel = "Temperature (°C)",
                    AxesRange = new AxesRange(0, -1, lowSpeedTimespan, 1),
                    GraphType = GraphType.Timestamp,
                    VerticalAutoscaleIndex = int.MaxValue,
                    
                };
                settings.Traces.Add(new Trace() { Color = Colors.Blue, Name = "Gyroscope And Accelerometer", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Cyan, Name = "Environmental Sensor", MaxDataPoints = graphSampleBufferSize });

                settings.ShowLegend = settings.Traces.Count > 1;
                graphSettings.Add(settings.Title, settings);
            }

            {
                GraphSettings settings = new GraphSettings()
                {
                    Title = "Humidity",
                    YAxisLabel = "Humidity (%)",
                    AxesRange = new AxesRange(0, -1, lowSpeedTimespan, 1),
                    GraphType = GraphType.Timestamp,
                    VerticalAutoscaleIndex = int.MaxValue,
                    
                };
                settings.Traces.Add(new Trace() { Color = Colors.Yellow, Name = "Humidity", MaxDataPoints = graphSampleBufferSize });

                settings.ShowLegend = settings.Traces.Count > 1;
                graphSettings.Add(settings.Title, settings);
            }

            {
                GraphSettings settings = new GraphSettings()
                {
                    Title = "Battery Percentage",
                    YAxisLabel = "Percentage (%)",
                    AxesRange = new AxesRange(0, -1, lowSpeedTimespan, 1),
                    GraphType = GraphType.Timestamp,
                    VerticalAutoscaleIndex = int.MaxValue,
                    
                };
                settings.Traces.Add(new Trace() { Color = Colors.Yellow, Name = "Battery Percentage", MaxDataPoints = graphSampleBufferSize });

                settings.ShowLegend = settings.Traces.Count > 1;
                graphSettings.Add(settings.Title, settings);
            }

            {
                GraphSettings settings = new GraphSettings()
                {
                    Title = "Battery Time To Empty",
                    YAxisLabel = "Time (minutes)",
                    AxesRange = new AxesRange(0, -1, lowSpeedTimespan, 1),
                    GraphType = GraphType.Timestamp,
                    VerticalAutoscaleIndex = int.MaxValue,
                    
                };
                settings.Traces.Add(new Trace() { Color = Colors.Yellow, Name = "Battery Time To Empty", MaxDataPoints = graphSampleBufferSize });

                settings.ShowLegend = settings.Traces.Count > 1;
                graphSettings.Add(settings.Title, settings);
            }

            {
                GraphSettings settings = new GraphSettings()
                {
                    Title = "Battery Voltage",
                    YAxisLabel = "Voltage (V)",
                    AxesRange = new AxesRange(0, -1, lowSpeedTimespan, 1),
                    GraphType = GraphType.Timestamp,
                    VerticalAutoscaleIndex = int.MaxValue,
                    
                };
                settings.Traces.Add(new Trace() { Color = Colors.Yellow, Name = "Battery Voltage", MaxDataPoints = graphSampleBufferSize });

                settings.ShowLegend = settings.Traces.Count > 1;
                graphSettings.Add(settings.Title, settings);
            }

            {
                GraphSettings settings = new GraphSettings()
                {
                    Title = "Battery Current",
                    YAxisLabel = "Current (mA)",
                    AxesRange = new AxesRange(0, -1, lowSpeedTimespan, 1),
                    GraphType = GraphType.Timestamp,
                    VerticalAutoscaleIndex = int.MaxValue,
                    
                };
                settings.Traces.Add(new Trace() { Color = Colors.Yellow, Name = "Battery Current", MaxDataPoints = graphSampleBufferSize });

                settings.ShowLegend = settings.Traces.Count > 1;
                graphSettings.Add(settings.Title, settings);
            }

            {
                GraphSettings settings = new GraphSettings()
                {
                    Title = "Analogue Inputs",
                    YAxisLabel = "Voltage (V)",
                    AxesRange = new AxesRange(0, -1, highSpeedTimespan, 1),
                    GraphType = GraphType.Timestamp,
                    VerticalAutoscaleIndex = int.MaxValue,
                    
                };
                settings.Traces.Add(new Trace() { Color = Colors.Red, Name = "1", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Green, Name = "2", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Blue, Name = "3", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Cyan, Name = "4", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Yellow, Name = "5", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Magenta, Name = "6", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.Orange, Name = "7", MaxDataPoints = graphSampleBufferSize });
                settings.Traces.Add(new Trace() { Color = Colors.White, Name = "8", MaxDataPoints = graphSampleBufferSize });

                settings.ShowLegend = settings.Traces.Count > 1;
                graphSettings.Add(settings.Title, settings);
            }

            {
                GraphSettings settings = new GraphSettings()
                {
                    Title = "RSSI Power",
                    YAxisLabel = "Power (dBm)",
                    AxesRange = new AxesRange(0, -1, lowSpeedTimespan, 1),
                    GraphType = GraphType.Timestamp,
                    VerticalAutoscaleIndex = int.MaxValue,
                    
                };
                settings.Traces.Add(new Trace() { Color = Colors.Yellow, Name = "Power", MaxDataPoints = graphSampleBufferSize });

                settings.ShowLegend = settings.Traces.Count > 1;
                graphSettings.Add(settings.Title, settings);
            }

            {
                GraphSettings settings = new GraphSettings()
                {
                    Title = "RSSI Percentage",
                    YAxisLabel = "Percentage (%)",
                    AxesRange = new AxesRange(0, -1, lowSpeedTimespan, 1),
                    GraphType = GraphType.Timestamp,
                    VerticalAutoscaleIndex = int.MaxValue,
                    
                };
                settings.Traces.Add(new Trace() { Color = Colors.Yellow, Name = "Percentage (%)", MaxDataPoints = graphSampleBufferSize });

                settings.ShowLegend = settings.Traces.Count > 1;
                graphSettings.Add(settings.Title, settings);
            }

            #endregion Define Default Configurations
        }

        public static GraphSettings GetSettings(string name)
        {
            return graphSettings[name];
        }

        public static void UpdateGraphSampleBufferSize()
        {
            uint graphSampleBufferSize = GetGraphSampleBufferSize();

            foreach (GraphSettings config in graphSettings.Values)
            {
                foreach (Trace trace in config.Traces)
                {
                    trace.MaxDataPoints = graphSampleBufferSize;
                }
            }
        }

        private static uint GetGraphSampleBufferSize()
        {
            return Options.GraphSampleBufferSize * 1000;
        }
    }
}