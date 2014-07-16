namespace Site

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.Html5
open IntelliFactory.WebSharper.JQuery
open IntelliFactory.WebSharper.Highmaps

[<JavaScript; AutoOpen>]
module Helpers =
    [<Inline "$global.Highcharts.maps[\'custom/europe\']">]
    let map = X<obj []>

    [<Inline "$0.toString()">]
    let toS a = X<string>

    type StyleCfg = { fontWeight: string }
    type DataC = { code: string }

[<Require(typeof<Resources.MapModuleForStock>)>]
type EuropeMap() =
    inherit Resources.BaseResource "http://code.highcharts.com/mapdata/custom/europe.js"

[<JavaScript>]
[<Require(typeof<EuropeMap>)>]
module MapControl =
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
                        JoinBy = [|"iso-a2"; "code"|],
                        DataLabels = PlotOptionsSeriesDataLabelsCfg(
                            Enabled = true,
                            Color = "white",
                            Formatter = As 
                                (Func<obj,obj>(fun self -> 
                                                let a = self?point?properties
                                                if As a then
                                                    if 5 > (As <| toS a?labelrank) then
                                                        a?``iso-a2``
                                                    else null
                                                else null)),
                            Format = null,
                            Style = { fontWeight = "bold" }
                        ),
                        Tooltip = PlotOptionsSeriesTooltipCfg(
                            HeaderFormat = "",
                            PointFormat = "{point.name}: <b>{series.name}</b>"
                        )
                    )
                ),
                Series = [|
                    SeriesCfg(
                        Name = "UTC",
                        MapData = map,
                        Data = ([|"IE"; "IS"; "GB"; "PT"|] |> Array.map (fun e -> As { code = e }))
                    )
                    SeriesCfg(
                        Name = "UTC +1",
                        MapData = map,
                        Data = ([|"NO"; "SE"; "DK"; "DE"; "NL"; "BE"; "LU"; "ES"; "FR"; "PL"; "CZ"; "AT"; "CH"; "LI"; "SK"; "HU"; "SI"; "IT"; "SM"; "HR"; "BA"; "YF"; "ME"; "AL"; "MK"|]
                                |> Array.map (fun e -> As { code = e }))
                    )
                    SeriesCfg(
                        Name = "UTC +2",
                        MapData = map,
                        Data = ([|"FI"; "EE"; "LV"; "LT"; "BY"; "UA"; "MD"; "RO"; "BG"; "GR"; "TR"; "CY"|]
                                |> Array.map (fun e -> As { code = e }))
                    )
                    SeriesCfg(
                        Name = "UTC +3",
                        MapData = map,
                        Data = ([|"RU"|] |> Array.map (fun e -> As { code = e }))
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