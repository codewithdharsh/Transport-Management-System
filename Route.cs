namespace TransportManagementSystem.entity
{
    public class Route
    {
        private int routeID;
        private string startDestination;
        private string endDestination;
        private decimal distance;

        public Route() { }

        public Route(int routeID, string startDestination, string endDestination, decimal distance)
        {
            this.routeID = routeID;
            this.startDestination = startDestination;
            this.endDestination = endDestination;
            this.distance = distance;
        }

        public int RouteID { get => routeID; set => routeID = value; }
        public string StartDestination { get => startDestination; set => startDestination = value; }
        public string EndDestination { get => endDestination; set => endDestination = value; }
        public decimal Distance { get => distance; set => distance = value; }
    }
}
