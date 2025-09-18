using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPFSolarAnimationNiklas
{
    public partial class MainWindow : Window
    {
        private SolarSystem solarSystem;
        private DispatcherTimer timer;
        private List<Ellipse> planetEllipses = new();
        private double animationAngle = 0;

        public MainWindow()
        {
            InitializeComponent();

            // Solar System initialisieren
            solarSystem = new SolarSystem();
            solarSystem.AddPlanet(new SmallPlanet("Merkur", 0.39, 88, 0.206, 4879, "Merkur"));
            solarSystem.AddPlanet(new SmallPlanet("Venus", 0.72, 225, 0.007, 12104, "Venus"));
            solarSystem.AddPlanet(new SmallPlanet("Erde", 1, 365, 0.017, 12756, "Erde"));
            solarSystem.AddPlanet(new SmallPlanet("Mars", 1.52, 687, 0.093, 6792, "Mars"));
            solarSystem.AddPlanet(new SmallPlanet("Jupiter", 5.2, 4333, 0.049, 142984, "Jupiter"));
            solarSystem.AddPlanet(new SmallPlanet("Saturn", 9.58, 10759, 0.056, 120536, "Saturn"));
            solarSystem.AddPlanet(new SmallPlanet("Uranus", 19.22, 30687, 0.046, 50724, "Uranus"));

            DrawPlanets();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(30);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        //Deletes Canvas and ellipses
        //Calculates center of canvas
        //Draws sun in center
        //Draws orbits and planets
        private void DrawPlanets()
        {
            SolarSystemCanvas.Children.Clear();
            planetEllipses.Clear();

            double centerX = SolarSystemCanvas.ActualWidth / 2;
            double centerY = SolarSystemCanvas.ActualHeight / 2;

            if (centerX == 0 || centerY == 0)
            {
                //Retry drawing when canvas is loaded
                SolarSystemCanvas.Loaded += (s, e) => DrawPlanets();
                return;
            }

            //Paints sun in center
            Ellipse sun = new()
            {
                Width = 40,
                Height = 40,
                Fill = Brushes.Yellow
            };
            Canvas.SetLeft(sun, centerX - 20);
            Canvas.SetTop(sun, centerY - 20);
            SolarSystemCanvas.Children.Add(sun);

            //Draws orbits and planets
            var planets = solarSystem.GetPlanets();
            for (int i = 0; i < planets.Count; i++)
            {
                // Umlaufbahn als feine Linie zeichnen
                double orbitRadius = 50 + i * 40;
                Ellipse orbit = new()
                {
                    Width = orbitRadius * 2,
                    Height = orbitRadius * 2,
                    Stroke = Brushes.DimGray,
                    StrokeThickness = 1,
                    Fill = Brushes.Transparent
                };
                Canvas.SetLeft(orbit, centerX - orbitRadius);
                Canvas.SetTop(orbit, centerY - orbitRadius);
                SolarSystemCanvas.Children.Add(orbit);

                
                var planet = planets[i];
                double size = Math.Max(planet.Diameter / 4000, 8); //Scale for planet size, min 8 pixels
                Ellipse ellipse = new()
                {
                    Width = size,
                    Height = size,
                    Fill = GetPlanetBrush(i)
                };
                planetEllipses.Add(ellipse);
                SolarSystemCanvas.Children.Add(ellipse);
            }
        }

        private Brush GetPlanetBrush(int index)
        {
            //Colors for planets
            Brush[] brushes = { Brushes.Gray, Brushes.Orange, Brushes.Blue, Brushes.Red, Brushes.Brown, Brushes.Gold, Brushes.LightBlue };
            return brushes[index % brushes.Length];
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            AnimatePlanets();
        }

        //Animates the planets around the sun
        //Calculates position based on orbital period and updates position

        private void AnimatePlanets()
        {
            double centerX = SolarSystemCanvas.ActualWidth / 2;
            double centerY = SolarSystemCanvas.ActualHeight / 2;
            var planets = solarSystem.GetPlanets();

            for (int i = 0; i < planets.Count; i++)
            {
                var planet = planets[i];
                double orbitRadius = 50 + i * 40; //Size of orbit
                double angle = animationAngle * (365.0 / planet.OrbitalPeriod); //Soeed of planet
                double x = centerX + orbitRadius * Math.Cos(angle) - planetEllipses[i].Width / 2;
                double y = centerY + orbitRadius * Math.Sin(angle) - planetEllipses[i].Height / 2;
                Canvas.SetLeft(planetEllipses[i], x);
                Canvas.SetTop(planetEllipses[i], y);
            }
            animationAngle += 0.02;
        }
    }
}
