namespace Site

open WebSharper
open WebSharper.UI
open WebSharper.UI.Html
open WebSharper.UI.Client

[<JavaScript>]
module Client =
    let All =
        let ( !+ ) x = Samples.Set.Singleton(x)

        Samples.Set.Create [
            !+ MapControl.Sample
            !+ ChartControl.Sample
            !+ StockControl.Sample
        ]

    [<SPAEntryPoint>]
    let Main() = 
        All.Show()