namespace WPFSolarAnimationNiklas
{
    internal class SmallPlanet
    {
        public string Name { get; }
        public double DistanceFromSun { get; }
        public double OrbitalPeriod { get; }
        public double Eccentricity { get; }
        public double Diameter { get; }
        public string DisplayName { get; }

        public SmallPlanet(string name, double distanceFromSun, double orbitalPeriod, double eccentricity, double diameter, string displayName)
        {
            Name = name;
            DistanceFromSun = distanceFromSun;
            OrbitalPeriod = orbitalPeriod;
            Eccentricity = eccentricity;
            Diameter = diameter;
            DisplayName = displayName;
        }
    }
}