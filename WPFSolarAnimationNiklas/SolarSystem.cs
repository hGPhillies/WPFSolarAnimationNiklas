using System.Collections.Generic;

    namespace WPFSolarAnimationNiklas
{
    internal class SolarSystem
    {
        private readonly List<SmallPlanet> planets = new();

        public void AddPlanet(SmallPlanet planet)
        {
            planets.Add(planet);
        }

        public List<SmallPlanet> GetPlanets()
        {
            return planets;
        }
    }
}