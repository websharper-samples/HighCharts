namespace Site

open WebSharper
open WebSharper.JavaScript
open WebSharper.Html.Client
open WebSharper.JQuery
open WebSharper.Highcharts

[<JavaScript>]
module ChartControl =
    let Main (el : Dom.Element) =
        Highcharts.Create(JQuery.Of el,
            HighchartsCfg(
                Title = TitleCfg(
                    Text = "Monthly Average Temperature",
                    X = -20.
                ),
                Subtitle = SubtitleCfg(
                    Text = "Source: WorldClimate.com",
                    X = -20.
                ),        
                XAxis = XAxisCfg(
                    Categories = [| "Jan"; "Feb"; "Mar"; "Apr"; "May"; "Jun"; "Jul"; "Aug"; "Sep"; "Oct"; "Nov"; "Dec" |]
                ),
                YAxis = YAxisCfg(
                    Title = YAxisTitleCfg(
                        Text = "Temperature (°C)"
                    ),
                    PlotLines = [|
                        YAxisPlotLinesCfg(
                            Value = 0.,
                            Width = 1.,
                            Color = "#808080"
                        )
                    |]
                ),
                Tooltip = TooltipCfg(
                    ValueSuffix = "°C"
                ), 
                Legend = LegendCfg(
                    Layout = "vertical",
                    Align = "right",
                    VerticalAlign = "middle",
                    BorderWidth = 0.
                ),
                Series = [|
                    SeriesCfg(
                        Name = "Tokyo",
                        Data = As [| 7.0; 6.9; 9.5; 14.5; 18.2; 21.5; 25.2; 26.5; 23.3; 18.3; 13.9; 9.6 |]
                    )
                    SeriesCfg(
                        Name = "New York",
                        Data = As [| -0.2; 0.8; 5.7; 11.3; 17.0; 22.0; 24.8; 24.1; 20.1; 14.1; 8.6; 2.5 |]
                    )
                    SeriesCfg(
                        Name = "Berlin",
                        Data = As [| -0.9; 0.6; 3.5; 8.4; 13.5; 17.0; 18.6; 17.9; 14.3; 9.0; 3.9; 1.0 |]
                    )
                    SeriesCfg(
                        Name = "London",
                        Data = As [| 3.9; 4.2; 5.7; 8.5; 11.9; 15.2; 17.0; 16.6; 14.2; 10.3; 6.6; 4.8 |]
                    )
                |]
            )
        ) |> ignore

    let Sample =
        Samples.Build()
            .Id("Highcharts")
            .FileName(__SOURCE_FILE__)
            .Keywords(["highcharts";"charts"])
            .Render(Main)
            .Create()