using AwtApplication.Params;
using AwtApplication.Services;
using System;
using System.Collections.Generic;
namespace AwtApplication
{
	public class DashboardMultipleTilesViewModel
	{
		public DashboardMultipleTilesViewModel()
		{
            DashboardMutipleTilesList = new List<DashboardMultipleTileItem> {
                new DashboardMultipleTileItem
                {
                    Title = Messages.PAGE_TITLE_TIMELINE,
                    BackgroundColor = "#E4ECF0",
                    IconColor = "#F38300",
                    ShowBackgroundColor = true,
                    BackgroundImage = "",
                    Icon = GrialShapesFont.Event,
                    Badge = 0,
                    Callback = ViewService.ShowEventList
                },

                new DashboardMultipleTileItem
                {
                    Title = Messages.PAGE_TITLE_MY_TIMELINE,
                    BackgroundColor = "#E4ECF0",
                    IconColor = "#3C96D2",
                    ShowBackgroundColor = true,
                    BackgroundImage = "",
                    Icon = GrialShapesFont.FavoriteBorder,
                    Badge = 0, // TODO: bage implementieren
                    Callback = ViewService.ShowPersonalEventList
                },
                new DashboardMultipleTileItem
                {
                    Title = Messages.PAGE_TITLE_REFERENTS,
                    BackgroundColor = "#E4ECF0",
                    IconColor = "#C00222",
                    ShowBackgroundColor = true,
                    BackgroundImage = "",
                    Icon = GrialShapesFont.Group,
                    Badge = 0,
                    Callback = ViewService.ShowReferentList
                },
                new DashboardMultipleTileItem
                {
                    Title = Messages.PAGE_TITLE_MAP,
                    BackgroundColor = "#E4ECF0",
                    IconColor = "#BECB00",
                    ShowBackgroundColor = true,
                    BackgroundImage = "",
                    Icon = GrialShapesFont.Place,
                    Badge = 0,
                    Callback = ViewService.ShowMap
                },
                new DashboardMultipleTileItem
                {
                    Title = Messages.PAGE_TITLE_TAXI,
                    BackgroundColor = "#E4ECF0",
                    IconColor = "#5A6E78",
                    ShowBackgroundColor = true,
                    BackgroundImage = "",
                    Icon = GrialShapesFont.PhoneIphone,
                    Badge = 0,
                    Callback = ViewService.ShowTaxi
                }
                ,
                new DashboardMultipleTileItem
                {
                    Title = "DEVELOP",
                    BackgroundColor = "#E4ECF0",
                    IconColor = "#5A6E78",
                    ShowBackgroundColor = true,
                    BackgroundImage = "",
                    Icon = GrialShapesFont.PhoneIphone,
                    Badge = 0,
                    Callback = ()=>ViewService.ShowBreakoutSession("01.09.2017 13:00")
                }
            };
		}

		public List<DashboardMultipleTileItem> DashboardMutipleTilesList { get; set; }
	}
}

public class DashboardMultipleTileItem
{
	public string Title { get; set; }
	public string BackgroundColor { get; set; }
    public string IconColor { get; set; }
	public string BackgroundImage { get; set; }
	public bool ShowBackgroundColor { get; set; }
	public string Icon { get; set; }
	public int Badge { get; set; }
    public ShowView Callback { get; set; }
}
