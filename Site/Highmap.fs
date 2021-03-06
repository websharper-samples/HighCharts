﻿namespace Site

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.Highmaps

[<Require(typeof<Resources.MapModuleForStock>)>]
type EuropeMap() =
    inherit Resources.BaseResource "http://code.highcharts.com/mapdata/custom/europe.js"

[<JavaScript>]
[<Require(typeof<EuropeMap>)>]
module MapControl =
    let map = JS.Global?Highcharts?maps?``custom/europe``
    let Main (el: Dom.Element) =
        Highcharts.Create(JQuery.Of el,
            HighmapsCfg(
                Chart = ChartCfg(
                    SpacingBottom = 20.
                ),
                Title = TitleCfg(
                    Text = "Europe time zones"
                ),
                Legend = LegendCfg(
                    Enabled = true
                ),
                PlotOptions = PlotOptionsCfg(
                    Map = PlotOptionsMapCfg(
                        AllAreas = false,
                        JoinBy = Union2Of2 [|"iso-a2"; "code"|],
                        DataLabels = PlotOptionsMapDataLabelsCfg(
                            Enabled = true,
                            Color = "white",
                            Formatter = 
                                (FuncWithThis(fun self () -> 
                                    let a = self?point?properties
                                    if As a then
                                        if 5 > a?labelrank then
                                            a?``iso-a2``
                                        else null
                                    else null)
                                ),
                            Format = null,
                            Style = New [ "fontWeight" => "bold" ]
                        ),
                        Tooltip = PlotOptionsMapTooltipCfg(
                            HeaderFormat = "",
                            PointFormat = "{point.name}: <b>{series.name}</b>"
                        )
                    )
                ),
                Series = [|
                    SeriesCfg(
                        Name = "UTC",
                        MapData = map,
                        Data = ([|"IE"; "IS"; "GB"; "PT"|] |> Array.map (fun e -> New [ "code" => e ]))
                    )
                    SeriesCfg(
                        Name = "UTC +1",
                        MapData = map,
                        Data = ([|"NO"; "SE"; "DK"; "DE"; "NL"; "BE"; "LU"; "ES"; "FR"; "PL"; "CZ"; "AT";
                                     "CH"; "LI"; "SK"; "HU"; "SI"; "IT"; "SM"; "HR"; "BA"; "YF"; "ME"; "AL"; "MK"|]
                                |> Array.map (fun e -> New [ "code" => e ]))
                    )
                    SeriesCfg(
                        Name = "UTC +2",
                        MapData = map,
                        Data = ([|"FI"; "EE"; "LV"; "LT"; "BY"; "UA"; "MD"; "RO"; "BG"; "GR"; "TR"; "CY"|]
                                |> Array.map (fun e -> New [ "code" => e ]))
                    )
                    SeriesCfg(
                        Name = "UTC +3",
                        MapData = map,
                        Data = ([|"RU"|] |> Array.map (fun e -> New [ "code" => e ]))
                    )
                |]
           )
        ) |> ignore

    let Sample =
        Samples.Build()
            .Id("Highmaps")
            .FileName(__SOURCE_FILE__)
            .Keywords(["highmaps";"charts"])
            .Render(Main)
            .Create()