namespace Site

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html.Client

[<JavaScript>]
module Client =
    let All =
        let ( !+ ) x = Samples.Set.Singleton(x)

        Samples.Set.Create [
            !+ MapControl.Sample
            !+ ChartControl.Sample
            !+ StockControl.Sample
        ]

    let Main = 
        Div [] |> (fun e -> StockControl.Main e.Dom)
        All.Show()