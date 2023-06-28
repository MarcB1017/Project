namespace MVC_CarsForSale.ViewModels.DashBoard
{
    public class DashBoardViewModel
    {
        public List<Models.Cars> Cars { get; internal set; }
        public List<Models.Bikes> Bikes { get; internal set; }
        public List<Models.Vans> Vans { get; internal set; }
    }
}
